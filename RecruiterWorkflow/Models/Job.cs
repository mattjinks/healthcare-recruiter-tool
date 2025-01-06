using Microsoft.AspNetCore.Builder;
using RecruiterWorkflow.Models;

namespace RecruiterWorkflow.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? Schedule { get; set; }
        public string? Responsibilities { get; set; }
        public string? Requirements { get; set; }
        public string? CompensationAndBenefits { get; set; }
        public int ClinicId { get; set; } // Foreign key
        public Clinic? Clinic { get; set; } // Navigation property
        public List<Position> AvailablePositions { get; set; }
        public List<Match>? Matches { get; set; }
    }

    public class JobUtils
    {
        public static string ToString(Job job)
        {
            Console.WriteLine("JobUtils.ToString(Job job) called.");
            if (job == null)
            {
                Console.WriteLine("Job is null");
                throw new ArgumentNullException(nameof(job));
            }

            Console.WriteLine("Job not null: " + job.Title);

            var positions = job.AvailablePositions != null
                ? string.Join(", ", job.AvailablePositions.Select(p => p.Type.ToString()))
                : "None";

            return $"Job: {job.Title}\n" +
                   $"Clinic: {job.Clinic?.Name}\n" +
                   $"Location: {job.Clinic?.Address}\n" +
                   $"Description: {job.Description}\n" +
                   $"Responsibilities: {job.Responsibilities}\n" +
                   $"Requirements: {job.Requirements}\n" +
                   $"Compensation and Benefits: {job.CompensationAndBenefits}\n" +
                   $"Available Positions: {positions}\n";
        }
    }
}
