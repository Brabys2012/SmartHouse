using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ControlsNOBD
{
    public class data_source
    {

        public data_source()
        {
            this._conn = new SqlConnection();
            this._ds = new DataSet();
            this._conn_status = "DISCONNECTED";
            if (!(this.set_connection_settings("Persist Security Info=False;Integrated Security=true;Initial Catalog=ControlerDevices;Server=User-ПК\\SQLEXPRESS")))
                MessageBox.Show("Неверная строка подключения!");
        }

        private string _active_table; 
        private DataSet _ds;
        private SqlConnection _conn;
        private SqlDataAdapter _da;
        private SqlCommandBuilder _cb;
        private string _conn_status;
        private string _connection_string;

        //установка данных для подключения 
        public bool set_connection_settings(string conn_string)
        {
            this._connection_string = conn_string;
            try
            {
                this._conn.ConnectionString = this._connection_string;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //подключение к базе данных
        public string connect_to_db()
        {
            this._conn_status = "PROCESS_CONNECTING";
            try
            {
                this._conn.Open();
            }
            catch (Exception ex)
            {
                this._conn_status = "DISCONNECTED";
                return ex.Data + " " + ex.Message;
            }
            this._conn_status = "CONNECTED";
            return "OK";
        }

        //отключеие от бызы данных
        public string disconnect_db()
        {
            this._conn_status = "PROCESS_DISCONNECTING";
            try
            {
                this._conn.Close();
            }
            catch (Exception ex)
            {
                this._conn_status = "CONNECTED";
                return ex.Data + " " + ex.Message;
            }
            this._conn_status = "DISCONNECTED";
            return "OK";
        }

        // возвращает состояние подключения
        public string get_connect_status()
        {
            return this._conn_status;
        }

        //извлекает информацию из базы данных
        public DataSet get_data_table(string type_name)
        {
            string query = "";
            query += "select";
            query += " Name , Num_Port, Adress, currentVaule";
            query += " from ";
            query += "Devices ";
            query += "Where TypeDev='" + type_name + "'";
            try
            {
                this._ds = new DataSet();
                this._da = new SqlDataAdapter(query, this._conn);
                this._cb = new SqlCommandBuilder(this._da);

                this._da.Fill(this._ds, "Devices");
                this._active_table = "Devices";
                return this._ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data + " " + ex.Message);
                return null;
            }
        }

        //применяет изменения внесённые в базу данных
        public void apply_changes(string table_name)
        {
            this._da.Update(this._ds, table_name);
        }

        //добавляет новое устройство в базу данных
        public string AddDevices(string Name, string Num_port, string Adress, string TypeDev, string currentValue)
        {
            string query = "Insert into Devices ( Name, currentVaule, Num_Port, Adress, TypeDev) values (";
            if (Name != "")
            {
                query += "'" + Name + "', ";
            }
            else
            {
                query += "null, ";
            }
            if (currentValue != "")
            {
                query += "'" + currentValue + "', ";
            }
            else
            {
                query += "null, ";
            }
            query += Num_port + ", ";
            query += Adress + ", ";
            query += "'"+TypeDev + "')";
            try
            {
                SqlCommand com = this._conn.CreateCommand();
                com.CommandText = query;
                com.ExecuteNonQuery();
                com.Dispose();
            }

            catch (Exception ex)
            {
                return (ex.Message + " " + ex.Data);
            }

            return "OK";
        }

        //обновляет значения некоторого устройства
        public string UpdDevices(string Name, string Num_port, string Adress, string TypeDev, string curVaul)
        {
            string query = "Update Devices set ";
            if (Name != "")
            {
                query += "Name = '" + Name + "', ";
            }
            else
            {
                query += "Name = null, ";
            }
            query += "TypeDev = '" + TypeDev + "', ";
            if (curVaul != "")
            {
                query += "currentVaule = " + curVaul + "  ";
            }
            else
            {
                query += "currentVaule = null, ";
            }
            query += "Where Num_Port = " + Num_port + " and " + "Adress = " + Adress;
            try
            {
                SqlCommand com = this._conn.CreateCommand();
                com.CommandText = query;
                com.ExecuteNonQuery();
                com.Dispose();
            }

            catch (Exception ex)
            {
                return ("ERROR_" + ex.Message + " " + ex.Data);
            }

            return "OK";
        }

        //удаляет некотрое устройство
        public string DeleteDevices(string Num_port, string Adress)
        {
            string query = "Delete from Devices where ";
            query += " Num_port = " + Num_port + " and " + "Adress = " + Adress;
            try
            {
                SqlCommand com = this._conn.CreateCommand();
                com.CommandText = query;
                com.ExecuteNonQuery();
                com.Dispose();
            }
            catch (Exception ex)
            {
                return ("ERROR" + " " + ex.Message + " " + ex.Data);
            }

            return "OK";

        }

        //считывает значение у некоторого устройства
        public string FindCurentVal(string Num_Port, string Adress)
        {
            string curVal=null;
            string query = "select currentVaule from Devices where Num_Port= " + Num_Port + " and " + "Adress=" + Adress;
            try
            {
                SqlCommand com = this._conn.CreateCommand();
                com.CommandText = query;
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    if (!rd.IsDBNull(0))
                    {
                        curVal = rd.GetInt32(0).ToString();
                    }
                    else
                    {
                        curVal = "";
                    }
                }
                rd.Close();
                rd.Dispose();
                com.Dispose();
            }
            catch (Exception ex)
            {
                curVal = "Error";
            }
            return curVal;
        }

        //обновляет значение некоторого устройства
        public bool UpdDeviceVal(string Num_port, string Adress, string curVaul)
        {
            string TypeDevice = FindTypeDev(Num_port, Adress);
            if (TypeDevice == "Диммер" || ((TypeDevice == "Простое устройство")&(Convert.ToInt32(curVaul) >= 1)))
            {
                string query = "Update Devices set ";
                if (curVaul != "")
                {
                    query += "currentVaule = " + curVaul + "  ";
                }
                else
                {
                    query += "currentVaule = null, ";
                }
                query += "Where Num_Port = " + Num_port + " and " + "Adress = " + Adress;
                try
                {
                    SqlCommand com = this._conn.CreateCommand();
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                    com.Dispose();
                }

                catch (Exception ex)
                {
                    return (false);
                }

                return true;
            }

            else
            {
                return false;
            }
        }

        public string FindTypeDev(string Num_port, string Adress)
        {
            string TypeDev = "";
            string query = "Select TypeDev from Devices where Num_Port = " + Num_port + " and " + "Adress = " + Adress;
            try
            {
                SqlCommand com = this._conn.CreateCommand();
                com.CommandText = query;
                SqlDataReader rd = com.ExecuteReader();
                if (rd.Read())
                {
                    if (!rd.IsDBNull(0))
                    {
                        TypeDev = rd.GetString(0);
                    }
                    else
                    {
                        TypeDev = "";
                    }
                }
                rd.Close();
                rd.Dispose();
                com.Dispose();
            }
            catch (Exception ex)
            {
                TypeDev = "Error";
            }
            return TypeDev;

        }
    }
}
