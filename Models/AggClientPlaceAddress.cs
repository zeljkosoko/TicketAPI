using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class AggClientPlaceAddress
    {
        public AggClientPlaceAddress()
        {
        }
        public AggClientPlaceAddress(int codeClientId, int codePlaceId, int codeAddressId)
        {
            CodeClientId = codeClientId;
            CodePlaceId = codePlaceId;
            CodeAddressId = codeAddressId;
        }
        public int Id { get; set; }
        public int CodeClientId { get; set; }
        public int CodePlaceId { get; set; }
        public int CodeAddressId { get; set; }
    }
}
