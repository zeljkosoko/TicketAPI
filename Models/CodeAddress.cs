using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class CodeAddress
    {
        public CodeAddress()
        {
        }
        public CodeAddress(string name, DateTime date, bool createdAutomatic)
        {
            Name = name;
            Date = date;
            CreatedAutomatic = createdAutomatic;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool CreatedAutomatic { get; set; }
    }
}
