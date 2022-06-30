﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXAM.DAL;
using EXAM.Models;
using X.PagedList;

namespace EXAM.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manage/Settings
        public  IActionResult Index(int page)
        {
            return View( _context.Settings.ToPagedList(page,10));
        }

   

        // GET: Manage/Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Settings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Value")] Settings settings)
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
        public async Task<IActionResult> Edit(Settings settings)
        {
            if (settings == null) return NotFound();
            
            return View(settings);
        }


        // POST: Manage/Settings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var settings = await _context.Settings.FindAsync(id);
            if (settings == null) return NotFound();
            _context.Settings.Remove(settings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}