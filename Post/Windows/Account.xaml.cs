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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
        private User user;
        public Account(User user)
        {
            InitializeComponent();
            this.user = user;
            updateParcels();
        }
        public void updateParcels()
        {
            List<Parcel> parcels = dataBase.GetAllParcels().FindAll(parcel => parcel.UserId == user.Id);
            if (parcels != null)
            {
                ParcelsListBox.ItemsSource = parcels;
            }
        }
        private void DetailButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Parcel parcel = (Parcel)button.Tag;
            ShowParcel showParcel = new ShowParcel(parcel);
            showParcel.Show();
            Hide();
        }

        private void CreateParcelButton_Click(object sender, RoutedEventArgs e)
        {
            CreateParcel createParcel = new CreateParcel(user, this);
            createParcel.Show();
            Hide();
        }

        private void UpdateListButton_Click(object sender, RoutedEventArgs e)
        {
            updateParcels();
        }
    }
}
