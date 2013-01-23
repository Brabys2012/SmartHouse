using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{

    /// <summary>
    /// Класс содержит методы для работы с журналом Windows.
    /// </summary>
    public class WinLog
    {

        /// <summary>
        /// Журнал событий Windows.
        /// </summary>
        private static EventLog _log;

        /// <summary>
        /// Метод инициализации журнала событий Windows.
        /// </summary>
        public static void Init()
        {
            // Иницируем журнал событий Windows
            try
            {
                // Если не существует windows лога службы, то создаем его и назначаем по-умолчанию для записи
                if (!EventLog.SourceExists("SmartHouseService"))
                    EventLog.CreateEventSource("SmartHouseService", "SmartHouseServiceLog");
                // Иницируем журнал событий Windows
                _log = new EventLog();
                _log.Source = "SmartHouseService";
                _log.Log = "SmartHouseServiceLog";
                _log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 30);
            }
            catch (Exception)
            {
                if (_log != null)
                    _log.Dispose();
                _log = null;
            }
        }

        /// <summary>
        /// Записывает событие в журнал Windows.
        /// </summary>
        /// <param name="mess">Сообщение.</param>
        public static void Write(string mess)
        {
            try
            {
                _log.WriteEntry(mess);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Записывает событие в журнал Windows.
        /// </summary>
        /// <param name="mess">Сообщение.</param>
        /// <param name="etype">Тип произошедшего события.</param>
        public static void Write(string mess, EventLogEntryType etype)
        {
            try
            {
                _log.WriteEntry(mess, etype);
            }
            catch (Exception) { }
        }

    }
}
