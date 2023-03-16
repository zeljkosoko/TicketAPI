using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class CodeProfile
    {
        public CodeProfile()
        {
        }
        public CodeProfile(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
