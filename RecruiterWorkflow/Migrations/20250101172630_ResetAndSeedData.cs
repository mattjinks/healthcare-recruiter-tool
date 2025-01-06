using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RecruiterWorkflow.Models;

#nullable disable

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class ResetAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Matches");
            migrationBuilder.Sql("DELETE FROM Positions");
            migrationBuilder.Sql("DELETE FROM Skills");
            migrationBuilder.Sql("DELETE FROM Credentials");
            migrationBuilder.Sql("DELETE FROM Experiences");
            migrationBuilder.Sql("DELETE FROM Jobs");
            migrationBuilder.Sql("DELETE FROM Clinics");
            migrationBuilder.Sql("DELETE FROM Candidates");

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "Phone", "State", "Occupation", "Specialty" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "johndoe@example.com", "123-456-7890", "California", "Nurse", "Pediatrics" },
                    { 2, "Jane", "Smith", "janesmith@example.com", "987-654-3210", "Texas", "Doctor", "Cardiology" },
                    { 3, "Jessica", "Moore", "jessicam@example.com", "890-123-4567", "New York", "Nurse", "Neurology" },
                }
            );

            // Insert seed data into Clinics table
            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Name", "Address", "Email" },
                values: new object[,]
                {
                    { 1, "City Health Clinic", "Sacramento, California", "contact@cityhealth.com" },
                    { 2, "Suburb Care Center", "Austin, Texas", "info@suburbcare.com" },
                    { 3, "Downtown Wellness Center", "Brooklyn, New York", "hello@downtownwellness.com" },
                    { 4, "Atlanta Health Clinic", "Atlanta, Georgia", "support@countrysidemedical.com" },
                }
            );

            // Insert seed data into Jobs table
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Title", "Description", "Duration", "Responsibilities", "Requirements", "CompensationAndBenefits", "ClinicId" },
                values: new object[,]
                {
                    // Job for John Doe
                    { 1, "Pediatric Nurse", "Provide care for pediatric patients.", "Full-Time", "Monitor and manage pediatric patient care.", "Licensed Nurse with a specialty in Pediatrics.", "Competitive salary, health insurance, and PTO.", 1 },
                    // Job for Jane Smith
                    { 2, "Cardiologist", "Diagnose and treat heart-related conditions.", "Full-Time", "Perform surgeries and consultations for cardiac patients.", "Licensed Medical Doctor with a specialty in Cardiology.", "Competitive salary, retirement plan, and health benefits.", 2 },
                    // Job for Jessica Moore
                    { 3, "Neurology Nurse", "Assist in neurological patient care.", "Full-Time", "Support neurologists and care for patients with neurological disorders.", "Licensed Nurse with a specialty in Neurology.", "Flexible hours, health insurance, and performance bonuses.", 3 },
                    // Job for Daniel Thomas
                }
            );

            // Insert seed data into Experiences table
            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Employer", "Role", "Description", "Start", "End", "CandidateId" },
                values: new object[,]
                {
                    { 1, "St. Mary's Hospital", "Pediatric Nurse", "Cared for pediatric patients in a busy hospital ward, administering medications and providing post-surgical care.", "2015-06-01", "2018-08-01", 1 },
                    { 2, "City Medical Center", "Nurse Practitioner", "Assisted pediatricians in diagnosing and treating young patients, including performing physical exams and writing prescriptions.", "2018-09-01", "2022-03-01", 1 },

                    // Candidate 2 - Jane Smith (Doctor, Cardiology)
                    { 3, "Heart Health Clinic", "Cardiologist", "Diagnosed and treated a wide range of heart conditions, including performing diagnostic tests and patient consultations.", "2010-05-01", "2015-09-01", 2 },
                    { 4, "University Hospital", "Cardiology Fellow", "Completed a fellowship in cardiology, specializing in electrophysiology and advanced heart failure management.", "2015-10-01", "2018-06-01", 2 },

                    // Candidate 9 - Jessica Moore (Nurse, Neurology)
                    { 5, "NeuroCare Hospital", "Neurology Nurse", "Assisted in diagnosing and treating neurological disorders, including stroke, epilepsy, and neurodegenerative diseases.", "2015-03-01", "2019-06-01", 3 },
                    { 6, "Brain and Spine Institute", "Neurology Nurse Specialist", "Worked with neurologists in managing patients with complex neurological conditions and coordinating care for patients with brain and spinal injuries.", "2019-07-01", "Present", 3 },

                    // Candidate 10 - Daniel Thomas (Doctor, Dermatology)
                }
            );

            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "Name", "Issuer", "IssueDate", "ExpirationDate", "CandidateId" },
                values: new object[,]
                {   
                    //John Doe
                    { 1, "Registered Nurse License", "California Board of Registered Nursing", "2020-01-15", "2025-01-15", 1 },
                    { 2, "Pediatric Advanced Life Support (PALS)", "American Heart Association", "2021-03-10", "2023-03-10", 1 },
                    //Jane Smith
                    { 3, "Board Certification in Cardiology", "American Board of Internal Medicine", "2018-07-01", null, 2 },
                    { 4, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2019-05-01", "2024-05-01", 2 },
                    //Jessica Moore
                    { 5, "Board Certification in Neurology", "American Board of Psychiatry and Neurology", "2019-06-01", null, 3 },
                    { 6, "Basic Life Support (BLS)", "American Heart Association", "2020-08-01", "2023-08-01", 3 },
                    //Daniel Thomas
                   
                }
            );

            migrationBuilder.InsertData(
               table: "Skills",
               columns: new[] { "Id", "Name", "CandidateId" },
               values: new object[,]
               {
                    // Candidate 1 - John Doe (Nurse, Pediatrics)
                    { 1, "Pediatric Care", 1 },
                    { 2, "Patient Communication", 1 },
                    { 3, "Child Development Knowledge", 1 },

                    // Candidate 2 - Jane Smith (Doctor, Cardiology)
                    { 4, "Cardiac Diagnostics", 2 },
                    { 5, "ECG Interpretation", 2 },
                    { 6, "Interventional Cardiology", 2 },    

                    // Candidate 9 - Jessica Moore (Nurse, Neurology)
                    { 7, "Neurological Assessment", 3 },
                    { 8, "Seizure Management", 3 },
                    { 9, "Neurocritical Care", 3 },

                    // Candidate 10 - Daniel Thomas (Doctor, Dermatology)
                    
               }
            );

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Type", "CandidateId", "JobId" },
                values: new object[,]
                {
                    //{ 1, "John", "Doe", "johndoe@example.com", "123-456-7890", "California", "Nurse", "Pediatrics" },
                    { 1, (int)PositionType.FullTime, 1, 1},
                    { 2, (int)PositionType.PartTime, 1, 1 },
                    { 3, (int)PositionType.Remote, 1, 1 },
                    { 4, (int)PositionType.InOffice, 1, 1 },
                    //{ 2, "Jane", "Smith", "janesmith@example.com", "987-654-3210", "Texas", "Doctor", "Cardiology" },
                    { 5, (int)PositionType.PartTime, 2, 2 },
                    { 6, (int)PositionType.Temporary, 2, 2 },
                    { 7, (int)PositionType.Permanent, 2, 2 },
                    { 8, (int)PositionType.InOffice, 2, 2 },
                    //{ 3, "Emily", "Johnson", "emilyj@example.com", "234-567-8901", "New York", "Nurse", "Emergency Medicine" },
                    { 9, (int)PositionType.FullTime, 3, 3 },
                    { 10, (int)PositionType.Permanent, 3, 3 },
                    { 11, (int)PositionType.Remote, 3, 3 },
                    { 12, (int)PositionType.InOffice, 3, 3 },                                     
                }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompensationAndBenefits", "Duration", "Requirements", "Responsibilities", "Schedule" },
                values: new object[] { null, null, null, null, "Full-time" });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CompensationAndBenefits", "Description", "Duration", "Requirements", "Responsibilities", "Schedule" },
                values: new object[] { null, "Perform heart surgeries.", null, null, null, "Full-time" });
        }
    }
}
