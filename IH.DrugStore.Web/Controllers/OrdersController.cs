using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IH.DrugStore.Web.Data;
using IH.DrugStore.Web.Models.Orders;
using AutoMapper;
using IH.DrugStore.Web.Data.Entities;

namespace IH.DrugStore.Web.Controllers
{
    public class OrdersController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var orderVMs = await GetOrderVMs();

            return View(orderVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            var createUpdateOrderVM = new CreateUpdateOrderViewModel();
            createUpdateOrderVM.CustomerSelectList = new SelectList(_context.Customers, "Id", "FullName");

            createUpdateOrderVM.DrugMultiSelectList = new MultiSelectList(_context.Drugs, "Id", "Name");

            return View(createUpdateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateOrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<CreateUpdateOrderViewModel, Order>(orderVM);

                order.OrderTime = DateTime.Now;

                await UpdateOrderDrugsAsync(order, orderVM.DrugIds);

                order.TotalPrice = GetTotalPrice(order.Drugs);


                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                orderVM.CustomerSelectList = new SelectList(_context.Customers, "Id", "FullName", orderVM.CustomerId);
                orderVM.DrugMultiSelectList = new MultiSelectList(_context.Drugs, "Id", "Name", orderVM.DrugIds);

                return View(orderVM);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context
                                .Orders
                                .Include(order => order.Drugs)
                                .Where(order => order.Id == id)
                                .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var createUpdateOrderVM = _mapper.Map<Order, CreateUpdateOrderViewModel>(order);

            createUpdateOrderVM.CustomerSelectList = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.DrugMultiSelectList = new MultiSelectList(_context.Drugs, "Id", "Name");




            createUpdateOrderVM.DrugIds = order.Drugs.Select(drug => drug.Id).ToList();
            // Those two do the same thing, the above one is newer and better
            //foreach(var drug in order.Drugs)
            //{
            //    createUpdateOrderVM.DrugIds.Add(drug.Id);
            //}




            return View(createUpdateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateOrderViewModel orderVM)
        {
            if (id != orderVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the order including drugs form the DATABASE first 
                    // THEN patch it using AutoMapper
                    var order = await GetOrderIncludingDrugs(id);

                    if (order == null)
                    {
                        return NotFound();
                    }

                    // This is Map the third Overload:
                    // Copies data from first param to the second param
                    // i.e.: Patch from source to destination
                    _mapper.Map<CreateUpdateOrderViewModel, Order>(orderVM, order);

                    await UpdateOrderDrugsAsync(order, orderVM.DrugIds);

                    order.TotalPrice = GetTotalPrice(order.Drugs);

                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderVM.Id))
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
            else
            {

                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", orderVM.CustomerId);
                return View(orderVM);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private async Task<List<OrderViewModel>> GetOrderVMs()
        {
            var orders = await _context
                                        .Orders
                                        .Include(order => order.Customer)
                                        .ToListAsync();

            var orderVMs = _mapper.Map<List<Order>, List<OrderViewModel>>(orders);
            return orderVMs;
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private async Task UpdateOrderDrugsAsync(Order order, List<int> drugIds) // [1, 4]
        {
            // 1 - Get Drugs from the DB using drugIds
            var drugsFromDb = await _context
                            .Drugs // [1, 2, 3, 4]
                            .Where(drug => drugIds.Contains(drug.Id)) // [1, 4] Cross [1, 2, 3, 4]
                            .ToListAsync();

            // 2 - Clear the Drugs from the Order
            order.Drugs.Clear();

            // 3 - Add new drugs to the order
            order.Drugs.AddRange(drugsFromDb);
        }

        private async Task<Order?> GetOrderIncludingDrugs(int id)
        {
            return await _context
                            .Orders
                            .Include(order => order.Drugs)
                            .Where(order => order.Id == id)
                            .SingleOrDefaultAsync();
        }

        private double GetTotalPrice(List<Drug> drugs)
        {
            return drugs.Sum(drug => drug.Price);
        }

        #endregion
    }
}
