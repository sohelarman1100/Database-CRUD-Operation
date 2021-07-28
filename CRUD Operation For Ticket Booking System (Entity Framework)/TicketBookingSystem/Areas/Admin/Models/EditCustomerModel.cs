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
    public class EditCustomerModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }

        [Required, Range(1, 150)]
        public int? Age { get; set; }

        [Required, MaxLength(300, ErrorMessage = "Address should be less than 300 charcaters")]
        public string Address { get; set; }

        private readonly ICustomerService _customerService;

        public EditCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public EditCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void LoadModelData(int id)
        {
            var data = _customerService.GetCustomers(id);
            Id = data?.Id;
            Name = data?.Name;
            Age = data?.Age;
            Address = data?.Address;
        }

        internal void Update()
        {
            var customerinfo = new CustomerBO
            {
                Id=Id.HasValue? Id.Value : 0,   //eikhane ei class er Id er value direct na bosiye Id.Value diye bosanor karon holo
                                                //amra ei class e Id ke declare korar somoy nullable akare declare korechi ar eivabe
                                                //declare korle valu sorasori property er vitor paowa jayna property_name.Value diye
                                                //property tir value retrive korte hoy, ar HasValue diye dekha hocche er moddhe value
                                                //ache kina.
                
                Name=Name,
                Age=Age.HasValue? Age.Value : 0,
                Address=Address,
            };
            _customerService.UpdateCustomer(customerinfo);
        }
    }
}
