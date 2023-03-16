using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    public class ActTicket
    {
        public ActTicket()
        {
        }
        public ActTicket(int aggUserClientPlaceAddressId,
                        int clientTicketId,
                        string clientTicketDocNo,
                        int status,
                        string title,
                        string description,
                        DateTime createdDate,
                        int? userStartedId,
                        DateTime? startedDate,
                        int? userFinishedId,
                        DateTime? finishedDate,
                        int? problemTypeId,
                        string solutionDescription,
                        int? timeSpent,
                        int? billingTime
                        )
        {
            AggUserClientPlaceAddressId = aggUserClientPlaceAddressId;
            ClientTicketId = clientTicketId;
            ClientTicketDocumentNo = clientTicketDocNo;
            Status = status;
            Title = title;
            Description = description;
            CreatedDate = createdDate;
            UserStartedId = userStartedId;
            StartedDate = startedDate;
            UserFinishedId = userFinishedId;
            FinishedDate = finishedDate;
            ProblemTypeId = problemTypeId;
            SolutionDescription = solutionDescription;
            TimeSpent = timeSpent;
            BillingTime = billingTime;
        }
        public int Id { get; set; }
        public int AggUserClientPlaceAddressId { get; set; }
        public int? CodeUserId { get; set; }
        public int ClientTicketId { get; set; }

        [MaxLength(9)]
        public string ClientTicketDocumentNo { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserStartedId { get; set; }
        public DateTime? StartedDate { get; set; }
        public int? UserFinishedId { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int? ProblemTypeId { get; set; }
        public string SolutionDescription { get; set; }
        public int? TimeSpent { get; set; }
        public int? BillingTime { get; set; }
    }
}
