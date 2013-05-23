using System;
using System.Data;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;

namespace Server
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с таблицей USER базы данных.
    /// </summary>
    public class TableUser
    {
        /// <summary>
        /// Инициализирует экземпляр класса TableUser.
        /// </summary>
        public TableUser() { }
        /// <summary>
        /// Проверяет есть ли такой пользователь с указанным логином и паролем в базе данных.
        /// </summary>
        /// <param name="login">Логин пользователя, сушествование которого нужно проверить.</param>
        /// <param name="password">Пароль пользователя.</param>
        public static string CheckUser(string login, string password)
        {
            string res = "";
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
                            string sqlQuery = string.Format("select U.Role from Useres U where U.Login = '{0}' and U.Password = '{1}'", login, password);
                            using (FbCommand command = new FbCommand(sqlQuery, SQL.FB_dbConnection, transaction))
                            using (FbDataReader r = command.ExecuteReader())
                            {
                                // Читаем результат запроса построчно - строка за строкой
                                if (r.Read())
                                    res = r.GetString(0);
                            }
                            transaction.Commit();
                        }
                    }
                    catch (Exception exc)
                    {
                        // Делаем запись в журнал событий
                        WinLog.Write(string.Format("Ошибка получения данных о пользователе из БД: ", exc.Message), System.Diagnostics.EventLogEntryType.Error);
                    }
                // Сбрасываем признак транзакции
                SQL.IsLockedTransaction = false;
            }
            return res;
        }
        /// <summary>
        /// Добавляет пользователя с заданными параметрами в базу данных.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="pass">Пароль пользователя.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <returns>Признак успешного завершения операции.</returns>
        public static bool AddUser(string login, string pass, string role)
        {
            return SQL.SQL_ExecuteNoneQueryCommitTransaction(string.Format("insert into Useres (login, password, role) values ('{0}', '{1}', '{2}')", login, pass, role));
        }

        /// <summary>
        /// Удаляет пользователя из базы
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <returns></returns>
        public static bool DeleteUser(string login)
        {
            return SQL.SQL_ExecuteNoneQueryCommitTransaction("Delete from Useres where login = '" + login + "'");
        }

        /// <summary>
        /// Обновляет пароль пользователя
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="newPassword">новый пароль</param>
        /// <returns></returns>
        public static bool UpdatePassword(string login, string newPassword)
        {
           return SQL.SQL_ExecuteNoneQueryCommitTransaction("Update Useres set Password = '" + newPassword + "'" +
                                                            "where login = '" + login + "'");
        }
    }
}
