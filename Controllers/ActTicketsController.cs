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

        /// <summary>
        /// Creates ticket from a client to the central db
        /// </summary>
        /// <param name="ticketVM"></param>
        /// <returns>A newly created one.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ActTickets
        ///     {
        ///        "CodeClientName": "Pakograf",
        ///        "CodePlaceName" : "Nova Pazova",
        ///        "CodeAddressName" : "Miloša Obilica 33",
        ///        "CodeUserCreaterFirstname" : "Nemanja",
        ///        "CodeUserCreaterLastName" : "Vujinic",
        ///        "CodeClientIpAddress" : "109.108.107.106",
        ///        "ActTicketClientTicketId" : 1216,
        ///        "ActTicketClientTicketDocNo" : "T23-00009",
        ///        "ActTicketTitle" :"Implementing pattern 3",
        ///        "ActTicketDescription" : "DI with Unit of Work 1",
        ///        "ActTicketCreatedDate": "2023-03-30"
        ///     }
        /// </remarks>
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
