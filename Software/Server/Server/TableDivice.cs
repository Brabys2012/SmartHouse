using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System.Collections;

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
                                if (!r.IsDBNull(1))
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
                                                    " t1.NumOfDev = " + NumDev.ToString(),
                                                     fbc, Transaction);
                    try
                    {

                        Transaction.Commit();
                        fbc.Close();
                    }
                    catch
                    {
                        lock (Storage.MessegesForUser)
                        {
                            Storage.MessegesForUser.Enqueue("Что то произошло с базой данных сервера, проверьте не поврежден ли сервер");
                        }
                        fbc.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Метод для обновление клиентского приложения
        /// </summary>
        public void UpdateUserApp()
        {
            //Создаем класс соединения
            using (FbConnection fbc = new FbConnection(fbParam.ToString()))
            {
                Device result;
                lock (Storage.lockerBdDev)
                {
                    //Открываем соединение
                    fbc.Open();
                    //Создаем транзакцию
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select Name, Type, State" +
                                                    " from Device ",fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
                        {
                            lock (Storage.ArrayUpdate)
                            {
                                Storage.ArrayUpdate = new ArrayList();
                            
                            // Читаем результат запроса построчно - строка за строкой
                                while (r.Read())
                                {
                                    result = new Device();
                                    if (!r.IsDBNull(0))
                                    {
                                        result.Name = r.GetString(0);
                                    }
                                    else
                                    {
                                        result.Name = "";
                                    }
                                    if (!r.IsDBNull(1))
                                    {
                                        result.Type = r.GetString(1);
                                    }
                                    else
                                    {
                                        result.Type = "";
                                    }
                                    if (!r.IsDBNull(2))
                                    {
                                        result.State = r.GetInt32(2).ToString();
                                    }
                                    else
                                    {
                                        result.State = "";
                                    }
                                    Storage.ArrayUpdate.Add(result);
                                }
                            }
                        }
                        Transaction.Commit();
                        fbc.Close();
                        Storage.Updated = true;
                    }
                    catch
                    {
                        fbc.Close();
                    }
                }
            }
        }
    }
}
