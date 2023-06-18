using NHibernate;
using Org.BouncyCastle.Asn1.BC;
using Quiz_App.DAOs;
using Quiz_App.Models;
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
        //private User User { set; get; }

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
            UserDao userDao = new UserDao();
            
            if (userDao.checkIfLoginIsAvaliable(loginBox.Text) && checkCredentials(loginBox.Text, paswdBox.Password))//bool wskazujący czy credentials są poprawne 
            {
               
                User user = new User { Login = loginBox.Text, Password = paswdBox.Password };
                var isUSerSaved = userDao.saveUser(user);
                if (isUSerSaved)
                {
                    showUserSavedPopup();
                }
            }
            else
            {
                showTheCredentialsError();
            }
        }
        private async void showUserSavedPopup()
        {
            UserSavedPopup.IsOpen = true;
            await Task.Delay(2000);
            UserSavedPopup.IsOpen = false;
        }

        private async void showTheCredentialsError()
        {
            ErrorPopup.IsOpen = true;
            await Task.Delay(2000);
            ErrorPopup.IsOpen = false;
        }

        private bool checkCredentials(string login, string password)
        {
            if (login.Length>=5 && password.Length>=8)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
    }
}
