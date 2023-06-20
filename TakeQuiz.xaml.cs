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
using System.Windows.Threading;

namespace Quiz_App
{
    /// <summary>
    /// Interaction logic for TakeQuiz.xaml
    /// </summary>
    public partial class TakeQuiz : Page
    {
        public static Quiz Quiz { get; set; }

        public List<Question> questions { get; set; }
        public List<Answer> answers { get; set; }
        public int QuestionIndex = 0;
        public int points = 0;
        public int secondsPassed = 0;
        private static Random random;
        public DispatcherTimer DispatcherTimer { get; set; }

        public TakeQuiz(Quiz quiz)
        {
            InitializeComponent();

            DispatcherTimer = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Background, t_Tick, Dispatcher.CurrentDispatcher);
            DispatcherTimer.IsEnabled = true;

            TakeQuiz.Quiz = quiz;
            QuestionDao questionDao = new QuestionDao();
            questions = questionDao.findQuizQuestions(Quiz);

            // shuffle questions
            random = new Random();
            questions = questions.OrderBy(a=>random.Next()).ToList();
            TimeTextBlock.Text = FormatTimer((int)quiz.TimeInMin*60);
           QuizNameTextBlock.Text = quiz.Name;
            setAllTextblock();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (secondsPassed< (Quiz.TimeInMin * 60))
            {
                secondsPassed++;
                TimeTextBlock.Text = FormatTimer((int)((Quiz.TimeInMin * 60) - secondsPassed));
                if (secondsPassed == Quiz.TimeInMin * 60)
                {
                    TotalScoreTextBlock.Text = points + "/" + questions.Count;
                    FinishQuizPopup.IsOpen = true;
                }
            }          
        }

        private string FormatTimer(int seconds)
        {
            string output = "";
            int h = seconds / 3600;
            if (h==0)
            {
                output+= "00:";
            }
            else if (h >0 && h <10)
            {
                output += "0" + h + ":";
            }
            else
            {
                output += h + ":";
            }
            int m = (seconds % 3600) / 60;
            if (m == 0)
            {
                output += "00:";
            }
            else if (m > 0 && m < 10)
            {
                output += "0" + m + ":";
            }
            else
            {
                output += m + ":";
            }
            int s = (seconds % 360) % 60;
            if (s == 0)
            {
                output += "00";
            }
            else if (s > 0 && s < 10)
            {
                output += "0" + s;
            }
            else
            {
                output += s;
            }
            return output;
        }

        private void navigateToHomePage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        //private void showTheFinishQuizPopup(object sender, RoutedEventArgs e)
        //{
        //    FinishQuizPopup.IsOpen = true;
        //}

        private void submitAnswerClick(object sender, RoutedEventArgs e)
        {
            int correctAnswers = 0;
            for (int i = 0; i < AnswersStackPanel.Children.Count; i++)
            {
                if (((RadioButton)AnswersStackPanel.Children[i]).IsChecked == answers.ElementAt(i).IsCorrect)
                {
                    correctAnswers++;
                }

            }
            if (correctAnswers == answers.Count)
            {
                points++;
            }
            if (QuestionIndex< questions.Count-1)
            {
              
                QuestionIndex++;
                AnswersStackPanel.Children.Clear();
                setAllTextblock();
                this.NavigationService.Refresh();
            }
            else
            {
                TotalScoreTextBlock.Text = points + "/" + questions.Count;
                FinishQuizPopup.IsOpen = true;
            }
        }

        private void saveResultAndCloseQuiz(object sender, RoutedEventArgs e)
        {
            Result result = new Result(points, Quiz);
            ResultDao resultDao = new ResultDao();
            resultDao.makePersistent(result);
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomepage();
            }
        }

        private void setAllTextblock()
        {
            TestProgress.Text = (QuestionIndex +1) + "/" + questions.Count.ToString();
            QuestionTextBlock.Text = questions.ElementAt(0).Content;

            AnswerDao answerDao = new AnswerDao();
            answers = answerDao.findQuestionAnswers(questions.ElementAt(QuestionIndex));

            // shuffle answers

            answers = answers.OrderBy(a => random.Next()).ToList();

            for (int i = 0; i < answers.Count; i++)
            {
                RadioButton AnswerRadioButton;
                if (i == 0)
                {
                    AnswerRadioButton = new RadioButton() { Content = answers.ElementAt(i).Content, GroupName = "AnswerOptions", IsChecked = true, Margin = new Thickness(50, 10, 50, 0), HorizontalAlignment = HorizontalAlignment.Left, FontSize = 16, Visibility = Visibility.Visible };
                }
                else
                {
                    AnswerRadioButton = new RadioButton() { Content = answers.ElementAt(i).Content, GroupName = "AnswerOptions", IsChecked = false, Margin = new Thickness(50, 10, 50, 0), HorizontalAlignment = HorizontalAlignment.Left, FontSize = 16, Visibility = Visibility.Visible };
                }
                AnswersStackPanel.Children.Add(AnswerRadioButton);
            }
        }
    }
}
