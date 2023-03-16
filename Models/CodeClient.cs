using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class CodeClient
    {
        public CodeClient()
        {
        }
        public CodeClient(string name, DateTime date, string ipAddress, bool createdAutomatic)
        {
            Name = name;
            Date = date;
            IpAddress = ipAddress;
            CreatedAutomatic = createdAutomatic;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public bool CreatedAutomatic { get; set; }
    }
}
