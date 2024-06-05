using Post.Classes;
using Post.Repositories;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Post.Windows
{
    public partial class CreateParcel : Window
    {
        private bool name = false;
        private bool description = false;
        private bool recipient = false;
        private ParcelRepository parcelRepository;
        private User user;
        private Window previousWindow;
        private CheckPointRepository checkPointRepository;

        public CreateParcel(User user, Window previousWindow)
        {
            InitializeComponent();
            this.user = user;
            this.previousWindow = previousWindow;
            string connectionString = ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString;
            parcelRepository = new ParcelRepository(connectionString, new CheckPointRepository(connectionString));
            checkPointRepository = new CheckPointRepository(connectionString);
        }

        private void InputRecipient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(InputRecipient.Text) && InputRecipient.Text != "Recipient") { recipient = true; }
        }

        private void InputParcelName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(InputParcelName.Text) && InputParcelName.Text != "Parcel Name") { name = true; }
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(InputDescription.Text) && InputDescription.Text != "Description") { description = true; }
        }

        private void InputRecipient_GotFocus(object sender, RoutedEventArgs e)
        {
            InputRecipient.Text = string.Empty;
        }

        private void InputParcelName_GotFocus(object sender, RoutedEventArgs e)
        {
            InputParcelName.Text = string.Empty;
        }

        private void InputDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            InputDescription.Text = string.Empty;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (name && description && recipient)
            {
                string number = RandomNumber();

                Parcel parcel = new Parcel(0, user.Id, number, InputParcelName.Text, InputDescription.Text, InputRecipient.Text, DateTime.Now, null, null, false);
                parcelRepository.AddParcel(parcel);

                List<CheckPoint> checkPoints = new List<CheckPoint>();
                checkPoints.Add(new CheckPoint(parcel.Id, "Creation of the parcel", DateTime.Now));
                checkPoints.Add(new CheckPoint(parcel.Id, "Parcel waiting for dispatch", DateTime.Now));

                foreach (CheckPoint checkPoint in checkPoints)
                {
                    checkPointRepository.AddCheckPoint(checkPoint);
                }

                Warning.ShowMessage("Parcel created!");

                previousWindow.Show();
                Close();
            }
            else
            {
                Warning.ShowMessage("Fields are not filled");
            }
        }

        private string RandomNumber()
        {
            Random random = new Random();
            string number = "";
            for (int i = 0; i < 12; i++) { number += random.Next(10).ToString(); }
            return number;
        }

        private void ButtonBackRegister_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }
    }
}
