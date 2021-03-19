﻿using System;
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
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;

        public OrdersController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                var shoppingContext = _context.Orders.Include(o => o.Customer).Include(o => o.SuperMarket);
                return View(await shoppingContext.ToListAsync());
            }
            else if (User.IsInRole("buyer"))
            {
                var shoppingContext = _context.Orders.Where(x => x.Customer.Email == User.Identity.Name).Include(o => o.Customer).Include(o => o.SuperMarket);
                return View(await shoppingContext.ToListAsync());
            }
            else
                return new RedirectToActionResult("NoAccess", "Home", null);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.SuperMarket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            if (User.IsInRole("buyer"))
            {
                if (order.Customer.Email != User.Identity.Name)
                    return new RedirectToActionResult("NoAccess", "Home", null);
            }

            return View(order);
        }

        // GET: Orders/Create
        [RedirectToNoAcessAuthorize]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID");
            ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "ID", "ID");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,CustomerId,SuperMarketId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "ID", "ID", order.SuperMarketId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "ID", "ID", order.SuperMarketId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,CustomerId,SuperMarketId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            ViewData["SuperMarketId"] = new SelectList(_context.SuperMarkets, "ID", "ID", order.SuperMarketId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.SuperMarket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RedirectToNoAcessAuthorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}