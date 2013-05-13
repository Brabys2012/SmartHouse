using System;
using System.Data;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;

namespace Server
{

    /// <summary>
    /// Класс для работы с БД.
    /// </summary>
    public class SQL
    {

        /// <summary>
        /// Полное имя файла базы данных.
        /// </summary>
        private static string _DB_FullFileName;
        /// <summary>
        /// Имя пользователя для доступа к БД.
        /// </summary>
        private const string _DB_Login = "SYSDBA";
        /// <summary>
        /// Пароль для доступа к БД.
        /// </summary>
        private const string _DB_Paswd = "masterkey";
        /// <summary>
        /// Полное имя файла базы данных.
        /// </summary>
        private static string _DB_ClientLibraryPath;


        /// <summary>
        /// Подключение к БД Firebird.
        /// </summary>
        public static FbConnection FB_dbConnection;
        /// <summary>
        /// Опции транзакции чтения в БД Firebird.
        /// </summary>
        public static FbTransactionOptions FB_dbReadTransactionOptions;
        /// <summary>
        /// Опции транзакции исполнения в БД Firebird.
        /// </summary>
        public static FbTransactionOptions FB_dbCommitTransactionOptions;

        /// <summary>
        /// Осуществляет инициализацию работы с БД службы.
        /// </summary>
        public static void Init()
        {
            // Инициализируем подключение к базе данных
            _DB_FullFileName = AppDomain.CurrentDomain.BaseDirectory + "\\DEVICES.FB";
            _DB_ClientLibraryPath = AppDomain.CurrentDomain.BaseDirectory + "\\fbdb\\fbembed.dll";
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
                // Делаем запись в журнал событий
                WinLog.Write(string.Format("Ошибка инициализации БД: ", exc.Message), System.Diagnostics.EventLogEntryType.Error);
                // Освобождаем ресурсы
                if (FB_dbConnection != null)
                    FB_dbConnection.Dispose();
                FB_dbConnection = null;
            }
            // Устанавливаем признак того, что в данный момент транзакций нет
            IsLockedTransaction = false;
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
        public static bool IsLockedTransaction { set; get; }
        /// <summary>
        /// Выполняет определенный пользователем SQL запрос, не возвращающий каких-либо данных.
        /// </summary>
        /// <param name="sqlProcedureName">SQL запрос для выполнения в БД.</param>
        public static bool SQL_ExecuteNoneQueryCommitTransaction(string sqlQuery)
        {
            // Ожадаем пока не закончится начатая транзакция
            while (IsLockedTransaction)
                Thread.Sleep(5);
            // Устанавливаем признак начала транзакции
            IsLockedTransaction = true;
            // Выполняем транзакцию
            if (FB_dbConnection != null)
                try
                {
                    if (FB_dbConnection.State == ConnectionState.Closed)
                        FB_dbConnection.Open();
                    using (FbTransaction transaction = FB_dbConnection.BeginTransaction(FB_dbCommitTransactionOptions))
                    {
                        using (FbCommand command = new FbCommand(sqlQuery, FB_dbConnection, transaction))
                            command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                catch (Exception exc)
                {
                    // Делаем запись в журнал событий
                    WinLog.Write(string.Format("Ошибка выполнения запроса к БД: ", exc.Message), System.Diagnostics.EventLogEntryType.Error);
                    // Сбрасываем признак транзакции
                    IsLockedTransaction = false;
                    // Возвращаем результ
                    return false;
                }
            // Сбрасываем признак транзакции
            IsLockedTransaction = false;
            // Возвращаем результ
            return true;
        }

    }

}
