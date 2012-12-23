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
    }
}
