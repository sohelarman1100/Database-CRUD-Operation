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
    public class CreateTicketModel
    {
        [Required, Range(1, 500000)]
        public int CustomerId { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Title should be less than 200 charcaters")]
        public string Destination { get; set; }

        [Required, Range(200, 1000)]
        public int TicketFee { get; set; }

        private ITicketService _ticketService;

        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }
        public CreateTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public void CreateTicket()
        {
            var ticket = new TicketBO
            {
                CustomerId = CustomerId,
                Destination = Destination,
                TicketFee=TicketFee
            };

            _ticketService.CreateTicket(ticket);
        }
    }
}
