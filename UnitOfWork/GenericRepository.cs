using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TicketAPI.Models;

namespace TicketAPI.UnitOfWork
{
    //Generic repository for each entity type.
    //It creates dbSet for some entity that can be used to query and save instance of that entity
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        internal TicketDbContext dbContext; //one dbContext 
        internal DbSet<Entity> dbSetEntity = null; //dbSet holds new dbSet that is created by dbContext
        public GenericRepository(TicketDbContext contextFromUnitOfWork)
        {
            dbContext = contextFromUnitOfWork;
            dbSetEntity = dbContext.Set<Entity>(); //creates DbSet<CodeClient>..
        }
        public bool Exists(Expression<Func<Entity, bool>> queryWithName)
        {
            try
            {
                IQueryable<Entity> filteredDbSet = dbSetEntity;
                return filteredDbSet.Where(queryWithName).Any();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Ne postoji takav entitet");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Vise entiteta sa istim imenom");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- ne postoji entitet.");
            }
        }
        public Entity GetSingle(Expression<Func<Entity, bool>> queryWithName)
        {
            try
            {
                return dbSetEntity.Single(queryWithName);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Ne postoji takav entitet");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Vise entiteta sa istim imenom");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- nema nijedan entitet.");
            }
        }
        public void SaveToDB(Entity entity)
        {
            try
            {
                dbSetEntity.Add(entity);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException("Entitet nije sacuvan u bazi, servisu nisu prosledjeni ispravni podaci.");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- Entitet nije sacuvan u bazi.");
            }
        }
    }
}
