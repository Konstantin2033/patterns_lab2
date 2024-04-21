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
using Post.Classes;

namespace Post
{
    /// <summary>
    /// Interaction logic for ShowParcel.xaml
    /// </summary>
    public partial class ShowParcel : Window
    {
        private Parcel parcel;
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);

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

                User user = dataBase.GetAllUsers().Find(userTMP => userTMP.Id == parcel.UserId); ;

                if (user != null) { 
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
