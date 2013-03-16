using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System.Collections;
using System.Data;

namespace Server
{
    /// <summary>
    /// Класс отвечает за работу с таблицей "Устройства" в БД
    /// </summary>
    public class TableDivice
    {
        /// <summary>
        /// Строка настроек БД
        /// </summary>
        private FbConnectionStringBuilder fbParam = new FbConnectionStringBuilder();
        /// <summary>
        /// При инициализации класса происходит создание строки настройки базы данных
        /// </summary>
        public TableDivice()
        {
            // Указываем тип используемого сервера
            fbParam.ServerType = FbServerType.Embedded;

            // Путь до файла с базой данных
            fbParam.Database = @"DataBase\DIVICES.FB";

            // Настройка параметров "общения" клиента с сервером
            fbParam.Charset = "WIN1251";
            fbParam.Dialect = 3;
            // Путь до бибилиотеки-сервера Firebird
            // Если библиотека находится в тойже папке
            // что и exe фаил - указывать путь не надо
            // Если используется не embedded - эта строчка не нужна
            fbParam.ClientLibrary = @"DataBase\fbclient.dll";


            // Настройки аутентификации
            fbParam.UserID = "SYSDBA";
            fbParam.Password = "masterkey";
        }

        /// <summary>
        /// Метод нужен при срабатывание датчика он определяет к какому устройству он привязан что нужно выключить и какое сообщение прислать пользователям
        /// </summary>
        /// <param name="numPort">№ порта сработывшего датчика</param>
        /// <param name="numDev">Номер самого датчика</param>
        public ConfigForMess DeterDevice(byte numPort, byte numDev)
        {
            ConfigForMess result = new ConfigForMess();
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select t1.Messege, t2.NumOfPort, t2.NumOfDev" + 
                                                    " from Device t1 left join Device t2 on t1.IDFK=t2.ID " +
                                                    " where t1.NumOfPort = " + numPort.ToString() +
                                                    " t1.NumOfDev = " + numDev.ToString() + " Rows(1)",
                                                     fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
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
                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        result.messege = "Что то произошло с базой данных сервера, проверьте не поврежден ли сервер";
                        result.number = null;
                        fbc.Close();
                        return result;

                    }
                }
            }
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
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("update Device set State = " + State.ToString() +
                                                    " where t1.NumOfPort = " + NumPort.ToString() +
                                                    " and t1.NumOfDev = " + NumDev.ToString(),
                                                     fbc, Transaction);
                    try
                    {
                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        WinLog.Write("Ошибка: базы данных при обновления статуса утсройства!", System.Diagnostics.EventLogEntryType.Error);
                        fbc.Close();
                    }
                }
            }
            string Name = DeterDevByNumber(NumPort,NumDev);
            if (Name != "")
            lock (Storage.ArrayUpdate)
            {
               Storage.ArrayUpdate.Tables[0].Rows.Find(Name).SetField<int>(Storage.ArrayUpdate.Tables[0].Columns["State"], State); 
            }
            else
            WinLog.Write("Ошибка обновления устройства!",System.Diagnostics.EventLogEntryType.Error);           
        }

        /// <summary>
        /// Метод для обновление клиентского приложения
        /// </summary>
        public void UpdateUserApp()
        {
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    try
                    {
                        Storage.ArrayUpdate = new DataSet();
                        //Создаем fbDataAdapter, что бы потом перенести обновления в DataSet
                        FbDataAdapter DAFB = new FbDataAdapter("select Name, Type, State" +
                                                               " from Device ", fbc);
                        //Заполнили dataSet обновления, который будет пересылаться 
                        //пользователям
                        lock (Storage.ArrayUpdate)
                        {
                            Storage.ArrayUpdate.Clear();
                            DAFB.Fill(Storage.ArrayUpdate);
                            Storage.ArrayUpdate.Tables[0].PrimaryKey = new DataColumn[] { Storage.ArrayUpdate.Tables[0].Columns["Name"] }; ;
                        }
                        fbc.Close();
                    }
                    catch
                    {
                        fbc.Close();
                    }
                }
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
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select NumOfPort, NumOfDev" +
                                                    " from Device " +
                                                    " where Name = " + Name + " Rows(1)",
                                                     fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
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
                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        Result.len = 0;
                        fbc.Close();
                        return Result;

                    }
                }
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
            String Result = "";
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select Name" +
                                                    " from Device " +
                                                    " where NumOfPort = " + NumPort.ToString() + " and NumOfDev = " + NumDev.ToString() + " Rows(1)",
                                                     fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                if (!r.IsDBNull(0))
                                {
                                    Result = r.GetString(0);
                                }
                            } 
                        }
                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        fbc.Close();
                        return Result;

                    }
                }
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
             String result = "";
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select ID" + 
                                                    " from Device " +
                                                    " where Name = " + NameDev,
                                                     fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
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
                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        result = "";
                        fbc.Close();
                        return result;

                    }
                }
            }
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
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                
                string query = " (" + NumPort + "," + NumDev + ", '" + NameDev + "', '" +
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
                    query +=  ParentID + ")";
                }
                else
                    query += " null)";
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("insert into Device (NUMOFPORT, NUMOFDEV," +
                                                    " Name, Type, MESSEGE, IDFK) " + query, 
                                                     fbc, Transaction);
                    try
                    {

                        Transaction.Commit();
                        if (NameDev != "")
                            lock (Storage.ArrayUpdate)
                            {
                                DataRow RowDev = Storage.ArrayUpdate.Tables[0].NewRow();
                                RowDev[0] = NameDev;
                                RowDev[1] = TypeDev;
                                Storage.ArrayUpdate.Tables[0].Rows.Add(RowDev);
                            }
                        else
                            WinLog.Write("Ошибка обновления устройства!", System.Diagnostics.EventLogEntryType.Error);
                        fbc.Close();
                    }
                    catch
                    {
                        WinLog.Write("Ошибка: базы данных при обновления статуса утсройства!", System.Diagnostics.EventLogEntryType.Error);
                        fbc.Close();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
