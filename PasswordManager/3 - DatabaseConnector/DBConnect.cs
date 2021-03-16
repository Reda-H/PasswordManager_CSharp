using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.IO;
using PasswordManager._1___Classes;
using System.Diagnostics;


namespace PasswordManager._3___DatabaseConnector
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string charset = "utf8";


        private string table = "accountsinfo";

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
                server = "localhost";
                database = "connectcsharp";
                uid = "root";
                password = "Kv12Rs3C4Rb";
                string connectionString =
                    "SERVER=" + server + ";"
                    + "DATABASE=" + database + ";"
                    + "UID=" + uid + ";"
                    + "PASSWORD=" + password + ";"
                    ;
                connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            } catch (MySqlException ex)
            {
                // 0 : Can't connect to server
                // 1045 : Invalid Credentials
                // Other
                switch (ex.Number)
                {
                    case 0: 
                        MessageBox.Show("Cannot Connect to Server. \nError: " + ex.Message);
                        break;
                    case 1045:
                        MessageBox.Show("Invalid Credentials. \nError: " + ex.Message);
                        break;
                    default:
                        MessageBox.Show("Expection: " + ex.Message);
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            } catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string accountName, string email, string password,
            string extraInfo1 = "", string extraInfo2 = "", string extraInfo3 = "")
        {
            string query = $"INSERT INTO {table} (accountName, email, password, extraInfo1, extraInfo2, extraInfo3) VALUES('{accountName}','{email}', '{password}', '{extraInfo1}', '{extraInfo2}', '{extraInfo3}')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string accountName, string email, string password,
            string extraInfo1, string extraInfo2, string extraInfo3)
        {
            string query = $"UPDATE {table} SET password='{password}'," +
                $" extraInfo1='{extraInfo1}', extraInfo2='{extraInfo2}', extraInfo3='{extraInfo3}'" +
                $" WHERE accountName='{accountName}' AND email='{email}'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string accountName, string email)
        {
            string query = $"DELETE FROM {table} WHERE accountName='{accountName}' AND email='{email}'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        ////Select statement
        public List<Account> Select()
        {
            string query = $"SELECT * FROM {table} ORDER BY id";

            //Create a list to store the result
            List<Account> listAccounts = new List<Account>();
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    listAccounts.Add(
                        new Account(
                            Int32.Parse(dataReader["id"].ToString()),
                            dataReader["accountName"].ToString(), 
                            dataReader["email"].ToString(), 
                            dataReader["password"].ToString(),
                            dataReader["extraInfo1"].ToString(),
                            dataReader["extraInfo2"].ToString(),
                            dataReader["extraInfo3"].ToString()
                            ));
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return listAccounts;
            }
            else
            {
                return listAccounts;
            }
        }

        ////Count statement
        //public int Count()
        //{
        //}

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore!");
            }
        }
    }
}
