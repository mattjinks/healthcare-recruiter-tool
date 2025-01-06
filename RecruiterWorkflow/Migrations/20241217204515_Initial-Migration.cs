using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availibility = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    DayofWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    End = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullTime = table.Column<bool>(type: "bit", nullable: false),
                    PartTime = table.Column<bool>(type: "bit", nullable: false),
                    Temporary = table.Column<bool>(type: "bit", nullable: false),
                    Permanent = table.Column<bool>(type: "bit", nullable: false),
                    Remote = table.Column<bool>(type: "bit", nullable: false),
                    InOffice = table.Column<bool>(type: "bit", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompensationAndBenefits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobMatches_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobMatches_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });
            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Availibility", "Email", "FirstName", "LastName", "Occupation", "Phone", "Specialty", "State" },
                values: new object[,]
                {
                    { 1, null, "johndoe@example.com", "John", "Doe", "Nurse", "123-456-7890", "Pediatrics", "California" },
                    { 2, null, "janesmith@example.com", "Jane", "Smith", "Doctor", "987-654-3210", "Cardiology", "Texas" }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main St, Cityville", "contact@cityhealth.com", "City Health Clinic" },
                    { 2, "456 Elm St, Suburbia", "info@suburbcare.com", "Suburb Care Center" }
                });

            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "CandidateId", "ExpirationDate", "IssueDate", "Issuer", "Name" },
                values: new object[,]
                {
                    { 1, 1, "2024-01-01", "2019-01-01", "State Board of Nursing", "RN License" },
                    { 2, 2, "2025-01-01", "2018-01-01", "Medical Board", "Board Certification - Cardiology" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "CandidateId", "Description", "Employer", "End", "Role", "Start" },
                values: new object[,]
                {
                    { 1, 1, "Worked in the pediatrics ward.", "City General Hospital", "2023-01-01", "Nurse", "2020-01-01" },
                    { 2, 2, "Performed heart surgeries.", "County Heart Center", "2023-06-01", "Cardiologist", "2019-05-01" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "ClinicId", "CompensationAndBenefits", "Description", "Duration", "Requirements", "Responsibilities", "Schedule", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, "Provide patient care in pediatrics.", null, null, null, "Full-time", "Registered Nurse" },
                    { 2, 2, null, "Perform heart surgeries and consultations.", null, null, null, "Full-time", "Cardiologist" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "CandidateId", "Name" },
                values: new object[,]
                {
                { 1, 1, "Pediatric Care" },
                { 2, 2, "Surgical Procedures" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_CandidateId",
                table: "Availabilities",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_CandidateId",
                table: "Credentials",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CandidateId",
                table: "Experiences",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMatches_CandidateId",
                table: "JobMatches",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMatches_JobId",
                table: "JobMatches",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ClinicId",
                table: "Jobs",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CandidateId",
                table: "Positions",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CandidateId",
                table: "Skills",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Availabilities");
            migrationBuilder.DropTable(name: "Credentials");
            migrationBuilder.DropTable(name: "Experiences");
            migrationBuilder.DropTable(name: "JobMatches");
            migrationBuilder.DropTable(name: "Positions");
            migrationBuilder.DropTable(name: "Skills");
            migrationBuilder.DropTable(name: "Jobs");
            migrationBuilder.DropTable(name: "Candidates");
            migrationBuilder.DropTable(name: "Clinics");
        }

    }
}

