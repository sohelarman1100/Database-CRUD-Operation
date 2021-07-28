using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Functionality.BusinessObject;
using TicketBooking.Functionality.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class EditTicketModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, Range(1, 500000)]
        public int? CustomerId { get; set; }

        [Required, MaxLength(300, ErrorMessage = "Address should be less than 300 charcaters")]
        public string Destination { get; set; }

        [Required, Range(150, 1000)]
        public int? TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }
        public EditTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        internal void LoadTicketData(int id)
        {
            var data = _ticketService.GetTickets(id);
            Id = data?.Id;
            CustomerId = data?.CustomerId;
            Destination = data?.Destination;
            TicketFee = data?.TicketFee;
        }

        internal void UpdateTicket()
        {
            var ticketBO = new TicketBO
            {
                Id = Id.HasValue ? Id.Value : 0,
                CustomerId = CustomerId.HasValue ? CustomerId.Value : 0,
                Destination = Destination,
                TicketFee = TicketFee.HasValue ? TicketFee.Value : 0
            };

            _ticketService.UpdateTicket(ticketBO);
        }
    }
}
