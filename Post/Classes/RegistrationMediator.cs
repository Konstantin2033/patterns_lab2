using Post.Windows;

namespace Post.Classes
{
    public class RegistrationMediator
    {
        private readonly Registration _registration;
        private bool _isLoginValid;
        private bool _isFirstPasswordValid;
        private bool _isSecondPasswordValid;
        private bool _isNameValid;
        private bool _isSecondNameValid;

        public RegistrationMediator(Registration registration)
        {
            _registration = registration;
        }

        public void NotifyLoginChanged(bool isValid)
        {
            _isLoginValid = isValid;
            CheckAndUpdateButtonState();
        }

        public void NotifyFirstPasswordChanged(bool isValid)
        {
            _isFirstPasswordValid = isValid;
            CheckAndUpdateButtonState();
        }

        public void NotifySecondPasswordChanged(bool isValid)
        {
            _isSecondPasswordValid = isValid;
            CheckAndUpdateButtonState();
        }

        public void NotifyNameChanged(bool isValid)
        {
            _isNameValid = isValid;
            CheckAndUpdateButtonState();
        }

        public void NotifySecondNameChanged(bool isValid)
        {
            _isSecondNameValid = isValid;
            CheckAndUpdateButtonState();
        }

        private void CheckAndUpdateButtonState()
        {
            bool areAllFieldsValid =
                _isLoginValid &&
                _isFirstPasswordValid &&
                _isSecondPasswordValid &&
                _isNameValid &&
                _isSecondNameValid &&
                !string.IsNullOrEmpty(_registration.InputLoginRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputPasswordRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputSecondPasswordRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputNameRegister.Text) &&
                !string.IsNullOrEmpty(_registration.InputLastNameRegister.Text);

            if (areAllFieldsValid)
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
