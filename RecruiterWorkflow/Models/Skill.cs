namespace RecruiterWorkflow.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
