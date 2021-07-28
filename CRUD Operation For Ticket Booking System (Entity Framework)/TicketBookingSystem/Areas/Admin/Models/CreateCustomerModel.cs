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
    public class CreateCustomerModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }
        
        [Required, Range(1, 200)]
        public int Age { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Address should be less than 200 charcaters")]
        public string Address { get; set; }

        private readonly ICustomerService _customerService;

        public CreateCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }
        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        internal void CreateCustomer()
        {
            var customer = new CustomerBO
            {
                Name = Name,
                Age = Age,
                Address = Address
            };

            _customerService.CreateCustomer(customer);
        }

    }
}
