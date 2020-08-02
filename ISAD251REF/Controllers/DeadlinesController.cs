﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISAD251REF.Models;

namespace ISAD251REF.Controllers
{
    public class DeadlinesController : Controller
    {
        private readonly ISAD251_RHarrisContext _context;

        public DeadlinesController(ISAD251_RHarrisContext context)
        {
            _context = context;
        }

        // GET: Deadlines
        public async Task<IActionResult> Index()
        {
            var iSAD251_RHarrisContext = _context.Deadlines.Include(d => d.DeadlineType).Include(d => d.Subject);
            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        // GET: Deadlines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlines = await _context.Deadlines
                .Include(d => d.DeadlineType)
                .Include(d => d.Subject)
                .FirstOrDefaultAsync(m => m.DeadlineId == id);
            if (deadlines == null)
            {
                return NotFound();
            }

            return View(deadlines);
        }

        // GET: Deadlines/Create
        public IActionResult Create()
        {
            ViewData["DeadlineTypeId"] = new SelectList(_context.DeadlineTypes, "DeadlineTypeId", "DeadlineTypeName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Deadlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeadlineId,SubjectId,DeadlineTypeId,DeadlineDate,DeadlineNotes")] Deadlines deadlines)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deadlines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeadlineTypeId"] = new SelectList(_context.DeadlineTypes, "DeadlineTypeId", "DeadlineTypeName", deadlines.DeadlineTypeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", deadlines.SubjectId);
            return View(deadlines);
        }

        // GET: Deadlines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlines = await _context.Deadlines.FindAsync(id);
            if (deadlines == null)
            {
                return NotFound();
            }
            ViewData["DeadlineTypeId"] = new SelectList(_context.DeadlineTypes, "DeadlineTypeId", "DeadlineTypeName", deadlines.DeadlineTypeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", deadlines.SubjectId);
            return View(deadlines);
        }

        // POST: Deadlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeadlineId,SubjectId,DeadlineTypeId,DeadlineDate,DeadlineNotes")] Deadlines deadlines)
        {
            if (id != deadlines.DeadlineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deadlines);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeadlinesExists(deadlines.DeadlineId))
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
            ViewData["DeadlineTypeId"] = new SelectList(_context.DeadlineTypes, "DeadlineTypeId", "DeadlineTypeName", deadlines.DeadlineTypeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", deadlines.SubjectId);
            return View(deadlines);
        }

        // GET: Deadlines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlines = await _context.Deadlines
                .Include(d => d.DeadlineType)
                .Include(d => d.Subject)
                .FirstOrDefaultAsync(m => m.DeadlineId == id);
            if (deadlines == null)
            {
                return NotFound();
            }

            return View(deadlines);
        }

        // POST: Deadlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deadlines = await _context.Deadlines.FindAsync(id);
            _context.Deadlines.Remove(deadlines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeadlinesExists(int id)
        {
            return _context.Deadlines.Any(e => e.DeadlineId == id);
        }
    }
}