using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Models;
using RecruiterWorkflow.Services;

namespace RecruiterWorkflow.Controllers
{
    public class MatchCandidatesController : Controller
    {
        private readonly RecruiterWorkflowContext _context;
        private readonly AIService _aiService;
        public MatchCandidatesController(AIService aiService, RecruiterWorkflowContext context)
        {
            _aiService = aiService;
            _context = context;
        }
        
        public async Task<IActionResult> SaveMatches(int jobId, List<int> savedCandidateIds, List<int> candidateIds)
        {
            //Create Match for each Candidate
            //Save Match to Database
            Console.WriteLine("Saving Matches" );
            Console.WriteLine("SavedCandidateIds: " + savedCandidateIds.Count);
            Console.WriteLine("candidateIds: " + candidateIds.Count);

            var job = _context.Jobs.Include(j => j.Matches).FirstOrDefault(j => j.Id == jobId);

            if (job == null)
            {
                return NotFound("Job not found");
            }

            if (job.Matches == null)
            {
                job.Matches = new List<Match>();
            }

            var matchesToRemove = new List<Match>();

            foreach (var match in job.Matches)
            {
                if (!savedCandidateIds.Any(c => c == match.CandidateId))
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

            foreach (var candidateId in savedCandidateIds)
            {
                if (!job.Matches.Any(m => m.CandidateId == candidateId))
                {
                    job.Matches.Add(new Match { JobId = jobId, CandidateId = candidateId });
                }
            }

            await _context.SaveChangesAsync();

            var candidates = await _context.Candidates
                .Include(c => c.Matches)
                .Where(c => candidateIds.Contains(c.Id))
                .ToListAsync();

            var viewModel = new MatchCandidatesViewModel
            {
                Candidates = candidates,
                Job = job
            };

            if (viewModel.Candidates == null)
            {
                Console.WriteLine("Candidates are Null");
            }
            else
            {
                Console.WriteLine("Candidates are not Null");
            }

            if (viewModel.Job == null)
            {
                Console.WriteLine("Job is Null");
            }
            else
            {
                Console.WriteLine("Job is not Null");
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("MatchJobWithCandidates", new { jobId });
            //return View("~/Views/Jobs/MatchCandidates.cshtml", viewModel);
        }


        public async Task<IActionResult> MatchJobWithCandidates(int jobId)
        {
            Console.WriteLine("Matching Job with Candidates");
            //Get All Candidates
            //Evalaute Their Location
            //Evaluate Their Occupation
            //Evaluate Their Specialty
            //Evaluate Desired Positions
            //Check if candidates already exists in Matched Canidates
            var candidates = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .ToListAsync();
           
            var job = await _context.Jobs
                .Include(c => c.Clinic)
                .Include(c => c.AvailablePositions)
                .FirstOrDefaultAsync(c => c.Id == jobId);

            if (job == null || candidates == null)
            {
                Console.WriteLine("Job or Candidates are Null");
            }

            var evaluatedCandidates = await Task.WhenAll(
                 candidates.Select(async candidate => new
                 {
                     Candidate = candidate,
                     IsMatch = await _aiService.EvaluateCandidateMatchWithJob(job, candidate)
                 })
             );

            var matchedCandidates = evaluatedCandidates
                .Where(result => result.IsMatch)
                .Select(result => result.Candidate)
                .ToList();

            var viewModel = new MatchCandidatesViewModel
            {
                Candidates = matchedCandidates,
                Job = job
            };

            return View("~/Views/Jobs/MatchCandidates.cshtml", viewModel);
        }

        public async Task<List<Candidate>> GetMatchedCandidates(int jobId)
        {
            Console.WriteLine("Matching Job with Candidates");
            
            var candidates = await _context.Candidates
                .Include(c => c.Credentials)
                .Include(e => e.Experiences)
                .Include(s => s.Skills)
                .Include(j => j.Matches)
                .Include(p => p.Positions)
                .ToListAsync();

            var job = await _context.Jobs
                .Include(c => c.Clinic)
                .Include(c => c.AvailablePositions)
                .FirstOrDefaultAsync(c => c.Id == jobId);

            if (job == null || candidates == null)
            {
                Console.WriteLine("Job or Candidates are Null");
            }

            var evaluatedCandidates = await Task.WhenAll(
                 candidates.Select(async candidate => new
                 {
                     Candidate = candidate,
                     IsMatch = await _aiService.EvaluateCandidateMatchWithJob(job, candidate)
                 })
             );

            var matchedCandidates = evaluatedCandidates
                .Where(result => result.IsMatch)
                .Select(result => result.Candidate)
                .ToList();


            return matchedCandidates;
        }


        public async Task<IActionResult> SearchMatchedCandidates(int jobId, List<int> candidateIds, string view, string name, string state, string occupation, string specialty, List<string> positionTypes)
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

            var candidates = await GetMatchedCandidates(jobId);

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

            var job = await _context.Jobs
                .Include(c => c.Clinic)
                .Include(c => c.AvailablePositions)
                .FirstOrDefaultAsync(c => c.Id == jobId);

            var viewModel = new MatchCandidatesViewModel
            {
                Candidates = filteredCandidates,
                Job = job
            };

            return View("~/Views/Jobs/MatchCandidates.cshtml", viewModel);
        }

        public async Task<IActionResult> AISearchMatchedCandidates(int jobId, string view, string userPrompt)
        {
            var candidates = await GetMatchedCandidates(jobId);

            var filteredCandidates = new List<Candidate>();
            foreach (var candidate in candidates)
            {
                if (await _aiService.EvaluateCandidateNaturalLanguage(candidate, userPrompt))
                {
                    filteredCandidates.Add(candidate);
                }
            }

            var job = await _context.Jobs
                .Include(c => c.Clinic)
                .Include(c => c.AvailablePositions)
                .FirstOrDefaultAsync(c => c.Id == jobId);

            var viewModel = new MatchCandidatesViewModel
            {
                Candidates = filteredCandidates,
                Job = job
            };

            return View("~/Views/Jobs/MatchCandidates.cshtml", viewModel);
        }
    }
}
