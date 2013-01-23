using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{

    public struct DevCommand
    {
        public byte port;
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

        public static byte[] Pack(byte addrController, byte addrDevice, byte[] command)
        {

        }

        public static DevCommand Unpack(Stream dataStream)
        {
            
        }

    }
}
