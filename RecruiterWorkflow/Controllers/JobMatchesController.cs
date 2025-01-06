using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;

namespace RecruiterWorkflow.Controllers
{
    public class JobMatchesController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        public JobMatchesController(RecruiterWorkflowContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var job = await _context.Jobs.ToListAsync();
            return View(job);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            return View(job);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
