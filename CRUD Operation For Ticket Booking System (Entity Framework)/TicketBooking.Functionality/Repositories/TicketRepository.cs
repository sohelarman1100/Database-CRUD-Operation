using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Functionality.Contexts;
using TicketBooking.Functionality.Entities;

namespace TicketBooking.Functionality.Repositories
{
    public class TicketRepository : Repository<Ticket, int>, ITicketRepository
    {
        public TicketRepository(ITicketBookingContext context)
            : base((DbContext)context)
        {
        }
    }
}
