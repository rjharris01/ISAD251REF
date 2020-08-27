using System;
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


        public async Task<IActionResult> PastDeadlines(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var iSAD251_RHarrisContext = _context.Deadlines.Include(d => d.DeadlineType).Include(d => d.Subject).Where(a => a.DeadlineDate < DateTime.Now);
            TempData["returnURL"] = "Past";

            if (!String.IsNullOrEmpty(searchString))
            {
                iSAD251_RHarrisContext = iSAD251_RHarrisContext.Where(a => a.Subject.SubjectName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.DeadlineDate);
                    break;

                case "date_desc":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderByDescending(a => a.DeadlineDate);
                    break;

                default:
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.DeadlineDate);
                    break;

            }

            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        public async Task<IActionResult> FutureDeadlines(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var iSAD251_RHarrisContext = _context.Deadlines.Include(d => d.DeadlineType).Include(d => d.Subject).Where(a => a.DeadlineDate > DateTime.Now);
            TempData["returnURL"] = "Future";

            if (!String.IsNullOrEmpty(searchString))
            {
                iSAD251_RHarrisContext = iSAD251_RHarrisContext.Where(a => a.Subject.SubjectName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.DeadlineDate);
                    break;

                case "date_desc":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderByDescending(a => a.DeadlineDate);
                    break;

                default:
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.DeadlineDate);
                    break;

            }

            return View(await iSAD251_RHarrisContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("DeadlineId,SubjectID,DeadlineTypeID,DeadlineDate,DeadlineNotes")] New_Deadline New_Deadline)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw("EXEC New_Deadline @SubjectID,@DeadlineTypeID,@DeadlineDate, @DeadlineNotes",
                new Microsoft.Data.SqlClient.SqlParameter("@SubjectID", Int32.Parse(New_Deadline.SubjectID.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@DeadlineTypeID", Int32.Parse(New_Deadline.DeadlineTypeID.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@DeadlineDate", DateTime.Parse(New_Deadline.DeadlineDate.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@DeadlineNotes", New_Deadline.DeadlineNotes.ToString()));
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Child");
            }
            ViewData["DeadlineTypeId"] = new SelectList(_context.DeadlineTypes, "DeadlineTypeId", "DeadlineTypeName", New_Deadline.DeadlineTypeID);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", New_Deadline.SubjectID);
            return View(New_Deadline);
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

            string returnURL = TempData["returnURL"].ToString();

            if (id != deadlines.DeadlineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("EXEC Edit_Deadline @DeadlineID,@SubjectID, @DeadlineTypeID, @DeadlineDate, @DeadlineNotes",
                    new Microsoft.Data.SqlClient.SqlParameter("@DeadlineID",id),
                    new Microsoft.Data.SqlClient.SqlParameter("@SubjectID", Int32.Parse(deadlines.SubjectId.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@DeadlineTypeID", Int32.Parse(deadlines.DeadlineTypeId.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@DeadlineDate", DateTime.Parse(deadlines.DeadlineDate.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@DeadlineNotes", deadlines.DeadlineNotes.ToString()));
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

                if (returnURL == "Past")
                {
                    return RedirectToAction("PastDeadlines", "Deadlines");
                }

                else if (returnURL == "Future")
                {
                    return RedirectToAction("FutureDeadlines", "Deadlines");
                }
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
            string returnURL = TempData["returnURL"].ToString();

            var deadlines = await _context.Deadlines.FindAsync(id);
            _context.Database.ExecuteSqlRaw("EXEC Delete_Deadline @DeadlineID",

            new Microsoft.Data.SqlClient.SqlParameter("@DeadlineID", id));
            await _context.SaveChangesAsync();

            if (returnURL == "Past")
            {
                return RedirectToAction("PastDeadlines", "Deadlines");
            }

            else if (returnURL == "Future")
            {
                return RedirectToAction("FutureDeadlines", "Deadlines");
            }

            return RedirectToAction("Index", "Child");
        }

        private bool DeadlinesExists(int id)
        {
            return _context.Deadlines.Any(e => e.DeadlineId == id);
        }
    }
}
