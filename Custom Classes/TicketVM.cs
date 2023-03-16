using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Custom_Classes
{
    public class TicketVM
    {
        public TicketVM() { }
   
        public TicketVM(string codeClientName, string codePlaceName, string codeAddressName, string codeUserCreaterFirstname, string codeUserCreaterLastname,
            string codeClientIpAddress, int clientTicketId, string clientTicketDocNo, string ticketTitle, string ticketDescription, 
            DateTime actTicketCreatedDate, string initiatorFirstName, string initiatorLastName)
        {
            CodeClientName = codeClientName;
            CodePlaceName = codePlaceName;
            CodeAddressName = codeAddressName;
            CodeUserCreaterFirstname = codeUserCreaterFirstname;
            CodeUserCreaterLastName = codeUserCreaterLastname;
            CodeClientIpAddress = codeClientIpAddress;
            ActTicketClientTicketId = clientTicketId;
            ActTicketClientTicketDocNo = clientTicketDocNo;
            ActTicketTitle = ticketTitle;
            ActTicketDescription = ticketDescription;
            ActTicketCreatedDate = actTicketCreatedDate;
            CodeUserInitiatorFirstName = initiatorFirstName;
            CodeUserInitiatorLastName = initiatorLastName;
        }
        public int Id { get; set; }

        [Required(ErrorMessage ="Client name is required")]
        public string CodeClientName { get; set; }

        [Required(ErrorMessage = "Place name is required")]
        public string CodePlaceName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string CodeAddressName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string CodeUserCreaterFirstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string CodeUserCreaterLastName { get; set; }

        [Required(ErrorMessage = "Client IP address is required")]
        public string CodeClientIpAddress { get; set; }

        [Required(ErrorMessage = "ClientTicket ID is required")]
        public int ActTicketClientTicketId { get; set; }

        [MaxLength(9)]
        [Required(ErrorMessage = "ClientTicket doc number is required")]
        public string ActTicketClientTicketDocNo { get; set; }

        [Required(ErrorMessage = "Ticket title is required")]
        public string ActTicketTitle { get; set; }

        [Required(ErrorMessage = "Ticket description is required")]
        public string ActTicketDescription { get; set; }

        [Required(ErrorMessage = "Ticket created date is required")]
        public DateTime ActTicketCreatedDate { get; set; }

        public string CodeUserInitiatorFirstName { get; set; }
        public string CodeUserInitiatorLastName { get; set; }
    }
}
