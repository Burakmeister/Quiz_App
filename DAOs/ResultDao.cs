using NHibernate;
using Quiz_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAOs
{
    public class ResultDao : GenericDAO<Result, long>
    {
        public ResultDao() { }

        public List<Result> findQuizResults(Quiz quiz)
        {
            ISession mySession = getSession();

            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.Result q WHERE q.Quiz = :quiz");
            query.SetParameter("quiz", quiz);

            List<Result> list = (List<Result>)query.List<Result>();
            mySession.Close();
            return list;
        }
    }
}
