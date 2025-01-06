using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RecruiterWorkflow.Models;

#nullable disable

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
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
                    { 3, "Emily", "Johnson", "emilyj@example.com", "234-567-8901", "New York", "Nurse", "Emergency Medicine" },
                    { 4, "Michael", "Brown", "michaelb@example.com", "345-678-9012", "Florida", "Doctor", "Orthopedics" },
                    { 5, "Sarah", "Davis", "sarahd@example.com", "456-789-0123", "Illinois", "Nurse", "Oncology" },
                    { 6, "David", "Wilson", "davidw@example.com", "567-890-1234", "Georgia", "Doctor", "General Surgery" },
                    { 7, "Laura", "Taylor", "laurat@example.com", "678-901-2345", "North Carolina", "Nurse", "Critical Care" },
                    { 8, "James", "Anderson", "jamesa@example.com", "789-012-3456", "Ohio", "Doctor", "Pulmonology" },
                    { 9, "Jessica", "Moore", "jessicam@example.com", "890-123-4567", "Pennsylvania", "Nurse", "Neurology" },
                    { 10, "Daniel", "Thomas", "danielt@example.com", "901-234-5678", "Michigan", "Doctor", "Dermatology" },
                    { 11, "Megan", "Martin", "meganm@example.com", "012-345-6789", "Virginia", "Nurse", "Labor and Delivery" },
                    { 12, "Joshua", "Lee", "joshual@example.com", "123-456-7891", "Washington", "Doctor", "Radiology" }
                    //{ 13, "Anna", "White", "annaw@example.com", "234-567-8902", "Massachusetts", "Nurse", "Cardiology" },
                    //{ 14, "Christopher", "Harris", "chrish@example.com", "345-678-9013", "Arizona", "Doctor", "Pediatrics" },
                    //{ 15, "Olivia", "Clark", "oliviac@example.com", "456-789-0124", "Colorado", "Nurse", "Geriatrics" },
                    //{ 16, "Matthew", "Rodriguez", "mattr@example.com", "567-890-1235", "Oregon", "Doctor", "Anesthesiology" },
                    //{ 17, "Sophia", "Lewis", "sophial@example.com", "678-901-2346", "Indiana", "Nurse", "Rehabilitation" },
                    //{ 18, "Andrew", "Walker", "andreww@example.com", "789-012-3457", "Tennessee", "Doctor", "Psychiatry" },
                    //{ 19, "Isabella", "Hall", "isabellah@example.com", "890-123-4568", "Kentucky", "Nurse", "Primary Care" },
                    //{ 20, "Ryan", "Allen", "ryana@example.com", "901-234-5679", "Missouri", "Doctor", "Endocrinology" },
                    //{ 21, "Grace", "Young", "gracey@example.com", "012-345-6780", "Minnesota", "Nurse", "Nephrology" },
                    //{ 22, "Benjamin", "King", "benjamink@example.com", "123-456-7892", "Alabama", "Doctor", "Ophthalmology" },
                    //{ 23, "Chloe", "Wright", "chloew@example.com", "234-567-8903", "Nevada", "Nurse", "ICU" },
                    //{ 24, "Ethan", "Lopez", "ethanl@example.com", "345-678-9014", "Wisconsin", "Doctor", "Hematology" }
                    //{ 25, "Abigail", "Hill", "abigailh@example.com", "456-789-0125", "Oklahoma", "Nurse", "Family Medicine" },
                    //{ 26, "Alexander", "Scott", "alexanders@example.com", "567-890-1236", "Arkansas", "Doctor", "Rheumatology" },
                    //{ 27, "Victoria", "Green", "victoriag@example.com", "678-901-2347", "Louisiana", "Nurse", "Trauma" },
                    //{ 28, "Tyler", "Adams", "tylera@example.com", "789-012-3458", "Kansas", "Doctor", "Neurosurgery" },
                    //{ 29, "Ella", "Baker", "ellab@example.com", "890-123-4569", "South Carolina", "Nurse", "Wound Care" },
                    //{ 30, "Nathan", "Carter", "nathanc@example.com", "901-234-5670", "Iowa", "Doctor", "Gastroenterology" },
                    //{ 31, "Samantha", "Mitchell", "samantham@example.com", "012-345-6781", "Mississippi", "Nurse", "Emergency Medicine" },
                    //{ 32, "Dylan", "Perez", "dylanp@example.com", "123-456-7893", "New Mexico", "Doctor", "Infectious Diseases" },
                    //{ 33, "Madison", "Roberts", "madisonr@example.com", "234-567-8904", "Connecticut", "Nurse", "Oncology" },
                    //{ 34, "Elijah", "Turner", "elijaht@example.com", "345-678-9015", "Utah", "Doctor", "Pulmonology" },
                    //{ 35, "Ava", "Phillips", "avap@example.com", "456-789-0126", "Nebraska", "Nurse", "Critical Care" },
                    //{ 36, "Lucas", "Campbell", "lucasc@example.com", "567-890-1237", "Montana", "Doctor", "Cardiology" },
                    //{ 37, "Natalie", "Parker", "nataliep@example.com", "678-901-2348", "West Virginia", "Nurse", "Labor and Delivery" },
                    //{ 38, "Logan", "Evans", "logane@example.com", "789-012-3459", "Hawaii", "Doctor", "Orthopedics" },
                    //{ 39, "Ella", "Edwards", "ellae@example.com", "890-123-4570", "Idaho", "Nurse", "Neurology" },
                    //{ 40, "Anthony", "Collins", "anthonyc@example.com", "901-234-5671", "Alaska", "Doctor", "Psychiatry" },
                    //{ 41, "Mia", "Stewart", "mias@example.com", "012-345-6782", "Maine", "Nurse", "ICU" },
                    //{ 42, "Christian", "Sanchez", "christians@example.com", "123-456-7894", "Vermont", "Doctor", "Pediatrics" },
                    //{ 43, "Savannah", "Morris", "savannahm@example.com", "234-567-8905", "Wyoming", "Nurse", "Rehabilitation" },
                    //{ 44, "Jacob", "Rogers", "jacobr@example.com", "345-678-9016", "South Dakota", "Doctor", "Dermatology" },
                    //{ 45, "Hannah", "Reed", "hannah@example.com", "456-789-0127", "North Dakota", "Nurse", "Nephrology" },
                    //{ 46, "Liam", "Cook", "liamc@example.com", "567-890-1238", "Rhode Island", "Doctor", "Radiology" },
                    //{ 47, "Ella", "Morgan", "ellam@example.com", "678-901-2349", "Delaware", "Nurse", "Primary Care" },
                    //{ 48, "Owen", "Bell", "owenb@example.com", "789-012-3460", "Montana", "Doctor", "General Surgery" },
                    //{ 49, "Charlotte", "Murphy", "charlottem@example.com", "890-123-4571", "Alabama", "Nurse", "Oncology" },
                    //{ 50, "Luke", "Hughes", "lukeh@example.com", "901-234-5672", "Texas", "Doctor", "Endocrinology" }
                }
            );


            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Name", "Address", "Email" },
                values: new object[,]
                {
                    { 1, "City Health Clinic", "123 Main St, Cityville, California", "contact@cityhealth.com" },
                    { 2, "Suburb Care Center", "456 Elm St, Suburbia, Texas", "info@suburbcare.com" },
                    { 3, "Downtown Wellness Center", "789 Oak St, Metropolis, New York", "hello@downtownwellness.com" },
                    { 4, "Countryside Medical Clinic", "101 Maple Ave, Countryside, Kansas", "support@countrysidemedical.com" },
                    { 5, "Lakeside Family Health", "202 Lakeview Blvd, Lakeside, Michigan", "contact@lakesidehealth.com" },
                    { 6, "North Valley Health Clinic", "303 Pine St, Valleyview, Arizona", "contact@northvalleyhealth.com" },
                    { 7, "Eastside Specialty Clinic", "404 Birch Ave, Easttown, Ohio", "info@eastsideclinic.com" },
                    { 8, "Sunrise Medical Center", "505 Cedar Ln, Sunnyside, Florida", "hello@sunrisemedical.com" },
                    { 9, "Greenfield Health Hub", "606 Oak Rd, Greenfield, Illinois", "support@greenfieldhealth.com" },
                    { 10, "Riverbend Health Clinic", "707 River Rd, Rivertown, Oregon", "contact@riverbendhealth.com" },
                    { 11, "West Coast Family Care", "808 Coastal Blvd, Seaside, California", "info@westcoastfamily.com" },
                    { 12, "Mountainview Medical Clinic", "909 Alpine Dr, Mountainview, Colorado", "contact@mountainviewmed.com" },
                    { 13, "Pine Ridge Healthcare", "1001 Pine Hill Rd, Pine Ridge, South Dakota", "info@pineridgehealth.com" },
                    { 14, "Silverlake Healthcare Center", "1102 Lakeshore Dr, Silverlake, Washington", "hello@silverlakehealth.com" },
                    { 15, "Clearwater Family Health", "1203 Riverstone Rd, Clearwater, Texas", "support@clearwaterhealth.com" }
                }
            );

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Title", "Description", "Duration", "Responsibilities", "Requirements", "CompensationAndBenefits", "ClinicId" },
                values: new object[,]
                {
                    // Jobs for Clinic 1
                    { 1, "Registered Nurse", "Provide patient care in pediatrics.", "1 year contract", "Administer medications, monitor patient recovery, and maintain accurate records.", "Valid nursing license, 2+ years of pediatric experience.", "Competitive salary, health insurance, and retirement benefits.", 1 },
                    { 2, "Physician Assistant", "Assist doctors in patient diagnosis and treatment.", "2 year contract", "Examine patients, interpret test results, and prescribe treatments under supervision.", "PA certification, strong interpersonal skills.", "Healthcare benefits, retirement plans, and continuing education support.", 1 },
                    { 3, "Lab Technician", "Perform diagnostic tests and maintain lab equipment.", "1 year contract", "Analyze samples, ensure test accuracy, and maintain records.", "Certification in medical technology, attention to detail.", "Competitive hourly wage, flexible schedule.", 1 },
                    { 4, "Medical Receptionist", "Manage patient check-in and scheduling.", "Full-time", "Greeting patients, handling inquiries, and managing appointment schedules.", "Excellent communication skills and organizational abilities.", "Hourly pay, paid time off, and training.", 1 },

                    // Jobs for Clinic 2
                    { 5, "Cardiologist", "Perform heart surgeries and manage patients with cardiovascular diseases.", "Permanent position", "Conduct diagnostic tests, perform surgeries, and provide treatment plans for cardiovascular health.", "Board certification in cardiology, valid medical license, and 5+ years of experience.", "High salary, malpractice insurance, healthcare benefits, and professional development opportunities.", 2 },
                    { 6, "Medical Receptionist", "Manage patient check-in and scheduling.", "Full-time position", "Schedule appointments, handle inquiries, and manage patient records.", "Strong organizational skills, proficiency in medical software.", "Hourly pay, paid time off, and training opportunities.", 2 },
                    { 7, "Family Medicine Physician", "Provide comprehensive care to patients of all ages.", "Permanent position", "Diagnose and treat illnesses, manage chronic conditions, and promote wellness.", "Board certification in family medicine, active medical license.", "Lucrative salary, performance bonuses, relocation assistance.", 2 },

                    // Jobs for Clinic 3
                    { 8, "Physical Therapist", "Assist patients in recovering from injuries and surgeries.", "Contract-based position", "Develop treatment plans and provide physical therapy sessions.", "Physical therapist license, experience with sports rehabilitation preferred.", "Salary plus performance bonuses, health insurance.", 3 },
                    { 9, "Occupational Therapist", "Help patients regain daily life skills.", "1-year renewable contract", "Evaluate patients, implement therapeutic activities, and track progress.", "Certification in occupational therapy, compassionate nature.", "Comprehensive benefits, paid time off.", 3 },
                    { 10, "Dietitian", "Provide dietary consultation and meal planning for patients.", "Part-time position", "Assess nutritional needs, provide counseling, and adjust meal plans.", "Bachelor’s in nutrition, RD certification.", "Hourly rate, healthcare benefits, and flexible schedule.", 3 },
                    { 11, "Speech Therapist", "Assist patients in improving communication skills.", "Full-time position", "Provide therapy for speech and language disorders.", "Master's degree in speech therapy, certification.", "Competitive salary, healthcare coverage.", 3 },

                    // Jobs for Clinic 4
                    { 12, "Emergency Medicine Physician", "Treat patients in emergency situations.", "Full-time position", "Stabilize patients, diagnose conditions, and manage acute care.", "Board certification in emergency medicine.", "High compensation, insurance coverage, CME allowances.", 4 },
                    { 13, "Radiologist", "Interpret medical imaging and diagnose conditions.", "Permanent position", "Review X-rays, MRIs, and CT scans; provide reports to physicians.", "Board certification in radiology, attention to detail.", "Malpractice insurance, vacation time.", 4 },
                    { 14, "Pediatrician", "Provide healthcare for children and adolescents.", "Permanent position", "Diagnose and treat childhood illnesses, provide vaccinations, and advise parents.", "Board-certified in pediatrics, excellent communication skills.", "Attractive salary, loan repayment assistance.", 4 },
                    { 15, "Oncology Nurse", "Provide care for patients with cancer.", "Contract position", "Administer chemotherapy, monitor symptoms, and provide support.", "Nursing license, oncology experience preferred.", "Competitive hourly rate, health insurance.", 4 },

                    // Jobs for Clinic 5
                    { 16, "Mental Health Counselor", "Provide support for mental health patients.", "2-year contract", "Conduct therapy sessions, create treatment plans, and assess progress.", "Master's in psychology, LPC or LMHC certification.", "Competitive hourly rate, continuing education opportunities.", 5 },
                    { 17, "Dermatologist", "Diagnose and treat skin conditions.", "Permanent position", "Perform exams, prescribe treatments, and conduct minor procedures.", "Board-certified in dermatology, valid state license.", "High salary, professional growth opportunities.", 5 },
                    { 18, "Pharmacist", "Dispense medications and counsel patients.", "Full-time position", "Verify prescriptions, educate patients, and manage inventory.", "PharmD degree, state licensure.", "Comprehensive benefits, signing bonus.", 5 },

                    // New Jobs for Clinic 6
                    { 19, "Orthopedic Surgeon", "Treat musculoskeletal injuries and disorders.", "Full-time position", "Perform surgeries and manage non-surgical treatments.", "Board certification in orthopedic surgery.", "Competitive salary, health insurance, relocation assistance.", 6 },
                    { 20, "Nephrologist", "Treat kidney-related diseases.", "Full-time position", "Provide dialysis treatments and kidney disease management.", "Board certification in nephrology.", "Salary plus health benefits, paid time off.", 6 },

                    // New Jobs for Clinic 7
                    { 21, "Gastroenterologist", "Treat digestive system conditions.", "Full-time position", "Perform endoscopic procedures, diagnose gastrointestinal disorders.", "Board certification in gastroenterology.", "Excellent salary, malpractice coverage, and health benefits.", 7 },
                    { 22, "Surgeon", "Perform surgeries to treat injuries and diseases.", "Permanent position", "Conduct surgical operations and manage post-operative care.", "Board certification in surgery, 5+ years of experience.", "Competitive salary, paid leave, malpractice insurance.", 7 },

                    // New Jobs for Clinic 8
                    { 23, "Endocrinologist", "Treat hormone-related diseases.", "Full-time position", "Provide care for diabetes, thyroid disorders, and other hormone-related conditions.", "Board certification in endocrinology.", "Competitive salary, comprehensive benefits package.", 8 },
                    { 24, "Urologist", "Treat urinary tract and reproductive health disorders.", "Full-time position", "Perform surgeries and manage patients with urological issues.", "Board-certified urologist, excellent diagnostic skills.", "Competitive compensation, healthcare benefits.", 8 },

                    // New Jobs for Clinic 9
                    { 25, "Pediatric Surgeon", "Perform surgeries on children and adolescents.", "Full-time position", "Conduct pediatric surgeries, assist in recovery, and manage post-operative care.", "Board certification in pediatric surgery.", "Salary plus benefits, professional development support.", 9 },
                    { 26, "Plastic Surgeon", "Perform reconstructive and aesthetic surgeries.", "Permanent position", "Consult with patients, plan surgeries, and execute aesthetic and reconstructive procedures.", "Board certification in plastic surgery, aesthetic surgical experience.", "High salary, professional growth opportunities, relocation assistance.", 9 },

                    // New Jobs for Clinic 10
                    { 27, "General Surgeon", "Perform surgeries to treat a variety of conditions.", "Permanent position", "Conduct surgeries for general conditions such as appendicitis, gallbladder removal, etc.", "Board certification in general surgery, 3+ years of experience.", "Competitive salary, health insurance, paid leave.", 10 },
                    { 28, "Pathologist", "Analyze tissue and fluid samples to diagnose diseases.", "Full-time position", "Examine lab results, interpret findings, and collaborate with physicians.", "Board certification in pathology, strong attention to detail.", "Comprehensive health benefits, retirement plan.", 10 },

                    // New Jobs for Clinic 11
                    { 29, "Infectious Disease Specialist", "Treat infectious diseases and provide preventive care.", "Permanent position", "Diagnose and treat infectious diseases, manage disease prevention protocols.", "Board certification in infectious disease, expertise in epidemiology.", "Competitive salary, comprehensive benefits, CME support.", 11 },
                    { 30, "Rheumatologist", "Diagnose and treat arthritis and autoimmune diseases.", "Full-time position", "Evaluate, diagnose, and treat conditions affecting joints, muscles, and bones.", "Board certification in rheumatology, experience in autoimmune disease management.", "Competitive salary, health insurance, retirement plan.", 11 },

                    // New Jobs for Clinic 12
                    { 31, "Hematologist", "Treat blood disorders like anemia, hemophilia, and leukemia.", "Full-time position", "Provide diagnoses, treatment plans, and manage blood disease treatments.", "Board certification in hematology, strong communication skills.", "Generous salary, health insurance, malpractice coverage.", 12 },
                    { 32, "Allergist", "Treat allergies and immune system disorders.", "Permanent position", "Conduct allergy tests, recommend treatment plans, and manage patient care.", "Board certification in allergy and immunology.", "Competitive salary, paid time off, continuing education support.", 12 },

                    // New Jobs for Clinic 13
                    { 33, "Pulmonologist", "Treat respiratory conditions like asthma, COPD, and pneumonia.", "Full-time position", "Provide diagnostics and manage treatments for lung diseases.", "Board certification in pulmonology, extensive respiratory care knowledge.", "Health insurance, paid vacation, competitive salary.", 13 },
                    { 34, "Sleep Medicine Specialist", "Diagnose and treat sleep disorders.", "Full-time position", "Conduct sleep studies and manage treatments for disorders such as insomnia and sleep apnea.", "Board certification in sleep medicine, experience with sleep diagnostics.", "Competitive salary, health benefits, relocation assistance.", 13 },

                    // New Jobs for Clinic 14
                    { 35, "Neuropsychologist", "Diagnose and treat cognitive and neurological disorders.", "Permanent position", "Assess cognitive function, diagnose neurological conditions, and provide therapeutic support.", "PhD in psychology, experience with neuropsychological testing.", "Salary plus performance bonuses, healthcare benefits.", 14 },
                    { 36, "Neurologist", "Treat neurological disorders such as migraines, epilepsy, and multiple sclerosis.", "Full-time position", "Diagnose and treat neurological conditions, oversee patient treatment plans.", "Board certification in neurology, strong diagnostic skills.", "Competitive salary, health insurance, continuing education opportunities.", 14 },

                    // New Jobs for Clinic 15
                    { 37, "Geriatrician", "Specialize in the care of elderly patients.", "Permanent position", "Provide healthcare for aging patients, manage chronic conditions, and advise families.", "Board certification in geriatrics, experience in elderly care.", "Salary plus benefits, paid time off.", 15 },
                    { 38, "Psychiatrist", "Diagnose and treat mental health disorders.", "Full-time position", "Provide therapy, prescribe medications, and manage mental health conditions.", "Board certification in psychiatry, experience in mental health treatment.", "Generous salary, health benefits, professional development support.", 15 },

                }
            );


            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Employer", "Role", "Description", "Start", "End", "CandidateId" },
                values: new object[,]
                {
                    // Candidate 1 - John Doe (Nurse, Pediatrics)
                    { 1, "St. Mary's Hospital", "Pediatric Nurse", "Cared for pediatric patients in a busy hospital ward, administering medications and providing post-surgical care.", "2015-06-01", "2018-08-01", 1 },
                    { 2, "City Medical Center", "Nurse Practitioner", "Assisted pediatricians in diagnosing and treating young patients, including performing physical exams and writing prescriptions.", "2018-09-01", "2022-03-01", 1 },

                    // Candidate 2 - Jane Smith (Doctor, Cardiology)
                    { 3, "Heart Health Clinic", "Cardiologist", "Diagnosed and treated a wide range of heart conditions, including performing diagnostic tests and patient consultations.", "2010-05-01", "2015-09-01", 2 },
                    { 4, "University Hospital", "Cardiology Fellow", "Completed a fellowship in cardiology, specializing in electrophysiology and advanced heart failure management.", "2015-10-01", "2018-06-01", 2 },

                    // Candidate 3 - Emily Johnson (Nurse, Emergency Medicine)
                    { 5, "City General Hospital", "Emergency Room Nurse", "Provided immediate care in the emergency room, performing triage, administering first aid, and managing critical patient cases.", "2017-07-01", "2020-12-01", 3 },
                    { 6, "Greenwood Medical Center", "Trauma Nurse", "Worked in trauma care, stabilizing patients with severe injuries and coordinating with surgeons and physicians.", "2021-01-01", "Present", 3 },

                    // Candidate 4 - Michael Brown (Doctor, Orthopedics)
                    { 7, "Orthopedic Institute", "Orthopedic Surgeon", "Specialized in diagnosing and treating musculoskeletal conditions, including performing surgeries and managing post-operative recovery.", "2012-04-01", "2016-11-01", 4 },
                    { 8, "Sunshine General Hospital", "Orthopedic Consultant", "Provided expert consultations on complex orthopedic cases, including joint replacement and fracture management.", "2017-01-01", "Present", 4 },

                    // Candidate 5 - Sarah Davis (Nurse, Oncology)
                    { 9, "Hope Cancer Center", "Oncology Nurse", "Administered chemotherapy and supported patients through their cancer treatment journey, providing emotional and medical care.", "2016-02-01", "2019-06-01", 5 },
                    { 10, "St. John’s Hospital", "Oncology Nurse Specialist", "Provided specialized care for cancer patients, assisting in clinical trials and educating patients about treatment options.", "2019-07-01", "Present", 5 },

                    // Candidate 6 - David Wilson (Doctor, General Surgery)
                    { 11, "Wellness Surgical Center", "General Surgeon", "Performed surgeries on patients with a variety of conditions, focusing on gastrointestinal and abdominal surgeries.", "2011-03-01", "2016-08-01", 6 },
                    { 12, "City Medical Group", "Surgical Consultant", "Consulted on complex surgical cases, providing recommendations for treatment and surgical planning.", "2016-09-01", "Present", 6 },

                    // Candidate 7 - Laura Taylor (Nurse, Critical Care)
                    { 13, "Emergency Care Hospital", "ICU Nurse", "Provided life-saving care to critically ill patients, managing mechanical ventilation and monitoring vital signs.", "2014-05-01", "2018-03-01", 7 },
                    { 14, "Northern Health System", "Critical Care Nurse", "Assisted in managing and treating patients in intensive care units, including handling complex ventilator settings and monitoring multi-organ systems.", "2018-04-01", "Present", 7 },

                    // Candidate 8 - James Anderson (Doctor, Pulmonology)
                    { 15, "Lung Health Clinic", "Pulmonologist", "Diagnosed and treated respiratory diseases such as COPD, asthma, and lung cancer. Conducted pulmonary function tests and assisted in patient management.", "2013-02-01", "2017-09-01", 8 },
                    { 16, "State University Hospital", "Pulmonology Fellow", "Completed a pulmonology fellowship, specializing in advanced respiratory therapies and sleep disorders.", "2017-10-01", "2020-06-01", 8 },

                    // Candidate 9 - Jessica Moore (Nurse, Neurology)
                    { 17, "NeuroCare Hospital", "Neurology Nurse", "Assisted in diagnosing and treating neurological disorders, including stroke, epilepsy, and neurodegenerative diseases.", "2015-03-01", "2019-06-01", 9 },
                    { 18, "Brain and Spine Institute", "Neurology Nurse Specialist", "Worked with neurologists in managing patients with complex neurological conditions and coordinating care for patients with brain and spinal injuries.", "2019-07-01", "Present", 9 },

                    // Candidate 10 - Daniel Thomas (Doctor, Dermatology)
                    { 19, "Skin Health Clinic", "Dermatologist", "Diagnosed and treated skin disorders, including eczema, acne, and melanoma. Performed minor surgeries and cosmetic procedures.", "2014-01-01", "2018-06-01", 10 },
                    { 20, "Cosmetic Dermatology Center", "Cosmetic Dermatologist", "Specialized in cosmetic dermatology, including Botox injections, laser skin treatments, and facial rejuvenation.", "2018-07-01", "Present", 10 },
                    
                    // Candidate 11 - Megan Martin (Nurse, Labor and Delivery)
                    { 21, "St. Joseph's Hospital", "Labor and Delivery Nurse", "Provided care to mothers during childbirth, assisted with labor management, and supported post-delivery recovery.", "2016-01-01", "2019-12-01", 11 },
                    { 22, "Riverbend Medical Center", "Charge Nurse", "Managed labor and delivery units, overseeing staff and ensuring efficient care delivery during high-stress situations.", "2020-01-01", "Present", 11 },

                    // Candidate 12 - Joshua Lee (Doctor, Radiology)
                    { 23, "City Medical Imaging", "Radiologist", "Interpreted medical images to diagnose conditions, including MRIs, CT scans, and X-rays. Consulted with physicians on treatment options.", "2012-04-01", "2017-06-01", 12 },
                    { 24, "State University Hospital", "Radiology Fellow", "Completed fellowship in advanced diagnostic radiology, focusing on interventional radiology and imaging for cancer treatment.", "2017-07-01", "2020-12-01", 12 },

                    //// Candidate 13 - Anna White (Nurse, Cardiology)
                    //{ 25, "HeartCare Clinic", "Cardiology Nurse", "Assisted cardiologists in diagnosing and managing patients with heart diseases, including taking vital signs and conducting ECGs.", "2015-02-01", "2018-09-01", 13 },
                    //{ 26, "Wellness Heart Hospital", "Cardiology Nurse Practitioner", "Specialized in caring for patients with chronic heart conditions, providing education and support for patients undergoing cardiovascular treatment.", "2018-10-01", "Present", 13 },

                    //// Candidate 14 - Christopher Harris (Doctor, Pediatrics)
                    //{ 27, "Children's Health Center", "Pediatrician", "Provided primary healthcare to children, diagnosed illnesses, administered vaccinations, and monitored developmental milestones.", "2011-03-01", "2015-06-01", 14 },
                    //{ 28, "Pediatrics Specialty Clinic", "Pediatrician", "Treated children with specialized needs, including managing chronic conditions and providing consultations on growth and development.", "2015-07-01", "Present", 14 },

                    //// Candidate 15 - Olivia Clark (Nurse, Geriatrics)
                    //{ 29, "Sunset Hills Senior Living", "Geriatric Nurse", "Cared for elderly patients, helping with daily activities and managing long-term health conditions such as dementia and arthritis.", "2014-08-01", "2018-11-01", 15 },
                    //{ 30, "Golden Years Medical Center", "Geriatric Care Coordinator", "Coordinated care plans for elderly patients, ensuring that they received comprehensive healthcare services, including physical therapy and medications.", "2018-12-01", "Present", 15 },

                    //// Candidate 16 - Matthew Rodriguez (Doctor, Anesthesiology)
                    //{ 31, "Anesthesia Associates", "Anesthesiologist", "Administered anesthesia to patients during surgery and monitored vital signs to ensure patient safety and comfort throughout procedures.", "2013-01-01", "2017-05-01", 16 },
                    //{ 32, "Pacific Medical Center", "Anesthesiology Fellow", "Completed fellowship in anesthesia with a focus on pain management and critical care anesthesia for high-risk surgeries.", "2017-06-01", "2020-04-01", 16 },

                    //// Candidate 17 - Sophia Lewis (Nurse, Rehabilitation)
                    //{ 33, "Springfield Rehabilitation Center", "Rehabilitation Nurse", "Assisted patients in recovering from major surgeries and injuries, helping with physical therapy and administering pain relief treatments.", "2015-04-01", "2019-10-01", 17 },
                    //{ 34, "East Valley Health System", "Rehabilitation Nurse Specialist", "Worked with patients recovering from neurological and musculoskeletal injuries, including post-stroke rehabilitation and spinal cord injuries.", "2019-11-01", "Present", 17 },

                    //// Candidate 18 - Andrew Walker (Doctor, Psychiatry)
                    //{ 35, "Mindcare Psychiatry", "Psychiatrist", "Provided psychiatric evaluations and therapy for patients with mood disorders, anxiety, and other mental health issues.", "2011-07-01", "2015-12-01", 18 },
                    //{ 36, "New Horizons Mental Health Center", "Psychiatrist", "Specialized in treating patients with severe mental health disorders, including schizophrenia and bipolar disorder, using both therapy and medication management.", "2016-01-01", "Present", 18 },

                    //// Candidate 19 - Isabella Hall (Nurse, Primary Care)
                    //{ 37, "Family Health Clinic", "Primary Care Nurse", "Provided routine healthcare services, including administering vaccinations, conducting health screenings, and managing chronic disease care.", "2013-05-01", "2017-08-01", 19 },
                    //{ 38, "Good Health Medical Center", "Primary Care Nurse Practitioner", "Managed a broad range of health conditions in a primary care setting, including preventive care and patient education.", "2017-09-01", "Present", 19 },

                    //// Candidate 20 - Ryan Allen (Doctor, Endocrinology)
                    //{ 39, "Endocrine Health Institute", "Endocrinologist", "Diagnosed and treated patients with endocrine disorders, such as diabetes, thyroid diseases, and metabolic conditions.", "2014-01-01", "2018-05-01", 20 },
                    //{ 40, "Metabolism Health Center", "Endocrinology Fellow", "Completed fellowship in advanced endocrinology, focusing on hormone imbalances and disorders related to weight management and fertility.", "2018-06-01", "2021-04-01", 20 },
                    
                    //// Candidate 21 - Grace Young (Nurse, Nephrology)
                    //{ 41, "Minnesota Renal Center", "Nephrology Nurse", "Cared for patients with chronic kidney disease, assisting with dialysis and monitoring kidney function.", "2013-03-01", "2017-02-01", 21 },
                    //{ 42, "Northwest Kidney Care", "Nephrology Nurse Specialist", "Provided specialized care to patients undergoing dialysis and kidney transplants, and educated patients on kidney health.", "2017-03-01", "Present", 21 },

                    //// Candidate 22 - Benjamin King (Doctor, Ophthalmology)
                    //{ 43, "Ophthalmology Associates", "Ophthalmologist", "Diagnosed and treated a wide range of eye conditions, performed cataract surgeries, and provided vision correction treatments.", "2011-06-01", "2015-08-01", 22 },
                    //{ 44, "Eye Care Institute", "Senior Ophthalmologist", "Led the ophthalmology department, specializing in retina diseases and conducting complex surgical procedures for retinal conditions.", "2015-09-01", "Present", 22 },

                    //// Candidate 23 - Chloe Wright (Nurse, ICU)
                    //{ 45, "Nevada ICU Care", "ICU Nurse", "Provided intensive care to critically ill patients, managing ventilation and medication administration in a high-stress environment.", "2014-04-01", "2018-05-01", 23 },
                    //{ 46, "Mountain View ICU", "Critical Care Nurse", "Specialized in caring for patients recovering from surgeries and managing patients with acute conditions such as heart failure and stroke.", "2018-06-01", "Present", 23 },

                    //// Candidate 24 - Ethan Lopez (Doctor, Hematology)
                    //{ 47, "Hematology Clinic of Wisconsin", "Hematologist", "Diagnosed and treated blood disorders such as leukemia, anemia, and clotting disorders, and managed chemotherapy for hematological cancers.", "2012-07-01", "2016-09-01", 24 },
                    //{ 48, "Wisconsin Blood Disorders Center", "Senior Hematologist", "Led a team of hematologists and oncologists to treat patients with complex blood disorders and provided specialized care for bone marrow transplants.", "2016-10-01", "Present", 24 },

                    //// Candidate 25 - Abigail Hill (Nurse, Family Medicine)
                    //{ 49, "Oklahoma Family Health Clinic", "Family Nurse Practitioner", "Provided primary care services to patients of all ages, from routine check-ups to the management of chronic conditions.", "2015-08-01", "2019-10-01", 25 },
                    //{ 50, "Tulsa Family Care", "Registered Nurse", "Assisted with family health screenings, immunizations, and patient education on preventive healthcare practices.", "2019-11-01", "Present", 25 },

                    //// Candidate 26 - Alexander Scott (Doctor, Rheumatology)
                    //{ 51, "Arkansas Rheumatology Clinic", "Rheumatologist", "Diagnosed and treated autoimmune diseases and joint disorders, including rheumatoid arthritis and lupus, and managed long-term treatments.", "2010-03-01", "2014-06-01", 26 },
                    //{ 52, "Central Arkansas Rheumatology", "Senior Rheumatologist", "Specialized in biologic treatments and developed personalized care plans for patients with chronic inflammatory conditions.", "2014-07-01", "Present", 26 },

                    //// Candidate 27 - Victoria Green (Nurse, Trauma)
                    //{ 53, "Louisiana Trauma Center", "Trauma Nurse", "Provided critical care to patients with severe injuries, assisting with trauma surgery and post-operative care in a fast-paced environment.", "2012-01-01", "2016-05-01", 27 },
                    //{ 54, "Baton Rouge Trauma Center", "Emergency Trauma Nurse", "Managed trauma patients in emergency settings, delivering life-saving care during mass casualty incidents and multi-trauma emergencies.", "2016-06-01", "Present", 27 },

                    //// Candidate 28 - Tyler Adams (Doctor, Neurosurgery)
                    //{ 55, "Kansas Neurosurgery Associates", "Neurosurgeon", "Performed complex brain and spine surgeries, including tumor resections, spinal fusion, and deep brain stimulation.", "2010-05-01", "2014-08-01", 28 },
                    //{ 56, "Kansas Brain & Spine Institute", "Senior Neurosurgeon", "Led a team in neurosurgical procedures, specializing in minimally invasive spine surgeries and treatment of neurological disorders.", "2014-09-01", "Present", 28 },

                    //// Candidate 29 - Ella Baker (Nurse, Wound Care)
                    //{ 57, "South Carolina Wound Care Center", "Wound Care Nurse", "Managed care for chronic wounds, burns, and diabetic ulcers, providing wound debridement, dressing changes, and patient education.", "2013-04-01", "2017-06-01", 29 },
                    //{ 58, "Low Country Wound Care", "Advanced Wound Care Nurse", "Specialized in treating complex wound cases, using advanced therapies such as negative pressure wound therapy and hyperbaric oxygen therapy.", "2017-07-01", "Present", 29 },

                    //// Candidate 30 - Nathan Carter (Doctor, Gastroenterology)
                    //{ 59, "Iowa Gastroenterology Clinic", "Gastroenterologist", "Diagnosed and treated digestive disorders such as GERD, IBS, and liver diseases, and performed endoscopies and colonoscopies.", "2011-09-01", "2015-12-01", 30 },
                    //{ 60, "Iowa Digestive Health Center", "Senior Gastroenterologist", "Provided specialized care for patients with chronic gastrointestinal conditions and led initiatives to improve patient outcomes in digestive health.", "2016-01-01", "Present", 30 },

                    //// Candidate 31 - Samantha Mitchell (Nurse, Emergency Medicine)
                    //{ 61, "Mississippi Emergency Department", "Emergency Room Nurse", "Provided immediate care to patients in the emergency department, including trauma and cardiac emergencies, and collaborated with medical teams for fast interventions.", "2011-04-01", "2015-06-01", 31 },
                    //{ 62, "Jackson Memorial Hospital", "Lead Emergency Nurse", "Led emergency response teams, ensuring prompt treatment of patients with life-threatening injuries, and developed best practices for triage procedures.", "2015-07-01", "Present", 31 },

                    //// Candidate 32 - Dylan Perez (Doctor, Infectious Diseases)
                    //{ 63, "New Mexico Infectious Disease Center", "Infectious Disease Specialist", "Diagnosed and treated infectious diseases, including HIV, tuberculosis, and viral infections, and developed infection control protocols for healthcare facilities.", "2013-05-01", "2017-08-01", 32 },
                    //{ 64, "Santa Fe Medical Group", "Senior Infectious Disease Doctor", "Led efforts in controlling outbreaks of infectious diseases, and educated the public on prevention and treatment of contagious illnesses.", "2017-09-01", "Present", 32 },

                    //// Candidate 33 - Madison Roberts (Nurse, Oncology)
                    //{ 65, "Connecticut Cancer Center", "Oncology Nurse", "Provided chemotherapy and supportive care to cancer patients, monitored treatment side effects, and educated patients about their treatment plans.", "2012-07-01", "2016-08-01", 33 },
                    //{ 66, "Hartford Oncology Clinic", "Lead Oncology Nurse", "Led the oncology nursing team, coordinated care for patients with advanced cancer, and supported clinical trials and new treatments in cancer care.", "2016-09-01", "Present", 33 },

                    //// Candidate 34 - Elijah Turner (Doctor, Pulmonology)
                    //{ 67, "Utah Pulmonary Associates", "Pulmonologist", "Diagnosed and treated respiratory conditions such as asthma, COPD, and lung infections, and provided critical care to patients with respiratory failure.", "2012-11-01", "2016-02-01", 34 },
                    //{ 68, "Salt Lake City Respiratory Care", "Senior Pulmonologist", "Developed treatment plans for patients with chronic respiratory conditions, specializing in advanced pulmonary treatments and lung disease management.", "2016-03-01", "Present", 34 },

                    //// Candidate 35 - Ava Phillips (Nurse, Critical Care)
                    //{ 69, "Nebraska Critical Care Unit", "Critical Care Nurse", "Provided intensive care for patients with life-threatening illnesses, assisted in advanced life support procedures, and managed patient ventilation and sedation.", "2014-02-01", "2018-05-01", 35 },
                    //{ 70, "Omaha ICU", "Lead ICU Nurse", "Led ICU teams in managing critically ill patients, trained new staff, and helped develop guidelines for managing acute critical care scenarios.", "2018-06-01", "Present", 35 },

                    //// Candidate 36 - Lucas Campbell (Doctor, Cardiology)
                    //{ 71, "Montana Cardiology Associates", "Cardiologist", "Diagnosed and treated cardiovascular diseases such as hypertension, heart failure, and arrhythmias, and performed heart catheterizations and other diagnostic procedures.", "2011-03-01", "2015-05-01", 36 },
                    //{ 72, "Billings Heart Center", "Senior Cardiologist", "Led a team of cardiologists in developing treatment plans for patients with chronic heart conditions and managed heart surgery patients during recovery.", "2015-06-01", "Present", 36 },

                    //// Candidate 37 - Natalie Parker (Nurse, Labor and Delivery)
                    //{ 73, "West Virginia Birth Center", "Labor and Delivery Nurse", "Assisted with the delivery of babies, provided prenatal and postnatal care, and educated new parents on baby care and breastfeeding.", "2013-05-01", "2017-08-01", 37 },
                    //{ 74, "Charleston Women's Health", "Lead Labor and Delivery Nurse", "Coordinated care in labor and delivery, managed patient flow, and provided emotional support for mothers and families throughout the birthing process.", "2017-09-01", "Present", 37 },

                    //// Candidate 38 - Logan Evans (Doctor, Orthopedics)
                    //{ 75, "Hawaii Orthopedic Clinic", "Orthopedic Surgeon", "Specialized in diagnosing and treating musculoskeletal conditions, performed joint replacements, sports injuries surgeries, and provided post-surgical rehabilitation.", "2010-02-01", "2014-03-01", 38 },
                    //{ 76, "Honolulu Sports Medicine", "Senior Orthopedic Surgeon", "Led a team of surgeons in treating complex orthopedic cases, focusing on knee and hip replacements, and advanced arthroscopic techniques.", "2014-04-01", "Present", 38 },

                    //// Candidate 39 - Ella Edwards (Nurse, Neurology)
                    //{ 77, "Idaho Neurological Center", "Neurology Nurse", "Cared for patients with neurological disorders, including epilepsy, multiple sclerosis, and Parkinson's disease, and supported neurosurgeons during surgeries.", "2012-01-01", "2016-02-01", 39 },
                    //{ 78, "Boise Brain and Spine Institute", "Lead Neurology Nurse", "Managed a team of nurses specializing in neurological care, developed treatment protocols, and coordinated patient education on neurological conditions.", "2016-03-01", "Present", 39 },

                    //// Candidate 40 - Anthony Collins (Doctor, Psychiatry)
                    //{ 79, "Alaska Psychiatric Institute", "Psychiatrist", "Diagnosed and treated a variety of psychiatric disorders, including depression, anxiety, and schizophrenia, and provided individual and group therapy.", "2011-04-01", "2015-06-01", 40 },
                    //{ 80, "Anchorage Mental Health Center", "Senior Psychiatrist", "Led mental health programs and provided specialized care for patients with severe psychiatric disorders, focusing on long-term treatment and recovery.", "2015-07-01", "Present", 40 },
                
                    //// Candidate 41 - Mia Stewart (Nurse, ICU)
                    //{ 81, "Maine Medical Center", "ICU Nurse", "Provided critical care to patients in the intensive care unit, monitored vital signs, and assisted in emergency interventions for patients in life-threatening conditions.", "2012-06-01", "2016-08-01", 41 },
                    //{ 82, "Portland Hospital", "Lead ICU Nurse", "Led a team of ICU nurses, managed patient care in high-stress environments, and worked closely with physicians to ensure timely interventions.", "2016-09-01", "Present", 41 },

                    //// Candidate 42 - Christian Sanchez (Doctor, Pediatrics)
                    //{ 83, "Vermont Children's Hospital", "Pediatrician", "Diagnosed and treated children with various illnesses, provided preventive care, and educated parents on childhood development and disease prevention.", "2013-01-01", "2017-02-01", 42 },
                    //{ 84, "Burlington Pediatric Clinic", "Senior Pediatrician", "Led pediatric care teams, managed complex pediatric conditions, and ensured age-appropriate treatment for children from infancy through adolescence.", "2017-03-01", "Present", 42 },

                    //// Candidate 43 - Savannah Morris (Nurse, Rehabilitation)
                    //{ 85, "Wyoming Rehabilitation Center", "Rehabilitation Nurse", "Worked with patients recovering from surgeries or injuries, assisted in physical therapy, and monitored rehabilitation progress for post-operative recovery.", "2014-05-01", "2018-07-01", 43 },
                    //{ 86, "Cheyenne Medical Group", "Lead Rehabilitation Nurse", "Managed rehabilitation care teams, developed individualized patient recovery plans, and collaborated with physical therapists to improve patient mobility and recovery outcomes.", "2018-08-01", "Present", 43 },

                    //// Candidate 44 - Jacob Rogers (Doctor, Dermatology)
                    //{ 87, "South Dakota Skin Clinic", "Dermatologist", "Provided treatment for various skin conditions, including acne, eczema, and skin cancer, and performed dermatologic procedures like biopsies and laser treatments.", "2011-04-01", "2015-06-01", 44 },
                    //{ 88, "Sioux Falls Dermatology", "Senior Dermatologist", "Led a dermatology practice, diagnosing and treating complex skin diseases, and worked with a team to perform cosmetic dermatology procedures.", "2015-07-01", "Present", 44 },

                    //// Candidate 45 - Hannah Reed (Nurse, Nephrology)
                    //{ 89, "North Dakota Kidney Center", "Nephrology Nurse", "Cared for patients with kidney diseases, assisted with dialysis procedures, and educated patients on kidney health and lifestyle modifications for chronic kidney disease.", "2013-06-01", "2017-08-01", 45 },
                    //{ 90, "Fargo Renal Care", "Lead Nephrology Nurse", "Led nephrology care teams in the management of chronic kidney patients, developed care protocols, and supported patients undergoing dialysis and kidney transplant procedures.", "2017-09-01", "Present", 45 },

                    //// Candidate 46 - Liam Cook (Doctor, Radiology)
                    //{ 91, "Rhode Island Imaging Center", "Radiologist", "Interpreted X-rays, MRIs, and CT scans, diagnosed medical conditions through imaging, and collaborated with specialists to develop patient care plans based on imaging results.", "2010-09-01", "2014-11-01", 46 },
                    //{ 92, "Providence Radiology Group", "Senior Radiologist", "Led radiology teams in the interpretation of diagnostic imaging, supervised imaging procedures, and collaborated with oncologists, surgeons, and other specialists for treatment planning.", "2014-12-01", "Present", 46 },

                    //// Candidate 47 - Ella Morgan (Nurse, Primary Care)
                    //{ 93, "Delaware Family Practice", "Primary Care Nurse", "Provided care to patients in a primary care setting, performed routine health screenings, assisted with physical exams, and educated patients on general health maintenance.", "2014-03-01", "2018-04-01", 47 },
                    //{ 94, "Wilmington Health Center", "Lead Primary Care Nurse", "Managed patient care in a primary care clinic, coordinated patient follow-ups, and ensured patients received preventive care, including vaccinations and health assessments.", "2018-05-01", "Present", 47 },

                    //// Candidate 48 - Owen Bell (Doctor, General Surgery)
                    //{ 95, "Montana General Surgical Group", "General Surgeon", "Performed general surgical procedures such as appendectomies, hernia repairs, and cholecystectomies, and managed pre-operative and post-operative care for patients.", "2011-03-01", "2015-06-01", 48 },
                    //{ 96, "Billings Surgery Center", "Senior General Surgeon", "Led a team of surgeons, performed complex surgical operations, and mentored medical residents and junior surgeons in advanced surgical techniques.", "2015-07-01", "Present", 48 },

                    //// Candidate 49 - Charlotte Murphy (Nurse, Oncology)
                    //{ 97, "Alabama Cancer Center", "Oncology Nurse", "Provided chemotherapy treatments, pain management, and palliative care to cancer patients, and supported patients and their families throughout the treatment journey.", "2012-02-01", "2016-05-01", 49 },
                    //{ 98, "Birmingham Oncology Clinic", "Lead Oncology Nurse", "Managed oncology nursing teams, coordinated patient care for chemotherapy and radiation treatments, and developed support programs for cancer patients.", "2016-06-01", "Present", 49 },

                    //// Candidate 50 - Luke Hughes (Doctor, Endocrinology)
                    //{ 99, "Texas Endocrine Center", "Endocrinologist", "Diagnosed and treated endocrine disorders such as diabetes, thyroid conditions, and hormone imbalances, and provided patient education on lifestyle changes and medication management.", "2012-08-01", "2016-09-01", 50 },
                    //{ 100, "Dallas Hormone Center", "Senior Endocrinologist", "Led the diagnosis and treatment of complex endocrine disorders, supervised a team of endocrinologists, and worked with patients to develop personalized care plans.", "2016-10-01", "Present", 50 }
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
                    //Emily Johnson
                    { 5, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2020-03-15", "2025-03-15", 3 },
                    { 6, "Trauma Nursing Core Course (TNCC)", "Emergency Nurses Association", "2021-06-10", "2024-06-10", 3 },
                    //Michael Brown
                    { 7, "Board Certification in Orthopedic Surgery", "American Board of Orthopaedic Surgery", "2019-08-01", null, 4 },
                    { 8, "Basic Life Support (BLS)", "American Heart Association", "2020-06-01", "2023-06-01", 4 },
                    //Sarah Davis
                    { 9, "Oncology Nursing Certification", "Oncology Nursing Certification Corporation", "2020-09-15", "2023-09-15", 5 },
                    { 10, "Chemotherapy Biotherapy Certification", "Oncology Nursing Society", "2021-11-01", "2024-11-01", 5 },
                    //David Wilson
                    { 11, "Board Certification in Cardiology", "American Board of Internal Medicine", "2018-07-01", null, 6 },
                    { 12, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2019-05-01", "2024-05-01", 6 },
                    //Laura Taylor
                    { 13, "Board Certification in General Surgery", "American Board of Surgery", "2019-04-01", null, 7 },
                    { 14, "Basic Life Support (BLS)", "American Heart Association", "2020-06-01", "2023-06-01", 7 },
                    //James Anderson
                    { 15, "Board Certification in Pulmonology", "American Board of Internal Medicine", "2019-01-01", null, 8 },
                    { 16, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2020-03-01", "2025-03-01", 8 },
                    //Jessica Moore
                    { 17, "Board Certification in Neurology", "American Board of Psychiatry and Neurology", "2019-06-01", null, 9 },
                    { 18, "Basic Life Support (BLS)", "American Heart Association", "2020-08-01", "2023-08-01", 9 },
                    //Daniel Thomas
                    { 19, "Board Certification in Dermatology", "American Board of Dermatology", "2020-05-01", null, 10 },
                    { 20, "Basic Life Support (BLS)", "American Heart Association", "2021-01-01", "2024-01-01", 10 },
                    //Megan Martinez
                    { 21, "Registered Nurse License", "California Board of Registered Nursing", "2020-10-01", "2025-10-01", 11 },
                    { 22, "Pediatric Advanced Life Support (PALS)", "American Heart Association", "2021-12-01", "2023-12-01", 11 },
                    //Joshua Lee
                    { 23, "Board Certification in Radiology", "American Board of Radiology", "2019-02-01", null, 12 },
                    { 24, "Basic Life Support (BLS)", "American Heart Association", "2020-04-01", "2023-04-01", 12 },
                    //Anna White
                    //{ 25, "Board Certification in Cardiology", "American Board of Internal Medicine", "2018-07-01", null, 13 },
                    //{ 26, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2019-05-01", "2024-05-01", 13 },
                    ////Christopher Harris
                    //{ 27, "Board Certification in Pediatrics", "American Board of Pediatrics", "2019-06-01", null, 14 },
                    //{ 28, "Basic Life Support (BLS)", "American Heart Association", "2020-08-01", "2023-08-01", 14 },
                    ////Olivia Clark
                    //{ 29, "Board Certification in Geriatrics", "American Board of Internal Medicine", "2020-01-01", null, 15 },
                    //{ 30, "Basic Life Support (BLS)", "American Heart Association", "2021-03-01", "2024-03-01", 15 },
                    ////Matthew Rodriguez
                    //{ 31, "Board Certification in Anesthesiology", "American Board of Anesthesiology", "2019-07-01", null, 16 },
                    //{ 32, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2020-09-01", "2025-09-01", 16 },
                    ////Sophia Lewis
                    //{ 33, "Board Certification in Rehabilitation Nursing", "Rehabilitation Nursing Certification Board", "2020-12-01", null, 17 },
                    //{ 34, "Basic Life Support (BLS)", "American Heart Association", "2021-03-01", "2024-03-01", 17 },
                    ////Andrew Walker
                    //{ 35, "Board Certification in Psychiatry", "American Board of Psychiatry and Neurology (ABPN)", "2017-09-01", null, 18 },
                    //{ 36, "Certification in Cognitive Behavioral Therapy (CBT)", "Academy of Cognitive Therapy", "2019-05-15", "2024-05-15", 18 },
                    ////Isabella Hall
                    //{ 37, "Registered Nurse License", "Kentucky Board of Nursing", "2018-06-01", "2023-06-01", 19 },
                    //{ 38, "Certified Primary Care Nurse Practitioner (CPCNP)", "American Academy of Nurse Practitioners Certification Board (AANPCB)", "2020-09-15", "2025-09-15", 19 },
                    ////Ryan Allen
                    //{ 39, "Medical License", "Missouri Board of Healing Arts", "2015-07-10", "2025-07-10", 20 },
                    //{ 40, "Board Certification in Endocrinology, Diabetes, and Metabolism", "American Board of Internal Medicine", "2017-11-01", null, 20 },
                    ////Grace Young
                    //{ 41, "Registered Nurse License", "Minnesota Board of Nursing", "2018-06-15", "2024-06-15", 21 },
                    //{ 42, "Certified Nephrology Nurse (CNN)", "Nephrology Nursing Certification Commission", "2019-09-01", "2024-09-01", 21 },
                    ////Benjamin King
                    //{ 43, "Medical License", "Alabama Board of Medical Examiners", "2016-07-10", "2026-07-10", 22 },
                    //{ 44, "Board Certification in Ophthalmology", "American Board of Ophthalmology", "2018-11-01", null, 22 },
                    ////Chloe Wright
                    //{ 45, "Registered Nurse License", "Nevada State Board of Nursing", "2019-08-15", "2025-08-15", 23 },
                    //{ 46, "Critical Care Registered Nurse (CCRN)", "American Association of Critical-Care Nurses", "2021-05-20", "2024-05-20", 23 },
                    ////Ethan Lopez
                    //{ 47, "Medical License", "Wisconsin Medical Examining Board", "2017-03-10", "2025-03-10", 24 },
                    //{ 48, "Board Certification in Hematology", "American Board of Internal Medicine", "2019-11-01", null, 24 },
                    ////Abigail Hill
                    //{ 49, "Registered Nurse License", "Oklahoma Board of Nursing", "2019-07-01", "2024-07-01", 25 },
                    //{ 50, "Certified Family Nurse Practitioner (CFNP)", "American Academy of Nurse Practitioners Certification Board (AANPCB)", "2020-10-15", "2025-10-15", 25 },
                    ////Alexander Scott
                    //{ 51, "Medical License", "Arkansas State Medical Board", "2016-06-15", "2026-06-15", 26 },
                    //{ 52, "Board Certification in Rheumatology", "American Board of Internal Medicine", "2018-10-20", null, 26 },
                    ////Victoria Green
                    //{ 53, "Trauma Nursing Core Course (TNCC)", "Emergency Nurses Association", "2020-08-10", "2025-08-10", 27 },
                    //{ 54, "Advanced Trauma Care for Nurses (ATCN)", "American College of Surgeons", "2021-04-05", "2026-04-05", 27 },
                    ////Tyler Adams
                    //{ 55, "Board Certification in Neurosurgery", "American Board of Neurological Surgery", "2019-06-15", "2029-06-15", 28 },
                    //{ 56, "Advanced Trauma Life Support (ATLS)", "American College of Surgeons", "2021-03-20", "2026-03-20", 28 },
                    ////Ella Baker
                    //{ 57, "Wound Care Certification", "Wound, Ostomy and Continence Nurses Society (WOCN)", "2020-11-10", "2025-11-10", 29 },
                    //{ 58, "Basic Life Support (BLS)", "American Heart Association", "2022-06-15", "2024-06-15", 29 },
                    ////Nathan Carter
                    //{ 59, "Board Certification in Gastroenterology", "American Board of Internal Medicine", "2019-06-01", "2029-06-01", 30 },
                    //{ 60, "Advanced Cardiac Life Support (ACLS)", "American Heart Association", "2021-08-20", "2024-08-20", 30 },
                    ////Samantha Mitchell
                    //{ 61, "Certified Emergency Nurse (CEN)", "Board of Certification for Emergency Nursing", "2020-05-10", "2025-05-10", 31 },
                    //{ 62, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2021-03-15", "2024-03-15", 31 },
                    ////Dylan Perez
                    //{ 63, "Board Certification in Infectious Disease", "American Board of Internal Medicine", "2019-08-01", "2029-08-01", 32 },
                    //{ 64, "Advanced HIV Care Certification", "American Academy of HIV Medicine", "2020-05-25", "2025-05-25", 32 },
                    ////Madison Roberts
                    //{ 65, "Certified Oncology Nurse (OCN)", "Oncology Nursing Certification Corporation", "2021-06-01", "2026-06-01", 33 },
                    //{ 66, "Chemotherapy and Biotherapy Provider Certificate", "Oncology Nursing Society", "2020-09-15", "2025-09-15", 33 },
                    ////Elijah Turner
                    //{ 67, "Board Certification in Pulmonary Medicine", "American Board of Internal Medicine", "2018-08-01", "2028-08-01", 34 },
                    //{ 68, "Advanced Cardiac Life Support (ACLS)", "American Heart Association", "2020-05-01", "2025-05-01", 34 },
                    ////Ava Phillips
                    //{ 69, "Critical Care Registered Nurse (CCRN)", "American Association of Critical-Care Nurses", "2021-06-01", "2026-06-01", 35 },
                    //{ 70, "Basic Life Support (BLS)", "American Heart Association", "2022-03-15", "2024-03-15", 35 },
                    ////Lucas Campbell
                    //{ 71, "Board Certification in Cardiovascular Disease", "American Board of Internal Medicine", "2018-05-01", null, 36 },
                    //{ 72, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2020-08-15", "2024-08-15", 36 },
                    ////Natalie Parker
                    //{ 73, "Certified Nurse Midwife (CNM)", "American Midwifery Certification Board", "2019-04-10", "2024-04-10", 37 },
                    //{ 74, "Neonatal Resuscitation Program (NRP)", "American Academy of Pediatrics", "2020-09-15", "2023-09-15", 37 },
                    ////Logan Evans
                    //{ 75, "Board Certification in Orthopedic Surgery", "American Board of Orthopaedic Surgery", "2018-06-01", "2028-06-01", 38 },
                    //{ 76, "Advanced Trauma Life Support (ATLS)", "American College of Surgeons", "2020-03-05", "2023-03-05", 38 },
                    ////Ella Edwards
                    //{ 77, "Neurology Nursing Certification (CNRN)", "American Board of Neuroscience Nursing", "2019-09-15", "2024-09-15", 39 },
                    //{ 78, "Basic Life Support (BLS)", "American Heart Association", "2021-11-01", "2023-11-01", 39 },
                    ////Anthony Collins
                    //{ 79, "Board Certification in Psychiatry", "American Board of Psychiatry and Neurology", "2018-06-01", "2028-06-01", 40 },
                    //{ 80, "Advanced Psychiatric Nurse Practitioner Certification (PMHNP-BC)", "American Nurses Credentialing Center", "2020-04-15", "2025-04-15", 40 },
                    ////Mia Stewart
                    //{ 81, "Critical Care Registered Nurse (CCRN)", "American Association of Critical-Care Nurses", "2020-05-10", "2025-05-10", 41 },
                    //{ 82, "Advanced Cardiovascular Life Support (ACLS)", "American Heart Association", "2021-07-12", "2023-07-12", 41 },
                    ////Christian Sanchez
                    //{ 83, "Pediatric Advanced Life Support (PALS)", "American Heart Association", "2021-03-15", "2024-03-15", 42 },
                    //{ 84, "Board Certification in Pediatrics", "American Board of Pediatrics", "2019-06-01", "2029-06-01", 42 },
                    ////Savannah Morris
                    //{ 85, "Certified Rehabilitation Registered Nurse (CRRN)", "American Nurses Credentialing Center", "2020-05-10", "2025-05-10", 43 },
                    //{ 86, "Basic Life Support (BLS)", "American Heart Association", "2021-07-20", "2023-07-20", 43 },
                    ////Jacob Rodgers
                    //{ 87, "Board Certification in Dermatology", "American Board of Dermatology", "2019-03-15", null, 44 },
                    //{ 88, "Fellowship in Dermatologic Surgery", "American Society for Dermatologic Surgery", "2021-06-10", null, 44 },
                    ////Hannah Reed
                    //{ 89, "Certified Nephrology Nurse (CNN)", "Nephrology Nursing Certification Commission (NNCC)", "2020-05-15", "2025-05-15", 45 },
                    //{ 90, "Basic Life Support (BLS) Certification", "American Heart Association", "2021-03-01", "2023-03-01", 45 },
                    ////Liam Cook
                    //{ 91, "Board Certification in Radiology", "American Board of Radiology", "2018-07-20", null, 46 },
                    //{ 92, "Advanced Cardiac Life Support (ACLS) Certification", "American Heart Association", "2020-10-15", "2024-10-15", 46 },
                    ////Ella Morgan
                    //{ 93, "Family Nurse Practitioner (FNP-BC) Certification", "American Nurses Credentialing Center (ANCC)", "2019-05-10", "2024-05-10", 47 },
                    //{ 94, "Basic Life Support (BLS) Certification", "American Heart Association", "2021-09-15", "2023-09-15", 47 },
                    ////Owen Bell
                    //{ 95, "Board Certification in General Surgery", "American Board of Surgery", "2017-06-20", null, 48 },
                    //{ 96, "Advanced Trauma Life Support (ATLS) Certification", "American College of Surgeons", "2020-08-15", "2024-08-15", 48 },
                    ////Charlotte Murphy
                    //{ 97, "Oncology Certified Nurse (OCN)", "Oncology Nursing Certification Corporation", "2019-05-10", "2024-05-10", 49 },
                    //{ 98, "Chemotherapy and Biotherapy Provider Certification", "Oncology Nursing Society", "2020-03-15", "2023-03-15", 49 },
                    ////Luke Hughes
                    //{ 99, "Board Certification in Endocrinology, Diabetes, and Metabolism", "American Board of Internal Medicine", "2018-06-01", "2028-06-01", 50 },
                    //{ 100, "Advanced Diabetes Management Certification", "American Association of Clinical Endocrinologists", "2021-09-05", "2026-09-05", 50 },
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

                    // Candidate 3 - Emily Johnson (Nurse, Emergency Medicine)
                    { 7, "Trauma Care", 3 },
                    { 8, "Critical Thinking", 3 },
                    { 9, "Life Support Skills", 3 },

                    // Candidate 4 - Michael Brown (Doctor, Orthopedics)
                    { 10, "Bone Fracture Treatment", 4 },
                    { 11, "Joint Replacement Surgery", 4 },
                    { 12, "Sports Medicine", 4 },

                    // Candidate 5 - Sarah Davis (Nurse, Oncology)
                    { 13, "Chemotherapy Administration", 5 },
                    { 14, "Cancer Care", 5 },
                    { 15, "Patient Support Services", 5 },

                    // Candidate 6 - David Wilson (Doctor, General Surgery)
                    { 16, "Surgical Procedures", 6 },
                    { 17, "Post-operative Care", 6 },
                    { 18, "Anesthesia Management", 6 },

                    // Candidate 7 - Laura Taylor (Nurse, Critical Care)
                    { 19, "ICU Care", 7 },
                    { 20, "Cardiopulmonary Resuscitation (CPR)", 7 },
                    { 21, "Critical Condition Monitoring", 7 },

                    // Candidate 8 - James Anderson (Doctor, Pulmonology)
                    { 22, "Lung Disease Diagnosis", 8 },
                    { 23, "Respiratory Therapy", 8 },
                    { 24, "Bronchoscopy", 8 },

                    // Candidate 9 - Jessica Moore (Nurse, Neurology)
                    { 25, "Neurological Assessment", 9 },
                    { 26, "Seizure Management", 9 },
                    { 27, "Neurocritical Care", 9 },

                    // Candidate 10 - Daniel Thomas (Doctor, Dermatology)
                    { 28, "Skin Cancer Screening", 10 },
                    { 29, "Dermatological Surgery", 10 },
                    { 30, "Acne Treatment", 10 },

                    // Candidate 11 - Megan Martin (Nurse, Labor and Delivery)
                    { 31, "Labor Support", 11 },
                    { 32, "Newborn Care", 11 },
                    { 33, "Postpartum Care", 11 },

                    // Candidate 12 - Joshua Lee (Doctor, Radiology)
                    { 34, "Radiologic Imaging", 12 },
                    { 35, "CT Scanning", 12 },
                    { 36, "MRI Interpretation", 12 },

                    // Candidate 13 - Anna White (Nurse, Cardiology)
                    //{ 37, "ECG Monitoring", 13 },
                    //{ 38, "Cardiac Rehabilitation", 13 },
                    //{ 39, "Heart Disease Management", 13 },

                    //// Candidate 14 - Christopher Harris (Doctor, Pediatrics)
                    //{ 40, "Pediatric Care", 14 },
                    //{ 41, "Immunization", 14 },
                    //{ 42, "Child Growth and Development", 14 },

                    //// Candidate 15 - Olivia Clark (Nurse, Geriatrics)
                    //{ 43, "Elderly Care", 15 },
                    //{ 44, "Chronic Illness Management", 15 },
                    //{ 45, "Dementia Care", 15 },

                    //// Candidate 16 - Matthew Rodriguez (Doctor, Anesthesiology)
                    //{ 46, "Anesthesia Administration", 16 },
                    //{ 47, "Pain Management", 16 },
                    //{ 48, "Sedation Techniques", 16 },

                    //// Candidate 17 - Sophia Lewis (Nurse, Rehabilitation)
                    //{ 49, "Physical Therapy", 17 },
                    //{ 50, "Occupational Therapy", 17 },
                    //{ 51, "Rehabilitation Planning", 17 },

                    //// Candidate 18 - Andrew Walker (Doctor, Psychiatry)
                    //{ 52, "Mental Health Assessment", 18 },
                    //{ 53, "Psychotherapy", 18 },
                    //{ 54, "Medication Management", 18 },

                    //// Candidate 19 - Isabella Hall (Nurse, Primary Care)
                    //{ 55, "Health Screenings", 19 },
                    //{ 56, "Patient Education", 19 },
                    //{ 57, "Preventive Care", 19 },

                    //// Candidate 20 - Ryan Allen (Doctor, Endocrinology)
                    //{ 58, "Diabetes Management", 20 },
                    //{ 59, "Hormone Therapy", 20 },
                    //{ 60, "Thyroid Disorders", 20 },

                    // // Candidate 21 - Grace Young (Nurse, Nephrology)
                    //{ 61, "Dialysis Care", 21 },
                    //{ 62, "Kidney Disease Management", 21 },
                    //{ 63, "Fluid and Electrolyte Balance", 21 },

                    //// Candidate 22 - Benjamin King (Doctor, Ophthalmology)
                    //{ 64, "Retina Disorders", 22 },
                    //{ 65, "Cataract Surgery", 22 },
                    //{ 66, "Glaucoma Treatment", 22 },

                    //// Candidate 23 - Chloe Wright (Nurse, ICU)
                    //{ 67, "Critical Care Monitoring", 23 },
                    //{ 68, "Ventilator Management", 23 },
                    //{ 69, "Emergency Response", 23 },

                    //// Candidate 24 - Ethan Lopez (Doctor, Hematology)
                    //{ 70, "Blood Disorders", 24 },
                    //{ 71, "Bone Marrow Biopsy", 24 },
                    //{ 72, "Anemia Treatment", 24 },

                    //// Candidate 25 - Abigail Hill (Nurse, Family Medicine)
                    //{ 73, "Patient Assessment", 25 },
                    //{ 74, "Chronic Illness Care", 25 },
                    //{ 75, "Pediatric Care", 25 },

                    //// Candidate 26 - Alexander Scott (Doctor, Rheumatology)
                    //{ 76, "Arthritis Management", 26 },
                    //{ 77, "Autoimmune Disease Treatment", 26 },
                    //{ 78, "Joint Replacement Surgery", 26 },

                    //// Candidate 27 - Victoria Green (Nurse, Trauma)
                    //{ 79, "Trauma Care", 27 },
                    //{ 80, "Triage", 27 },
                    //{ 81, "Wound Management", 27 },

                    //// Candidate 28 - Tyler Adams (Doctor, Neurosurgery)
                    //{ 82, "Brain Tumor Surgery", 28 },
                    //{ 83, "Spinal Surgery", 28 },
                    //{ 84, "Neurotrauma Care", 28 },

                    //// Candidate 29 - Ella Baker (Nurse, Wound Care)
                    //{ 85, "Wound Dressing", 29 },
                    //{ 86, "Pressure Ulcer Prevention", 29 },
                    //{ 87, "Infection Control", 29 },

                    //// Candidate 30 - Nathan Carter (Doctor, Gastroenterology)
                    //{ 88, "Endoscopy Procedures", 30 },
                    //{ 89, "Liver Disease Treatment", 30 },
                    //{ 90, "IBS Management", 30 },

                    //// Candidate 31 - Samantha Mitchell (Nurse, Emergency Medicine)
                    //{ 91, "Trauma Management", 31 },
                    //{ 92, "Triage", 31 },
                    //{ 93, "Life Support", 31 },

                    //// Candidate 32 - Dylan Perez (Doctor, Infectious Diseases)
                    //{ 94, "Antibiotic Therapy", 32 },
                    //{ 95, "Viral Infections", 32 },
                    //{ 96, "Epidemiology", 32 },

                    //// Candidate 33 - Madison Roberts (Nurse, Oncology)
                    //{ 97, "Chemotherapy Administration", 33 },
                    //{ 98, "Pain Management", 33 },
                    //{ 99, "Cancer Care", 33 },

                    //// Candidate 34 - Elijah Turner (Doctor, Pulmonology)
                    //{ 100, "Respiratory Therapy", 34 },
                    //{ 101, "Asthma Management", 34 },
                    //{ 102, "Lung Disease Treatment", 34 },

                    //// Candidate 35 - Ava Phillips (Nurse, Critical Care)
                    //{ 103, "Cardiac Monitoring", 35 },
                    //{ 104, "Ventilator Support", 35 },
                    //{ 105, "Acute Care", 35 },

                    //// Candidate 36 - Lucas Campbell (Doctor, Cardiology)
                    //{ 106, "Cardiac Imaging", 36 },
                    //{ 107, "Angioplasty", 36 },
                    //{ 108, "Heart Disease Prevention", 36 },

                    //// Candidate 37 - Natalie Parker (Nurse, Labor and Delivery)
                    //{ 109, "Labor Monitoring", 37 },
                    //{ 110, "Delivery Assistance", 37 },
                    //{ 111, "Postpartum Care", 37 },

                    //// Candidate 38 - Logan Evans (Doctor, Orthopedics)
                    //{ 112, "Fracture Treatment", 38 },
                    //{ 113, "Joint Replacement Surgery", 38 },
                    //{ 114, "Sports Injuries", 38 },

                    //// Candidate 39 - Ella Edwards (Nurse, Neurology)
                    //{ 115, "Neurological Assessment", 39 },
                    //{ 116, "Seizure Management", 39 },
                    //{ 117, "Stroke Care", 39 },

                    //// Candidate 40 - Anthony Collins (Doctor, Psychiatry)
                    //{ 118, "Mental Health Diagnosis", 40 },
                    //{ 119, "Therapy Techniques", 40 },
                    //{ 120, "Psychopharmacology", 40 },

                    //// Candidate 41 - Mia Stewart (Nurse, ICU)
                    //{ 121, "Critical Care Nursing", 41 },
                    //{ 122, "Ventilator Management", 41 },
                    //{ 123, "Cardiac Monitoring", 41 },

                    //// Candidate 42 - Christian Sanchez (Doctor, Pediatrics)
                    //{ 124, "Childhood Vaccination", 42 },
                    //{ 125, "Pediatric Diagnostics", 42 },
                    //{ 126, "Neonatal Care", 42 },

                    //// Candidate 43 - Savannah Morris (Nurse, Rehabilitation)
                    //{ 127, "Physical Therapy", 43 },
                    //{ 128, "Pain Management", 43 },
                    //{ 129, "Post-Surgery Rehabilitation", 43 },

                    //// Candidate 44 - Jacob Rogers (Doctor, Dermatology)
                    //{ 130, "Skin Cancer Treatment", 44 },
                    //{ 131, "Dermatologic Surgery", 44 },
                    //{ 132, "Acne Treatment", 44 },

                    //// Candidate 45 - Hannah Reed (Nurse, Nephrology)
                    //{ 133, "Dialysis Care", 45 },
                    //{ 134, "Chronic Kidney Disease Management", 45 },
                    //{ 135, "Kidney Transplant Care", 45 },

                    //// Candidate 46 - Liam Cook (Doctor, Radiology)
                    //{ 136, "X-Ray Interpretation", 46 },
                    //{ 137, "CT Scan Analysis", 46 },
                    //{ 138, "MRI Imaging", 46 },

                    //// Candidate 47 - Ella Morgan (Nurse, Primary Care)
                    //{ 139, "Health Screenings", 47 },
                    //{ 140, "Chronic Disease Management", 47 },
                    //{ 141, "Patient Education", 47 },

                    //// Candidate 48 - Owen Bell (Doctor, General Surgery)
                    //{ 142, "Laparoscopic Surgery", 48 },
                    //{ 143, "Trauma Surgery", 48 },
                    //{ 144, "Postoperative Care", 48 },

                    //// Candidate 49 - Charlotte Murphy (Nurse, Oncology)
                    //{ 145, "Chemotherapy Administration", 49 },
                    //{ 146, "Cancer Pain Management", 49 },
                    //{ 147, "Palliative Care", 49 },

                    //// Candidate 50 - Luke Hughes (Doctor, Endocrinology)
                    //{ 148, "Diabetes Management", 50 },
                    //{ 149, "Thyroid Disorders", 50 },
                    //{ 150, "Hormonal Imbalance Treatment", 50 }
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
                    //{ 4, "Michael", "Brown", "michaelb@example.com", "345-678-9012", "Florida", "Doctor", "Orthopedics" },
                    { 13, (int)PositionType.FullTime, 4, 4 },
                    { 14, (int)PositionType.PartTime, 4, 4 },
                    { 15, (int)PositionType.Temporary, 4, 4 },
                    { 16, (int)PositionType.Permanent, 4, 4 },
                    { 17, (int)PositionType.Remote, 4, 4 },
                    { 18, (int)PositionType.InOffice, 4, 4 },
                    //{ 5, "Sarah", "Davis", "sarahd@example.com", "456-789-0123", "Illinois", "Nurse", "Oncology" },
                    { 19, (int)PositionType.PartTime, 5, 5 },
                    { 20, (int)PositionType.Temporary, 5, 5 },
                    { 21, (int)PositionType.InOffice, 5, 5 },
                    //{ 6, "David", "Wilson", "davidw@example.com", "567-890-1234", "Georgia", "Doctor", "General Surgery" },
                    { 22, (int)PositionType.FullTime, 6, 6 },
                    { 23, (int)PositionType.PartTime, 6, 6 },
                    { 24, (int)PositionType.Temporary, 6, 6 },
                    { 25, (int)PositionType.Remote, 6, 6 },
                    //{ 7, "Laura", "Taylor", "laurat@example.com", "678-901-2345", "North Carolina", "Nurse", "Critical Care" },
                    { 26, (int)PositionType.PartTime, 7, 7 },
                    { 27, (int)PositionType.Temporary, 7, 7 },
                    { 28, (int)PositionType.Permanent, 7, 7 },
                    { 29, (int)PositionType.InOffice, 7, 7 },
                    //{ 8, "James", "Anderson", "jamesa@example.com", "789-012-3456", "Ohio", "Doctor", "Pulmonology" },
                    { 30, (int)PositionType.PartTime, 8, 8 },
                    { 31, (int)PositionType.Temporary, 8, 8 },
                    { 32, (int)PositionType.Permanent, 8, 8 },
                    { 33, (int)PositionType.Remote, 8, 8 },
                    { 34, (int)PositionType.InOffice, 8, 8 },
                    //{ 9, "Jessica", "Moore", "jessicam@example.com", "890-123-4567", "Pennsylvania", "Nurse", "Neurology" },
                    { 35, (int)PositionType.FullTime, 9, 9 },
                    { 36, (int)PositionType.Permanent, 9, 9 },
                    { 37, (int)PositionType.InOffice, 9, 9 },
                    //{ 10, "Daniel", "Thomas", "danielt@example.com", "901-234-5678", "Michigan", "Doctor", "Dermatology" },
                    { 38, (int)PositionType.FullTime, 10, 10 },
                    { 39, (int)PositionType.PartTime, 10, 10 },
                    { 40, (int)PositionType.Temporary, 10, 10 },
                    { 41, (int)PositionType.Permanent, 10, 10 },
                    { 42, (int)PositionType.Remote, 10, 10 },
                    { 43, (int)PositionType.InOffice, 10, 10 },
                    //{ 11, "Megan", "Martin", "meganm@example.com", "012-345-6789", "Virginia", "Nurse", "Labor and Delivery" },
                    { 44, (int)PositionType.FullTime, 11, 11 },
                    { 45, (int)PositionType.PartTime, 11, 11 },
                    { 46, (int)PositionType.Temporary, 11, 11 },
                    { 47, (int)PositionType.Permanent, 11, 11 },
                    { 48, (int)PositionType.InOffice, 11, 11 },
                    //{ 12, "Joshua", "Lee", "joshual@example.com", "123-456-7891", "Washington", "Doctor", "Radiology" },
                    { 49, (int)PositionType.FullTime, 12, 12 },
                    { 50, (int)PositionType.PartTime, 12, 12 },
                    { 51, (int)PositionType.Temporary, 12, 12 },
                    { 52, (int)PositionType.Permanent, 12, 12 },
                    { 53, (int)PositionType.Remote, 12, 12 },
                    { 54, (int)PositionType.InOffice, 12, 12 },
                    //{ 13, "Anna", "White", "annaw@example.com", "234-567-8902", "Massachusetts", "Nurse", "Cardiology" },
                    //{ 55, (int)PositionType.PartTime, 13, 13 },
                    //{ 56, (int)PositionType.Temporary, 13, 13 },
                    //{ 57, (int)PositionType.Permanent, 13, 13 },
                    //{ 58, (int)PositionType.FullTime, 13, 13 },
                    //{ 59, (int)PositionType.InOffice, 13, 13 },
                    ////{ 14, "Christopher", "Harris", "chrish@example.com", "345-678-9013", "Arizona", "Doctor", "Pediatrics" },
                    //{ 60, (int)PositionType.FullTime, 14, 14 },
                    //{ 61, (int)PositionType.PartTime, 14, 14 },
                    //{ 62, (int)PositionType.Permanent, 14, 14 },
                    //{ 63, (int)PositionType.Temporary, 14, 14 },
                    //{ 64, (int)PositionType.InOffice, 14, 14 },
                    ////{ 15, "Olivia", "Clark", "oliviac@example.com", "456-789-0124", "Colorado", "Nurse", "Geriatrics" },
                    //{ 65, (int)PositionType.FullTime, 15, 15 },
                    //{ 66, (int)PositionType.PartTime, 15, 15 },
                    //{ 67, (int)PositionType.Permanent, 15, 15 },
                    //{ 68, (int)PositionType.Temporary, 15, 15 },
                    //{ 69, (int)PositionType.InOffice, 15, 15 },
                    //{ 70, (int)PositionType.Remote, 15, 15 },
                    ////{ 16, "Matthew", "Rodriguez", "mattr@example.com", "567-890-1235", "Oregon", "Doctor", "Anesthesiology" },
                    //{ 71, (int)PositionType.PartTime, 16, 16 },
                    //{ 72, (int)PositionType.Temporary, 16, 16 },
                    //{ 73, (int)PositionType.FullTime, 16, 16 },
                    //{ 74, (int)PositionType.Permanent, 16, 16 },
                    //{ 75, (int)PositionType.InOffice, 16, 16 },
                    ////{ 17, "Sophia", "Lewis", "sophial@example.com", "678-901-2346", "Indiana", "Nurse", "Rehabilitation" },
                    //{ 76, (int)PositionType.FullTime, 17, 17 },
                    //{ 77, (int)PositionType.PartTime, 17, 17 },
                    //{ 78, (int)PositionType.Temporary, 17, 17 },
                    //{ 79, (int)PositionType.Permanent, 17, 17 },
                    //{ 80, (int)PositionType.InOffice, 17, 17 },
                    //{ 81, (int)PositionType.Remote, 17, 17 },
                    ////{ 18, "Andrew", "Walker", "andreww@example.com", "789-012-3457", "Tennessee", "Doctor", "Psychiatry" },
                    //{ 83, (int)PositionType.PartTime, 18, 18 },
                    //{ 84, (int)PositionType.Temporary, 18, 18 },
                    //{ 86, (int)PositionType.InOffice, 18, 18 },
                    //{ 87, (int)PositionType.Remote, 18, 18 }, 
                    ////{ 19, "Isabella", "Hall", "isabellah@example.com", "890-123-4568", "Kentucky", "Nurse", "Primary Care" },
                    //{ 88, (int)PositionType.FullTime, 19, 19 },
                    //{ 89, (int)PositionType.PartTime, 19, 19 },
                    //{ 90, (int)PositionType.Permanent, 19, 19 },
                    //{ 91, (int)PositionType.Temporary, 19, 19 },
                    //{ 92, (int)PositionType.InOffice, 19, 19 },
                    //{ 93, (int)PositionType.Remote, 19, 19 },
                    ////{ 20, "Ryan", "Allen", "ryana@example.com", "901-234-5679", "Missouri", "Doctor", "Endocrinology" },
                    //{ 94, (int)PositionType.FullTime, 20, 20 },
                    //{ 95, (int)PositionType.Permanent, 20, 20 },
                    //{ 96, (int)PositionType.InOffice, 20, 20 },
                    ////{ 21, "Grace", "Young", "gracey@example.com", "012-345-6780", "Minnesota", "Nurse", "Nephrology" },
                    //{ 97, (int)PositionType.PartTime, 21, null },
                    //{ 98, (int)PositionType.Permanent, 21, null },
                    //{ 99, (int)PositionType.InOffice, 21, null },
                    ////{ 22, "Benjamin", "King", "benjamink@example.com", "123-456-7892", "Alabama", "Doctor", "Ophthalmology" },
                    //{ 100, (int)PositionType.FullTime, 22, null },
                    //{ 101, (int)PositionType.Temporary, 22, null },
                    //{ 102, (int)PositionType.Remote, 22, null },
                    ////{ 23, "Chloe", "Wright", "chloew@example.com", "234-567-8903", "Nevada", "Nurse", "ICU" },
                    //{ 103, (int)PositionType.FullTime, 23, null },
                    //{ 104, (int)PositionType.PartTime, 23, null },
                    //{ 105, (int)PositionType.Permanent, 23, null },
                    //{ 106, (int)PositionType.Temporary, 23, null },
                    //{ 107, (int)PositionType.InOffice, 23, null },
                    //{ 108, (int)PositionType.Remote, 23, null },
                    ////{ 24, "Ethan", "Lopez", "ethanl@example.com", "345-678-9014", "Wisconsin", "Doctor", "Hematology" },
                    //{ 109, (int)PositionType.FullTime, 24, null },
                    //{ 110, (int)PositionType.PartTime, 24, null },
                    //{ 111, (int)PositionType.Permanent, 24, null },
                    //{ 112, (int)PositionType.Temporary, 24, null },
                    //{ 113, (int)PositionType.InOffice, 24, null },
                    //{ 114, (int)PositionType.Remote, 24, null },
                    ////{ 25, "Abigail", "Hill", "abigailh@example.com", "456-789-0125", "Oklahoma", "Nurse", "Family Medicine" },
                    //{ 115, (int)PositionType.PartTime, 25, null },
                    //{ 116, (int)PositionType.Temporary, 25, null },
                    //{ 117, (int)PositionType.InOffice, 25, null },
                    //{ 118, (int)PositionType.Remote, 25, null },
                    ////{ 26, "Alexander", "Scott", "alexanders@example.com", "567-890-1236", "Arkansas", "Doctor", "Rheumatology" },
                    //{ 119, (int)PositionType.PartTime, 26, null },
                    //{ 120, (int)PositionType.Permanent, 26, null },
                    //{ 121, (int)PositionType.Remote, 26, null },
                    ////{ 27, "Victoria", "Green", "victoriag@example.com", "678-901-2347", "Louisiana", "Nurse", "Trauma" },
                    //{ 122, (int)PositionType.FullTime, 27, null },
                    //{ 123, (int)PositionType.PartTime, 27, null },
                    //{ 124, (int)PositionType.Permanent, 27, null },
                    //{ 125, (int)PositionType.Temporary, 27, null },
                    //{ 126, (int)PositionType.InOffice, 27, null },
                    //{ 127, (int)PositionType.Remote, 27, null },
                    ////{ 28, "Tyler", "Adams", "tylera@example.com", "789-012-3458", "Kansas", "Doctor", "Neurosurgery" },
                    //{ 128, (int)PositionType.FullTime, 28, null },
                    //{ 129, (int)PositionType.Permanent, 28, null },
                    //{ 130, (int)PositionType.Remote, 28, null },
                    ////{ 29, "Ella", "Baker", "ellab@example.com", "890-123-4569", "South Carolina", "Nurse", "Wound Care" },
                    //{ 131, (int)PositionType.PartTime, 29, null },
                    //{ 132, (int)PositionType.Temporary, 29, null },
                    //{ 133, (int)PositionType.Permanent, 29, null },
                    //{ 134, (int)PositionType.InOffice, 29, null },
                    //{ 135, (int)PositionType.Remote, 29, null },
                    ////{ 30, "Nathan", "Carter", "nathanc@example.com", "901-234-5670", "Iowa", "Doctor", "Gastroenterology" },
                    //{ 136, (int)PositionType.FullTime, 30, null },
                    //{ 137, (int)PositionType.PartTime, 30, null },
                    //{ 138, (int)PositionType.Permanent, 30, null },
                    //{ 139, (int)PositionType.Temporary, 30, null },
                    //{ 140, (int)PositionType.InOffice, 30, null },
                    //{ 141, (int)PositionType.Remote, 30, null },
                    ////{ 31, "Samantha", "Mitchell", "samantham@example.com", "012-345-6781", "Mississippi", "Nurse", "Emergency Medicine" },
                    //{ 142, (int)PositionType.FullTime, 31, null },
                    //{ 143, (int)PositionType.PartTime, 31, null },
                    //{ 144, (int)PositionType.Permanent, 31, null },
                    //{ 145, (int)PositionType.Temporary, 31, null },
                    //{ 146, (int)PositionType.InOffice, 31, null },
                    //{ 147, (int)PositionType.Remote, 31, null },
                    ////{ 32, "Dylan", "Perez", "dylanp@example.com", "123-456-7893", "New Mexico", "Doctor", "Infectious Diseases" },
                    //{ 148, (int)PositionType.FullTime, 32, null },
                    //{ 149, (int)PositionType.PartTime, 32, null },
                    //{ 150, (int)PositionType.Permanent, 32, null },
                    //{ 151, (int)PositionType.Temporary, 32, null },
                    //{ 152, (int)PositionType.InOffice, 32, null },
                    //{ 153, (int)PositionType.Remote, 32, null },
                    ////{ 33, "Madison", "Roberts", "madisonr@example.com", "234-567-8904", "Connecticut", "Nurse", "Oncology" },
                    //{ 154, (int)PositionType.FullTime, 33, null },
                    //{ 155, (int)PositionType.PartTime, 33, null },
                    //{ 156, (int)PositionType.Permanent, 33, null },
                    //{ 157, (int)PositionType.Temporary, 33, null },
                    //{ 158, (int)PositionType.InOffice, 33, null },
                    //{ 159, (int)PositionType.Remote, 33, null },
                    ////{ 34, "Elijah", "Turner", "elijaht@example.com", "345-678-9015", "Utah", "Doctor", "Pulmonology" },
                    //{ 160, (int)PositionType.PartTime, 34, null },
                    //{ 161, (int)PositionType.Permanent, 34, null },
                    //{ 162, (int)PositionType.InOffice, 34, null },
                    ////{ 35, "Ava", "Phillips", "avap@example.com", "456-789-0126", "Nebraska", "Nurse", "Critical Care" },
                    //{ 163, (int)PositionType.FullTime, 35, null },
                    //{ 164, (int)PositionType.PartTime, 35, null },
                    //{ 165, (int)PositionType.Permanent, 35, null },
                    //{ 166, (int)PositionType.Temporary, 35, null },
                    //{ 167, (int)PositionType.InOffice, 35, null },
                    //{ 168, (int)PositionType.Remote, 35, null },
                    ////{ 36, "Lucas", "Campbell", "lucasc@example.com", "567-890-1237", "Montana", "Doctor", "Cardiology" },
                    //{ 169, (int)PositionType.FullTime, 36, null },
                    //{ 170, (int)PositionType.PartTime, 36, null },
                    //{ 171, (int)PositionType.Permanent, 36, null },
                    //{ 172, (int)PositionType.Temporary, 36, null },
                    //{ 173, (int)PositionType.InOffice, 36, null },
                    //{ 174, (int)PositionType.Remote, 36, null },
                    ////{ 37, "Natalie", "Parker", "nataliep@example.com", "678-901-2348", "West Virginia", "Nurse", "Labor and Delivery" },
                    //{ 175, (int)PositionType.FullTime, 37, null },
                    //{ 176, (int)PositionType.PartTime, 37, null },
                    //{ 177, (int)PositionType.Permanent, 37, null },
                    //{ 178, (int)PositionType.Temporary, 37, null },
                    ////{ 38, "Logan", "Evans", "logane@example.com", "789-012-3459", "Hawaii", "Doctor", "Orthopedics" },
                    //{ 179, (int)PositionType.FullTime, 38, null },
                    //{ 180, (int)PositionType.Permanent, 38, null },
                    //{ 181, (int)PositionType.Temporary, 38, null },
                    //{ 182, (int)PositionType.InOffice, 38, null },
                    //{ 183, (int)PositionType.Remote, 38, null },
                    ////{ 39, "Ella", "Edwards", "ellae@example.com", "890-123-4570", "Idaho", "Nurse", "Neurology" },
                    //{ 184, (int)PositionType.FullTime, 39, null },
                    //{ 185, (int)PositionType.Temporary, 39, null },
                    //{ 186, (int)PositionType.InOffice, 39, null },
                    //{ 187, (int)PositionType.Remote, 39, null },
                    ////{ 40, "Anthony", "Collins", "anthonyc@example.com", "901-234-5671", "Alaska", "Doctor", "Psychiatry" },
                    //{ 188, (int)PositionType.FullTime, 40, null },
                    //{ 189, (int)PositionType.PartTime, 40, null },
                    //{ 190, (int)PositionType.Permanent, 40, null },
                    //{ 191, (int)PositionType.Temporary, 40, null },
                    //{ 192, (int)PositionType.InOffice, 40, null },
                    //{ 193, (int)PositionType.Remote, 40, null },
                    ////{ 41, "Mia", "Stewart", "mias@example.com", "012-345-6782", "Maine", "Nurse", "ICU" },
                    //{ 194, (int)PositionType.PartTime, 41, null },
                    //{ 195, (int)PositionType.Permanent, 41, null },
                    //{ 196, (int)PositionType.Temporary, 41, null },
                    //{ 197, (int)PositionType.InOffice, 41, null },
                    ////{ 42, "Christian", "Sanchez", "christians@example.com", "123-456-7894", "Vermont", "Doctor", "Pediatrics" },
                    //{ 198, (int)PositionType.PartTime, 42, null },
                    //{ 199, (int)PositionType.Permanent, 42, null },
                    //{ 200, (int)PositionType.Temporary, 42, null },
                    //{ 201, (int)PositionType.Remote, 42, null },
                    ////{ 43, "Savannah", "Morris", "savannahm@example.com", "234-567-8905", "Wyoming", "Nurse", "Rehabilitation" },
                    //{ 202, (int)PositionType.FullTime, 43, null },
                    //{ 203, (int)PositionType.PartTime, 43, null },
                    //{ 204, (int)PositionType.Permanent, 43, null },
                    //{ 205, (int)PositionType.Temporary, 43, null },
                    //{ 206, (int)PositionType.InOffice, 43, null },
                    //{ 207, (int)PositionType.Remote, 43, null },
                    ////{ 44, "Jacob", "Rogers", "jacobr@example.com", "345-678-9016", "South Dakota", "Doctor", "Dermatology" },
                    //{ 208, (int)PositionType.FullTime, 44, null },
                    //{ 209, (int)PositionType.Permanent, 44, null },
                    //{ 210, (int)PositionType.InOffice, 44, null },
                    ////{ 45, "Hannah", "Reed", "hannah@example.com", "456-789-0127", "North Dakota", "Nurse", "Nephrology" },
                    //{ 211, (int)PositionType.FullTime, 45, null },
                    //{ 212, (int)PositionType.PartTime, 45, null },
                    //{ 213, (int)PositionType.Permanent, 45, null },
                    //{ 214, (int)PositionType.Temporary, 45, null },
                    //{ 215, (int)PositionType.InOffice, 45, null },
                    //{ 216, (int)PositionType.Remote, 45, null },
                    ////{ 46, "Liam", "Cook", "liamc@example.com", "567-890-1238", "Rhode Island", "Doctor", "Radiology" },
                    //{ 217, (int)PositionType.FullTime, 46, null },
                    //{ 218, (int)PositionType.PartTime, 46, null },
                    //{ 219, (int)PositionType.Permanent, 46, null },
                    //{ 220, (int)PositionType.Temporary, 46, null },
                    //{ 221, (int)PositionType.InOffice, 46, null },
                    //{ 222, (int)PositionType.Remote, 46, null },
                    ////{ 47, "Ella", "Morgan", "ellam@example.com", "678-901-2349", "Delaware", "Nurse", "Primary Care" },
                    //{ 223, (int)PositionType.FullTime, 47, null },
                    //{ 224, (int)PositionType.PartTime, 47, null },
                    //{ 225, (int)PositionType.Permanent, 47, null },
                    //{ 226, (int)PositionType.Temporary, 47, null },
                    //{ 227, (int)PositionType.InOffice, 47, null },
                    //{ 228, (int)PositionType.Remote, 47, null },
                    ////{ 48, "Owen", "Bell", "owenb@example.com", "789-012-3460", "Montana", "Doctor", "General Surgery" },
                    //{ 229, (int)PositionType.FullTime, 48, null },
                    //{ 230, (int)PositionType.PartTime, 48, null },
                    //{ 231, (int)PositionType.Permanent, 48, null },
                    //{ 232, (int)PositionType.Temporary, 48, null },
                    //{ 233, (int)PositionType.InOffice, 48, null },
                    //{ 234, (int)PositionType.Remote, 48, null },
                    ////{ 49, "Charlotte", "Murphy", "charlottem@example.com", "890-123-4571", "Alabama", "Nurse", "Oncology" },
                    //{ 235, (int)PositionType.FullTime, 49, null },
                    //{ 236, (int)PositionType.PartTime, 49, null },
                    //{ 237, (int)PositionType.Permanent, 49, null },
                    //{ 238, (int)PositionType.Temporary, 49, null },
                    //{ 239, (int)PositionType.InOffice, 49, null },
                    //{ 240, (int)PositionType.Remote, 49, null },
                    ////{ 50, "Luke", "Hughes", "lukeh@example.com", "901-234-5672", "Texas", "Doctor", "Endocrinology" }
                    //{ 241, (int)PositionType.FullTime, 50, null },
                    //{ 242, (int)PositionType.PartTime, 50, null },
                    //{ 243, (int)PositionType.Permanent, 50, null },
                    //{ 244, (int)PositionType.Temporary, 50, null },
                    //{ 245, (int)PositionType.InOffice, 50, null },
                    //{ 246, (int)PositionType.Remote, 50, null },
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
