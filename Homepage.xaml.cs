using Quiz_App.DAOs;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
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
        public List<Result> Results { get; set; }
        public static User User { get; set; }

        public class Result
        {
            public string Name { get; set; }
            public string Date { get; set; }

            public Result()
            {
                Name = String.Empty;
                Date = String.Empty;
            }
        }

        public Homepage(User user) //nie jestem pewien ale wydaję mi się że trzeba rebuildować ten widok przy każdej zmianie czyli by użytkownik zobaczył zaktualizowany widok należy ... new Homepage();
        {
            InitializeComponent();
            QuizDao quizDao = new QuizDao();
            Quizzes = (List<Quiz>?)quizDao.findAll();
            Homepage.User = user;
            DataContext = this;
        }

        public Homepage()
        {
            InitializeComponent();
            QuizDao quizDao = new QuizDao();
            Quizzes = (List<Quiz>?)quizDao.findAll();
            Results = new List<Result>
            {
                new Result { Name = "Quiz 1", Date = "01.01.2021" },
                new Result { Name = "Quiz 2", Date = "01.01.2021" },
                new Result { Name = "Quiz 3", Date = "01.01.2021" },
                new Result { Name = "Quiz 4", Date = "01.01.2021" },
                new Result { Name = "Quiz 5", Date = "01.01.2021" },
                new Result { Name = "Quiz 6", Date = "01.01.2021" },
                new Result { Name = "Quiz 7", Date = "01.01.2021" },
                new Result { Name = "Quiz 8", Date = "01.01.2021" },
                new Result { Name = "Quiz 9", Date = "01.01.2021" },
                new Result { Name = "Quiz 10", Date = "01.01.2021" },
                new Result { Name = "Quiz 11", Date = "01.01.2021" },
                new Result { Name = "Quiz 12", Date = "01.01.2021" },
                new Result { Name = "Quiz 13", Date = "01.01.2021" },
                new Result { Name = "Quiz 14", Date = "01.01.2021" },
                new Result { Name = "Quiz 15", Date = "01.01.2021" },
                new Result { Name = "Quiz 16", Date = "01.01.2021" },
            };
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
            QuizResults.IsOpen = true;
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // to do obsługa zmiany wyboru w liście wyników quizu
        }
    }
}
