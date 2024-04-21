using Post.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Post.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
        public Login()
        {
            InitializeComponent();
        }

        private void InputPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPassword.Text = string.Empty;
        }

        private void InputLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLogin.Text = string.Empty;
        }

        private void ButtonEnterLogin_Click(object sender, RoutedEventArgs e)
        {
            string Login = InputLogin.Text;
            string Password = InputPassword.Text;

            List<User>users = dataBase.GetAllUsers();

            if (users != null)
            {
                User user = users.Find(userTMP => userTMP.Login == Login && userTMP.Password == Password);
                if (user != null) {
                    Account account = new Account(user);
                    account.Show();
                    Hide();
                    return;
                }
                else
                {
                    Warning.ShowMessage("Невірний логін або пароль");
                    return;
                }
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration(this);
            registration.Show();
            Hide();
        }
    }

}
