using Org.BouncyCastle.Asn1.BC;
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
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }


        private void navigateToLoginClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToLoginPage();
            }
        }

        private void checkTheRegisterCredentials(object sender, RoutedEventArgs e)
        {
            if (false)//bool wskazujący czy credentials są poprawne 
            {

            }
            else
            {
                showTheCredentialsError();
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
