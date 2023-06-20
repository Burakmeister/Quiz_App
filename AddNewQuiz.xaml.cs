using Microsoft.VisualBasic.FileIO;
using Quiz_App.DAOs;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Quiz_App

    // dodać popup czy na pewno chcesz wypierdalać bez zapisywania pytań

{
    /// <summary>
    /// Logika interakcji dla klasy AddNewQuiz.xaml
    /// </summary>
    public partial class AddNewQuiz : Page
    {

        public List<Question> Questions { get; set; }
        private int? time = null;

        public AddNewQuiz()
        {
            InitializeComponent();
            Questions = new List<Question>();
            DataContext = this;
        }
        private void navigateToHomepage(object sender, RoutedEventArgs e)
        {
            if (e.Source.Equals(goBackButton)){
                // popup czy aby na pewno, czy wolisz zapisać
            }else if (e.Source.Equals(addWholeWQuiz))
            {
                // zapisuje po prostu
                if(quizNameTextBox.Text.Length > 0) {
                    SaveQuiz(new Quiz(quizNameTextBox.Text, new HashSet<Question>(Questions), Homepage.User, time));
                }
                else
                {
                    // popup czemu nie ma nazwy elo
                    return;
                }
            }

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void SaveQuiz(Quiz quiz)
        {
            QuizDao dao = new QuizDao();
            dao.makePersistent(quiz);
        }

        private void refreshThisView(object sender, RoutedEventArgs e)
        {
            if (e.Source.Equals(deleteTime))
            {
                time = null;
            }else if (e.Source.Equals(addTime))
            {
                if(timeTextController.Text.Length > 0)
                {
                    try
                    {
                        int result = Int32.Parse(timeTextController.Text);
                        time = result;
                        TimePopup.IsOpen = false;
                        return;
                    }
                    catch (FormatException)
                    {
                        // popup nie wprowadzono czasu
                        return;
                    }
                }
            }

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
            NewQuestionPopup.IsEnabled = true;
            correctAnswerComboBox.SelectedIndex = 0;
            answerCTextBox.IsEnabled = false;
            answerDTextBox.IsEnabled = false;
            correctAnswerComboBox.Items.Add("A");
            correctAnswerComboBox.Items.Add("B");
        }


        private void addThisQuestion(object sender, RoutedEventArgs e)
        {
            if(questionTextBox.Text.Length > 0 && answerATextBox.Text.Length > 0 && answerBTextBox.Text.Length > 0)
            {
                ISet<Answer> answers = new HashSet<Answer>();
                // dodawanie odpowiedzi
                answers.Add(new Answer(answerATextBox.Text));
                answers.Add(new Answer(answerBTextBox.Text));
                if(answerCTextBox.Text.Length > 0)
                {
                    answers.Add(new Answer(answerCTextBox.Text));
                }

                if (answerCTextBox.Text.Length > 0)
                {
                    answers.Add(new Answer(answerDTextBox.Text));
                }

                Answer correctAnswer = (correctAnswerComboBox.SelectedItem.Equals("A")) ? answers.ElementAt(0) : (correctAnswerComboBox.SelectedItem.Equals("B") ? answers.ElementAt(1) : (correctAnswerComboBox.SelectedItem.Equals("C") ? answers.ElementAt(2) : answers.ElementAt(3)));
                correctAnswer.IsCorrect = true;
                Question question = new Question(questionTextBox.Text, correctAnswer, answers);
                Questions.Add(question);  //string content, int correctAnswerId, ISet<Answer> answers
                
                NewQuestionPopup.IsOpen = false;
                QuestionsListBox.Items.Refresh();
            }
            else
            {
                // komunikat o nieprawidłopwym wypełnieniu formularza
            }
        }

        private void onChangedAnswerText(object sender, EventArgs e)    // zmiana listy właściwych odpowiedzi oraz zmiana wyświetlania pól w zależności od liczby wprowadzonych odpowiedzi
        {
            answerCTextBox.IsEnabled = (answerATextBox.Text.Length > 0 && answerBTextBox.Text.Length > 0) ? true : false;
            answerDTextBox.IsEnabled = (answerCTextBox.Text.Length > 0) ? true : false;

            if (!answerCTextBox.IsEnabled)
            {
                if (answerATextBox.Text.Length == 0)
                {
                    answerATextBox.Text = answerCTextBox.Text;
                }else if(answerBTextBox.Text.Length == 0)
                {
                    answerBTextBox.Text = answerCTextBox.Text;
                }
                    answerCTextBox.Text = "";
            }else if(!answerDTextBox.IsEnabled)
            {
                if (answerATextBox.Text.Length == 0)
                {
                    answerATextBox.Text = answerDTextBox.Text;
                }
                else if (answerBTextBox.Text.Length == 0)
                {
                    answerBTextBox.Text = answerDTextBox.Text;
                }
                else if (answerCTextBox.Text.Length == 0)
                {
                    answerCTextBox.Text = answerDTextBox.Text;
                }
                answerDTextBox.Text = "";
            }

            if(answerDTextBox.Text.Length != 0 && !correctAnswerComboBox.Items.Contains("D"))
            {
                correctAnswerComboBox.Items.Add("D");
            }
            else
            {
                if (answerDTextBox.Text.Length == 0)
                {
                    correctAnswerComboBox.Items.Remove("D");
                }

                if (answerCTextBox.Text.Length != 0 && !correctAnswerComboBox.Items.Contains("C"))
                {
                    correctAnswerComboBox.Items.Add("C");
                }
                else if (answerCTextBox.Text.Length == 0)
                {
                    correctAnswerComboBox.Items.Remove("C");
                }
            }
        }

        private async void showTheInalidQuestionInputError(object sender, RoutedEventArgs e)
        {
            InvalidQuestionInputPopup.IsOpen = true;
            await Task.Delay(2000);
            InvalidQuestionInputPopup.IsOpen = false;
        }

        private void showCloseQuizCreationPopup(object sender, RoutedEventArgs e)
        {
            CloseQuizCreationPopup.IsOpen = true;
        }

        private void hideCloseQuizCreationPopup(object sender, RoutedEventArgs e)
        {
            CloseQuizCreationPopup.IsOpen = false;
        }
    }
}
