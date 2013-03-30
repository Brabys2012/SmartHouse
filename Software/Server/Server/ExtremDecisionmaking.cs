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
        public CProtocol ProtocolForExDM = new CProtocol();

        public TableDivice BdDevice = new TableDivice();
        /// <summary>
        /// Метод обработки команды
        /// </summary>
        public byte[] ParserComand(object comand)
        {
            //В данном массиве будет содержатся команда на порт исполнитель 
            byte[] result = null;
            DevCommand comandArray = (DevCommand)comand;
            //Если команда меньше 4 байтов и больше 5 то это ошибочная команда ее не возможно обработать
            //Приходится возврашать 0 в нулевом номере массива, что значит, что никакой команды пересылать 
            //не нужно
            if ((comandArray.len < 6) || (comandArray.len > 7))
            {
                result = new byte[1];
                result[0] = 0;
            }
            else
            {
                //Сработал датчик
                if (comandArray.command[0] == 100)
                {
                    //Получаем по запросу какое устройство нужно выключить и сообщение на TCP сервер

                    ConfigForMess CFM = BdDevice.DeterDevice(comandArray.port, comandArray.device);
                    if (CFM.messege != "")
                    {
                        lock (Storage.MessegesForUser)
                        {
                            Storage.MessegesForUser.Enqueue(CFM.messege);
                        }
                    }

                    if ((CFM.number != null))
                    {
                        //Формируем команду для ComPort, которая отключит устройство которое нужно отключи
                        byte[] com = new byte[1];
                        com[0] = 0;
                        result = ProtocolForExDM.Pack(CFM.number[0], CFM.number[1], com);
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
        public void ParserAnswer(DevCommand Answer)
        {
            //Проверяем соответсвует ли ответ заданному формату
            if (Answer.len == 6)
            {
                //Удачно или не удачно выполнена команда
                if (Answer.command[0] == 1)
                {
                    try
                    {
                        BdDevice.UpdateDeviceState(Answer.port, Answer.device, 0);
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

        /// <summary>
        /// Создает список команд, которые будут запрашивать состояние каждого датчика
        /// </summary>
        public void MakeComandForDatcik()
        {
            throw new System.NotImplementedException();
        }
    }
}
