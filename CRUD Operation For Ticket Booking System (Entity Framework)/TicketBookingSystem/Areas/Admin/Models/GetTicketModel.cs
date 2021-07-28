using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Functionality.Services;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class GetTicketModel
    {
        private ITicketService _ticketService;

        public GetTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public GetTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        internal object GetTickets(DataTablesAjaxRequestModel tableModel)
        {
            var data = _ticketService.GetTickets(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "CustomerId", "Destination", "TicketFee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.CustomerId.ToString(),    //ekhane je krome datagulo deya hobe amar view te thik oi kromei data
                                                              //dekhabe.
                             record.Destination,
                             record.TicketFee.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
