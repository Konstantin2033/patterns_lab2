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
    /// Interaction logic for CreateParcel.xaml
    /// </summary>
    public partial class CreateParcel : Window
    {
        private bool name = false;
        private bool description = false;
        private bool recipient = false;
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
        private User user;
        private Window previousWindow;
        public CreateParcel(User user, Window previousWindow)
        {
            InitializeComponent();
            this.user = user;
            this.previousWindow = previousWindow;
        }

        private void InputRecipient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputRecipient.Text != "" && InputRecipient.Text != "Логін") { recipient = true; }
        }

        private void InputParcelName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputParcelName.Text != "" && InputParcelName.Text != "Логін") { name = true; }
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputDescription.Text != "" && InputDescription.Text != "Логін") { description = true; }
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
                string number = RandomtNumber();
                
                Parcel parcel = new Parcel(0,user.Id, number, InputParcelName.Text,InputDescription.Text,InputRecipient.Text,DateTime.Now,null,null,false);
                dataBase.AddParsel(parcel);

                List<CheckPoint> checkPoints = new List<CheckPoint>();
                checkPoints.Add(new CheckPoint(dataBase.GetAllParcels().Find(par => par.Number == number).Id,"Створення посилки",DateTime.Now));
                checkPoints.Add(new CheckPoint(dataBase.GetAllParcels().Find(par => par.Number == number).Id, "Посилка очікує відправлення", DateTime.Now));
                foreach (CheckPoint checkPoint in checkPoints)
                {
                    dataBase.AddCheckPoint(checkPoint);
                }
                Warning.ShowMessage("Посилку створено!");
                
                previousWindow.Show();
                Close();
            }
            else
            {
                Warning.ShowMessage("Поля не заповнені");
            }
        }
        private string RandomtNumber()
        {
            Random random = new Random();
            string number = "";
            for(int i = 0; i < 12; i++) { number += random.Next(10).ToString(); }
            return number;
        }

        private void ButtonBackRegister_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }
    }
}
