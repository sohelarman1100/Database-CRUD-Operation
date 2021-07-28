using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Functionality.BusinessObject;
using TicketBooking.Functionality.Entities;
using TicketBooking.Functionality.Exceptions;
using TicketBooking.Functionality.UnitOfWorks;

namespace TicketBooking.Functionality.Services
{
    public class TicketService : ITicketService
    {
        private ITicketBookingSystemUnitOfWork _ticketBookingSystemUnitOfWork;

        public TicketService(ITicketBookingSystemUnitOfWork ticketBookingSystemUnitOfWork)
        {
            _ticketBookingSystemUnitOfWork = ticketBookingSystemUnitOfWork;
        }
        public void CreateTicket(TicketBO ticket)
        {
            if (ticket == null)
                throw new InvalidParameterException("Customer info was not provided");

            _ticketBookingSystemUnitOfWork.Tickets.Add(
                new Entities.Ticket
                {
                    CustomerId = ticket.CustomerId,
                    Destination = ticket.Destination,
                    TicketFee = ticket.TicketFee
                }
             );
            _ticketBookingSystemUnitOfWork.Save();
        }

        public (IList<TicketBO> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize, 
            string searchText, string sortText)
        {
            int srctxt = string.IsNullOrWhiteSpace(searchText) ? 0 : Convert.ToInt32(searchText[0]); 
            var TypeConvertedSearchText = (string.IsNullOrWhiteSpace(searchText)==false && srctxt >= 49 &&
                                           srctxt <= 57) ? int.Parse(searchText) : 0 ;

            var ticketData = _ticketBookingSystemUnitOfWork.Tickets.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.CustomerId== TypeConvertedSearchText,
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketData.data
                              select new TicketBO
                              {
                                  Id=ticket.Id,
                                  CustomerId = ticket.CustomerId,
                                  Destination = ticket.Destination,
                                  TicketFee = ticket.TicketFee
                              }).ToList();

            return (resultData, ticketData.total, ticketData.totalDisplay);
        }

        public TicketBO GetTickets(int id)
        {
            var ticketEntity = _ticketBookingSystemUnitOfWork.Tickets.GetById(id);

            if (ticketEntity == null) return null;

            return new TicketBO
            {
                Id = ticketEntity.Id,
                CustomerId = ticketEntity.CustomerId,
                Destination = ticketEntity.Destination,
                TicketFee = ticketEntity.TicketFee
            };
        }

        public void UpdateTicket(TicketBO ticketBO)
        {
            if (ticketBO == null)
                throw new InvalidOperationException("ticket is missing");

            var entityData = _ticketBookingSystemUnitOfWork.Tickets.GetById(ticketBO.Id);

            if (entityData != null)
            {
                entityData.CustomerId = ticketBO.CustomerId;
                entityData.Destination = ticketBO.Destination;
                entityData.TicketFee = ticketBO.TicketFee;
                _ticketBookingSystemUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find ticket");
        }

        public void DeleteTicket(int id)
        {
            _ticketBookingSystemUnitOfWork.Tickets.Remove(id);
            _ticketBookingSystemUnitOfWork.Save();
        }

    }
}
