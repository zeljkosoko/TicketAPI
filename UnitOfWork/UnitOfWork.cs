using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace TicketAPI.UnitOfWork
{
    //This class instantiates dbContext so implements IDisposable
    //It has generic repositories which use shared dbContext 
    public class UnitOfWork 
    {
        private readonly TicketDbContext dbContext = new TicketDbContext(new DbContextOptionsBuilder<TicketDbContext>()
            .UseSqlServer("Server=192.168.0.28,1433;Database=ZsTicketApp;User Id=zeljko;Password=Progr@mer2023;MultipleActiveResultSets=true;").Options);
        //Data Source=DESKTOP-SU9DAH4; Initial Catalog=ZsTicketApp;Integrated Security=True;User Id=zeljko; Password=Progr@mer2023;
        #region Generic Repositories - private fields
        private GenericRepository<CodeClient> clientRepository;
        private GenericRepository<CodePlace> placeRepository;
        private GenericRepository<CodeAddress> addressRepository;
        private GenericRepository<AggClientPlaceAddress> cpaRepository;
        private GenericRepository<AggUserClientPlaceAddress> ucpaRepository;
        private GenericRepository<CodeUser> userRepository;
        private GenericRepository<ActTicket> ticketRepository;
        #endregion

        #region Each GenericRepositories (public properties) use dbContext
        public GenericRepository<CodeClient> ClientRepository
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new GenericRepository<CodeClient>(dbContext);
                    return clientRepository;
                }
                return clientRepository;
            }
        }
        public GenericRepository<CodePlace> PlaceRepository
        {
            get
            {
                if (placeRepository == null)
                {
                    placeRepository = new GenericRepository<CodePlace>(dbContext);
                    return placeRepository;
                }
                return placeRepository;
            }
        }
        public GenericRepository<CodeAddress> AddressRepository
        {
            get
            {
                if (addressRepository == null)
                {
                    addressRepository = new GenericRepository<CodeAddress>(dbContext);
                    return addressRepository;
                }
                return addressRepository;
            }
        }
        public GenericRepository<AggClientPlaceAddress> CPARepository
        {
            get
            {
                if (cpaRepository == null)
                {
                    cpaRepository = new GenericRepository<AggClientPlaceAddress>(dbContext);
                    return cpaRepository;
                }
                return cpaRepository;
            }
        }
        public GenericRepository<AggUserClientPlaceAddress> UCPARepository
        {
            get
            {
                if (ucpaRepository == null)
                {
                    ucpaRepository = new GenericRepository<AggUserClientPlaceAddress>(dbContext);
                    return ucpaRepository;
                }
                return ucpaRepository;
            }
        }
        public GenericRepository<CodeUser> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<CodeUser>(dbContext);
                    return userRepository;
                }
                return userRepository;
            }
        }
        public GenericRepository<ActTicket> TicketRepository
        {
            get
            {
                if (ticketRepository == null)
                {
                    ticketRepository = new GenericRepository<ActTicket>(dbContext);
                    return ticketRepository;
                }
                return ticketRepository;
            }
        }
        #endregion

        //public void Save()
        //{
        //    dbContext.SaveChanges();
        //}

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            dbContext.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

    }
}
