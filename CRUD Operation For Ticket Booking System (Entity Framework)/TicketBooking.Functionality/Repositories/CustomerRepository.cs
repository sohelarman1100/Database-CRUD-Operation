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
    public class CustomerRepository : Repository<Customer,int>, ICustomerRepository
    {
        public CustomerRepository(ITicketBookingContext context)
            : base((DbContext)context)
        {
        }
    }
}
