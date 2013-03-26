using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;

namespace RUS_Project.libTCPServer
{

    /// <summary>
    /// Класс для работы с БД.
    /// </summary>
    public class SQL
    {

        /*
         * TODO: README:
         * 
         * Перед использованием необходимо задать корректные значения четырех следующих констант.
         * Кроме того, чтобы программа запускалась без ошибок необходимо, чтобы:
         * 1. Желательно чтобы все проекты были на .Net Framework 3.5 (полный, а не ClientProfile).
         * 2. Добавить в ссылки проекта библиотеку FirebirdSql.Data.FirebirdClient (ее лучше поместить
         * вместе с исходными файлами каждого проекта). Брать из каталога Ресурсы для проекта.
         * 3. Правой клавишей по файлу FirebirdSql.Data.FirebirdClient в обозревателе решений
         * в свойствах задать флаг Копировать локально = True.
         * 4. При компиляции проекта помещать в выходную папку каталог fbdb из Ресурсы для проекта.
         */


        /// <summary>
        /// Полное имя файла базы данных.
        /// </summary>
        private const string _DB_FullFileName = AppDomain.CurrentDomain.BaseDirectory + "\\ENERGYNET.FDB";
        /// <summary>
        /// Имя пользователя для доступа к БД.
        /// </summary>
        private const string _DB_Login = "admin";
        /// <summary>
        /// Пароль для доступа к БД.
        /// </summary>
        private const string _DB_Paswd = "admin";
        /// <summary>
        /// Полное имя файла базы данных.
        /// </summary>
        private const string _DB_ClientLibraryPath = AppDomain.CurrentDomain.BaseDirectory + "\\fbdb\\fbembed.dll";


        /// <summary>
        /// Подключение к БД Firebird.
        /// </summary>
        private static FbConnection FB_dbConnection;
        /// <summary>
        /// Опции транзакции чтения в БД Firebird.
        /// </summary>
        private static FbTransactionOptions FB_dbReadTransactionOptions;
        /// <summary>
        /// Опции транзакции исполнения в БД Firebird.
        /// </summary>
        private static FbTransactionOptions FB_dbCommitTransactionOptions;

        /// <summary>
        /// Осуществляет инициализацию работы с БД службы.
        /// </summary>
        public static void Init()
        {
            // Инициализируем подключение к базе данных
            try
            {
                // Задаем параметры подключения
                FbConnectionStringBuilder conn_str = new FbConnectionStringBuilder();
                conn_str.ServerType = FbServerType.Embedded;
                conn_str.Charset = "WIN1251";
                conn_str.Dialect = 3;
                conn_str.Role = "";
                conn_str.Database = _DB_FullFileName;
                //conn_str.DataSource = "localhost";
                //conn_str.Port = 3050;
                //conn_str.ConnectionLifeTime = 0;
                //conn_str.Pooling = true;
                //conn_str.MinPoolSize = 0;
                //conn_str.MaxPoolSize = 50;
                conn_str.UserID = _DB_Login;
                conn_str.Password = _DB_Paswd;
                conn_str.ClientLibrary = _DB_ClientLibraryPath;
                conn_str.PacketSize = 16384;
                // Создаем подключение к БД
                FB_dbConnection = new FbConnection(conn_str.ToString());
                // Инициализируем и задаем параметры транзакций
                FB_dbReadTransactionOptions = new FbTransactionOptions();
                FB_dbReadTransactionOptions.TransactionBehavior = FbTransactionBehavior.Read | FbTransactionBehavior.ReadCommitted | FbTransactionBehavior.RecVersion;
                FB_dbCommitTransactionOptions = new FbTransactionOptions();
                FB_dbCommitTransactionOptions.TransactionBehavior = FbTransactionBehavior.NoWait | FbTransactionBehavior.ReadCommitted | FbTransactionBehavior.RecVersion;
            }
            catch (Exception exc)
            {
                // Освобождаем ресурсы
                if (FB_dbConnection != null)
                    FB_dbConnection.Dispose();
                FB_dbConnection = null;
            }
            // Устанавливаем признак того, что в данный момент транзакций нет
            _isLockedTransaction = false;
        }
        /// <summary>
        /// Закрывает подключение к БД службы.
        /// </summary>
        public static void Close()
        {
            // Закрываем подключение к БД
            if (FB_dbConnection != null)
            {
                try
                {
                    FB_dbConnection.Close();
                    FB_dbConnection.Dispose();
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Признак того, что в данный момент выполняется транзакция.
        /// </summary>
        private static bool _isLockedTransaction;
        /// <summary>
        /// Выполняет определенный пользователем SQL запрос, не возвращающий каких-либо данных.
        /// </summary>
        /// <param name="sqlProcedureName">SQL запрос для выполнения в БД.</param>
        private static void SQL_ExecuteNoneQueryCommitTransaction(string sqlQuery)
        {
            // Ожадаем пока не закончится начатая транзакция
            while (_isLockedTransaction)
                Thread.Sleep(5);
            // Устанавливаем признак начала транзакции
            _isLockedTransaction = true;
            // Выполняем транзакцию
            if (FB_dbConnection != null)
                try
                {
                    if (FB_dbConnection.State == ConnectionState.Closed)
                        FB_dbConnection.Open();
                    using (FbTransaction transaction = FB_dbConnection.BeginTransaction(FB_dbCommitTransactionOptions))
                    using (FbCommand command = new FbCommand(sqlQuery, FB_dbConnection, transaction))
                        command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception) { }
            // Сбрасываем признак транзакции
            _isLockedTransaction = false;
        }


        /*
         * TODO: README:
         * 
         * Любое выполнение процедуры или запроса (которые не возвращают никаких данных)
         * можно вынести в отдельный метод (как в примере ниже).
         * Чтобы обрабатывать результат выполнения запроса необходимо сделать метод
         * аналогичный SQL_ExecuteNoneQueryTransaction, в котором осуществлять чтение полученных из БД данных.
         */

        /// <summary>
        /// Выполняет скрипт SQL запроса для обновления состояния клиента.
        /// </summary>
        /// <param name="state">Новое состояние.</param>
        public static void sqlQuery_AnyQuery(string state)
        {
            SQL_ExecuteNoneQueryCommitTransaction(string.Format("execute procedure UPDATE_CLIENT_STATE ('{0}')", state));
        }

    }

}
