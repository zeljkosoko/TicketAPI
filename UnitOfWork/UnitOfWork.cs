using System;
using System.Collections.Generic;
using System.Text;
using TicketAPI.Models;

namespace TicketAPI.UnitOfWork
{
    //This class exposes the properties for all repositories those can use shared dbContext
    public class UnitOfWork : IDisposable
    {
        private readonly TicketDbContext dbContext = new TicketDbContext();

        private GenericRepository<CodeClient> clientRepository;
        private GenericRepository<CodePlace> placeRepository;
        private GenericRepository<CodeAddress> addressRepository;
        private GenericRepository<AggClientPlaceAddress> cpaRepository;
        private GenericRepository<AggUserClientPlaceAddress> ucpaRepository;
        private GenericRepository<CodeUser> userRepository;
        private GenericRepository<ActTicket> ticketRepository;

        private bool disposed = false;

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
        //public void Save()
        //{
        //    dbContext.SaveChanges();
        //}
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposed = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
