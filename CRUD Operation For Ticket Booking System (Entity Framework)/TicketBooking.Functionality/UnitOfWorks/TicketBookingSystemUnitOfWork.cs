using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Functionality.Contexts;
using TicketBooking.Functionality.Repositories;

namespace TicketBooking.Functionality.UnitOfWorks
{
    public class TicketBookingSystemUnitOfWork : UnitOfWork, ITicketBookingSystemUnitOfWork
    {
        public ICustomerRepository Customers { get; private set; }
        public ITicketRepository Tickets { get; private set; }

        public TicketBookingSystemUnitOfWork(ITicketBookingContext context,
            ICustomerRepository customers, ITicketRepository tickets
            ) : base((DbContext)context)
        {
            Customers = customers;
            Tickets = tickets;
        }
    }
}
