using System;
using TicketAPI.Custom_Classes;
using TicketAPI.Models;
using TicketAPI.UnitOfWork;

namespace TicketAPI.DIservices
{
    public class EntitiesServices: IEntitiesServices
    {
        private readonly UnitOfWork.UnitOfWork repos = new UnitOfWork.UnitOfWork();

        public int GetCodeUserId(string firstName, string lastName)
        {
            int userId;
            if (repos.UserRepository.Exists(u => u.FirstName == firstName && u.LastName == lastName))
            {
                CodeUser oldUser = repos.UserRepository.GetSingle(u => u.FirstName == firstName && u.LastName == lastName);
                oldUser.CreatedAutomatic = true;
                userId = oldUser.Id;
            }
            else
            {
                CodeUser newCodeUser = new CodeUser(firstName, lastName, DateTime.Now, true);
                repos.UserRepository.SaveToDB(newCodeUser);
                userId = newCodeUser.Id;
            }
            return userId;
        }
        public int GetCodeClientId(string clientName, string ipAddress)
        {
            int clientId;
            if (repos.ClientRepository.Exists(c => c.Name == clientName))
            {
                CodeClient oldClient = repos.ClientRepository.GetSingle(c => c.Name == clientName);
                oldClient.IpAddress = ipAddress; //in case if client changes internet provider
                oldClient.CreatedAutomatic = true;
                clientId = oldClient.Id;
            }
            else
            {
                CodeClient newClient = new CodeClient(clientName, DateTime.Now, ipAddress, true);
                repos.ClientRepository.SaveToDB(newClient);
                clientId = newClient.Id;
            }

            return clientId;
        }
        public int GetCodePlaceId(string placeName)
        {
            int placeId;
            if (repos.PlaceRepository.Exists(p => p.Name == placeName))
            {
                CodePlace oldPlace = repos.PlaceRepository.GetSingle(p => p.Name == placeName); ;
                oldPlace.CreatedAutomatic = true;
                placeId = oldPlace.Id;
            }
            else
            {
                CodePlace newCodePlace = new CodePlace(placeName, DateTime.Now, true);
                repos.PlaceRepository.SaveToDB(newCodePlace);
                placeId = newCodePlace.Id;
            }

            return placeId;
        }
        public int GetCodeAddressId(string addressName)
        {
            int addressId;
            if (repos.AddressRepository.Exists(a => a.Name == addressName))
            {
                CodeAddress oldAddress = repos.AddressRepository.GetSingle(a => a.Name == addressName);
                oldAddress.CreatedAutomatic = true;
                addressId = oldAddress.Id;
            }
            else
            {
                CodeAddress newCodeAddress = new CodeAddress(addressName, DateTime.Now, true);
                repos.AddressRepository.SaveToDB(newCodeAddress);
                addressId = newCodeAddress.Id;
            }

            return addressId;
        }
        public int GetAggCPAid(int clientId, int placeId, int addressId)
        {
            int cpaId;

            if (repos.CPARepository.Exists(cpa => cpa.CodeClientId == clientId && cpa.CodePlaceId == placeId && cpa.CodeAddressId == addressId))
            {
                cpaId = repos.CPARepository.GetSingle(cpa => cpa.CodeClientId == clientId &&
                    cpa.CodePlaceId == placeId && cpa.CodeAddressId == addressId).Id;
            }
            else
            {
                AggClientPlaceAddress newAggCPA = new AggClientPlaceAddress(clientId, placeId, addressId);
                repos.CPARepository.SaveToDB(newAggCPA);
                cpaId = newAggCPA.Id;
            }

            return cpaId;
        }
        public int GetAggUCPAid(int cpaId, int codeUserId)
        {
            int ucpaId;

            if(repos.UCPARepository.Exists(ucpa => ucpa.AggClientPlaceAddressId == cpaId && ucpa.CodeUserId == codeUserId))
            {
                ucpaId = repos.UCPARepository.GetSingle(ucpa => ucpa.AggClientPlaceAddressId == cpaId &&
                    ucpa.CodeUserId == codeUserId).Id;
            }
            else
            {
                AggUserClientPlaceAddress newAggUCPA = new AggUserClientPlaceAddress(cpaId, codeUserId);
                repos.UCPARepository.SaveToDB(newAggUCPA);
                ucpaId = newAggUCPA.Id;
            }

            return ucpaId;
        }
        public void SaveTicket(ActTicket ticket)
        {
            repos.TicketRepository.SaveToDB(ticket);
        }
    }
}
