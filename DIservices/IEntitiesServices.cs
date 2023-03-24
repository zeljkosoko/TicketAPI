using TicketAPI.Custom_Classes;
using TicketAPI.Models;

namespace TicketAPI.DIservices
{
    public interface IEntitiesServices
    {
        int GetCodeUserId(string firstName, string lastName);
        int GetCodeClientId(string clientName, string ipAddress);
        int GetCodePlaceId(string placeName);
        int GetCodeAddressId(string addressName);
        int GetAggCPAid(int clientId, int placeId, int addressId);
        int GetAggUCPAid(int cpaId, int codeUserId);
        void SaveTicket(ActTicket ticket);
    }
}
