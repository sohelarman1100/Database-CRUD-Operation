using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Areas.Admin.Models;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Create()
        {
            var model = new CreateTicketModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTicket();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Ticket");
                    _logger.LogError(ex, "Create Ticket Failed");
                }
            }
            return RedirectToAction("Create");
        }

        public IActionResult GetTicketDataView()
        {
            var model = new GetTicketModel();
            return View(model);
        }

        public JsonResult GetTicketData()
        {
            var dataTablesModel=new DataTablesAjaxRequestModel(Request);
            var model = new GetTicketModel();
            var data = model.GetTickets(dataTablesModel);
            return Json(data);
        }

        public IActionResult DataViewWithEditAndDeleteButton()
        {
            var model = new GetTicketModel();
            return View(model);
        }

        public IActionResult EditTicket(int id)
        {
            var model = new EditTicketModel();
            model.LoadTicketData(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditTicket(EditTicketModel model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateTicket();
            }

            return RedirectToAction(nameof(DataViewWithEditAndDeleteButton));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteTicket(int id)
        {
            var model = new DeleteTicketModel();

            model.DeleteTicket(id);

            return RedirectToAction(nameof(DataViewWithEditAndDeleteButton));
        }
    }
}
