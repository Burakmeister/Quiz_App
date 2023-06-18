using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MySqlX.XDevAPI;
using NHibernate;
using NHibernate.Mapping;
using Quiz_App.Models;

namespace Quiz_App.DAOs
{
    public class UserDao : GenericDAO<User,int>
    {
        public UserDao() { }

        public bool findUserByCredentials(string login, string password)
        {
            ISession mySession = getSession();

            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.User u WHERE u.Login= :login AND u.Password = :password");
            query.SetParameter("login", login);
            query.SetParameter("password", password);
            object obj = query.UniqueResult();
            if (obj != null)
            {
                mySession.Close();
                return true;
            }
            mySession.Close();
            return false;
        }

        public bool saveUser(User user)
        {
            ISession mySession = getSession();
         
            using (mySession.BeginTransaction())
            {
                try
                {
                    mySession.Save(user);
                    mySession.Transaction.Commit();
                    return true;
                }
                catch
                {
                    mySession.Transaction.Rollback();
                    throw;
                }
            }
        }

        public bool checkIfLoginIsAvaliable(string login)
        {
            ISession mySession = getSession();
            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.User u WHERE u.Login= :login");
            query.SetParameter("login", login);
            object obj = query.UniqueResult();
            if (obj != null)
            {
                mySession.Close();
                return false;
            }
            mySession.Close();
            return true;
        }


    }
}
