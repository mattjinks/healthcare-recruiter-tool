namespace RecruiterWorkflow.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Job>? Jobs { get; set; }
    }
}
