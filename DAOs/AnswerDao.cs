using NHibernate;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAOs
{
    public class AnswerDao : GenericDAO<Answer, long>
    {
        public AnswerDao() { }

        public List<Answer> findQuestionAnswers(Question question)
        {
            ISession mySession = getSession();

            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.Answer a WHERE a.Question = :question");
            query.SetParameter("question", question);

            List<Answer> list = (List<Answer>)query.List<Answer>();
            mySession.Close();
            return list;
        }
    }
}
