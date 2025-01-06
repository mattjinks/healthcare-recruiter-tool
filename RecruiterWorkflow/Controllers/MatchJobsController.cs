using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;
using RecruiterWorkflow.Services;

namespace RecruiterWorkflow.Controllers
{
    public class MatchJobsController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        private readonly AIService _aiService;
        public MatchJobsController(AIService aiService, RecruiterWorkflowContext context)
        {
            _aiService = aiService;
            _context = context;
        }
        public async Task<List<Job>> GetMatchedJobs(int candidateId)
        {
            Console.WriteLine("Matching Candidate with Jobs");

            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(p => p.AvailablePositions)
                .Include(c => c.Clinic)
                .ToListAsync();

            var candidate = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (jobs == null || candidate == null)
            {
                Console.WriteLine("Jobs or Candidate are Null");
            }

            var evaluatedJobs = await Task.WhenAll(
                 jobs.Select(async job => new
                 {
                     Job = job,
                     IsMatch = await _aiService.EvaluateJobMatchWithCandidate(candidate, job)
                 })
             );

            var matchedJobs = evaluatedJobs
                .Where(result => result.IsMatch)
                .Select(result => result.Job)
                .ToList();


            return matchedJobs;
        }

        public async Task<IActionResult> MatchCandidateWithJobs(int candidateId)
        {
            Console.WriteLine("Matching Candidate with Jobs");

            Console.WriteLine("Candidate ID: " + candidateId);

            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(p => p.AvailablePositions)
                .Include(c => c.Clinic)
                .ToListAsync();

            var candidate = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (jobs == null || candidate == null)
            {
                Console.WriteLine("Jobs or Candidate are Null");
            }

            var evaluatedJobs = await Task.WhenAll(
                 jobs.Select(async job => new
                 {
                     Job = job,
                     IsMatch = await _aiService.EvaluateJobMatchWithCandidate(candidate, job)
                 })
             );

            var matchedJobs = evaluatedJobs
                .Where(result => result.IsMatch)
                .Select(result => result.Job)
                .ToList();
            
            var viewModel = new MatchJobsViewModel
            {
                Jobs = matchedJobs,
                Candidate = candidate
            };

            return View("~/Views/Candidates/MatchJobs.cshtml", viewModel);
        }

        public async Task<IActionResult> SearchMatchedJobs(int candidateId, List<int> jobIds, string view, string role, string clinicName, string location, List<string> positionTypes)
        {
            clinicName = !string.IsNullOrEmpty(clinicName) ? clinicName : "*";
            Console.WriteLine("clinic name: " + clinicName);

            location = !string.IsNullOrEmpty(location) ? location : "*";
            Console.WriteLine("state: " + location);

            role = !string.IsNullOrEmpty(role) ? role : "*";
            Console.WriteLine("role: " + role);

            var filters = $"Role: {role}\n" +
                          $"Clinic Name: {clinicName}\n" +
                          $"Location: {location}\n" +
                          $"Specialty: Not Provided\n";

            var jobs = await GetMatchedJobs(candidateId);

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

            var candidate = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            var viewModel = new MatchJobsViewModel
            {
                Jobs = filteredJobs,
                Candidate = candidate
            };

            return View("~/Views/Candidates/MatchJobs.cshtml", viewModel);
        }

        public async Task<IActionResult> AISearchMatchedJobs(int candidateId, string view, string userPrompt)
        {
            var jobs = await GetMatchedJobs(candidateId);

            var filteredJobs = new List<Job>();
            foreach (var job in jobs)
            {
                if (await _aiService.EvaluateJobNaturalLanguage(job, userPrompt))
                {
                    filteredJobs.Add(job);
                }
            }

            var candidate = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            var viewModel = new MatchJobsViewModel
            {
                Jobs = filteredJobs,
                Candidate = candidate
            };

            return View("~/Views/Candidates/MatchJobs.cshtml", viewModel);
        }
        public async Task<IActionResult> SaveMatches(int candidateId, List<int> savedJobIds, List<int> jobIds)
        {

            var candidate = _context.Candidates.Include(c => c.Matches).FirstOrDefault(c => c.Id == candidateId);

            if (candidate == null)
            {
                return NotFound("Job not found");
            }

            if (candidate.Matches == null)
            {
                candidate.Matches = new List<Match>();
            }

            var matchesToRemove = new List<Match>();

            foreach (var match in candidate.Matches)
            {
                if (!savedJobIds.Any(j => j == match.JobId))
                {
                    matchesToRemove.Add(match);
                }
            }

            // Remove matches outside the loop
            foreach (var match in matchesToRemove)
            {
                var savedMatch = await _context.Matches.FindAsync(match.Id);
                if (savedMatch != null)
                {
                    _context.Matches.Remove(savedMatch);
                }
            }

            // Save changes after all removals
            await _context.SaveChangesAsync();

            foreach (var jobId in savedJobIds)
            {
                if (!candidate.Matches.Any(m => m.JobId == jobId))
                {
                    candidate.Matches.Add(new Match { JobId = jobId, CandidateId = candidateId });
                }
            }

            await _context.SaveChangesAsync();

            var jobs = await _context.Jobs
                .Include(j => j.Matches)
                .Include(c => c.Clinic)
                .Where(j => jobIds.Contains(j.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();

            var viewModel = new MatchJobsViewModel
            {
                Jobs = jobs,
                Candidate = candidate
            };

            if (viewModel.Candidate == null)
            {
                Console.WriteLine("Candidate is Null");
            }
            else
            {
                Console.WriteLine("Candidate is not Null");
            }

            if (viewModel.Jobs == null)
            {
                Console.WriteLine("Jobs is Null");
            }
            else
            {
                Console.WriteLine("Jobs is not Null");
            }

            return View("~/Views/Candidates/MatchJobs.cshtml", viewModel);

            //return RedirectToAction("MatchJobWithCandidates", new { candidateId });
            //return View("~/Views/Jobs/MatchCandidates.cshtml", viewModel);
        }
    }
}
