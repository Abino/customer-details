using CustomerUI.Models;
using CustomerUI.Service;
using Microsoft.AspNetCore.Mvc;

namespace CustomerUI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;
        
        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {

            var customers = await _customerService.GetAllAsync();
            if (customers != null && customers.Any())
                return View(customers);
            else
                return View();
        }


        /// <summary>
        /// Create view
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        /// <summary>
        /// Edit the customer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        /// <summary>
        /// Delete the customer method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
