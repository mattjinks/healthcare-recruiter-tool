using System.Text;

namespace RecruiterWorkflow.Models
{
    public enum PositionType
    {
        FullTime,
        PartTime,
        Temporary,
        Permanent,
        Remote,
        InOffice
    }

    public class Position
    {
        public int Id { get; set; }
        public PositionType Type { get; set; }
        public Candidate? Candidate { get; set; }
        public int? CandidateId { get; set; }
        public int? JobId { get; set; }
        public Job? Job { get; set; }
    }

    public static class PositionExtensions
    {
        public static string ToStringRepresentation(List<Position> positions)
        {
            if (positions == null || !positions.Any())
                return "No position types provided.";

            var builder = new StringBuilder();
            foreach (var position in positions)
            {
                builder.AppendLine($"- Type: {position.Type}");
                builder.AppendLine(); // Add a blank line between positions
            }
            return builder.ToString();
        }
    }
}
