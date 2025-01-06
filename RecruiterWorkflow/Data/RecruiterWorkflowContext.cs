using Microsoft.EntityFrameworkCore;
using RecruiterWorkflow.Models;

namespace RecruiterWorkflow.Data
{
    public class RecruiterWorkflowContext : DbContext
    {
        public RecruiterWorkflowContext(DbContextOptions<RecruiterWorkflowContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().ToTable("Matches");
            // Relationships (already defined)
            modelBuilder.Entity<Job>()
                .HasOne(job => job.Clinic)
                .WithMany(clinic => clinic.Jobs)
                .HasForeignKey(job => job.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Credential>()
                .HasOne(c => c.Candidate)
                .WithMany(c => c.Credentials)
                .HasForeignKey(c => c.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Experience>()
                .HasOne(e => e.Candidate)
                .WithMany(c => c.Experiences)
                .HasForeignKey(e => e.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Candidate)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Candidate)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>()
                .HasOne(position => position.Candidate)
                .WithMany(c => c.Positions)
                .HasForeignKey(position => position.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Job)
                .WithMany(j => j.AvailablePositions)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(p => p.Job)
                .WithMany(j => j.Matches)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(p => p.Candidate)
                .WithMany(c => c.Matches)
                .HasForeignKey(p => p.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for Candidate
            modelBuilder.Entity<Candidate>().HasData(
                new Candidate
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Phone = "123-456-7890",
                    State = "California",
                    Occupation = "Nurse",
                    Specialty = "Pediatrics"
                },
                new Candidate
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "janesmith@example.com",
                    Phone = "987-654-3210",
                    State = "Texas",
                    Occupation = "Doctor",
                    Specialty = "Cardiology"
                }
            );

            // Seed data for Clinic
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "City Health Clinic",
                    Address = "123 Main St, Cityville",
                    Email = "contact@cityhealth.com"
                },
                new Clinic
                {
                    Id = 2,
                    Name = "Suburb Care Center",
                    Address = "456 Elm St, Suburbia",
                    Email = "info@suburbcare.com"
                }
            );

            // Seed data for Job
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    Title = "Registered Nurse",
                    Description = "Provide patient care in pediatrics.",
                    Duration = "1 year contract",
                    Responsibilities = "Administer medications, monitor patient recovery, and maintain accurate records.",
                    Requirements = "Valid nursing license, 2+ years of pediatric experience.",
                    CompensationAndBenefits = "Competitive salary, health insurance, and retirement benefits.",
                    ClinicId = 1
                },
                new Job
                {
                    Id = 2,
                    Title = "Cardiologist",
                    Description = "Perform heart surgeries and manage patients with cardiovascular diseases.",
                    Duration = "Permanent position",
                    Responsibilities = "Conduct diagnostic tests, perform surgeries, and provide treatment plans for cardiovascular health.",
                    Requirements = "Board certification in cardiology, valid medical license, and 5+ years of experience.",
                    CompensationAndBenefits = "High salary, malpractice insurance, healthcare benefits, and professional development opportunities.",
                    ClinicId = 2
                }
            );

            // Seed data for Experience
            modelBuilder.Entity<Experience>().HasData(
                new Experience
                {
                    Id = 1,
                    Employer = "City General Hospital",
                    Role = "Nurse",
                    Description = "Worked in pediatrics ward.",
                    Start = "2020-01-01",
                    End = "2023-01-01",
                    CandidateId = 1
                },
                new Experience
                {
                    Id = 2,
                    Employer = "County Heart Center",
                    Role = "Cardiologist",
                    Description = "Performed heart surgeries.",
                    Start = "2019-05-01",
                    End = "2023-06-01",
                    CandidateId = 2
                }
            );

            // Seed data for Credential
            modelBuilder.Entity<Credential>().HasData(
                new Credential
                {
                    Id = 1,
                    Name = "RN License",
                    Issuer = "State Board of Nursing",
                    IssueDate = "2019-01-01",
                    ExpirationDate = "2024-01-01",
                    CandidateId = 1
                },
                new Credential
                {
                    Id = 2,
                    Name = "Board Certification - Cardiology",
                    Issuer = "Medical Board",
                    IssueDate = "2018-01-01",
                    ExpirationDate = "2025-01-01",
                    CandidateId = 2
                }
            );

            // Seed data for Skill
            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 1,
                    Name = "Pediatric Care",
                    CandidateId = 1
                },
                new Skill
                {
                    Id = 2,
                    Name = "Surgical Procedures",
                    CandidateId = 2
                }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = 1,
                    Type = PositionType.FullTime,
                    CandidateId = 1
                },
                new Position
                {
                    Id = 2,
                    Type = PositionType.PartTime,
                    CandidateId = 1
                },
                new Position
                {
                    Id = 3,
                    Type = PositionType.Temporary,
                    CandidateId = 1
                },
                new Position
                {
                    Id = 4,
                    Type = PositionType.Permanent,
                    CandidateId = 1
                },
                new Position
                {
                    Id = 5,
                    Type = PositionType.FullTime,
                    CandidateId = 2
                },
                new Position
                {
                    Id = 6,
                    Type = PositionType.Temporary,
                    CandidateId = 2
                },
                new Position
                {
                    Id = 7,
                    Type = PositionType.Permanent,
                    CandidateId = 2
                },
                new Position
                {
                    Id = 8,
                    Type = PositionType.FullTime,
                    JobId = 1
                }
            );


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
