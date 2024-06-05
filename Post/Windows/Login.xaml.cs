using Post.Classes;
using System.Configuration;
using System.Windows;
using Post.Repositories;

namespace Post.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString;
        UserRepository userRepository = new UserRepository(connectionString, new ParcelRepository(connectionString, new CheckPointRepository(connectionString)));
        
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

            List<User>users = userRepository.GetAllUsers();

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
