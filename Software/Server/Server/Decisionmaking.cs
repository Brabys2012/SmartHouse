using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Класс отвечает за принятие решений исходя из команды, которая была послана пользователем
    /// </summary>
    public class Decisionmaking
    {
        private TableDivice BdDevice = new TableDivice();
        private CProtocol ProtocolForDM = new CProtocol();

        /// <summary>
        /// Основной парсер команд от пользователя, который определяеть тип команды 
        /// и выполняет соответствующие действия
        /// </summary>
        /// <param name="comand"></param>
        /// <returns></returns>
        public byte[] Parser(string comand, string users)
        {
            byte[] Result;
            string[] SplitComand = comand.Split('/');
            switch (SplitComand[0])
            {
                case "SetParam":
                    Result = SetParam(SplitComand);
                    break;
                case "AddUser":
                    bool ResCom = TableUser.AddUser(SplitComand[1], SplitComand[2], SplitComand[3]);
                    lock (Storage.MessegesForUser)
                    {
                        Storage.MessegesForUser.Enqueue("ResAddUser/" + ResCom.ToString() + "@" + users);
                    }
                    Result = new byte[1];
                    Result[0] = 0;
                    break;
                case "AddDev":
                    Result = AddDevInDB(SplitComand, users);
                    break;
                default:
                    Result = new byte[1];
                    Result[1] = 0;
                    break;
            }
            return Result;
        }

        /// <summary>
        /// Метод, который формирует команду для изменения состояния
        /// устроив Формат команды который был SetParam/ИмяУстройства/Параметр
        /// </summary>
        /// <param name="SplitComand">Массив которы содержит команду от пользователя</param>
        /// <returns></returns>
        private byte[] SetParam(string[] SplitComand)
        {
            DevCommand ForNum = new DevCommand();
            byte[] MyDC;
            if (SplitComand[1] != "")
            {
                ForNum = BdDevice.DeterDevByName(SplitComand[1]);
                int Com = Convert.ToInt32(SplitComand[2]);
                //Перевод int переменной в массив из байт
                byte[] intBytes = BitConverter.GetBytes(Com);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(intBytes);
                MyDC = ProtocolForDM.Pack(ForNum.port,ForNum.device, intBytes);
            }
            else
            {
                MyDC = new byte[1];
                MyDC[0] = 0;
            }
            return MyDC;
        }

        /// <summary>
        /// Метод отвечает за обработку команды от пользователя связанную 
        /// с добавлением новго устройства в базу данных
        /// этот метод так же формирует команду запроса состояния добавленного устройства
        /// </summary>
        private byte[] AddDevInDB(string[] SplitComand, string user)
        {
            byte[] result;
            bool ResCom = false;
            if (SplitComand.Count() == 6)
            {
                ResCom = BdDevice.AddNewDevice(SplitComand[0], SplitComand[1], SplitComand[2],
                    SplitComand[3], SplitComand[4], SplitComand[5]);
                lock (Storage.MessegesForUser)
                {
                    Storage.MessegesForUser.Enqueue("ResAddDev/" + ResCom.ToString() + "@" + user);
                }
                if (ResCom)
                {
                    result = ProtocolForDM.Pack(Convert.ToByte(SplitComand[0]), Convert.ToByte(SplitComand[1]), null);
                }
                else
                {
                    result = new byte[1];
                    result[0] = 0;
                }
            }
            else
            {
                result = new byte[1];
                result[0] = 0;
                lock (Storage.MessegesForUser)
                {
                    Storage.MessegesForUser.Enqueue("ResAddDev/" + ResCom.ToString() + "@" + user);
                }
            }
            return result;
        }

        public void ParseAnswer(DevCommand Answer, string Com)
        {
            string[] SplitCom = Com.Split('/');
            switch (SplitCom[0])
            {
                case "SetParam":
                    SetParamAnswer(Answer, SplitCom);
                    break;
                case "AddDev":
                    AddDevInDbAnswer(Answer);
                    break;
            }
        }

        private void SetParamAnswer(DevCommand Answer, string[] SplitCom)
        {
            try
            {
                if (Answer.command[0] == 1)
                {
                    BdDevice.UpdateDeviceState(Answer.port, Answer.device, Convert.ToInt32(SplitCom[2]));
                }
                else
                {
                    string Mess = "Устройство " + SplitCom[1] + " не изменило свое состояние!";
                    WinLog.Write(Mess, System.Diagnostics.EventLogEntryType.Error);
                }
            }
            catch
            {
            }
        }

        private void AddDevInDbAnswer(DevCommand Answer)
        {
            try
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(Answer.command);
                int i = BitConverter.ToInt32(Answer.command, 0);
                BdDevice.UpdateDeviceState(Answer.port, Answer.device, i);
            }
            catch
            {
            }
        }

    }
}
