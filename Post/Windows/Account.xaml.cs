using Post.Classes;
using Post.Repositories;
using System.Configuration;
using System.Windows;

namespace Post.Windows
{
    public partial class Account : Window
    {
        private ParcelRepository parcelRepository;
        private User user;

        public Account(User user)
        {
            InitializeComponent();
            this.user = user;
            string connectionString = ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString;
            CheckPointRepository checkPointRepository = new CheckPointRepository(connectionString);
            parcelRepository = new ParcelRepository(connectionString, checkPointRepository);
            UpdateParcelsList();
        }

        private void UpdateParcelsList(string searchText = "", string sortField = "Id", bool sortAscending = true)
        {
            try
            {
                List<Parcel> parcels = parcelRepository.GetAllParcels().FindAll(parcel => parcel.UserId == user.Id);

                if (!string.IsNullOrEmpty(searchText))
                {
                    parcels = parcels.Where(p => p.Number.Contains(searchText) || p.Name.Contains(searchText)).ToList();
                }

                switch (sortField)
                {
                    case "Number":
                        parcels = sortAscending ? parcels.OrderBy(p => p.Number).ToList() : parcels.OrderByDescending(p => p.Number).ToList();
                        break;
                    case "Name":
                        parcels = sortAscending ? parcels.OrderBy(p => p.Name).ToList() : parcels.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "SendingTime":
                        parcels = sortAscending ? parcels.OrderBy(p => p.SendingTime).ToList() : parcels.OrderByDescending(p => p.SendingTime).ToList();
                        break;
                    default:
                        parcels = sortAscending ? parcels.OrderBy(p => p.Id).ToList() : parcels.OrderByDescending(p => p.Id).ToList();
                        break;
                }
                ParcelsListBox.ItemsSource = parcels;
                
            } catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні списку посилок: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DetailButtonClick(object sender, RoutedEventArgs e)
        {
            if (ParcelsListBox.SelectedItem is Parcel parcel)
            {
                ShowParcel showParcel = new ShowParcel(parcel);
                showParcel.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть посилку для перегляду деталей.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void CreateParcelButton_Click(object sender, RoutedEventArgs e)
        {
            CreateParcel createParcel = new CreateParcel(user, this);
            createParcel.Show();
            Hide();
        }

        private void UpdateListButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateParcelsList();
        }
    }
}