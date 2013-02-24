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
        public DevCommand Parser(string comand)
        {
            string[] SplitComand = comand.Split('/');
            DevCommand Result = new DevCommand();
            switch (SplitComand[0])
            {
                case "SetParam":
                    Result = SetParam(SplitComand);
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
        public DevCommand SetParam(string[] SplitComand)
        {
            DevCommand MyDC = new DevCommand();
            if (SplitComand[1] != "")
            {
                MyDC = BdDevice.DeterDevByName(SplitComand[1]);
                if (MyDC.len != 0)
                {
                    int Com = Convert.ToInt32(SplitComand[2]);
                    //Перевод int переменной в массив из байт
                    byte[] intBytes = BitConverter.GetBytes(Com);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(intBytes);
                    MyDC.command = intBytes;
                    MyDC.len =Convert.ToByte(MyDC.len + MyDC.command.Count());
                }
            }
            else
            {
                MyDC.len = 0;
            }
            return MyDC;
        }

    }
}
