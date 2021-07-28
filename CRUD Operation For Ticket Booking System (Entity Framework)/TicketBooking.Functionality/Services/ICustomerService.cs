using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Functionality.BusinessObject;

namespace TicketBooking.Functionality.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(CustomerBO customer);
        (IList<CustomerBO> records, int total, int totalDisplay) GetAllCustomers(int pageIndex, int pageSize,
           string searchText, string sortText);
        CustomerBO GetCustomers(int id);
        void UpdateCustomer(CustomerBO customerinfo);
        void DeleteCustomer(int id);
    }
}
