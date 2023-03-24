using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TicketAPI.UnitOfWork
{
    //Generic repository for each entity type.
    //It creates dbSet for some entity that can be used to query and save instance of that entity
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly TicketDbContext dbContext; //one dbContext 
        private readonly DbSet<Entity> dbSetEntity = null; //dbSet holds new dbSet that is created by dbContext
        public GenericRepository(TicketDbContext context)
        {
            dbContext = context;
            dbSetEntity = dbContext.Set<Entity>(); //creates DbSet<CodeClient>..
        }
        public bool Exists(Expression<Func<Entity, bool>> queryWithName)
        {
            IQueryable<Entity> filteredDbSet = dbSetEntity;

            return filteredDbSet.Where(queryWithName).Any();
        }
        public Entity GetSingle(Expression<Func<Entity, bool>> queryWithName)
        {
            return dbSetEntity.Single(queryWithName);
        }
        public void SaveToDB(Entity entity)
        {
            dbSetEntity.Add(entity);
            dbContext.SaveChanges();
        }
    }
}
