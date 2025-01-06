namespace RecruiterWorkflow.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Employer { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
        public int? CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
