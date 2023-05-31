using Org.BouncyCastle.Asn1.BC;
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
    /// Logika interakcji dla klasy Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public List<Quiz> Quizzes { get; set; }

        public class Quiz
        {
            public string Name { get; set; }
            public string Date { get; set; }

            public Quiz()
            {
                Name = String.Empty;
                Date = String.Empty;
            }
        }

        public Homepage() //nie jestem pewien ale wydaję mi się że trzeba rebuildować ten widok przy każdej zmianie czyli by użytkownik zobaczył zaktualizowany widok należy ... new Homepage();
        {
            InitializeComponent();
            Quizzes = new List<Quiz>
        {
            new Quiz { Name = "Quiz 1", Date = "01.01.2021" },
            new Quiz { Name = "Test 2", Date = "02.02.2021" },
            new Quiz { Name = "Test 3", Date = "03.03.2021" },
            new Quiz { Name = "Quiz 1", Date = "04.01.2021" },
            new Quiz { Name = "Test 2", Date = "05.02.2021" },
            new Quiz { Name = "Test 3", Date = "06.03.2021" },
            new Quiz { Name = "Quiz 1", Date = "07.01.2021" },
            new Quiz { Name = "Test 2", Date = "08.02.2021" },
            new Quiz { Name = "Test 3", Date = "09.03.2021" },
            new Quiz { Name = "Quiz 1", Date = "10.01.2021" },
            new Quiz { Name = "Test 2", Date = "11.02.2021" },
            new Quiz { Name = "Test 3", Date = "12.03.2021" },
            new Quiz { Name = "Quiz 1", Date = "13.01.2021" },
            new Quiz { Name = "Test 2", Date = "14.02.2021" },
            new Quiz { Name = "Test 3", Date = "15.03.2021" },
        };
            DataContext = this;
        }

        private void navigateToLoginClick(object sender, RoutedEventArgs e)
        {
            //wyloguj użytkownika

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToLoginPage();
            }
        }

    }
}
