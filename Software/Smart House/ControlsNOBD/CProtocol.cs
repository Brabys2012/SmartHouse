using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ControlsNOBD
{

    public struct DevCommand
    {
        public byte port;
        public byte len;
        public byte device;
        public byte[] command;
    }

    /// <summary>
    /// Протокол обмена управляющими сообщениями и данными.
    /// </summary>
    public class CProtocol
    {

        /*
         * Формат пакета протокола:
         * [0xB5][l][p][d][c]0x4A]
         * [0xB5][l][p][d][c][0x4A]
         * [0xB5]  - символ начала пакета;
         * [l]     - длина пакета в байтах;
         * [p]     - номер порта;
         * [d]     - номер устройства, подключенного к порту;
         * [c]     - команда или ответ на команду;
         * [0x4A]  - символ конца пакета.
        */

        public byte[] Pack(byte addrController, byte addrDevice, byte[] command)
        {
            byte len = (byte)(5 + command.Count());
            byte[] result = new byte[len];
            result[0] = 181;
            result[1] = len;
            result[2] = addrController;
            result[3] = addrDevice;
            for (int i = 4; i < command.Count() + 4; i++)
            {
                result[i] = command[i - 4];
            }
            result[4 + command.Count()] = 74;
            return result;
        }

       public ArrayList Unpack(Stream dataStream)
        {
            byte buffer;
            byte count;
            bool IsFindComand = false;
            DevCommand comand;
            ArrayList result = new ArrayList();
            int SubBuffer = (int)dataStream.ReadByte(); 
            while (SubBuffer != -1)
            {
                try
                {
                    buffer = (byte)SubBuffer;
                    if ((buffer == 181) || (IsFindComand))
                    {
                        comand = new DevCommand();
                        count = (byte)dataStream.ReadByte();
                        comand.len = count;
                        comand.command = new byte[count - 5];
                        comand.port = (byte)dataStream.ReadByte();
                        comand.device = (byte)dataStream.ReadByte();
                        for (int i = 0; i < count - 5; i++)
                        {
                            comand.command[i] = (byte)dataStream.ReadByte();
                        }
                        if (dataStream.ReadByte() != 74)
                        {
                        }
                        else
                        {
                            result.Add(comand);
                        }
                        IsFindComand = false;
                    }
                    else
                    {
                        while ((SubBuffer != 181) || (SubBuffer != -1))
                        {
                            SubBuffer = (byte)dataStream.ReadByte();
                        }
                    }
                    SubBuffer = (int)dataStream.ReadByte(); 
                }
                catch
                {
                    return result;
                }
            }
            return result;
        }
    }
}

