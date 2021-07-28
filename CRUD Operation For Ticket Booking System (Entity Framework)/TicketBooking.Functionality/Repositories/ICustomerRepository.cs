using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Functionality.Entities;

namespace TicketBooking.Functionality.Repositories
{
    public interface ICustomerRepository : IRepository<Customer,int>
    {

    }
}
