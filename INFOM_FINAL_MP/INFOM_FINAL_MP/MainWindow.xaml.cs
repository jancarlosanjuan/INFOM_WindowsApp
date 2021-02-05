//sql stuff

using System;
using System.Data;
using System.Windows;

namespace INFOM_FINAL_MP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DB.EstablishConnection();
            Steam.InitializeApi("0DC598364E5B52EBB5D7427B15096FCB");
        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            DataView playerDataView = DBQuery.GetPlayerOverviewFromId(playerId).DefaultView;

            DataGrid.ItemsSource = playerDataView;
            PlayerName.Text = DBQuery.GetPlayerName(playerId);
        }

        private void MapStatsButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            DataGrid.ItemsSource = DBQuery.GetPlayerMapsFromId(playerId).DefaultView;
            PlayerName.Text = DBQuery.GetPlayerName(playerId);
        }

        private void AchievementsButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            DataGrid.ItemsSource = DBQuery.GetPlayerAchievementsFromId(playerId).DefaultView;
            PlayerName.Text = DBQuery.GetPlayerName(playerId);
        }

        private void WeaponsButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            DataGrid.ItemsSource = DBQuery.GetPlayerWeaponsFromId(playerId).DefaultView;
            PlayerName.Text = DBQuery.GetPlayerName(playerId);
        }

        private async void WriteOrRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;

            Player player = await Steam.GetPlayer(playerId);

            if (player == null)
            {
                MessageBox.Show("Player does not exist in Steam!");
                return;
            }

            if (DBQuery.DoesPlayerExist(playerId))
            {
                if (DBQuery.UpdatePlayer(player))
                {
                    MessageBox.Show("Player updated!");
                }
                else
                {
                    MessageBox.Show("Player update failed!");
                }
            }
            else
            {
                if (DBQuery.CreatePlayer(player))
                {
                    MessageBox.Show("Player created!");
                }
                else
                {
                    MessageBox.Show("Player creation failed!");
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;

            if (DBQuery.DeletePlayer(playerId))
            {
                MessageBox.Show("Player deleted!");
            }
            else
            {
                MessageBox.Show("Player not found!");
            }
        }
    }
}
