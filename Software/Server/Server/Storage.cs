using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace Server
{
    /// <summary>
    /// Класс хранит общие для всех потоков переменные, такие как очередь заявок на создание команд и др.
    /// </summary>
    public static class Storage
    {
        /// <summary>
        /// Очередь из команд присланных пользователями
        /// </summary>
        public static Queue QueueTCP;
        /// <summary>
        /// Очередь из команд присланных компортом слушателем
        /// </summary>
        public static Queue QueueList;
        /// <summary>
        /// Массив объектов устройств, которые будут присылатся для обновления пользователям, у которых включен конфигуратор
        /// </summary>
        public static DataSet ArrayUpdate;
        /// <summary>
        /// Флаг который показывает используется или нет база данных
        /// </summary>
        public static bool BD;
        /// <summary>
        /// Показывает занят или нет ком порт
        /// </summary>
        public static bool  flagComPort = true;
        /// <summary>
        /// Блокирует доступ к ком порту исполнителю, если он занят другим потоком.
        /// </summary>
        public static object lockerComPort;
        /// <summary>
        /// Очередь из сообщений, которые должен получить пользователь
        /// </summary>
        public static Queue MessegesForUser;
        /// <summary>
        /// Блокирует доступ к БД если она занята другим потоком
        /// </summary>
        public static object lockerBdDev;
        /// <summary>
        /// Флаг показывает обновлен список устройств для бд или нет
        /// </summary>
        public static bool Updated;
        /// <summary>
        /// Команды для запроса состояния датчиков
        /// </summary>
        private static Queue DatchikComand;
    }

    /// <summary>
    /// Класс служит для того что бы в объект считать нужные данные в БД и вернуть их
    /// </summary>
    public class ConfigForMess
    {
        public byte[] number = new byte[2];
        public string messege;
    }
    /// <summary>
    /// Класс который хранит информацию об устройтвах
    /// нужен для обновления клиентских приложений
    /// </summary>
    public class Device
    {
        //Имя устройства
        public string Name;
        //Тип устройства
        public string Type;
        //Состояние устройства
        public string State;
    }
}
