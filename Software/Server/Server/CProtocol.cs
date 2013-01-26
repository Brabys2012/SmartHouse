using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Server
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
            while (dataStream.Length > 0)
            {
                buffer = (byte) dataStream.ReadByte();
                try
                {
                    if ((buffer == 181) || (IsFindComand))
                    {
                        comand = new DevCommand();
                        count = (byte)dataStream.ReadByte();
                        comand.len = count;
                        comand.port = (byte)dataStream.ReadByte();
                        comand.device = (byte)dataStream.ReadByte();
                        for (int i = 0; i < count - 4; i++)
                        {
                            comand.command[i] = (byte)dataStream.ReadByte();
                        }
                        if (dataStream.ReadByte() != 74)
                        {
                            WinLog.Write("Пакет поврежден");
                        }
                        else
                        {
                            result.Add(comand);
                        }
                        IsFindComand = false;
                    }
                    else
                    {
                        while (buffer != 181)
                        {
                            buffer = (byte)dataStream.ReadByte();
                        }
                    }
                }
                catch
                {
                    WinLog.Write("Пакет поврежден");
                    return null;
                }
            }
            return result;
        }
    }
}
