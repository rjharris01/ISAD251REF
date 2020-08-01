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

        public async Task<IActionResult> PastAppointments()
        {
            var iSAD251_RHarrisContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.FamilyMember).Where(a => a.AppointmentDate < DateTime.Now);
            TempData["returnURL"] = "Past";
            return View(await iSAD251_RHarrisContext.ToListAsync());
        }

        public async Task<IActionResult> FutureAppointments()
        {
            var iSAD251_RHarrisContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.FamilyMember).Where(a => a.AppointmentDate > DateTime.Now);
            TempData["returnURL"] = "Future";
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
        public async Task<IActionResult> Create([Bind("AppointmentId,FamilyMemberId,AppointmentTypeId,AppointmentDate,AppointmentNotes")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Parent");
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypesId", "AppointmentTypeName", appointments.AppointmentTypeId);
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMembers, "FamilyMemberId", "FamilyMemberName", appointments.FamilyMemberId);
            return View(appointments);
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
                    _context.Update(appointments);
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
            _context.Appointments.Remove(appointments);
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
