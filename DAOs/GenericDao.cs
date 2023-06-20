using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quiz_App.DAOs
{
    public abstract class GenericDAO<T, ID>
    {
        private readonly ISessionFactory mySessionFactory = new Configuration().Configure().BuildSessionFactory();

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
            ISession session = getSession();
            try
            {
                IList < T > list = session.CreateCriteria(typeof(T)).List<T>();
           
                session.Close();
                return list;
            }
            catch
            {
                throw;
            }
        }

        public T makePersistent(T entity)
        {
            ISession session = getSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                    session.Close();
                    return entity;
                }
                catch
                {
                    transaction.Rollback();
                    session.Close();
                    throw;
                }
            }
        }

        public void delete(T entity)
        {
            ISession session = getSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            session.Close();
        }

        protected ISession getSession()
        {
            return mySessionFactory.OpenSession();
        }
    }
}