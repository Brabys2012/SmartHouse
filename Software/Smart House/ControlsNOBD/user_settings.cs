using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace ControlsNOBD
{
    public class user_settings
    {
        //считывание настроек по умолчанию из файла
        public user_settings()
        {
            if (!(this.readSettingsFromFile("settings.vrconf")))
            {
                this.setDefaultSettings();
                MessageBox.Show("Настройки сгенерированы автоматически по-умолчанию");
            }
            this.setDBSettings(this._server, this._schema, this._login, this._password, this._security, this._win_auth);
        }

        public ArrayList _settingsFromFile;
        public string _server;
        public string _schema;
        public string _login;
        public string _password;
        public bool _security;
        public bool _win_auth;

        //Установка данных для подключения к базе
        public bool setDBSettings(String t_server, String t_schema, String t_user, String t_pass, bool psec, bool win_auth)
        {
            string final_conn_string = "";
            if (!(win_auth))
            {
                //string final_conn_string = "";
                //Сервер...
                final_conn_string += "Data Source=";
                final_conn_string += t_server;
                final_conn_string += ";";
                //Схема...
                final_conn_string += "Initial Catalog=";
                final_conn_string += t_schema;
                final_conn_string += ";";
                //Безосность...
                final_conn_string += "Persist Security Info=";
                if (psec) { final_conn_string += "true"; }
                else { final_conn_string += "false"; };
                final_conn_string += ";";
                //Пользователь...
                final_conn_string += "User ID=";
                final_conn_string += t_user;
                final_conn_string += ";";
                //Пароль...
                final_conn_string += "Password=";
                final_conn_string += t_pass;
            }
            else
            {
                //Безосность...
                final_conn_string += "Persist Security Info=False;";
                final_conn_string += "Integrated Security=true;";
                //Схема...
                final_conn_string += "Initial Catalog=";
                final_conn_string += t_schema;
                final_conn_string += ";";
                //Сервер
                final_conn_string += "Server=";
                final_conn_string += t_server;
            }
            if (!(Program.data_module.set_connection_settings(final_conn_string)))
            {
                MessageBox.Show("Параметры указаны неверно");
                return false;
            }
            //Сохранение настроек в user_set
            this._server = t_server;
            this._schema = t_schema;
            this._login = t_user;
            this._password = t_pass;
            this._security = psec;
            this._win_auth = win_auth;
            return true;
        }

        //установка параметров пользователя по умолчанию
        public void setDefaultSettings()
        {
            this._server = "LOCALHOST";
            this._schema = "vrdb";
            this._login = "sa";
            this._password = "server";
            this._security = true;
            this._win_auth = true;
        }

        // считывание  настроек из файла
        public bool readSettingsFromFile(String setFile)
        {

            _settingsFromFile = new ArrayList();
            if (File.Exists(setFile))
            {
                this._settingsFromFile.Clear();
                StreamReader inFile = new StreamReader(setFile);
                String line = "";
                while ((line = inFile.ReadLine()) != null)
                {
                    if (line.Length != 0)
                        if (line[0] != '#') //ignore comments in settings (config) file
                        {
                            this._settingsFromFile.Add(line);
                            //MessageBox.Show(line);
                        }
                }
                inFile.Close();
                //parsing data from settings file...
                String param_name = "";
                String param_value = "";
                bool b_param_value = true;
                foreach (String conf in this._settingsFromFile)
                {
                    param_name = conf.Substring(0, conf.IndexOf(" ")).Trim();
                    param_value = conf.Substring(conf.IndexOf(" ")).Trim();
                    if ((param_value == "0") || (param_value == "1") || (param_value.ToLower() == "true") || (param_value.ToLower() == "false")) //this is logical value (true or false)
                    {
                        switch (param_value)
                        {
                            case "0":
                            case "False":
                            case "false":
                            case "FALSE":
                                b_param_value = false;
                                break;
                            case "1":
                            case "True":
                            case "true":
                            case "TRUE":
                                b_param_value = true;
                                break;

                        }
                        switch (param_name)
                        {
                            //проверка подлинности Windows или базы
                            case "db_pers_sec":
                                this._security = b_param_value;
                                break;
                            case "db_win_auth":
                                this._win_auth = b_param_value;
                                break;
                        }
                    }
                    else // this is not logical value (example "192.168.0.1" or "Local User")
                    {
                        switch (param_name)
                        {
                            case "db_server":
                                this._server = param_value;
                                break;
                            case "db_schema":
                                this._schema = param_value;
                                break;
                            case "db_user":
                                this._login = param_value;
                                break;
                            case "db_pass":
                                this._password = param_value;
                                break;
                        }
                    }
                }
                this._settingsFromFile.Clear();
                return true;
            }
            MessageBox.Show("Файл конфигурации приложения не найден!");
            return false;
        }

        //Сохраненние данных подключения в файл
        public void saveSettingsToFile()
        {
            String[] sets = {"db_pers_sec " + this._security.ToString(),
                             "db_win_auth " + this._win_auth.ToString(),
                             "db_server " + this._server.ToString(),
                             "db_schema " + this._schema.ToString(),
                             "db_user " + this._login.ToString(),
                             "db_pass " + this._password.ToString()
                            };
            System.IO.File.WriteAllLines(@"settings.vrconf", sets);
        }

    }

}
