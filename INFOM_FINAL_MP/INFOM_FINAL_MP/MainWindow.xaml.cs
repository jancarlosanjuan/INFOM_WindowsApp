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
         //   MessageBox.Show("Sign In", )
        }


        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            //User user = DBQuery.GetUserFromId(playerId);

            //PlayerName.Text = user.SteamName;
            // PlayerKD.Text = user.KdRatio.ToString("F2");
            // PlayerKills.Text = user.TotalKills.ToString();
            // PlayerDeaths.Text = user.TotalDeaths.ToString();
            //  PlayerWins.Text = user.TotalWins.ToString();
            //  PlayerLosses.Text = user.TotalLosses.ToString();
            DataGrid.ItemsSource = DBQuery.GetPlayerFromName2(playerId).DefaultView;

        }

        private void MatchStatsButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;
            DataGrid.ItemsSource = DBQuery.getUserMatches(playerId).DefaultView;
        }
    }
}
