using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TicketAPI.UnitOfWork
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        bool Exists(Expression<Func<TEntity, bool>> queryWithName);
        TEntity GetSingle(Expression<Func<TEntity, bool>> queryWithName);
        void SaveToDB(TEntity entity);
    }
}
