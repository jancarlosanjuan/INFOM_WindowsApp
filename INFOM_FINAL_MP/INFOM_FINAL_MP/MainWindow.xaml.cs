using System.Windows;

namespace INFOM_FINAL_MP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Steam.InitializeApi("0DC598364E5B52EBB5D7427B15096FCB");
            DB.EstablishConnection();
        }

        private async void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;

            Player player = DBQuery.GetPlayerFromId(playerId);

            if (player != null) // Player exists in our database
            {
                UpdateWindow(player);
            }
            else // Must get player from steam
            {
                player = await Steam.GetPlayer(playerId);

                if (DBQuery.CreatePlayer(player))
                {
                    UpdateWindow(player);
                }
                else
                {
                    
                }
            }
        }

        private void UpdateWindow(Player player)
        {
            PlayerName.Text = player.SteamName;
            PlayerKD.Text = player.KdRatio.ToString("F2");
            PlayerKills.Text = player.TotalKills.ToString();
            PlayerDeaths.Text = player.TotalDeaths.ToString();
            PlayerWins.Text = player.TotalWins.ToString();
            PlayerLosses.Text = player.TotalLosses.ToString();
        }

        private void MapStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
