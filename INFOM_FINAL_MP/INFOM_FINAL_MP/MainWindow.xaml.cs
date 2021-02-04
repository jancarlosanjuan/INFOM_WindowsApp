//sql stuff
using System.Windows;

namespace INFOM_FINAL_MP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DB.EstablishConnection();
        }

        private void MapStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            User user = DBQuery.GetUserFromId(playerId);

            PlayerName.Text = user.SteamName;
            PlayerKD.Text = user.KdRatio.ToString("F2");
            PlayerKills.Text = user.TotalKills.ToString();
            PlayerDeaths.Text = user.TotalDeaths.ToString();
            PlayerWins.Text = user.TotalWins.ToString();
            PlayerLosses.Text = user.TotalLosses.ToString();
        }
    }
}
