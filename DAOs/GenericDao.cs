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
            return mySessionFactory.OpenSession();
        }
    }
}