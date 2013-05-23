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
                case "DeleteDevice":
                    Result = new byte[1];
                    Result[0] = 0;
                    DeleteDevice(SplitComand, users);
                    break;
                case "DeleteUser":
                    Result = new byte[1];
                    Result[0] = 0;
                    DeleteUser(SplitComand, users);
                    break;
                case "UpdatePassword":
                    Result = new byte[1];
                    Result[0] = 0;
                    UpdatePassword(SplitComand, users);
                    break;
                default:
                    Result = new byte[1];
                    Result[0] = 0;
                    WinLog.Write("Команда не распознана", System.Diagnostics.EventLogEntryType.Error);
                    lock (Storage.MessegesForUser)
                        Storage.MessegesForUser.Enqueue("mess/Ваша команда не корректна@" + users); 
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
            if (SplitComand.Count() == 7)
            {
                ResCom = BdDevice.AddNewDevice(SplitComand[1], SplitComand[2], SplitComand[3],
                    SplitComand[4], SplitComand[5], SplitComand[6]);
                lock (Storage.MessegesForUser)
                {
                        Storage.MessegesForUser.Enqueue("ResAddDev/" + ResCom.ToString() + "@" + user);
                }
                if (ResCom)
                {
                    result = ProtocolForDM.Pack(Convert.ToByte(SplitComand[1]), Convert.ToByte(SplitComand[2]), null);
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

        /// <summary>
        /// Парсит ответ от контролера с учетом команды, присланной пользователем
        /// </summary>
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

        /// <summary>
        /// Парсит ответ от контролера установил, контролер значение или нет
        /// </summary>
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
                string Mess = "Устройство " + SplitCom[1] + " не изменило свое состояние!";
                WinLog.Write(Mess, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Метод обрабатывает отвте от контролера, в котором содержится состояние нового добавленного устройства.
        /// </summary>
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
                WinLog.Write("Не удалось обновить состояние устройства, после его добавления", System.Diagnostics.EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Метод отвечает за обработку команды удаления устройства
        /// </summary>
        private void DeleteDevice(string[] splitComand, string user)
        {
            bool ResCom = BdDevice.DeleteDevice(splitComand[1]);
            lock (Storage.MessegesForUser)
                Storage.MessegesForUser.Enqueue("ResDeleteDev/" + ResCom.ToString() + "@" + user);
        }

        private void DeleteUser(string[] splitComand, string user)
        {
            bool ResCom = TableUser.DeleteUser(splitComand[1]);
            lock (Storage.MessegesForUser)
                Storage.MessegesForUser.Enqueue("ResDeleteUser/" + ResCom.ToString() + "@" + user);
        }

        private void UpdatePassword(string[] splitComand, string users)
        {
            string Role = TableUser.CheckUser(splitComand[1], splitComand[3]);
            if (Role == "")
            {
                lock (Storage.MessegesForUser)
                    Storage.MessegesForUser.Enqueue("ResUpdatePas/" + "Неправильно введен текущий пароль@" + users);
            }
            else
            {
                bool ResCom = TableUser.UpdatePassword(splitComand[1], splitComand[2]);
                if (ResCom)
                {
                    lock (Storage.MessegesForUser)
                        Storage.MessegesForUser.Enqueue("ResUpdatePas/" + "Пароль успешно изменен@" + users);
                }
                else
                {
                    lock (Storage.MessegesForUser)
                        Storage.MessegesForUser.Enqueue("ResUpdatePas/" + "При изменении пароля возникли ошибки, обратитесь к администратору@" + users);
                }
            }

        }
    }
}
