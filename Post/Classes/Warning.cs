using System.Windows;

namespace Post.Classes
{
    public static class Warning
    {
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
