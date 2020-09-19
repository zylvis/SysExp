using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Charting.Data;
using SysExp.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using SysExp.Utility;

namespace SysExp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.ManagerUser)]
    public class StrategyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StrategyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Strategy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Strategy.Include(s => s.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Strategy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategy
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strategy == null)
            {
                return NotFound();
            }

            return View(strategy);
        }

        // GET: Admin/Strategy/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/Strategy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Picture,Details,CategoryId")] Strategy strategy)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    strategy.Picture = p1;


                }
                _context.Strategy.Add(strategy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(strategy);
        }

        // GET: Admin/Strategy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategy.FindAsync(id);
            if (strategy == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", strategy.CategoryId);
            return View(strategy);
        }

        // POST: Admin/Strategy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Picture,Details,CategoryId")] Strategy strategy)
        {
            if (strategy.Id == 0)
            {
                return NotFound();
            }

            var strategyFromDb = await _context.Strategy.FindAsync(id);

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    strategyFromDb.Picture = p1;

                }
                strategyFromDb.Name = strategy.Name;
                strategyFromDb.Details = strategy.Details;
                strategyFromDb.CategoryId = strategy.CategoryId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(strategy);
        }

        // GET: Admin/Strategy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategy
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strategy == null)
            {
                return NotFound();
            }

            return View(strategy);
        }

        // POST: Admin/Strategy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strategy = await _context.Strategy.FindAsync(id);
            _context.Strategy.Remove(strategy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrategyExists(int id)
        {
            return _context.Strategy.Any(e => e.Id == id);
        }
    }
}
