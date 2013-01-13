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
        public TableDivice BdDevice = new TableDivice();
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
                    if (CFM.messege != "")
                    {
                        lock (Storage.MessegesForUser)
                        {
                            Storage.MessegesForUser.Enqueue(CFM.messege);
                        }
                    }

                    if ((CFM.number != null))
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

        /// <summary>
        /// Метод обработки ответа от исполняющего ком порта, выключил он устройство или нет
        /// </summary>
        public void ParserAnswer(byte[] Answer)
        {
            //Проверяем соответсвует ли ответ заданному формату
            if (Answer.Count() == 4)
            {
                //Удачно или не удачно выполнена команда
                if (Answer[3] == 1)
                {
                    try
                    {
                        BdDevice.UpdateDeviceState(Answer[0], Answer[1], 0);
                    }
                    catch
                    {
                        lock (Storage.MessegesForUser)
                            Storage.MessegesForUser.Enqueue("Произошла ошибка сервера");
                    }
                }
                else
                {
                    lock (Storage.MessegesForUser)
                        Storage.MessegesForUser.Enqueue("Произошла ошибка сервера");
                }
            }
        }
    }
}
