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
        public MainWindow()
        {
            InitializeComponent();
            NavigateToLoginPage();
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

        public void NavigateToLoginPage()
        {
            MainContent.NavigationService.Navigate(new Login());
        }

        public void NavigateToRegisterPage()
        {
            MainContent.NavigationService.Navigate(new Register());
        }

        public void NavigateToHomepage()
        {
            MainContent.NavigationService.Navigate(new Homepage());
        }

        public void NavigateToHomepage(User user)
        {
            MainContent.NavigationService.Navigate(new Homepage(user));
        }

        public void NavigateToAddNewQuiz()
        {
            MainContent.NavigationService.Navigate(new AddNewQuiz());
        }
        public void NavigateToTakeQuiz()
        {
            MainContent.NavigationService.Navigate(new TakeQuiz());
        }
    }
}

