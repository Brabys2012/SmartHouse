using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
        public static ArrayList ArrayUpdate;
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
    }

    /// <summary>
    /// Класс служит для того что бы в объект считать нужные данные в БД и вернуть их
    /// </summary>
    public class ConfigForMess
    {
        public byte[] number = new byte[2];
        public string messege;
    }
}
