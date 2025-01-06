namespace RecruiterWorkflow.Models
{
    public class Credential
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Issuer { get; set; }
        public string? IssueDate { get; set; }
        public string? ExpirationDate { get; set; }
        public int? CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        //public DateTime IssueDate { get; set; }
        //public DateTime ExpirationDate { get; set; }
    }
}
