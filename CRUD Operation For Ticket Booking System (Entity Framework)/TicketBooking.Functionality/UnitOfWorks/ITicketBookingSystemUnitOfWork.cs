using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Functionality.Repositories;

namespace TicketBooking.Functionality.UnitOfWorks
{
    public interface ITicketBookingSystemUnitOfWork : IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ITicketRepository Tickets { get; }
    }
}
