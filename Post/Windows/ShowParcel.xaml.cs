using Post.Classes;
using Post.Repositories;
using System.Configuration;
using System.Windows;

namespace Post
{
    /// <summary>
    /// Interaction logic for ShowParcel.xaml
    /// </summary>
    public partial class ShowParcel : Window
    {
        private Parcel parcel;
        static string connectionString = ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString;
        private UserRepository userRepository = new UserRepository(connectionString, new ParcelRepository(connectionString, new CheckPointRepository(connectionString)));

        
        public ShowParcel(Parcel parcel)
        {
            InitializeComponent();
            this.parcel = parcel;
            ShowParcels();
        }

        public void ShowParcels()
        {
            if (this.parcel != null)
            {
                checkPointsListBox.ItemsSource = parcel.CheckPoints;

                User user = userRepository.GetAllUsers().Find(userTMP => userTMP.Id == parcel.UserId);

                if (user != null)
                {
                    string nameText = $"{user.GetFullName()}";
                    string descriptionText = $"Посилка #{parcel.Number}\n" +
                                             $"Назва: {parcel.Name}\n" +
                                             $"Отримувач: {parcel.Recipient}\n" +
                                             $"Опис: {parcel.Description}\n" +
                                             $"Дата відправки: {parcel.SendingTime.ToString("dd.MM.yy")}\nСтатус:";
                    descriptionText += parcel.IsDelivered ? " Прибув у відділення" : " В дорозі";

                    NameShowParcel.Content = nameText;
                    InfoLabelShowParcel.Content = descriptionText;
                }
            }
        }

        private void ButtonCloseShowParcel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }
    }
}