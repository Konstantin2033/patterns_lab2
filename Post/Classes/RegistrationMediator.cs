using Post.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Post.Classes
{
    public class RegistrationMediator
    {
        private bool login = false;
        private bool firstPassword = false;
        private bool secondPassword = false;
        private bool name = false;
        private bool secondName = false;

        private Registration _registration;

        public RegistrationMediator(Registration registration)
        {
            _registration = registration;
        }

        public void NotifyLoginChanged(bool value)
        {
            login = value;
            CheckFields();
        }

        public void NotifyFirstPasswordChanged(bool value)
        {
            firstPassword = value;
            CheckFields();
        }

        public void NotifySecondPasswordChanged(bool value)
        {
            secondPassword = value;
            CheckFields();
        }

        public void NotifyNameChanged(bool value)
        {
            name = value;
            CheckFields();
        }

        public void NotifySecondNameChanged(bool value)
        {
            secondName = value;
            CheckFields();
        }

        private void CheckFields()
        {
            if (login && firstPassword && secondPassword && name && secondName &&
                !string.IsNullOrEmpty(_registration.InputLoginRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputPasswordRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputSecondPasswordRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputNameRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputLastNameRegister.Text))
            {
                _registration.EnableRegisterButton();
            }
            else
            {
                _registration.DisableRegisterButton();
            }
        }
    }
}
