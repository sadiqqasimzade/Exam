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
using EXAM.Utilities;
using EXAM.Utilities.Extentions;

namespace EXAM.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manage/Teams
        public IActionResult Index(int page=1)
        {
            return View( _context.Teams.ToPagedList(page,10));
        }

        // GET: Manage/Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid)
                return View(team);
            if(team.File==null)
            {
                ModelState.AddModelError("File", "You must add File");
                return View(team);
            }
            if(team.File.Length/1024>Consts.TeamImgMaxSizeKb)
            {
                ModelState.AddModelError("File", "Size cant be more than:" + Consts.TeamImgMaxSizeKb + "Kb");
                return View(team);
            }
            if(!team.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Wrong file type");
                return View(team);
            }
            team.Img=await team.File.SaveFile(Consts.TeamImgRootPath,Consts.TeamImgMaxNameLen);
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Manage/Teams/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
                return NotFound();
            
            return View(team);
        }

        // POST: Manage/Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Team team)
        {
            if (team == null) return NotFound();
            Team dbteam =await _context.Teams.FindAsync(team.Id);
            if (dbteam == null) return NotFound();
            dbteam.Name = team.Name.Trim();
            dbteam.Position = team.Position.Trim();
            dbteam.Desc = team.Desc.Trim();
            if(team.File!=null)
            {
                if (team.File.Length / 1024 < Consts.TeamImgMaxSizeKb|| team.File.ContentType.Contains("image"))
                {
                    dbteam.Img.DeleteFile(Consts.TeamImgRootPath);
                    dbteam.Img = await team.File.SaveFile(Consts.TeamImgRootPath, Consts.TeamImgMaxNameLen);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // POST: Manage/Teams/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();
            team.Img.DeleteFile(Consts.TeamImgRootPath);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
