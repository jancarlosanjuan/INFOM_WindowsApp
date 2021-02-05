namespace INFOM_FINAL_MP
{
    public class ClientUser
    {
        public string username;
        public string password;
        public string isadmin;
        public ClientUser (string username, string password, string isadmin)
        {
            UserName = username;
            Password = password;
            IsAdmin = isadmin;
        }

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string IsAdmin
        {
            get { return isadmin; }
            set { isadmin = value; }
        }
    }
}
