*** ASP.NET CORE Web API

# The service receives data from the client, based on which a ticket is created in the central database

# Implementation procedure:
     The controller should instantiate the dbContext with itself, use it to get the id of the client, place, address...based on the sent names.
     If the names are new, it should create them in the database and return the new id.
     In order to eliminate direct access to dbContext, Unit of Work with Repository pattern was implemented.
     Based on the repository pattern, a generic repository was created, which for a specific entity (data model)
     implements methods for manipulating the given entity in the dbContext.
     Based on the Unit of Work pattern, a class of the same name was created, which instantiates dbContext and exposes public properties
     for access to all repositories. It passes an instantiated dbContext to each repository.
     Now, so that the controller does not instantiate the unit of work with itself, the service class will do it for it.
     For this approach, the Dependency Injection pattern was implemented, ie a service was injected into the constructor of the controller.
     For the specific needs of the controller, the service has methods using the appropriate repositories and their methods.


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