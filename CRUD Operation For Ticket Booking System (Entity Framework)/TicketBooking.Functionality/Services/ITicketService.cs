using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Functionality.BusinessObject;

namespace TicketBooking.Functionality.Services
{
    public interface ITicketService
    {
        void CreateTicket(TicketBO ticket);
        (IList<TicketBO> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize,
            string searchText, string sortText);
        TicketBO GetTickets(int id);
        void UpdateTicket(TicketBO ticketBO);
        void DeleteTicket(int id);
    }
}
