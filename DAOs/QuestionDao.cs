using NHibernate;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAOs
{
    public class QuestionDao : GenericDAO<Question, long>
    {
        public QuestionDao() { }

        public List<Question> findQuizQuestions(Quiz quiz)
        {
            ISession mySession = getSession();

            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.Question q WHERE q.Quiz = :quiz");
            query.SetParameter("quiz", quiz);

            List<Question> list = (List<Question>)query.List<Question>();
            mySession.Close();
            return list;
        }
    }
}
