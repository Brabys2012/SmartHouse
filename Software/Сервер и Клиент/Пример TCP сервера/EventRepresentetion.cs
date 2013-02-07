using System.Net;

namespace SimpleAsyncServer
{

    /// <summary>
    /// Предоставляет универсальный метод, позволяющий получить информацию о произошедшем событии.
    /// </summary>
    /// <param name="eventType">Тип произошедшего события.</param>
    /// <param name="eventStr">Текстовое представление случившегося события.</param>
    public delegate void EventRepresentationDelegate(EventRepresentetionType eventType, string eventStr);

    /// <summary>
    /// Содержит список возможных представлений информации о случившемся событии, передаваемые конфигуратору сервера.
    /// </summary>
    public enum EventRepresentetionType
    {
        /// <summary>
        /// Подключение клиента. string.Format("{0};;{1};;{2}", Name, client.Name, client.IMEI)
        /// </summary>
        CLIENT_CONNECT = 0,
        /// <summary>
        /// Отключение клиента. string.Format("{0};;{1};;{2}", Name, client.Name, EventMessage)
        /// </summary>
        CLIENT_DISCONNECT = 1,
        /// <summary>
        /// Произошла ошибка при подключении клиента.
        /// </summary>
        CLIENT_ERROR = 2,
        /// <summary>
        /// Серверный TCP порт инициирован. string.Format("{0}", Name)
        /// </summary>
        SERVER_PORT_INITIALIZED = 10,
        /// <summary>
        /// Серверный TCP порт запущен. string.Format("{0}", Name)
        /// </summary>
        SERVER_PORT_RUNNING = 11,
        /// <summary>
        /// Серверный порт TCP остановлен. string.Format("{0}", Name)
        /// </summary>
        SERVER_PORT_STOPPED = 12,
        /// <summary>
        /// Ошибка в работе TCP порта. string.Format("{0}", Name)
        /// </summary>
        SERVER_PORT_ERROR = 13,
        /// <summary>
        /// Признак передачи записи из журнала событий. string.Format("{0};;{1}", Name, LogMessage)
        /// </summary>
        SERVER_LOG_ENTRY = 14,

        /// <summary>
        /// Признак необходимости перезагрузить настройки службы и перезапустить ее.
        /// </summary>
        SERVICE_RELOAD_PREFERENCES = 100,
        /// <summary>
        /// Признак необходимости проверить текущее состояние всех подключенных клиентов.
        /// </summary>
        SERVICE_CHECK_CLIENT_ACTIVITY = 101
    }

}