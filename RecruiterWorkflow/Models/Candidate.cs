using System.Net;
using RecruiterWorkflow.Models;

namespace RecruiterWorkflow.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Occupation { get; set; }
        public string? Specialty { get; set; }
        public string? Availibility { get; set; }
        public List<Position> Positions { get; set; } // Define as a collection
        public List<Credential>? Credentials { get; set; }
        public List<Experience>? Experiences { get; set; }
        public List<Match>? Matches { get; set; }
        public List<Availability>? Availabilities { get; set; }
        public List<Skill>? Skills { get; set; }
    }

    public class CandidateUtils
    {
        public static string ToString(Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate));
            }

            var positions = candidate.Positions != null
                ? string.Join(", ", candidate.Positions.Select(p => p.Type.ToString()))
                : "None";

            var credentials = candidate.Credentials != null
                ? string.Join(", ", candidate.Credentials.Select(c => c.Name))
                : "None";

            var skills = candidate.Skills != null
                ? string.Join(", ", candidate.Skills.Select(s => s.Name))
                : "None";

            var experiences = candidate.Experiences != null
                ? string.Join("; ", candidate.Experiences.Select(e => $"{e.Employer} ({e.Start}, {e.End})"))
                : "None";

            return $"Candidate: {candidate.FirstName} {candidate.LastName}\n" +
                   $"Email: {candidate.Email}\n" +
                   $"Phone: {candidate.Phone}\n" +
                   $"State: {candidate.State}\n" +
                   $"Occupation: {candidate.Occupation}\n" +
                   $"Specialty: {candidate.Specialty}\n" +
                   $"Desired Positions/Availibility: {positions}\n" +
                   $"Credentials: {credentials}\n" +
                   $"Skills: {skills}\n" +
                   $"Experiences: {experiences}\n";
        }
    }
}