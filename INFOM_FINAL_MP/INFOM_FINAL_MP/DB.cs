using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace INFOM_FINAL_MP
{
    public static class DB
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static DataTable dataTable;
        private static MySqlDataAdapter dataAdapter;

        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
                stringBuilder.Server = "127.0.0.1";
                stringBuilder.UserID = "root";
                stringBuilder.Password = "1225";
                stringBuilder.Database = "csgo";
                stringBuilder.SslMode = MySqlSslMode.None;
                stringBuilder.AllowPublicKeyRetrieval = true;
                stringBuilder.AutoEnlist = true;
                stringBuilder.AllowUserVariables = true;
                connection = new MySqlConnection(stringBuilder.ToString());

                // Get the logger
                // MessageBox.Show("Database connection successful", "Connection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Failed!");
            }
        }

        // E.g. "DB.RunQuery("SELECT * FROM players;");
        // E.g. "DB.RunQuery("SELECT * FROM players WHERE player_id = @player_id;", new MySqlParameter("@player_id", "1234567890"));
        public static MySqlCommand RunQuery(string query, params MySqlParameter[] parameters)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    foreach (MySqlParameter parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    if (command.ExecuteNonQuery() == 0)
                    {
                        command = null;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Invalid query!", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }

            return command;
        }
    }
}
