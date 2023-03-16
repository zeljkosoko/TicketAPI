using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class AggUserClientPlaceAddress
    {
        public AggUserClientPlaceAddress()
        {
        }
        public AggUserClientPlaceAddress(int aggClientPlaceAddressId, int codeUserId)
        {
            AggClientPlaceAddressId = aggClientPlaceAddressId;
            CodeUserId = codeUserId;
        }
        public int Id { get; set; }
        public int AggClientPlaceAddressId { get; set; }
        public int CodeUserId { get; set; }
    }
}
