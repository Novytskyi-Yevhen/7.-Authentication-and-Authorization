using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Filters;
using TaskAuthenticationAuthorization.Models;

namespace TaskAuthenticationAuthorization.Controllers
{
    public class SuperMarketsController : Controller
    {
        private readonly ShoppingContext _context;

        public SuperMarketsController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: SuperMarkets
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuperMarkets.ToListAsync());
        }

        // GET: SuperMarkets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superMarket = await _context.SuperMarkets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (superMarket == null)
            {
                return NotFound();
            }

            return View(superMarket);
        }

        // GET: SuperMarkets/Create
        [RedirectToNoAcessAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperMarkets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Create([Bind("ID,Name,Address")] SuperMarket superMarket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superMarket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(superMarket);
        }

        // GET: SuperMarkets/Edit/5
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superMarket = await _context.SuperMarkets.FindAsync(id);
            if (superMarket == null)
            {
                return NotFound();
            }
            return View(superMarket);
        }

        // POST: SuperMarkets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address")] SuperMarket superMarket)
        {
            if (id != superMarket.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superMarket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperMarketExists(superMarket.ID))
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
            return View(superMarket);
        }

        // GET: SuperMarkets/Delete/5
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superMarket = await _context.SuperMarkets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (superMarket == null)
            {
                return NotFound();
            }

            return View(superMarket);
        }

        // POST: SuperMarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superMarket = await _context.SuperMarkets.FindAsync(id);
            _context.SuperMarkets.Remove(superMarket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperMarketExists(int id)
        {
            return _context.SuperMarkets.Any(e => e.ID == id);
        }
    }
}
