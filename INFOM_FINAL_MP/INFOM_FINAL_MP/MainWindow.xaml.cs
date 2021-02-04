//sql stuff

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //establish connection to db
            DB.EstablishConnection();
        }


        private void MapStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            sql_2_2.Text = "Wadizup";
            //grab the username
            string steam_name = SearchBar.Text;

            //   Users user = QUERY.GetUser(steam_name);
            DataGrid.ItemsSource = QUERY.getDataTable(steam_name).DefaultView;
        }
    }
}
