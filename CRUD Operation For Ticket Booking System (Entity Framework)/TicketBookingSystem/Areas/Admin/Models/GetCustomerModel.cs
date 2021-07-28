using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Functionality.Services;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class GetCustomerModel
    {
        private ICustomerService _customerService;

        public GetCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public GetCustomerModel(ICustomerService courseService)
        {
            _customerService = courseService;
        }

        internal object GetCustomers(DataTablesAjaxRequestModel tableModel)
        {
            var data = _customerService.GetAllCustomers(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name", "Age", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Age.ToString(),
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
