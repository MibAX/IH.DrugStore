using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IH.DrugStore.Web.Data;
using IH.DrugStore.Web.Data.Entities;
using AutoMapper;
using IH.DrugStore.Web.Models.DrugTypes;

namespace IH.DrugStore.Web.Controllers
{
    public class DrugTypesController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DrugTypesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            var drugTypes = await _context
                                    .DrugTypes
                                    .ToListAsync();

            var drugTypeVMs = _mapper.Map<List<DrugType>, List<DrugTypeViewModel>>(drugTypes);

            return View(drugTypeVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context
                                    .DrugTypes
                                    .Include(drugType => drugType.Drugs)
                                    .Where(drugType => drugType.Id == id)
                                    .SingleOrDefaultAsync();

            if (drugType == null)
            {
                return NotFound();
            }

            var drugTypeVMs = _mapper.Map<DrugType, DrugTypeDetailsViewModel>(drugType);

            return View(drugTypeVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrugTypeViewModel drugTypeVM)
        {
            if (ModelState.IsValid)
            {
                var drugType = _mapper.Map<DrugTypeViewModel, DrugType>(drugTypeVM);

                _context.Add(drugType);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(drugTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes.FindAsync(id);
            if (drugType == null)
            {
                return NotFound();
            }
            return View(drugType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DrugTypeViewModel drugTypeVM)
        {
            if (id != drugTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var drugType = _mapper.Map<DrugTypeViewModel, DrugType>(drugTypeVM);

                    _context.Update(drugType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugTypeExists(drugTypeVM.Id))
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
            return View(drugTypeVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drugType = await _context.DrugTypes.FindAsync(id);
            if (drugType != null)
            {
                _context.DrugTypes.Remove(drugType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DrugTypeExists(int id)
        {
            return _context.DrugTypes.Any(e => e.Id == id);
        } 

        #endregion
    }
}
