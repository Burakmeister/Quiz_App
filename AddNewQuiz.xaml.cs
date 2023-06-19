using Microsoft.VisualBasic.FileIO;
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
    /// Logika interakcji dla klasy AddNewQuiz.xaml
    /// </summary>
    public partial class AddNewQuiz : Page
    {

        public List<Question> Questions { get; set; }

        //public class Question
        //{
        //    public string Name { get; set; }

        //    public Question()
        //    {
        //        Name = String.Empty;
        //    }
        //}

        public AddNewQuiz()
        {
            InitializeComponent();
            Questions = new List<Question>
            {
                //new Question { Name = "Question 1"},
                //new Question { Name = "Question 2"},
                //new Question { Name = "Question 3"},
                //new Question { Name = "Question 4"},
                //new Question { Name = "Question 5"},
                //new Question { Name = "Question 6"},
                //new Question { Name = "Question 7"},
                //new Question { Name = "Question 8"},
                //new Question { Name = "Question 9"},
                //new Question { Name = "Question 10"},
                //new Question { Name = "Question 11"},
                //new Question { Name = "Question 12"},
                //new Question { Name = "Question 13"},
                //new Question { Name = "Question 14"},
                //new Question { Name = "Question 15"},
                //new Question { Name = "Question 16"},
                //new Question { Name = "Question 17"},
            };
            DataContext = this;
        }
        private void navigateToHomepage(object sender, RoutedEventArgs e)
        {

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void refreshThisView(object sender, RoutedEventArgs e)
        {

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToAddNewQuiz();
            }
        }

        private void showTheTimePopup(object sender, RoutedEventArgs e)
        {
            TimePopup.IsOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //index wybranego Quizu
        {
            QuestionOption.IsOpen = false;
            ListBox listBox = (ListBox)sender;
            int selectedIndex = listBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Console.WriteLine("Selected Index: " + selectedIndex);
            }
            QuestionOption.IsOpen = true;
        }
        private void showNewQuestionPopup(object sender, RoutedEventArgs e)
        {
            NewQuestionPopup.IsOpen = true;
        }

        private void addQuestionToQuiz()
        {

        }


    }
}
