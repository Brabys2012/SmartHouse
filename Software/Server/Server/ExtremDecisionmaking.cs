using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Класс отвечает за принятие решений исходя из команды, которая была послана каналом слушателем
    /// </summary>
    public class ExtremDecisionmaking
    {
        private TableDivice BdDevice = new TableDivice();
        /// <summary>
        /// Метод обработки команды
        /// </summary>
        public byte[] ParserComand(object comand)
        {
            //В данном массиве будет содержатся команда на порт исполнитель 
            byte[] result = null;
            //Т.к. ComPort слушатель пересылает команды как массив из байтов то и приводим их к первоначальному виду
            byte[] comandArray = (byte[])comand;
            //Если команда меньше 4 байтов и больше 5 то это ошибочная команда ее не возможно обработать
            //Приходится возврашать 0 в нулевом номере массиво, что значит, что никакой команды пересылать 
            //не нужно
            if ((comandArray.Count() < 4) && (comandArray.Count() > 5))
            {
                result = new byte[1];
                result[0] = 0;
            }
            else
            {
                byte numPort = comandArray[1];
                byte numDev = comandArray[2];
                //Сработал датчик
                if (comandArray[3] == 100)
                {
                    //Получаем по запросу какое устройство нужно выключить и сообщение на TCP сервер

                    ConfigForMess CFM = BdDevice.DeterDevice(numPort, numDev);

                    Storage.MessegesForUser.Enqueue(CFM.messege);

                    if ((CFM.number != null) && (CFM.number[0] != null) && (CFM.number[1] != null))
                    {
                        //Формируем команду для ComPort, которая отключит устройство которое нужно отключить
                        result = new byte[4];
                        result[0] = 4;
                        result[1] = CFM.number[0];
                        result[2] = CFM.number[1];
                        result[3] = 0;
                    }
                    else
                    {
                        result = new byte[1];
                        result[0] = 0;
                    }
                }
            }
            return result;
        }
    }
}
