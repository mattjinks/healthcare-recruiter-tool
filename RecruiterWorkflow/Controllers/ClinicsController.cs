using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;

namespace RecruiterWorkflow.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        public ClinicsController(RecruiterWorkflowContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var clinic = await _context.Clinics.ToListAsync();
            return View(clinic);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Id, Name, Address, Email")] Clinic clinic
        )
        {
            if (ModelState.IsValid)
            {
                _context.Clinics.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var clinic = await _context.Clinics.FirstOrDefaultAsync(x => x.Id == id);
            return View(clinic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id, Name, Address, Email")] Clinic clinic
        )
        {
            if (ModelState.IsValid)
            {
                _context.Update(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clinic = await _context.Clinics.FirstOrDefaultAsync(x => x.Id == id);
            return View(clinic);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
