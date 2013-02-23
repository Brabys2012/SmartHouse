using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace Server
{
    public class TableUser
    {
           /// <summary>
        /// Строка настроек БД
        /// </summary>
        private FbConnectionStringBuilder fbParam = new FbConnectionStringBuilder();
        /// <summary>
        /// При инициализации класса происходит создание строки настройки базы данных
        /// </summary>
        public TableUser()
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
        /// Проверяет есть ли такой пользователь с таким логином и паролем в Базе.
        /// </summary>
        /// <param name="login">Логин пользователя, сушествование которого нужно проверить</param>
        /// <param name="Password">Пароль этого пользователся</param>
        public string CheckUser(string login, string Password)
        {
            string res = "";
            lock (Storage.lockerBdDev)
            {
                using (FbConnection fbc = new FbConnection(fbParam.ToString()))
                {
                    //Открывает соединение
                    fbc.Open();
                    FbTransaction Transaction = fbc.BeginTransaction();
                    FbCommand Query = new FbCommand("select U.Role from Users U" + 
                                                     "where U.Login = " + login + " and U.Password = " + Password,
                                                     fbc, Transaction);
                    try
                    {
                        using (FbDataReader r = Query.ExecuteReader())
                        {
                            // Читаем результат запроса построчно - строка за строкой
                            if (r.Read())
                            {
                                res = r.GetString(0);
                            }
                        }
                    }
                    catch
                    {
                        WinLog.Write("Ошибка работы с базой данных, таблицей пользователь");
                        return res;
                    }
                }
            }
            return res;
        }
    }
}
