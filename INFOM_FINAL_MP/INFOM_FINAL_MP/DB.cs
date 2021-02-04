using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace INFOM_FINAL_MP
{
    public static class DB
    {
        private static MySqlConnection connection;
        private static MySqlCommand command = null;
        private static DataTable dataTable;
        private static MySqlDataAdapter dataAdapter;

        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
                stringBuilder.Server = "127.0.0.1";
                stringBuilder.UserID = "root";
                stringBuilder.Password = "";
                stringBuilder.Database = "CSGO";
                stringBuilder.SslMode = MySqlSslMode.None;
                stringBuilder.AllowPublicKeyRetrieval = true;
                stringBuilder.AutoEnlist = true;
                connection = new MySqlConnection(stringBuilder.ToString());

                //get the logger thingy
                MessageBox.Show("Database connection successful", "Connection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!");
            }
        }


        //get mysql commands
        public static MySqlCommand RunQuery(string query, string username)
        {
            try
            {
                if(connection != null)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@steam_name", username);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Invalid!", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }
            return command;
        }
    }
}
