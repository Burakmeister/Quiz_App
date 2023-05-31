using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Quiz_App
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }


        private void navigateToRegisterClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToRegisterPage();
            }
        }

        private void checkTheLoginCredentials(object sender, RoutedEventArgs e)
        {

            if (true)//bool wskazujący czy credentials są poprawne 
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.NavigateToHomepage();
                }
            }
            else 
            {
                //showTheCredentialsError();
            }
        }

        private async void showTheCredentialsError()
        {
            ErrorPopup.IsOpen = true;
            await Task.Delay(2000);
            ErrorPopup.IsOpen = false;
        }
    }
}
