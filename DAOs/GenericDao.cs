using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAOs
{
    public abstract class GenericDAO<T, ID>
    {

        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;

        public T findByID(ID id)
        {
            try
            {
                return getSession().Load<T>(id);
            }
            catch
            {
                throw;
            }
        }

        public T findByIDLock(ID id)
        {
            try
            {
                return getSession().Load<T>(id, LockMode.Upgrade);
            }
            catch
            {
                throw;
            }
        }

        public IList<T> findAll()
        {
            try
            {
                return getSession().CreateCriteria(typeof(T)).List<T>();
            }
            catch
            {
                throw;
            }
        }

        public T makePersistent(T entity)
        {
            using (ITransaction transaction = getSession().BeginTransaction())
            {
                try
                {
                    getSession().Save(entity);
                    transaction.Commit();
                    return entity;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void delete(T entity)
        {
            using (ITransaction transaction = getSession().BeginTransaction())
            {
                try
                {
                    getSession().Delete(entity);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected ISession getSession()
        {

            // to do: check if session is openned

            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();

            return mySession;
        }

    }
}
