using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data;
using System.Collections;
using System.Data;
using System.Threading;

namespace Server
{
    /// <summary>
    /// Класс отвечает за работу с таблицей "Устройства" в БД
    /// </summary>
    public class TableDivice
    {


        /// <summary>
        /// Метод нужен при срабатывание датчика он определяет к какому устройству он привязан что нужно выключить и какое сообщение прислать пользователям
        /// </summary>
        /// <param name="numPort">№ порта сработывшего датчика</param>
        /// <param name="numDev">Номер самого датчика</param>
        public ConfigForMess DeterDevice(byte numPort, byte numDev)
        {
            ConfigForMess result = new ConfigForMess();
            lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                        //Создаем транзакцию
                        using (FbTransaction transaction = SQL.FB_dbConnection.BeginTransaction(SQL.FB_dbReadTransactionOptions))
                        {
                            string Query = "select t1.Messege, t2.NumOfPort, t2.NumOfDev" +
                                                           " from Device t1 left join Device t2 on t1.IDFK=t2.ID " +
                                                           " where t1.NumOfPort = " + numPort.ToString() + " and " +
                                                           " t1.NumOfDev = " + numDev.ToString() + " Rows(1)";
                            using (FbCommand command = new FbCommand(Query, SQL.FB_dbConnection, transaction))
                            using (FbDataReader r = command.ExecuteReader())
                            {
                                // Читаем результат запроса построчно - строка за строкой
                                if (r.Read())
                                {
                                    if (!r.IsDBNull(0))
                                    {
                                        result.messege = r.GetString(0);
                                    }
                                    else
                                    {
                                        result.messege = "";
                                    }
                                    if (r.IsDBNull(1))
                                    {
                                        result.number = null;
                                    }
                                    else
                                    {
                                        result.number[0] = (byte)r.GetInt32(1);
                                        if (!r.IsDBNull(2))
                                        {
                                            result.number[1] = (byte)r.GetInt32(2);
                                        }
                                        else
                                        {
                                            result.number = null;
                                        }
                                    }
                                }
                                else
                                {
                                    result.messege = "";
                                    result.number = null;
                                }
                            }

                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        result.messege = "Сбой при поиске устройства в базе данных, при обработке срабатывания датчика";
                        WinLog.Write(result.messege, System.Diagnostics.EventLogEntryType.Error);
                        result.number = null;
                        SQL.IsLockedTransaction = false;
                        return result;
                    }
            }
            SQL.IsLockedTransaction = false;
            return result;
        }

        /// <summary>
        /// Метод меняет состояние устройства или датчика в базе
        /// </summary>
        /// <param name="NumPort">Номер порта устройства, у которого изменилось состояние</param>
        /// <param name="NumDev">Номер устройства у которого изменилось состояние</param>
        /// <param name="State">Состояние</param>
        public void UpdateDeviceState(byte NumPort, byte NumDev, Int32 State)
        {
            string KeyID = DeterIdDevByName(DeterDevByNumber(NumPort, NumDev));
            SQL.SQL_ExecuteNoneQueryCommitTransaction("insert into countersdata(state, DATE_REC, IDDEVICES)" +
                                                      "values (" + State.ToString() + ", '" +Convert.ToString(DateTime.Now) + "'," + KeyID + ")");  
            string Name = DeterDevByNumber(NumPort, NumDev);
            if (Name != "")
                lock (Storage.ArrayUpdate)
                {
                    Storage.ArrayUpdate.Tables[0].Rows.Find(Name).SetField<int>(Storage.ArrayUpdate.Tables[0].Columns["State"], State);
                    Storage.ArrayUpdate.Tables[0].AcceptChanges();
                }
        }

        /// <summary>
        /// Метод для обновление клиентского приложения
        /// </summary>
        public void UpdateUserApp()
        {
            //Создаем класс соединения
            lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                // Устанавливаем признак начала транзакции
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                            Storage.ArrayUpdate = new DataSet();
                            //Создаем fbDataAdapter, что бы потом перенести обновления в DataSet
                            FbDataAdapter DAFB = new FbDataAdapter("select Type, Name,  State" +
                                                                   " from Device ", SQL.FB_dbConnection);
                            //Заполнили dataSet обновления, который будет пересылаться 
                            //пользователям
                            lock (Storage.ArrayUpdate)
                            {
                                Storage.ArrayUpdate.Clear();
                                DAFB.Fill(Storage.ArrayUpdate);
                                Storage.ArrayUpdate.Tables[0].PrimaryKey = new DataColumn[] { Storage.ArrayUpdate.Tables[0].Columns["Name"] }; ;
                            }
                    }
                    catch
                    {
                        WinLog.Write("Ошибка при считывание всех устройств в DataSet", System.Diagnostics.EventLogEntryType.Error);
                        SQL.IsLockedTransaction = false;
                    }
                SQL.IsLockedTransaction = false;
            }
        }

        /// <summary>
        /// Метод возвращает номер порта и номер устройства по имени
        /// </summary>
        /// <param name="Name">Имя устройства</param>
        /// <returns></returns>
        public DevCommand DeterDevByName(string Name)
        {
            DevCommand Result = new DevCommand();
            lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                // Устанавливаем признак начала транзакции
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                        using (FbTransaction transaction = SQL.FB_dbConnection.BeginTransaction(SQL.FB_dbReadTransactionOptions))
                        {
                            string Query = "select NumOfPort, NumOfDev" +
                                          " from Device " +
                                          " where Name = '" + Name + "' Rows(1)";
                            using (FbCommand command = new FbCommand(Query, SQL.FB_dbConnection, transaction))
                            using (FbDataReader r = command.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    if (r.IsDBNull(0))
                                    {
                                        Result.len = 0;
                                    }
                                    else
                                    {
                                        if (!r.IsDBNull(1))
                                        {
                                            Result.port = (byte)r.GetInt32(0);
                                            Result.device = (byte)r.GetInt16(1);
                                            Result.len = 3;
                                        }
                                        else
                                        {
                                            Result.len = 0;
                                        }
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        Result.len = 0;
                        WinLog.Write("Не удалось определить порт устройства по его имени", System.Diagnostics.EventLogEntryType.Error);
                        SQL.IsLockedTransaction = false;
                        return Result;

                    }
                // Сбрасываем признак транзакции
                SQL.IsLockedTransaction = false;
            }
            return Result;

        }

        /// <summary>
        /// Метод возвращает имя устройства по номеру порта и номеру устройства
        /// </summary>
        /// <param name="NumPort">номер порта</param>
        /// <param name="NumDev">номер устройства</param>
        /// <returns>имя устройства</returns>
        public String DeterDevByNumber(byte NumPort, byte NumDev)
        {
            string Result = "";
            lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                // Устанавливаем признак начала транзакции
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                        using (FbTransaction transaction = SQL.FB_dbConnection.BeginTransaction(SQL.FB_dbReadTransactionOptions))
                        {
                            string Query = "select Name" +
                                          " from Device " +
                                          " where NumOfPort = " + NumPort.ToString() + " and NumOfDev = " + NumDev.ToString() +
                                          " Rows(1)";
                            using (FbCommand command = new FbCommand(Query, SQL.FB_dbConnection, transaction))
                            using (FbDataReader r = command.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    if (!r.IsDBNull(0))
                                    {
                                        Result = r.GetString(0);
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        WinLog.Write("Не удалось установит имя устройства по номеру порта", System.Diagnostics.EventLogEntryType.Error);
                        SQL.IsLockedTransaction = false;
                        return Result;
                    }
                // Сбрасываем признак транзакции
                SQL.IsLockedTransaction = false;
            }
            return Result;
        }

        /// <summary>
        /// Возвращает по имение устройства его ID
        /// </summary>
        /// <param name="NameDev">имя устройства</param>
        /// <returns>ID устройства</returns>
        public String DeterIdDevByName(string NameDev)
        {
            string result = "";
            lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                // Устанавливаем признак начала транзакции
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                        using (FbTransaction transaction = SQL.FB_dbConnection.BeginTransaction(SQL.FB_dbReadTransactionOptions))
                        {
                            string Query = "select ID" +
                                           " from Device " +
                                           " where Name = '" + NameDev + "'";
                            using (FbCommand command = new FbCommand(Query, SQL.FB_dbConnection, transaction))
                            using (FbDataReader r = command.ExecuteReader())
                            {
                                // Читаем результат запроса построчно - строка за строкой
                                if (r.Read())
                                {
                                    if (!r.IsDBNull(0))
                                    {
                                        byte IdDev = r.GetByte(0);
                                        result = IdDev.ToString();
                                    }
                                }
                                else
                                {
                                    result = "";
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        result = "";
                        WinLog.Write("Не удалось считать из базы ID устройства", System.Diagnostics.EventLogEntryType.Error);
                        SQL.IsLockedTransaction = false;
                        return result;
                    }
            }
            SQL.IsLockedTransaction = false;
            return result;
        }

        /// <summary>
        /// Добавляет новое устройства
        /// </summary>
        /// <param name="NumPort">Номер порта</param>
        /// <param name="NumDev">Номер устройства</param>
        /// <param name="NameDev">Имя устройство</param>
        /// <param name="TypeDev">Тип устройства</param>
        /// <param name="Messege">Сообщение</param>
        /// <param name="NameParent">Имя родителя</param>
        /// <returns></returns>
        public bool AddNewDevice(string NumPort, string NumDev, string NameDev,
                                 string TypeDev, string Messege, string NameParent)
        {
            bool result;
            //Создаем класс соединения                
            string query = "values (" + NumPort + "," + NumDev + ", '" + NameDev + "', '" +
                TypeDev + "', ";
            if (Messege != "")
            {
                query += " '" + Messege + "', ";
            }
            else
                query += " null, ";

            if (NameParent != "")
            {
                string ParentID = DeterIdDevByName(NameParent);
                query += ParentID + ")";
            }
            else
                query += " null)";
            result = SQL.SQL_ExecuteNoneQueryCommitTransaction("insert into Device (NUMOFPORT, NUMOFDEV," +
                                            " Name, Type, MESSEGE, IDFK) " + query);
            if ((NameDev != "") && (result))
                lock (Storage.ArrayUpdate)
                {
                    DataRow RowDev = Storage.ArrayUpdate.Tables[0].NewRow();
                    RowDev[1] = NameDev;
                    RowDev[0] = TypeDev;
                    Storage.ArrayUpdate.Tables[0].Rows.Add(RowDev);
                    Storage.ArrayUpdate.Tables[0].AcceptChanges();
                }
            else
            {
                WinLog.Write("Ошибка обновления устройства!", System.Diagnostics.EventLogEntryType.Error);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Удаляет устройства из базы данных
        /// </summary>
        public bool DeleteDevice(string Name)
        {
            bool result = SQL.SQL_ExecuteNoneQueryCommitTransaction("delete from Device " +
                                                                    " where Name = '" + Name + "'");
            if ((Name != "") && (result))
            {
                lock (Storage.ArrayUpdate)
                {
                    DataRow DR = Storage.ArrayUpdate.Tables[0].Rows.Find(Name);
                    if (DR != null)
                    {
                        DR.Delete();
                        Storage.ArrayUpdate.Tables[0].AcceptChanges();
                    }
                    else
                    {
                        result = false;
                        WinLog.Write("Не найдено удаленное устройство в DataSet");
                    }
                }
            }
            else 
            {
                result = false;
            }
            return result;    
        }

        /// <summary>
        /// Получает список номеров портов и номеров устройств
        /// </summary>
        /// <param name="Constrain">Ограничение отбора</param>
        /// <returns></returns>
        public DataSet GetListDevice(string Constrain)
        {
            DataSet DS = new DataSet();
             lock (Storage.lockerBdDev)
            {
                // Ожадаем пока не закончится начатая транзакция
                while (SQL.IsLockedTransaction)
                    Thread.Sleep(5);
                // Устанавливаем признак начала транзакции
                SQL.IsLockedTransaction = true;
                // Выполняем транзакцию
                if (SQL.FB_dbConnection != null)
                    try
                    {
                        if (SQL.FB_dbConnection.State == ConnectionState.Closed)
                            SQL.FB_dbConnection.Open();
                        FbDataAdapter DAFB = new FbDataAdapter("select NUMOFPORT, NUMOFDEV" +
                                                                   " from Device " +
                                                                   Constrain, SQL.FB_dbConnection);
                        //Заполнили dataSet
                        DAFB.Fill(DS);
                    }
                    catch
                    {
                        WinLog.Write("Ошибка при считывание устройств в DataSet", System.Diagnostics.EventLogEntryType.Error);
                        SQL.IsLockedTransaction = false;
                        return DS;
                    }
                SQL.IsLockedTransaction = false;
            }
             return DS;
        }
    }
}
