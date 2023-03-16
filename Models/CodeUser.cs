using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class CodeUser
    {
        public CodeUser()
        {
        }
        public CodeUser(string firstname, string lastname, DateTime date, bool createtAutomatic)
        {
            FirstName = firstname;
            LastName = lastname;
            Date = date;
            CreatedAutomatic = createtAutomatic;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public bool CreatedAutomatic { get; set; }
    }
}
