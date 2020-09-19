using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Charting.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SysExp.Models;
using SysExp.Utility;

namespace SysExp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.ManagerUser)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(await _db.ApplicationUser.Where(u => u.Id != claim.Value).ToListAsync());
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.ApplicationUser.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,LastName,Email")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }
            var userFromDb = await _db.ApplicationUser.FindAsync(id);

            if (ModelState.IsValid)
            {

                userFromDb.Email = applicationUser.Email;
                userFromDb.UserName = applicationUser.Email;
                userFromDb.NormalizedEmail = applicationUser.Email;
                userFromDb.NormalizedUserName = applicationUser.Email;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _db.ApplicationUser.FindAsync(id);
            _db.ApplicationUser.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}