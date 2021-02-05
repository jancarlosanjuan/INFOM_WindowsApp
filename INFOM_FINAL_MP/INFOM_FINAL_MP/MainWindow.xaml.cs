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
            Steam.InitializeApi("0DC598364E5B52EBB5D7427B15096FCB");
        }

        private void MapStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerId = SearchBar.Text;

            //Player player = await Steam.GetPlayer(playerId);
            //DBQuery.CreatePlayer(player);

            DataGrid.ItemsSource = DBQuery.GetPlayerDataTableFromId(playerId).DefaultView;
        }
    }
}
