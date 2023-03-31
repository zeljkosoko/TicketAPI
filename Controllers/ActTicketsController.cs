using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;
using TicketAPI.Custom_Classes;
using System.Web;
using TicketAPI.DIservices;

namespace TicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActTicketsController : ControllerBase
    {
        private readonly IEntitiesServices services = null;

        public ActTicketsController(IEntitiesServices _services)
        {
            services = _services;
        }

        [HttpPost]
        public string ActTickets(TicketVM ticketVM)
        {
            try
            {
                int creatorOrInitiatorId, clientId, placeId, addressId, cpaId, ucpaId;
                int? codeUserId;

                //Client creates ticket -> CodeUserId=null 
                if (String.IsNullOrEmpty(ticketVM.CodeUserInitiatorFirstName) || String.IsNullOrEmpty(ticketVM.CodeUserInitiatorLastName))
                {
                    creatorOrInitiatorId = services.GetCodeUserId(ticketVM.CodeUserCreaterFirstname, ticketVM.CodeUserCreaterLastName);
                    clientId = services.GetCodeClientId(ticketVM.CodeClientName, ticketVM.CodeClientIpAddress);
                    placeId = services.GetCodePlaceId(ticketVM.CodePlaceName);
                    addressId = services.GetCodeAddressId(ticketVM.CodeAddressName);
                    cpaId = services.GetAggCPAid(clientId, placeId, addressId);
                    ucpaId = services.GetAggUCPAid(cpaId, creatorOrInitiatorId);
                    codeUserId = null;
                }
                else //ZebraconSolutions creates ticket on initiator - CodeUserId = ZebraconId
                {
                    creatorOrInitiatorId = services.GetCodeUserId(ticketVM.CodeUserInitiatorFirstName, ticketVM.CodeUserInitiatorLastName);
                    clientId = services.GetCodeClientId(ticketVM.CodeClientName, ticketVM.CodeClientIpAddress);
                    placeId = services.GetCodePlaceId(ticketVM.CodePlaceName);
                    addressId = services.GetCodeAddressId(ticketVM.CodeAddressName);
                    cpaId = services.GetAggCPAid(clientId, placeId, addressId);
                    ucpaId = services.GetAggUCPAid(cpaId, creatorOrInitiatorId);
                    codeUserId = services.GetCodeUserId(ticketVM.CodeUserCreaterFirstname, ticketVM.CodeUserCreaterLastName);
                }

                ActTicket newActTicket = new ActTicket
                {
                    AggUserClientPlaceAddressId = ucpaId,
                    CodeUserId = codeUserId,
                    ClientTicketId = ticketVM.ActTicketClientTicketId,
                    ClientTicketDocumentNo = ticketVM.ActTicketClientTicketDocNo,
                    Status = 1,
                    Title = ticketVM.ActTicketTitle,
                    Description = ticketVM.ActTicketDescription,
                    CreatedDate = ticketVM.ActTicketCreatedDate,
                };
                services.SaveTicket(newActTicket);

                return "Created.";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
