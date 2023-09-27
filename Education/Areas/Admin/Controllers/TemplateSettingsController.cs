using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Education.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Education.Models;

namespace Education.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemplateSettingsController : Controller
    {
        private readonly AppDbContext _context;

        public TemplateSettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TemplateSettings


        public async Task<IActionResult> Index()
        {
            return _context.TemplateSettings != null ? 
                          View(await _context.TemplateSettings.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.TemplateSettings'  is null.");
        }

        // GET: Admin/TemplateSettings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TemplateSettings == null)
            {
                return NotFound();
            }

            var templateSetting = await _context.TemplateSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateSetting == null)
            {
                return NotFound();
            }

            return View(templateSetting);
        }

        // GET: Admin/TemplateSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TemplateSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BannerText,Card1,Card2,Card3,Card4")] TemplateSetting templateSetting)
        {
            if (ModelState.IsValid)
            {
                templateSetting.Id = Guid.NewGuid();
                _context.Add(templateSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(templateSetting);
        }

        // GET: Admin/TemplateSettings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TemplateSettings == null)
            {
                return NotFound();
            }

            var templateSetting = await _context.TemplateSettings.FindAsync(id);
            if (templateSetting == null)
            {
                return NotFound();
            }
            return View(templateSetting);
        }

        // POST: Admin/TemplateSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BannerText,Card1,Card2,Card3,Card4")] TemplateSetting templateSetting)
        {
            if (id != templateSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(templateSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateSettingExists(templateSetting.Id))
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
            return View(templateSetting);
        }

        // GET: Admin/TemplateSettings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            
            if (id == null || _context.TemplateSettings == null)
            {
                return NotFound();
            }

            var templateSetting = await _context.TemplateSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateSetting == null)
            {
                return NotFound();
            }

            return View(templateSetting);
        }

        // POST: Admin/TemplateSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TemplateSettings == null)
            {
                return Problem("Entity set 'AppDbContext.TemplateSettings'  is null.");
            }
            var templateSetting = await _context.TemplateSettings.FindAsync(id);
            if (templateSetting != null)
            {
                _context.TemplateSettings.Remove(templateSetting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateSettingExists(Guid id)
        {
          return (_context.TemplateSettings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
