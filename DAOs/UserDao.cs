using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlX.XDevAPI;
using NHibernate;
using Quiz_App.Models;

namespace Quiz_App.DAOs
{
    public class UserDao : GenericDAO<User,int>
    {
        public UserDao() { }

        public bool findUserByCredentials(string login, string password)
        {
            ISession mySession = getSession();

            IQuery query = getSession().CreateQuery("FROM Quiz_App.Models.User u WHERE u.Login= :login AND u.Password = :password");
            query.SetParameter("login", login);
            query.SetParameter("password", password);
            object obj = query.UniqueResult();
            if (obj != null)
            {
                return true;
            }
            return false;

            mySession.Close();
        }


    }
}
