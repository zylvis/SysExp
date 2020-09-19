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

namespace SysExp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Description
        public async Task<IActionResult> Index()
        {
            return View(await _context.Description.ToListAsync());
        }

        // GET: Admin/Description/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Description
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // GET: Admin/Description/Create
        public IActionResult Create()
        {
             ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/Description/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Picture,TextArea1,TextArea2,TextArea3,TextArea4,CategoryId")] Description description)
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
                    description.Picture = p1;


                }
                _context.Description.Add(description);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(description);
        }

        // GET: Admin/Description/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Description.FindAsync(id);
            if (description == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View(description);
        }

        // POST: Admin/Description/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TextArea1,TextArea2,TextArea3,TextArea4,CategoryId")] Description description)
        {
            if (id != description.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(description);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptionExists(description.Id))
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
           
            return View(description);
        }

        // GET: Admin/Description/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Description
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // POST: Admin/Description/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var description = await _context.Description.FindAsync(id);
            _context.Description.Remove(description);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptionExists(int id)
        {
            return _context.Description.Any(e => e.Id == id);
        }
    }
}
