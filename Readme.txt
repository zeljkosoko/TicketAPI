*** ASP.NET CORE Web API

# Company API service receives json from the client, based on which a ticket is created in the central database.

# Expected json data:
        CodeClientName 
        CodePlaceName 
        CodeAddressName 
        CodeUserCreaterFirstname
        CodeUserCreaterLastName
        CodeClientIpAddress
        ActTicketClientTicketId
        ActTicketClientTicketDocNo
        ActTicketTitle
        ActTicketDescription
        ActTicketCreatedDate
        (CodeUserInitiatorFirstName)
        (CodeUserInitiatorLastName)

# Business Logic:
    The controller ActTickets, has method ActTickets that accepts TicketVM data and save data to ActTicket in db.

Architecture:

* IGenericRepository<T> has methods Exists, GetOne and SaveToDB.
    -Exist and GetOne has linq -> linq expression query which delegate has T and bool.
    -Save has T

* GenericRepository<T> implements IGenericRepository.
    -fields: TicketDbContext, dbSet
    -constructor will accept dbContext,from UnitOfWork. Initialize fields: TicketDbContext with injected one, dbset sets with T.
    -methods has linq query with name param.
        -Exists create IQueryable dbset, apply Where with query parameter.
        -GetSingle create IQueryable dbset, apply Single with query param.
        -Save adds T to dbSet,then saveChanges to DB.

* TicketDbContext

* UnitOfWork implements IDesignTimeContextFactory
    - field: TicketDbContext, clientRepository,placeRepository,addressRepository...
    - TicketDbContext field initialize within interface method CreateDbContext
    - clientRepository field initialize with GenericRepository<Client> with shared TicketDbContext field.