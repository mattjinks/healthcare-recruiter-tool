using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;
using RecruiterWorkflow.Services;

namespace RecruiterWorkflow.Controllers
{
    public class JobsController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        private readonly AIService _aiService;
        public JobsController(AIService aiService, RecruiterWorkflowContext context)
        {
            _aiService = aiService;
            _context = context;
        }


        public async Task<IActionResult> MatchCandidates(int jobId)
        {
            var job = await _context.Jobs
                                  .Include(c => c.Clinic)
                                  .Include(c => c.AvailablePositions)
                                  .FirstOrDefaultAsync(c => c.Id == jobId);

            if (job == null)
            {
                return NotFound(); // Handle case where candidate is not found
            }

            // Fetch the jobs and related data
            var candidates = await _context.Candidates
                          .Include(c => c.Credentials)
                          .Include(c => c.Experiences)
                          .Include(c => c.Positions)
                          .Include(c => c.Skills)
                          .ToListAsync();

            // Create the view model
            var viewModel = new MatchCandidatesViewModel
            {
                Candidates = candidates,
                Job = job
            };

            // Pass the view model to the view
            return View(viewModel);
        }

        public async Task<IActionResult> Show(int jobId)
        {
            Console.WriteLine("Show Job: " + jobId);
            var job = await _context.Jobs
                                  .Include(c => c.Clinic)
                                  .Include(c => c.AvailablePositions)
                                  .FirstOrDefaultAsync(c => c.Id == jobId);

            if (job == null)
            {
                return NotFound(); // Return a 404 if the candidate is not found
            }

            // Pass the candidate to the View
            return View(job);
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(p => p.AvailablePositions)
                .Include(c => c.Clinic)
                .ToListAsync();
            return View(jobs);
        }

        public async Task<IActionResult> SmartFilterSearch(string view, string role, string clinicName, string specialty, string location, List<string> positionTypes)
        {
            clinicName = !string.IsNullOrEmpty(clinicName) ? clinicName : "*";
            Console.WriteLine("clinic name: " + clinicName);

            location = !string.IsNullOrEmpty(location) ? location : "*";
            Console.WriteLine("state: " + location);

            role = !string.IsNullOrEmpty(role) ? role : "*";
            Console.WriteLine("role: " + role);

            specialty = !string.IsNullOrEmpty(specialty) ? specialty : "*";
            Console.WriteLine("specialty: " + specialty);

            var filters = $"Role: {role}\n" +
                          $"Clinic Name: {clinicName}\n" +
                          $"Location: {location}\n" +
                          $"Specialty: {specialty}\n";

            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(p => p.AvailablePositions)
                .Include(c => c.Clinic)
                .ToListAsync();

            var filteredJobs = new List<Job>();
            foreach (var job in jobs)
            {
                if (await _aiService.EvaluateJobSmartFilters(job, filters))
                {
                    filteredJobs.Add(job);
                }
            }

            var positions = 0;

            if (positionTypes != null && positionTypes.Any())
            {
                filteredJobs = filteredJobs.Where(job =>
                {
                    // Check if the candidate has any position that matches the selected position types
                    bool hasMatchingPosition = false;

                    Console.WriteLine(job.Title + " positions: " + job.AvailablePositions.Count);
                    Console.WriteLine("position types: " + positionTypes.Count);


                    foreach (var position in job.AvailablePositions)
                    {
                        // Parse the position type from the string to the enum
                        if (Enum.TryParse<PositionType>(position.Type.ToString(), out PositionType positionEnum))
                        {
                            // Check if the position type matches any of the selected position types
                            if (positionTypes.Contains(positionEnum.ToString()))
                            {
                                hasMatchingPosition = true;
                                break;  // Exit loop once a match is found
                            }
                        }
                    }

                    return hasMatchingPosition;
                }).ToList();
            }

            return View("~/Views/Jobs/Index.cshtml", filteredJobs);
        }

        public async Task<IActionResult> NaturalLanguageSearch(string view, string userPrompt)
        {
            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(p => p.AvailablePositions)
                .Include(c => c.Clinic)
                .ToListAsync();

            var filteredJobs = new List<Job>();
            foreach (var job in jobs)
            {
                if (await _aiService.EvaluateJobNaturalLanguage(job, userPrompt))
                {
                    filteredJobs.Add(job);
                }
            }
            return View("~/Views/Jobs/Index.cshtml", filteredJobs);
        }

        public async Task<IActionResult> AddPositions(int jobId)
        {
            Console.WriteLine("Create New Position: " + jobId);

            var job = await _context.Jobs
                                 .Include(c => c.Clinic)
                                 .Include(c => c.AvailablePositions)
                                 .FirstOrDefaultAsync(c => c.Id == jobId);

            var availablePosition = new Position { Job = job, Type = PositionType.FullTime };
            _context.Positions.Add(availablePosition);
            await _context.SaveChangesAsync();
            return View("~/Views/Jobs/Show.cshtml", job);
        }

        public async Task<IActionResult> DeletePositions(int posId)
        {
            var pos = await _context.Positions.Include(c => c.Job).FirstOrDefaultAsync(c => c.Id == posId);
            var job = pos.JobId;
            if (pos == null) { Console.WriteLine("pos Null"); }
            if (job == null) { Console.WriteLine("job Null"); }
            if (pos != null)
            {
                _context.Positions.Remove(pos);
                await _context.SaveChangesAsync();
            }
            return View("~/Views/Jobs/Show.cshtml", await _context.Jobs.FindAsync(job));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Id, Title, Description, Duration, Schedule, Responsibilities, Requirements, CompensationAndBenefits, ClinicId")] Job job
        )
        {
            if (ModelState.IsValid)
            {
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id, Title, Description, Duration, Schedule, Responsibilities, Requirements, CompensationAndBenefits, ClinicId")] Job job
        )
        {
            if (ModelState.IsValid)
            {
                _context.Update(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
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
