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
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        public IActionResult Create()
        {
            var model = new CreateCustomerModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateCustomer();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create customer");
                    _logger.LogError(ex, "Create Customer Failed");
                }
            }
            return RedirectToAction("Create");
        }

        public IActionResult GetCustomerDataView()
        {
            var model = new GetCustomerModel();
            return View(model);
        }

        public JsonResult GetCustomerData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetCustomerModel();
            var data = model.GetCustomers(dataTablesModel);
            return Json(data);
        }

        public IActionResult DataViewWithEditAndDeleteButton()
        {
            var model = new GetCustomerModel();
            return View(model);
        }

        public IActionResult EditCustomer(int id)
        {
            var model = new EditCustomerModel();
            model.LoadModelData(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditCustomer(EditCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return RedirectToAction(nameof(DataViewWithEditAndDeleteButton));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteCustomer(int id)
        {
            var model = new DeleteCustomerModel();

            model.DeleteCustomer(id);

            return RedirectToAction(nameof(DataViewWithEditAndDeleteButton));  //delete korar por Ui te DataViewWithEditAndDeleteButton
                                                                               //controller er view er kache jabe
        }

    }
}
