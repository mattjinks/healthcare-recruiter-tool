using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;
using RecruiterWorkflow.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecruiterWorkflow.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        private readonly AIService _aiService;
        public CandidatesController(AIService aiService, RecruiterWorkflowContext context)
        {
            _aiService = aiService;
            _context = context;
        }
        public async Task<IActionResult> Show(int candidateId) 
        {
            var candidate = await _context.Candidates
                                  .Include(c => c.Credentials)
                                  .Include(c => c.Experiences)
                                  .Include(c => c.Positions)
                                  .Include(c => c.Skills)
                                  .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null)
            {
                return NotFound(); // Return a 404 if the candidate is not found
            }

            // Pass the candidate to the View
            return View(candidate);
        }

        public async Task<IActionResult> Index()
        {
            var candidates = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .ToListAsync();

            if (candidates == null)
            {
                return NotFound(); // Return a 404 if the candidate is not found
            }

            // Pass the candidate to the View
            return View(candidates);
        }

        public async Task<IActionResult> MatchJobs(int candidateId)
        {
            var candidate = await _context.Candidates
                          .Include(c => c.Credentials)
                          .Include(c => c.Experiences)
                          .Include(c => c.Positions)
                          .Include(c => c.Skills)
                          .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null)
            {
                return NotFound(); // Handle case where candidate is not found
            }

            // Fetch the jobs and related data
            var jobs = await _context.Jobs
                .Include(j => j.Clinic)
                .ToListAsync();

            // Create the view model
            var viewModel = new MatchJobsViewModel
            {
                Candidate = candidate,
                Jobs = jobs
            };

            // Pass the view model to the view
            return View(viewModel);
        }

        public async Task<IActionResult> SmartFilterSearch(string view, string name, string state, string occupation, string specialty, List<string> positionTypes) 
        {
            name = !string.IsNullOrEmpty(name) ? name : "*";
            Console.WriteLine("name: " + name);

            state = !string.IsNullOrEmpty(state) ? state : "*";
            Console.WriteLine("state: " + state);

            occupation = !string.IsNullOrEmpty(occupation) ? occupation : "*";
            Console.WriteLine("occupation: " + occupation);

            specialty = !string.IsNullOrEmpty(specialty) ? specialty : "*";
            Console.WriteLine("specialty: " + specialty);

            var filters = $"Name: {name}\n" +
                          $"State: {state}\n" +
                          $"Occupation: {occupation}\n" +
                          $"Specialty: {specialty}\n";

            var candidates = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .ToListAsync();

            var filteredCandidates = new List<Candidate>();
            foreach (var candidate in candidates)
            {
                if (await _aiService.EvaluateCandidateSmartFilters(candidate, filters))
                {
                    filteredCandidates.Add(candidate);
                }
            }

            var positions = 0;

            if (positionTypes != null && positionTypes.Any())
            {
                filteredCandidates = filteredCandidates.Where(candidate =>
                {
                    // Check if the candidate has any position that matches the selected position types
                    bool hasMatchingPosition = false;

                    Console.WriteLine(candidate.FirstName + " positions: " + candidate.Positions.Count);
                    Console.WriteLine("position types: " + positionTypes.Count);


                    foreach (var position in candidate.Positions)
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

            return View("~/Views/Candidates/Index.cshtml", filteredCandidates);
        }

        public async Task<IActionResult> NaturalLanguageSearch(string view, string userPrompt)
        {
            var candidates = await _context.Candidates
               .Include(c => c.Credentials)
               .Include(e => e.Experiences)
               .Include(s => s.Skills)
               .Include(j => j.Matches)
               .Include(p => p.Positions)
               .ToListAsync();

            var filteredCandidates = new List<Candidate>();
            foreach (var candidate in candidates)
            {
                if (await _aiService.EvaluateCandidateNaturalLanguage(candidate, userPrompt))
                {
                    filteredCandidates.Add(candidate);
                }
            }
            return View("~/Views/Candidates/Index.cshtml", filteredCandidates);
        }

        public IActionResult Create()
        {
            Console.WriteLine("Creating candidate");
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Id, FirstName, LastName, Email, Phone, State, Occupation, Specialty, Credentials, Experiences")] Candidate candidate
        )
        {   
            Console.WriteLine("Async Creating candidate");
            if (ModelState.IsValid)
            {
                if (candidate.Credentials == null)
                {
                    candidate.Credentials = new List<Credential>();
                }

                if (candidate.Experiences == null)
                {
                    candidate.Experiences = new List<Experience>();
                }
                Console.WriteLine("Model state Valid");
                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            Console.WriteLine("Invalid model state");
            return View(candidate);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == id);
            return View(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id, FirstName, LastName, Email, Phone, State, Occupation, Specialty")] Candidate candidate
        )
        {
            if (ModelState.IsValid)
            {
                _context.Update(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("~/Views/Create.cshtml", candidate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == id);
            return View(candidate);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddCredential(int candidateId, Credential credential)
        {
            var candidate = await _context.Candidates.Include(c => c.Credentials).FirstOrDefaultAsync(c => c.Id == candidateId);
            if (candidate == null) return NotFound();
            if (credential.Id > 0)
            {
                Console.WriteLine("Updating existing credential");
                // Handle updating an existing credential if necessary
                var existingCredential = candidate.Credentials.FirstOrDefault(c => c.Id == credential.Id);
                if (existingCredential != null)
                {
                    existingCredential.Name = credential.Name; // Update other fields as necessary
                                                               // more field updates
                }
                await _context.SaveChangesAsync();
                //return RedirectToAction("Edit", new { id = candidateId });
                return Ok();
            }
            else
            {
                Console.WriteLine("Adding new credential");
                // Otherwise, this is a new credential
                candidate.Credentials.Add(credential);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Create", new { id = candidateId }); // Redirect to Create or the appropriate action
                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCredential(int candidateId, int credentialId)
        {
            var candidate = await _context.Candidates.Include(c => c.Credentials)
                                                     .FirstOrDefaultAsync(c => c.Id == candidateId);
            if (candidate == null) return NotFound();

            var credential = candidate.Credentials.FirstOrDefault(c => c.Id == credentialId);
            if (credential != null)
            {
                candidate.Credentials.Remove(credential);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = candidateId });
        }
    }
}
