using NHibernate;
using Quiz_App.Models;

namespace Quiz_App.DAOs
{
    public class UserDao : GenericDAO<User, long>
    {
        public UserDao() { }

        public User findUserByCredentials(string login, string password)
        {
            ISession mySession = getSession();

            IQuery query = mySession.CreateQuery("FROM Quiz_App.Models.User u WHERE u.Login= :login AND u.Password = :password");
            query.SetParameter("login", login);
            query.SetParameter("password", password);
            User user = (User)query.UniqueResult();
            mySession.Close();
            return user;
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
