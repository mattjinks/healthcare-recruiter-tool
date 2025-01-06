namespace RecruiterWorkflow.Models
{
    public enum Weekday {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Availability
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public Weekday DayofWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class AvailabilityRepresentation
    {
        public static string GenerateAvailabilityReport(List<Availability> availabilities)
        {
            var groupedByDay = availabilities
                .GroupBy(a => a.DayofWeek)
                .OrderBy(g => g.Key);  // Order by Day of the Week

            var report = "";

            foreach (var group in groupedByDay)
            {
                report += $"{group.Key}:\n";
                foreach (var availability in group)
                {
                    report += $"- {availability.Candidate.Id}: {availability.StartTime} - {availability.EndTime}\n";
                }
                report += "\n";
            }

            return report;
        }
    }
}
