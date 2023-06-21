using MySqlX.XDevAPI.Common;
using Quiz_App.DAOs;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Quiz_App
{
    /// <summary>
    /// Logika interakcji dla klasy Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public List<Quiz> Quizzes { get; set; }
        public List<Models.Result> Results { get; set; }
        private Models.Result currentResult;
        public static User User { get; set; }

        public Homepage(User user) //nie jestem pewien ale wydaję mi się że trzeba rebuildować ten widok przy każdej zmianie czyli by użytkownik zobaczył zaktualizowany widok należy ... new Homepage();
        {
            InitializeComponent();
            QuizDao quizDao = new QuizDao();
            Quizzes = (List<Quiz>?)quizDao.findAll();
            Results = new List<Models.Result>();
            Homepage.User = user;
            DataContext = this;
        }

        public Homepage()
        {
            InitializeComponent();
            QuizDao quizDao = new QuizDao();
            Quizzes = (List<Quiz>?)quizDao.findAll();
            Results = new List<Models.Result>();
            DataContext = this;
        }

        private void navigateToLoginClick(object sender, RoutedEventArgs e)
        {
            //TODO: wyloguj użytkownika

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToLoginPage();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //index wybranego Quizu
        {
            QuizOption.IsOpen = false;
            ListBox listBox = (ListBox)sender;
            int selectedIndex = listBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Console.WriteLine("Selected Index: " + selectedIndex);
            }
            QuizOption.IsOpen = true;
        }

        private void refreshHomepage(object sender, RoutedEventArgs e)
        {
            //wyloguj użytkownika

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void navigateToAddNewQuizClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToAddNewQuiz();
            }
        }

        private void navigateToEditQuizClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToAddNewQuiz((Quiz)QuizesListBox.SelectedItem);
            }
        }

        private void removeQuiz(object sender, RoutedEventArgs e)
        {
            QuizDao dao = new QuizDao();
            Quiz quiz = (Quiz)QuizesListBox.SelectedItem;
            dao.delete(quiz);
            Quizzes.Remove(quiz);
            QuizOption.IsOpen = false;
            QuizesListBox.Items.Refresh();
        }


        // kod odpowiedzialny za działanie Loaded="Page_Loaded" w Homepage.xaml 
        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Window parentWindow = Window.GetWindow(this);
        //    parentWindow.StateChanged += ParentWindow_StateChanged;
        //}

        //private void ParentWindow_StateChanged(object sender, EventArgs e)
        //{
        //    WindowState windowState = (sender as Window).WindowState;
        //    if (windowState == WindowState.Minimized)
        //    {
        //        QuizOption.IsOpen = false;
        //    }
        //}

        private void navigateToTakeQuizClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToTakeQuiz((Quiz)QuizesListBox.SelectedItem);
            }
        }

        private void showQuizResultsPopup(object sender, RoutedEventArgs e)
        {
            Quiz currect = QuizesListBox.SelectedItem as Quiz;
            foreach (Models.Result res in currect.Scores){
                res.Percentage = res.Score;
                Results.Add(res);
            }
            QuizResults.IsOpen = true;
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // to do obsługa zmiany wyboru w liście wyników quizu
        }
    }
}
