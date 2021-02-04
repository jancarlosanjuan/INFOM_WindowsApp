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
    public static class QUERY
    {
        private static MySqlCommand command = null;
        private static DataTable dataTable;
        private static MySqlDataAdapter dataAdapter;

        //dont use this this sucks ass
        public static Users GetUser(string steam_name)
        {
            string query = "select * from match_player left join players on players.player_id = match_player.player_id where players.steam_name = (@steam_name) limit 1";
            command = DB.RunQuery(query, steam_name);
            Users user = null;
            if(command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                MainWindow mainWindow = new MainWindow();

                mainWindow.DataGrid.ItemsSource = dataTable.DefaultView;

               // MW.ShowDialog();                               //Startup your MainWindow and check your

                foreach (DataRow row in dataTable.Rows)
                {
                    string steamName = row["steam_name"].ToString();
                    user = new Users(steamName);
                    
                }
            }
            return user;
        }

        //this is the one that works
        public static DataTable getDataTable(string steam_name)
        {
            string query = "select * from match_player left join players on players.player_id = match_player.player_id where players.steam_name = (@steam_name) limit 1";
            command = DB.RunQuery(query, steam_name);
            Users user = null;
            if (command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

    }
}
