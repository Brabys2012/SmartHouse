using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;

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
            throw new System.NotImplementedException();
        }
    }
}
