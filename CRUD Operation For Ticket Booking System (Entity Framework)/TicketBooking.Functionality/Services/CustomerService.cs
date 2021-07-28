using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Functionality.BusinessObject;
using TicketBooking.Functionality.Exceptions;
using TicketBooking.Functionality.UnitOfWorks;

namespace TicketBooking.Functionality.Services
{
    public class CustomerService : ICustomerService
    {
        private ITicketBookingSystemUnitOfWork _ticketBookingSystemUnitOfWork;

        public CustomerService(ITicketBookingSystemUnitOfWork ticketBookingSystemUnitOfWork)
        {
            _ticketBookingSystemUnitOfWork = ticketBookingSystemUnitOfWork;
        }
        public void CreateCustomer(CustomerBO customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer info was not provided");

            _ticketBookingSystemUnitOfWork.Customers.Add(
                new Entities.Customer
                {
                    Name = customer.Name,
                    Age = customer.Age,
                    Address = customer.Address
                }
            );

            _ticketBookingSystemUnitOfWork.Save();
        }

        public (IList<CustomerBO> records, int total, int totalDisplay) GetAllCustomers(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var customerData = _ticketBookingSystemUnitOfWork.Customers.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Address.Contains(searchText),   //ekhane contains er kaj holo
                                                                    // jodi ekta duita letter diye o amra search kori tahole je
                                                                    // Address gulor moddhe oi letter gulo ache oi address gulo jate
                                                                    // show kore.
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customerData.data
                              select new CustomerBO
                              {
                                  Id = customer.Id,
                                  Name = customer.Name,
                                  Age = customer.Age,
                                  Address = customer.Address
                              }).ToList();

            return (resultData, customerData.total, customerData.totalDisplay);
        }

        public CustomerBO GetCustomers(int id)
        {
            var customerEntity = _ticketBookingSystemUnitOfWork.Customers.GetById(id);
            if (customerEntity == null) return null;

            return new CustomerBO
            {
                Id = customerEntity.Id,
                Name = customerEntity.Name,
                Age = customerEntity.Age,
                Address = customerEntity.Address
            };
        }

        public void UpdateCustomer(CustomerBO customerinfo)
        {
            if (customerinfo == null)
                throw new InvalidOperationException("Customer is missing");

            var customerEntity = _ticketBookingSystemUnitOfWork.Customers.GetById(customerinfo.Id);

            if (customerEntity != null)
            {
                customerEntity.Name = customerinfo.Name;
                customerEntity.Age = customerinfo.Age;
                customerEntity.Address = customerinfo.Address;

                _ticketBookingSystemUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find course");
        }

        public void DeleteCustomer(int id)
        {
            _ticketBookingSystemUnitOfWork.Customers.Remove(id);
            _ticketBookingSystemUnitOfWork.Save();
        }
    }
}
