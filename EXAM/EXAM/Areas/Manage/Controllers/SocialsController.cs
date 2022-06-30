using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXAM.DAL;
using EXAM.Models;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace EXAM.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class SocialsController : Controller
    {
        private readonly AppDbContext _context;

        public SocialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manage/Socials
        public IActionResult Index(int page=1)
        {
            var appDbContext = _context.Socials.Include(s => s.Team);
            return View(appDbContext.ToPagedList(page,5));
        }


        // GET: Manage/Socials/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, nameof(Socials.Id), nameof(Socials.Team.Name));
            return View();
        }

        // POST: Manage/Socials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Socials socials)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TeamId"] = new SelectList(_context.Teams, nameof(Socials.Id), nameof(Socials.Team.Name));
                return View(socials);;
            }
            _context.Add(socials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Manage/Socials/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var socials = await _context.Socials.FindAsync(id);
            if (socials == null)
                return NotFound();
            
            ViewData["TeamId"] = new SelectList(_context.Teams,nameof(Socials.Id), nameof(Socials.Team.Name));
            return View(socials);
        }

        // POST: Manage/Socials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Socials socials)
        {
            if (socials == null) return BadRequest();
            Socials dbsocial =await _context.Socials.FindAsync(socials.Id);
            if (dbsocial == null) return NotFound();

            dbsocial.Link = socials.Link;
            dbsocial.TeamId = socials.TeamId;
            dbsocial.IconClass = socials.IconClass;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // POST: Manage/Socials/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null) return NotFound();
            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
