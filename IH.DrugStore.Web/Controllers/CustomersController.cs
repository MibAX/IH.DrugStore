using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IH.DrugStore.Web.Data;
using IH.DrugStore.Web.Data.Entities;
using AutoMapper;
using IH.DrugStore.Web.Models.Customers;

namespace IH.DrugStore.Web.Controllers
{
    public class CustomersController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var customers = await _context
                                    .Customers
                                    .OrderBy(customer => customer.FirstName)
                                    .ToListAsync();

            var customerVMs = _mapper.Map<List<Customer>, List<CustomerListViewModel>>(customers);

            return View(customerVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TO DO add Orders to customer details
            var customer = await _context
                                    .Customers
                                    .Where(customer => customer.Id == id)
                                    .SingleOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CustomerDetailsViewModel>(customer);

            return View(customerVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCustomerViewModel customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CreateUpdateCustomerViewModel, Customer>(customerVM);

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = await _context
            //                        .Customers
            //                        .FindAsync(id);

            var customer = await _context
                                    .Customers
                                    .Where(customer => customer.Id == id)
                                    .SingleOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CreateUpdateCustomerViewModel>(customer);

            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCustomerViewModel customerVM)
        {
            if (id != customerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<CreateUpdateCustomerViewModel, Customer>(customerVM);

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customerVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(customerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                    .Customers
                                    .Where(customer => customer.Id == id)
                                    .SingleOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CustomerDetailsViewModel>(customer);

            return View(customerVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        } 

        #endregion
    }
}
