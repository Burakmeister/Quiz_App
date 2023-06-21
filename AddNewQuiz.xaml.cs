using Microsoft.VisualBasic.FileIO;
using Quiz_App.DAOs;
using Quiz_App.Models;
using System;
using System.Collections.Concurrent;
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
        private Quiz? quiz = null;
        private Question? currectQuestion = null;

        public AddNewQuiz()
        {
            InitializeComponent();
            Questions = new List<Question>();
            DataContext = this;
        }

        public AddNewQuiz(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;
            Questions = new List<Question>();
            foreach (Question q in quiz.Questions)
                Questions.Add(q);
            time = quiz.TimeInMin;
            quizNameTextBox.Text = quiz.Name;
            DataContext = this;
        }

        private void navigateToHomepage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void deleteQuestionAction(object sender, RoutedEventArgs e)
        {
            DeletingCorfirmation.IsOpen = true;
        }
        private void deleteQuestionResignation(object sender, RoutedEventArgs e)
        {
            DeletingCorfirmation.IsOpen = false;
        }

        private void deleteQuestion(object sender, RoutedEventArgs e)
        {
            Question q = (Question)QuestionsListBox.SelectedItem;
            if (quiz != null && quiz.Questions.Contains(q))
            {
                QuestionDao dao = new QuestionDao();
                dao.delete(q);
            }
            Questions.Remove(q);
            QuestionsListBox.Items.Refresh();
            QuestionOption.IsOpen = false;
            DeletingCorfirmation.IsOpen = false;
        }

        private void refreshThisView(object sender, RoutedEventArgs e)
        {
            if (e.Source.Equals(deleteTime))
            {
                time = null;
                TimePopup.IsOpen = false;
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
                        showTimePopup();
                        return;
                    }
                }
                else
                {
                    showTimePopup();
                }
            }else if (e.Source.Equals(cancelQuestion))
            {
                showNewQuestionPopupReset();
            }

            QuestionsListBox.Items.Refresh();
        }

        private void editQuestion(object sender, RoutedEventArgs e)
        {
            correctAnswerComboBox.Items.Clear();
            Question question = QuestionsListBox.SelectedItem as Question;
            currectQuestion = question;
            NewQuestionPopup.IsOpen = true;
            questionTextBox.Text = question.Content;
            int i = 0;
            foreach(Answer ans in question.Answers)
            {
                if(ans.IsCorrect)
                {
                    break;
                }
                i++;
            }
            correctAnswerComboBox.Items.Add("A");
            correctAnswerComboBox.Items.Add("B");
            answerATextBox.Text = question.Answers.ElementAt(0).Content;
            answerBTextBox.Text = question.Answers.ElementAt(1).Content;
            if (question.Answers.Count > 2)
            {
                answerCTextBox.Text = question.Answers.ElementAt(2).Content;
            }
            else
            {
                answerDTextBox.IsEnabled = false;
            }
            if (question.Answers.Count > 3)
            {
                answerDTextBox.Text = question.Answers.ElementAt(3).Content;
            }
            correctAnswerComboBox.SelectedItem = correctAnswerComboBox.Items[i];
        }

        private void showTheTimePopup(object sender, RoutedEventArgs e)
        {
            TimePopup.IsOpen = true;
            timeTextController.Text = time.ToString();
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
            correctAnswerComboBox.Items.Clear();
            answerATextBox.Text = "";
            answerBTextBox.Text = "";
            answerCTextBox.Text = "";
            answerDTextBox.Text = "";
            questionTextBox.Text = "";
            this.currectQuestion = null;
            NewQuestionPopup.IsOpen = true;
            correctAnswerComboBox.SelectedIndex = 0;
            answerCTextBox.IsEnabled = false;
            answerDTextBox.IsEnabled = false;
            correctAnswerComboBox.Items.Add("A");
            correctAnswerComboBox.Items.Add("B");
        }


        private void addThisQuestion(object sender, RoutedEventArgs e)
        {
            if (questionTextBox.Text.Length > 0 && answerATextBox.Text.Length > 0 && answerBTextBox.Text.Length > 0 && correctAnswerComboBox.SelectedItem!=null)
            {
                if (currectQuestion != null)
                {
                    // usuń resulty z tego wyścigu po edycji
                    ISet<Answer> answers = new HashSet<Answer>();
                    // dodawanie odpowiedzi
                    answers.Add(new Answer(answerATextBox.Text));
                    answers.Add(new Answer(answerBTextBox.Text));
                    if (answerCTextBox.Text.Length > 0)
                    {
                        answers.Add(new Answer(answerCTextBox.Text));
                    }

                    if (answerDTextBox.Text.Length > 0)
                    {
                        answers.Add(new Answer(answerDTextBox.Text));
                    }

                    Answer correctAnswer = (correctAnswerComboBox.SelectedItem.Equals("A")) ? answers.ElementAt(0) : (correctAnswerComboBox.SelectedItem.Equals("B") ? answers.ElementAt(1) : (correctAnswerComboBox.SelectedItem.Equals("C") ? answers.ElementAt(2) : answers.ElementAt(3)));
                    correctAnswer.IsCorrect = true;

                    foreach (var ans in answers)
                    {
                        if (!currectQuestion.Answers.Contains(ans))
                        {
                            currectQuestion.Answers.Add(ans);
                        }
                    }
                    AnswerDao dao = new AnswerDao();
                    foreach (Answer ans in currectQuestion.Answers)
                    {
                        if (!answers.Contains(ans))
                        {
                            dao.delete(ans);
                            currectQuestion.Answers.Remove(ans);
                        }
                        ans.IsCorrect = false;
                        if (ans.Equals(correctAnswer))
                        {
                            ans.IsCorrect = true;
                        }
                    }

                    currectQuestion.Content = questionTextBox.Text;

                    showNewQuestionPopupReset();
                }
                else
                {

                    ISet<Answer> answers = new HashSet<Answer>();
                    // dodawanie odpowiedzi
                    answers.Add(new Answer(answerATextBox.Text));
                    answers.Add(new Answer(answerBTextBox.Text));
                    if (answerCTextBox.Text.Length > 0)
                    {
                        answers.Add(new Answer(answerCTextBox.Text));
                    }

                    if (answerDTextBox.Text.Length > 0)
                    {
                        answers.Add(new Answer(answerDTextBox.Text));
                    }

                    Answer correctAnswer = (correctAnswerComboBox.SelectedItem.Equals("A")) ? answers.ElementAt(0) : (correctAnswerComboBox.SelectedItem.Equals("B") ? answers.ElementAt(1) : (correctAnswerComboBox.SelectedItem.Equals("C") ? answers.ElementAt(2) : answers.ElementAt(3)));
                    correctAnswer.IsCorrect = true;
                    Question question = new Question(questionTextBox.Text, answers);
                    Questions.Add(question); 
                    showNewQuestionPopupReset();

                }
            }
            else
            {
                showErrorIncorrectlyCompletedForm();
            }
        }

        private void showNewQuestionPopupReset()
        {
            NewQuestionPopup.IsOpen = false;
            QuestionsListBox.Items.Refresh();
            NewQuestionPopup.IsOpen = false;
            answerATextBox.Text = null;
            answerBTextBox.Text = null;
            answerCTextBox.Text = null;
            answerDTextBox.Text = null;
            correctAnswerComboBox.Items.Clear();
            questionTextBox.Text = null;
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

        private async void showTimePopup()
        {
            ErrorTime.IsOpen = true;
            await Task.Delay(2000);
            ErrorTime.IsOpen = false;
        }

        private async void showErrorIncorrectlyCompletedForm()
        {
            InvalidQuestionInputPopup.IsOpen = true;
            await Task.Delay(2000);
            InvalidQuestionInputPopup.IsOpen = false;
        }

        private async void showTheInalidQuestionInputError(object sender, RoutedEventArgs e)
        {
            InvalidQuestionInputPopup.IsOpen = true;
            await Task.Delay(2000);
            InvalidQuestionInputPopup.IsOpen = false;
        }

        private void showCloseQuizCreationPopup(object sender, RoutedEventArgs e)
        {
            if (quizNameTextBox.Text.Length > 0)
            {
                QuizDao dao = new QuizDao();
                if (this.quiz == null)
                {
                    dao.makePersistent(new Quiz(quizNameTextBox.Text, new HashSet<Question>(Questions), Homepage.User, time));
                }
                else
                {
                    foreach (Question q in Questions)
                        if (!this.quiz.Questions.Contains(q))
                        {
                            this.quiz.Questions.Add(q);
                        }
                    quiz.TimeInMin = time;
                    quiz.Name = quizNameTextBox.Text;
                    dao.makeUpdate(this.quiz);
                }
            }
            else
            {
                // popup czemu nie ma nazwy elo
                return;
            }
            CloseQuizCreationPopup.IsOpen = true;
        }

        private void hideCloseQuizCreationPopup(object sender, RoutedEventArgs e)
        {
            CloseQuizCreationPopup.IsOpen = false;
        }
    }
}
