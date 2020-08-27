using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISAD251REF.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;

namespace ISAD251REF.Controllers
{
    public class AppointmentsController : Controller
    {    
        
            
        private readonly ISAD251_RHarrisContext _context;

        public AppointmentsController(ISAD251_RHarrisContext context)
        {
            _context = context;
        }



        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var iSAD251_RHarrisContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.FamilyMember);
            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        public async Task<IActionResult> PastAppointments(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var iSAD251_RHarrisContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.FamilyMember).Where(a => a.AppointmentDate < DateTime.Now);
            TempData["returnURL"] = "Past";

            if (!String.IsNullOrEmpty(searchString))
            {
                iSAD251_RHarrisContext = iSAD251_RHarrisContext.Where(a => a.FamilyMember.FamilyMemberName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.AppointmentDate);
                    break;

                case "date_desc":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderByDescending(a => a.AppointmentDate);
                    break;

                default:
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.AppointmentDate);
                    break;

            }

            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        public async Task<IActionResult> FutureAppointments(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var iSAD251_RHarrisContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.FamilyMember).Where(a => a.AppointmentDate > DateTime.Now);
            TempData["returnURL"] = "Future";

            if (!String.IsNullOrEmpty(searchString))
            {
                iSAD251_RHarrisContext = iSAD251_RHarrisContext.Where(a => a.FamilyMember.FamilyMemberName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.AppointmentDate);
                    break;

                case "date_desc":
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderByDescending(a => a.AppointmentDate);
                    break;

                default:
                    iSAD251_RHarrisContext = iSAD251_RHarrisContext.OrderBy(a => a.AppointmentDate);
                    break;

            }
            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.AppointmentType)
                .Include(a => a.FamilyMember)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            

            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypesId", "AppointmentTypeName");
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMembers, "FamilyMemberId", "FamilyMemberName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyMemberID,AppointmentTypeID,AppointmentDate,AppointmentNotes")] New_Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw("EXEC New_Appointment @FamilyMemberID, @AppointmentTypeID, @AppointmentDate, @AppointmentNotes",
                new Microsoft.Data.SqlClient.SqlParameter("@FamilyMemberID", Int32.Parse(appointment.FamilyMemberID.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@AppointmentTypeID", Int32.Parse(appointment.AppointmentTypeID.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@AppointmentDate", DateTime.Parse(appointment.AppointmentDate.ToString())),
                new Microsoft.Data.SqlClient.SqlParameter("@AppointmentNotes", appointment.AppointmentNotes.ToString()));
                

                await _context.SaveChangesAsync();
                TempData["IsValid"] = true;
                return RedirectToAction("Index","Parent");
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypesId", "AppointmentTypeName", appointment.AppointmentTypeID);
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMembers, "FamilyMemberId", "FamilyMemberName", appointment.FamilyMemberID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            


            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypesId", "AppointmentTypeName", appointments.AppointmentTypeId);
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMembers, "FamilyMemberId", "FamilyMemberName", appointments.FamilyMemberId);
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("AppointmentId,FamilyMemberId,AppointmentTypeId,AppointmentDate,AppointmentNotes")] Appointments appointments)
        {

            string returnURL = TempData["returnURL"].ToString();


            if (id != appointments.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("EXEC Edit_Appointment @AppointmentID,@FamilyMemberID, @AppointmentTypeID, @AppointmentDate, @AppointmentNotes",
                    new Microsoft.Data.SqlClient.SqlParameter("@AppointmentID", Int32.Parse(appointments.AppointmentId.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@FamilyMemberID", Int32.Parse(appointments.FamilyMemberId.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@AppointmentTypeID", Int32.Parse(appointments.AppointmentTypeId.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@AppointmentDate", DateTime.Parse(appointments.AppointmentDate.ToString())),
                    new Microsoft.Data.SqlClient.SqlParameter("@AppointmentNotes", appointments.AppointmentNotes.ToString()));

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.AppointmentId))
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
                    return RedirectToAction("PastAppointments", "Appointments");
                }

                else if (returnURL == "Future")
                {
                    return RedirectToAction("FutureAppointments", "Appointments");
                }
                
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypesId", "AppointmentTypeName", appointments.AppointmentTypeId);
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMembers, "FamilyMemberId", "FamilyMemberName", appointments.FamilyMemberId);
            return View(appointments);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.AppointmentType)
                .Include(a => a.FamilyMember)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string returnURL = TempData["returnURL"].ToString();
            var appointments = await _context.Appointments.FindAsync(id);


            _context.Database.ExecuteSqlRaw("EXEC Delete_Appointment @DeleteID",
 
            new Microsoft.Data.SqlClient.SqlParameter("@DeleteID", id));

            await _context.SaveChangesAsync();
            

            if (returnURL == "Past")
            {
                return RedirectToAction("PastAppointments", "Appointments");
            }

            else if (returnURL == "Future")
            {
                return RedirectToAction("FutureAppointments", "Appointments");
            }

            return RedirectToAction("Index","Parent");
        }

        private bool AppointmentsExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
