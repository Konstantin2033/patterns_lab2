using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Post.Classes;
using Post.Windows;

namespace Post
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
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
                List<Parcel> parcels = dataBase.GetAllParcels();

                if (parcels != null && parcels.Count > 0)
                {
                    Parcel parcel = parcels.Find(parcelTMP => parcelTMP.Number == InputTrackCode.Text);
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