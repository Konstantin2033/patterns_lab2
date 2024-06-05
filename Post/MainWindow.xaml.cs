using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Post.Classes;
using Post.Repositories;
using Post.Windows;

namespace Post
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseAdapter dataBase = new DatabaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
        static string connectionString = ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString;
        static CheckPointRepository checkPointRepository = new CheckPointRepository(connectionString);
        ParcelRepository parcelRepository = new ParcelRepository(connectionString, checkPointRepository);
        public MainWindow()
        {
            InitializeComponent();
            dataBase.CreateTables();
        }


        private void InputTrackCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void InputTrackCode_GotFocus(object sender, RoutedEventArgs e)
        {
            InputTrackCode.Text = string.Empty;
        }

        private void ButtonSubmitTrackCode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputTrackCode.Text))
            {
                Warning.ShowMessage("Введіть код");
                return;
            }
            else if (InputTrackCode.Text.Length != 12)
            {
                Warning.ShowMessage("Код складається з 12 цифр");
                return;
            }
            else if (InputTrackCode.Text.Equals("Номер посилки", StringComparison.OrdinalIgnoreCase))
            {
                Warning.ShowMessage("Введіть код");
                return;
            }
            else
            {
                List<Parcel> parcels = ParcelRepository.GetAllParcels();

                if (parcels != null && parcels.Count > 0)
                {
                    Parcel parcel = parcels.Find(p => p.Number == InputTrackCode.Text);
                    if (parcel != null)
                    {
                        ShowParcel showParcel = new ShowParcel(parcel);
                        showParcel.Show();
                        Hide();
                    }
                    else
                    {
                        Warning.ShowMessage("Такої посилки немає");
                        return;
                    }
                }
                else
                {
                    Warning.ShowMessage("Такої посилки немає");
                    return;
                }
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Hide();
        }
    }
}