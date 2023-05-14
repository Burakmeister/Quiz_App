using NHibernate;
using NHibernate.Cfg;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User User { set; get; }

        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;

        private bool isLoginMode = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void changeMode_Click(object sender, RoutedEventArgs e)
        {
            isLoginMode = !isLoginMode;
            if (isLoginMode)
            {
                changeMode.Content = "Nie masz konta?";
                submit.Content = "Zaloguj";
            }
            else
            {
                changeMode.Content = "Posiadasz już konto?";
                submit.Content = "Zarejestruj";
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();


            if (isLoginMode)
            {
                // List Users
                using (mySession.BeginTransaction())
                {
                    IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.User u WHERE u.Login= :login AND u.Password = :password");
                    query.SetParameter("login", loginBox.Text);
                    query.SetParameter("password", paswdBox.Password);
                    object obj = query.UniqueResult();
                    if (obj != null)
                    {
                        User = (User)obj;
                    }
                    mySession.Transaction.Commit();
                }
                if (User == null)
                {
                    MessageBox.Show("Wprowadzono niewłaściwe dane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else
                {
                    MessageBox.Show("Witaj " + User.Login + "!", "Siema", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                }
            }
            else
            {
                // Add Record
                using (mySession.BeginTransaction())
                {
                    User user = new User { Login = loginBox.Text, Password = paswdBox.Password };
                    mySession.Save(user);
                    mySession.Transaction.Commit();
                }
            }
        }

        private void gotFocusLoginBox(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "Login")
            {
                loginBox.Text = string.Empty;
            }
        }

        private void lostFocusLoginBox(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == string.Empty)
            {
                loginBox.Text = "Login";
            }
        }
    }
}

