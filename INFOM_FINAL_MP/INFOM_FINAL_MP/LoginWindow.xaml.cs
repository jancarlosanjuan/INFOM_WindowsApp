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
using System.Windows.Shapes;

namespace INFOM_FINAL_MP
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DB.EstablishConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         //   MessageBox.Show("HEY");
            string username = Username.Text;
            string password = Password.Text;
            ClientUser clientuser = DBQuery.getClientUser(username);

            if(clientuser == null)
            {
                MessageBox.Show("User does not exist!");
                return;
            }
            
            if (clientuser.Password.Equals(password))
            {
                MessageBox.Show("Login Success!");
             //   MessageBox.Show(clientuser.IsAdmin.Equals("True").ToString());

                //if admin
                if (clientuser.IsAdmin.Equals("True"))
                {
                    MessageBox.Show("Hello Admin!");
                    MainWindow mainwindow = new MainWindow();
                    mainwindow.Show();


                    
                    Close();
                    return;
                }
                else if(clientuser.IsAdmin.Equals("False"))
                {
                    MessageBox.Show("Hello User!");
                    MainWindow mainwindow = new MainWindow();
                    mainwindow.Show();

                    mainwindow.Pencil.Visibility = Visibility.Hidden;
                    mainwindow.Upload.Visibility = Visibility.Hidden;
                    mainwindow.Update.Visibility = Visibility.Hidden;
                    mainwindow.Delete.Visibility = Visibility.Hidden;
                    Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Login Failed!");
            }

        }
    }
}
