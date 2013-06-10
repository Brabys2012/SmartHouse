using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AsyncClient
{
    static class XMLLoader
    {
        /// <summary>
        /// Сохраняет параметры клиента в xml файл.
        /// </summary>
        /// <param name="pr">Объект хранящий параметры</param>
        public static void saveSettings(Params pr)
        {
             System.Xml.Serialization.XmlSerializer writer = 
                 new System.Xml.Serialization.XmlSerializer(typeof(Params));

             System.IO.StreamWriter file = new System.IO.StreamWriter(
                 "Configuration.xml");
             writer.Serialize(file, pr);
             file.Close();
        }

        /// <summary>
        /// Загружает ранее сохранённые параметры
        /// </summary>
        /// <returns>Параметры работы клиентского приложения</returns>
        public static Params getSetting()
        {
            Params pr = new Params();

            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Params));
            System.IO.StreamReader file = new System.IO.StreamReader(
                "Configuration.xml");
            pr = (Params)reader.Deserialize(file);
            file.Close();
            return pr;
        }
    }
}
