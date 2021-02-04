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
    public class Users
    {
        private string username;

        public Users(string username)
        {
            Username = username;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

    }
}
