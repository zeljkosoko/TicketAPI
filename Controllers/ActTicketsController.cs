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

namespace TicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActTicketsController : ControllerBase
    {
        private readonly TicketDbContext _context;

        public ActTicketsController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public bool ActTickets(TicketVM ticketVM)
        {
            if (!ModelState.IsValid)
                return false;

            int userId, agg2;
            int? codeUserId;

            //CodeUserId=null kada klijent kreira tiket inace je ZebraconSolutions na inicijativu klijenta
            if (String.IsNullOrEmpty(ticketVM.CodeUserInitiatorFirstName) || String.IsNullOrEmpty(ticketVM.CodeUserInitiatorLastName))
            {
                userId = GetCodeUserId(ticketVM.CodeUserCreaterFirstname, ticketVM.CodeUserCreaterLastName);
                int agg1 = GetAggClientPlaceAddressId(ticketVM);
                agg2 = GetAggUserClientPlaceAddressId(agg1, userId);
                codeUserId = null;
            }
            else
            {
                userId = GetCodeUserId(ticketVM.CodeUserInitiatorFirstName, ticketVM.CodeUserInitiatorLastName);
                int agg1 = GetAggClientPlaceAddressId(ticketVM);
                agg2 = GetAggUserClientPlaceAddressId(agg1, userId);
                codeUserId = GetCodeUserId(ticketVM.CodeUserCreaterFirstname, ticketVM.CodeUserCreaterLastName);
            }
            ActTicket newActTicket = new ActTicket
            {
                AggUserClientPlaceAddressId = agg2,
                CodeUserId = codeUserId,
                ClientTicketId = ticketVM.ActTicketClientTicketId,
                ClientTicketDocumentNo = ticketVM.ActTicketClientTicketDocNo,
                Status = 1,
                Title = ticketVM.ActTicketTitle,
                Description = ticketVM.ActTicketDescription,
                CreatedDate = ticketVM.ActTicketCreatedDate,
            };
            _context.ActTickets.Add(newActTicket);
            int newRow = _context.SaveChanges();

            if (newRow == 0)
                return false;
            return true;
        }

        #region ****************CUSTOM METHODS*************

        #region CodeClient
        private int GetCodeClientId(TicketVM ticketVM)
        {
            int id;
            if (CodeClientExists(ticketVM.CodeClientName))
            {
                CodeClient codeClient = _context.CodeClients.SingleOrDefault(cc => cc.Name == ticketVM.CodeClientName);
                codeClient.IpAddress = ticketVM.CodeClientIpAddress; //in case if client changes internet provider
                codeClient.CreatedAutomatic = true;
                id = codeClient.Id;
            }
            else
            {
                CodeClient newClient = new CodeClient(ticketVM.CodeClientName, DateTime.Now, ticketVM.CodeClientIpAddress, true);
                _context.CodeClients.Add(newClient);
                _context.SaveChanges();
                id = newClient.Id;
            }

            return id;
        }
        private bool CodeClientExists(string name)
        {
            return _context.CodeClients.Any(cc => cc.Name == name);
        }
        #endregion

        #region CodeAddress
        private int GetCodeAddressId(TicketVM ticketVM)
        {
            int id;
            if (CodeAddressExists(ticketVM.CodeAddressName))
            {
                CodeAddress codeAddress = _context.CodeAddresses.SingleOrDefault(ca => ca.Name == ticketVM.CodeAddressName);
                codeAddress.CreatedAutomatic = true;
                id = codeAddress.Id;
            }
            else
            {
                CodeAddress newCodeAddress = new CodeAddress(ticketVM.CodeAddressName, DateTime.Now, true);
                _context.CodeAddresses.Add(newCodeAddress);
                _context.SaveChanges();
                id = newCodeAddress.Id;
            }

            return id;
        }
        private bool CodeAddressExists(string name)
        {
            return _context.CodeAddresses.Any(cd => cd.Name == name);
        }
        #endregion

        #region CodePlace
        private int GetCodePlaceId(TicketVM ticketVM)
        {
            int id;
            if (CodePlaceExists(ticketVM.CodePlaceName))
            {
                CodePlace codePlace = _context.CodePlaces.SingleOrDefault(cp => cp.Name == ticketVM.CodePlaceName);
                codePlace.CreatedAutomatic = true;
                id = codePlace.Id;
            }
            else
            {
                CodePlace newCodePlace = new CodePlace(ticketVM.CodePlaceName, DateTime.Now, true);
                _context.CodePlaces.Add(newCodePlace);
                _context.SaveChanges();
                id = newCodePlace.Id;
            }

            return id;
        }
        private bool CodePlaceExists(string name)
        {
            return _context.CodePlaces.Any(cp => cp.Name == name);
        }
        #endregion

        #region AggClientPlaceAddress
        private int GetAggClientPlaceAddressId(TicketVM ticketVM)
        {
            int id;
            int clientID = GetCodeClientId(ticketVM);
            int placeID = GetCodePlaceId(ticketVM);
            int addressID = GetCodeAddressId(ticketVM);

            if (AggClientPlaceAddressExists(clientID, placeID, addressID))
            {
                id = _context.AggClientPlaceAddresses.SingleOrDefault(cpa =>
                    cpa.CodeClientId == clientID &&
                    cpa.CodePlaceId == placeID &&
                    cpa.CodeAddressId == addressID).Id;
            }
            else
            {
                AggClientPlaceAddress newAggCPA = new AggClientPlaceAddress(clientID, placeID, addressID);
                _context.AggClientPlaceAddresses.Add(newAggCPA);
                _context.SaveChanges();
                id = newAggCPA.Id;
            }

            return id;
        }
        private bool AggClientPlaceAddressExists(int clientId, int placeId, int addressId)
        {
            return _context.AggClientPlaceAddresses.Any(cpa =>
                    cpa.CodeClientId == clientId && cpa.CodePlaceId == placeId && cpa.CodeAddressId == addressId);
        }
        #endregion

        #region CodeUser
        private int GetCodeUserId(string firstName, string lastName)
        {
            //kreator moze biti zebracon ili klijentov zaposleni 
            int userId;
            if (CodeUserExists(firstName, lastName))
            {
                CodeUser codeUser = _context.CodeUsers.SingleOrDefault(cu =>
                    (cu.FirstName == firstName && cu.LastName == lastName));
                codeUser.CreatedAutomatic = true;
                userId = codeUser.Id;
            }
            else
            {
                CodeUser newCodeUser = new CodeUser(firstName, lastName, DateTime.Now, true);
                _context.CodeUsers.Add(newCodeUser);
                _context.SaveChanges();
                userId = newCodeUser.Id;
            }

            return userId;
        }
        private bool CodeUserExists(string firstName, string lastName)
        {
            return _context.CodeUsers.Any(cu => cu.FirstName == firstName && cu.LastName == lastName);
        }
        #endregion

        #region AggUserClientPlaceAddress
        private int GetAggUserClientPlaceAddressId(int clientPlaceAddressId, int codeUserId)
        {
            int id;

            if (AggUserClientPlaceAddressExists(clientPlaceAddressId, codeUserId))
            {
                id = _context.AggUserClientPlaceAddresses.SingleOrDefault(ucpa =>
                    ucpa.AggClientPlaceAddressId == clientPlaceAddressId &&
                    ucpa.CodeUserId == codeUserId).Id;
            }
            else
            {
                AggUserClientPlaceAddress newAggUCPA = new AggUserClientPlaceAddress(clientPlaceAddressId, codeUserId);
                _context.AggUserClientPlaceAddresses.Add(newAggUCPA);
                _context.SaveChanges();
                id = newAggUCPA.Id;
            }

            return id;
        }
        private bool AggUserClientPlaceAddressExists(int clientPlaceAddressId, int codeUserId)
        {
            return _context.AggUserClientPlaceAddresses.Any(ucpa =>
                    ucpa.AggClientPlaceAddressId == clientPlaceAddressId && ucpa.CodeUserId == codeUserId);
        }
        #endregion

        #endregion

        #region Generic methods
        //// GET: api/ActTickets
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ActTicket>>> GetActTickets()
        //{
        //    return await _context.ActTickets.ToListAsync();
        //}

        //// GET: api/ActTickets/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ActTicket>> GetActTicket(int id)
        //{
        //    var actTicket = await _context.ActTickets.FindAsync(id);

        //    if (actTicket == null)
        //    {
        //        return NotFound();
        //    }

        //    return actTicket;
        //}

        //// PUT: api/ActTickets/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutActTicket(int id, ActTicket actTicket)
        //{
        //    if (id != actTicket.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(actTicket).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ActTicketExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/ActTickets
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<ActTicket>> PostActTicket(ActTicket actTicket)
        //{
        //    _context.ActTickets.Add(actTicket);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetActTicket", new { id = actTicket.Id }, actTicket);
        //}

        //// DELETE: api/ActTickets/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ActTicket>> DeleteActTicket(int id)
        //{
        //    var actTicket = await _context.ActTickets.FindAsync(id);
        //    if (actTicket == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ActTickets.Remove(actTicket);
        //    await _context.SaveChangesAsync();

        //    return actTicket;
        //}
        #endregion
    }
}
