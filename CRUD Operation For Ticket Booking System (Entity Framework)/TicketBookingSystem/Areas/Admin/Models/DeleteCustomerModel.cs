using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Functionality.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class DeleteCustomerModel
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public DeleteCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        internal void DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
        }
    }
}
