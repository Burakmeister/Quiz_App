﻿using System;
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
    /// Interaction logic for TakeQuiz.xaml
    /// </summary>
    public partial class TakeQuiz : Page
    {
        public TakeQuiz()
        {
            InitializeComponent();
        }

        private void navigateToHomePage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void showTheFinishQuizPopup(object sender, RoutedEventArgs e)
        {
            FinishQuizPopup.IsOpen = true;
        }
    }
}
