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
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manage/Settings
        public  IActionResult Index(int page=1)
        {
            return View( _context.Settings.ToPagedList(page,5));
        }

   

        // GET: Manage/Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Settings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Value")] Setting settings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(settings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(settings);
        }

        // GET: Manage/Settings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var settings = await _context.Settings.FindAsync(id);
            if (settings == null)
                return NotFound();
            
            return View(settings);
        }

        // POST: Manage/Settings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting setting)
        {
            if (setting == null) return NotFound();
            Setting dbsetting =await _context.Settings.FindAsync(setting.Id);
            if (dbsetting == null) return NotFound();
            dbsetting.Key = setting.Key;
            dbsetting.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Manage/Settings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            if (setting == null) return NotFound();
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
