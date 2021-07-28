using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Functionality.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class DeleteTicketModel
    {
        private readonly ITicketService _ticketService;

        public DeleteTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public DeleteTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        internal void DeleteTicket(int id)
        {
            _ticketService.DeleteTicket(id);
        }
    }
}
