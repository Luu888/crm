using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crm.Models;
using crm.Services;

namespace crm
{
    public class CurrenciesController : Controller
    {
        private readonly CurrencyContext _context;
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(CurrencyContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        // GET: Currencies
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.SymbolSortParam = sortOrder == "Symbol_desc" ? "Symbol_asc" : "Symbol_desc";
            ViewBag.NameSortParam = sortOrder==  "Name_desc" ? "Name_asc" : "Name_desc";
            ViewBag.RateSortParam = sortOrder == "Rate_desc" ? "Rate_asc" :"Rate_desc";
            ViewBag.CreatedSortParam = sortOrder == "Created_desc" ? "Created_asc" :"Created_desc";
            ViewBag.UpdatedSortParam = sortOrder == "Updated_desc" ? "Updated_asc" : "Updated_desc";
            var currencies = from c in _context.Currency select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                currencies = currencies.Where(s => s.Name.Contains(searchString)
                                       || s.Symbol.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Symbol_desc":
                    currencies = currencies.OrderByDescending(c => c.Symbol);
                    break;
                case "Symbol_asc":
                    currencies = currencies.OrderBy(c => c.Symbol);
                    break;
                case "Name_desc":
                    currencies = currencies.OrderByDescending(c => c.Name);
                    break;
                case "Name_asc":
                    currencies = currencies.OrderBy(c => c.Name);
                    break;
                case "Rate_desc":
                    currencies = currencies.OrderByDescending(c => c.Rate);
                    break;
                case "Rate_asc":
                    currencies = currencies.OrderBy(c => c.Rate);
                    break;
                case "Created_desc":
                    currencies = currencies.OrderByDescending(c => c.Created_at);
                    break;
                case "Created_asc":
                    currencies = currencies.OrderBy(c => c.Created_at);
                    break;
                case "Updated_desc":
                    currencies = currencies.OrderByDescending(c => c.Updated_at);
                    break;
                case "Updated_asc":
                    currencies = currencies.OrderBy(c => c.Updated_at);
                    break;
                default:
                    currencies = currencies.OrderBy(c => c.Id);
                    break;
            }
            return View(currencies.ToList());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Symbol,Name,Rate,Is_Sync,Created_at,Updated_at,Ghosted")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,Name,Rate,Is_Sync,Ghosted")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currency.Updated_at = DateTime.Now;

                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
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
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            var rowToUpdate = _context.Currency.Where(x => x.Id == id).FirstOrDefault();
            rowToUpdate.Ghosted = true;
            rowToUpdate.Is_Sync = false;
            _context.Update(rowToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/test")]
        public IEnumerable<Currency> GetAll()
        {
            return _currencyService.GetAll();
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
