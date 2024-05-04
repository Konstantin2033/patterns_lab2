﻿using Post.Classes;
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
using System.Xml.Linq;

namespace Post.Windows
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private RegistrationMediator mediator;
        private DataBaseAdapter dataBase = new DataBaseAdapter(ConfigurationManager.ConnectionStrings["PostBase"].ConnectionString);
        private Window previousWindow;
        public Registration(Window previousWindow)
        {
            InitializeComponent();
            mediator = new RegistrationMediator(this);
            this.previousWindow = previousWindow;
            ButtonRegister.IsEnabled = false;
        }

        public void EnableRegisterButton() { ButtonRegister.IsEnabled = true; }
        public void DisableRegisterButton() { ButtonRegister.IsEnabled = false; }


        private void InputLoginRegister_GotFocus(object sender, RoutedEventArgs e) { InputLoginRegister.Text = string.Empty; }
        private void InputPasswordRegister_GotFocus(object sender, RoutedEventArgs e){InputPasswordRegister.Text = string.Empty;}
        private void InputSecondPasswordRegister_GotFocus(object sender, RoutedEventArgs e){InputSecondPasswordRegister.Text = string.Empty;}
        private void InputNameRegister_GotFocus(object sender, RoutedEventArgs e){InputNameRegister.Text = string.Empty;}
        private void InputLastNameRegister_GotFocus(object sender, RoutedEventArgs e){InputLastNameRegister.Text = string.Empty;}


        private void InputLoginRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputLoginRegister.Text != "" && InputLoginRegister.Text != "Логін") { mediator.NotifyLoginChanged(true); }
        }

        private void InputPasswordRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputPasswordRegister.Text != "" && InputPasswordRegister.Text != "Пароль") { mediator.NotifyFirstPasswordChanged(true); }
        }

        private void InputLastNameRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputLastNameRegister.Text != "" && InputLastNameRegister.Text != "Прізвище") { mediator.NotifySecondNameChanged(true); }
        }

        private void InputSecondPasswordRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputSecondPasswordRegister.Text != "" && InputSecondPasswordRegister.Text != "Повторіть пароль") { mediator.NotifySecondPasswordChanged(true); }
        }

        private void InputNameRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputNameRegister.Text != "" && InputNameRegister.Text != "Ім'я") { mediator.NotifyNameChanged(true); }
        }



        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonRegister.IsEnabled)
            {
                if (InputPasswordRegister.Text != InputSecondPasswordRegister.Text)
                {
                    Warning.ShowMessage("Паролі не співпадають");
                    return;
                }
                else
                {
                    User user = new User(0, InputNameRegister.Text, InputLastNameRegister.Text,
                        InputLoginRegister.Text, InputPasswordRegister.Text, null);
                    dataBase.AddUser(user);
                    Warning.ShowMessage("Акаунт створено!");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Hide();
                }
            }
            else
            {
                Warning.ShowMessage("Ви не ввели значення");
                return;
            }
        }

        

        private void ButtonBackRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }
    }
}
