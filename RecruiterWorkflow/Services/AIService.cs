using Azure.AI.OpenAI;
using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenAI.Chat;
using static System.Environment;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc;
using RecruiterWorkflow.Models;
using System.Text.Json;
using System;

namespace RecruiterWorkflow.Services

{
    class ParsedAvailability
    {
        public string Day { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class CandidateFilters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Occupation { get; set; }
        public string Specialty { get; set; }
        public string Experience { get; set; }
        public string Availability { get; set; }
        public string Skills { get; set; }
        public string CredentialsAndCertifications { get; set; }
    }

    public class JobFilters
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string CompensationAndBenefits { get; set; }
        public string Availability { get; set; }
        public string Location { get; set; }
    }

    public class AIService
    {
        private readonly ChatClient _chatClient;
        private readonly ILogger<AIService> _logger;
        private readonly string _azureAIUrl;
        private readonly string _azureAIKey;
        private readonly string _deploymentName;
        //private readonly AzureOpenAIClient azureClient;

        public AIService(ChatClient chatClient, ILogger<AIService> logger)
        {
            _chatClient = chatClient;
            _logger = logger;
        }

        //public async Task<List<Candidate>> FindCandidates(string query)
        //{
        //    // Use the Azure OpenAI API to find candidates based on the query if query is something like "Find me candidates who live in Georgia"
        //}

        public async Task<bool> EvaluateCandidateNaturalLanguage(Candidate candidate, string prompt)
        {
            Console.WriteLine("EvaluateCandidateNaturalLanguage: " + CandidateUtils.ToString(candidate));
            var chatMessage = new UserChatMessage(
                "Evaluate whether the candidate matches the provided user query. " +
                "Use the candidate overview and natural language user search query to make your decision. " +
                "For example, if the user query is 'John' you should return true for candidates that have John in their name or a name similar like 'Jon'." +
                "If the user query is 'Sarah' and the candidate's name is 'John' you should return false." +
                "If a user query is 'heart' and the candidate has 'cardiac' in their specialty you should return true." +
                "If a user query is 'Find me a nurse' and the candidate is a nurse you should return true." +
                "If a user query is 'Find me a doctor' and the candidate is a nurse you should return false." +
                "If a user query is 'State Board of Nursing RN License' and the candidate has that credential or something similar you should return true." +
                "Do your best to match the user query with the candidate overview, even if there's not alot of information. Evaluate the candidate's name, occupation, location, specialty, credentials, availability/desired positions, skills, and experiences before determining invalid." +
                "Provide one of the following responses:\n" +
                "   - 'true': The candidate aligns with the query parameters.\n" +
                "   - 'false': The candidate does not align with the query parameters.\n" +
                "   - 'Invalid (Give brief explanation as to why you determined invalid)': The query lacks enough clarity or information.\n\n" +
                "Result: 'true'\n\n" +
                $"Candidate Overview:\n{CandidateUtils.ToString(candidate)}\n" +
                $"User Query:\n{prompt}\n"
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("true"))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("false"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating candidate match with query: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateCandidateSmartFilters(Candidate candidate, string filters)
        {
            Console.WriteLine("EvaluateCandidateSmartFilters: " + CandidateUtils.ToString(candidate));
            Console.WriteLine("Filters: " + filters);

            var chatMessage = new UserChatMessage(
                "Evaluate whether the candidate matches the provided query parameters. " +
                "Use the candidate overview and the search query parameters to make your decision. " +
                "Rules for evaluation:\n" +
                "If a query parameter is '*', that filter can be ignored.\n" +
                "The candidate's name does not need to be a perfect match. For example:\n" +
                "   - Query name: 'Jon' or 'Jo'\n" +
                "   - Candidate name: 'John'\n" +
                "   - Result: Match (true)\n" +
                "Provide one of the following responses:\n" +
                "   - 'true'\n" +
                "   - 'false'\n" +
                "   - 'Invalid'\n"+
                "Example:\n" +
                "Candidate Overview: { Name: John Doe, Email: johndoe@example.com, Phone: 123-456-7890, State: California, Occupation: Nurse, Specialty: Pediatrics, Desired Positions/Availability: [FullTime, PartTime, Temporary, Permanent], Credentials: State Board of Nursing RN License, Skills: None, Experiences: City General Hospital - Nurse }\n" +
                "Query Parameters: { Name: '*', State: 'CA', Specialty: '*' }\n" +
                "For this example your response should be 'true'\n\n" +
                "Example: Candidate: Jane Smith\r\nEmail: janesmith@example.com\r\nPhone: 987-654-3210\r\nState: Texas\r\nOccupation: Doctor\r\nSpecialty: Cardiology\r\nDesired Positions/Availibility: FullTime, Temporary, Permanent\r\nCredentials: Board Certification - Cardiology\r\nSkills: Surgical Procedures\r\nExperiences: County Heart Center (2019-05-01, 2023-06-01)" +
                "Query Parameters: Name: *\r\nState: *\r\nOccupation: *\r\nSpecialty: *\r\n" +
                "Should return true" +
                "If the query parameters lists doctor for occupation and the candidate is a nurse you should return false. " +
                "If the query parameters lists heart for specialty and the candidate is a cardiologist should return true. " +
                "If the candidate can't meet all the query parameters (Name, State, Occupation, Specialty) that don't have value '*' then you should return false. " +
                "Example: Candidate: Jane Smith\r\nEmail: janesmith@example.com\r\nPhone: 987-654-3210\r\nState: Texas\r\nOccupation: Doctor\r\nSpecialty: Cardiology\r\nDesired Positions/Availibility: FullTime, Temporary, Permanent\r\nCredentials: Board Certification - Cardiology\r\nSkills: Surgical Procedures\r\nExperiences: County Heart Center (2019-05-01, 2023-06-01)" +
                "Query Parameters: Name: *\r\nState: Georgia\r\nOccupation: Doctor\r\nSpecialty: Cardiology\r\n" +
                "Should return false" +
                "Example: Candidate: Jane Smith\r\nEmail: janesmith@example.com\r\nPhone: 987-654-3210\r\nState: Texas\r\nOccupation: Doctor\r\nSpecialty: Cardiology\r\nDesired Positions/Availibility: FullTime, Temporary, Permanent\r\nCredentials: Board Certification - Cardiology\r\nSkills: Surgical Procedures\r\nExperiences: County Heart Center (2019-05-01, 2023-06-01)" +
                "Query Parameters: Name: *\r\nState: Texas\r\nOccupation: Doctor\r\nSpecialty: Heart\r\n" +
                "Should return true" +
                $"Candidate Overview:\n{CandidateUtils.ToString(candidate)}\n" +
                $"Query Parameters:\n{filters}\n"
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("true"))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("false"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating candidate match with filters: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateJobSmartFilters(Job job, string filters)
        {
            Console.WriteLine("EvaluateJobSmartFilters: " + JobUtils.ToString(job));
            Console.WriteLine("Filters: " + filters);

            var chatMessage = new UserChatMessage(
                "Evaluate whether the job matches the provided query parameters. " +
                "Use the job overview and the search query parameters to make your decision. " +
                "Rules for evaluation:\n" +
                "If a query parameter is '*', that filter can be ignored.\n" +
                "The clinic's name does not need to be a perfect match. For example:\n" +
                "   - Query Clinic Name: 'Grady'\n" +
                "   - Job Clinic Name: 'Grady Hospital'\n" +
                "   - Result: Match (true)\n" +
                "Provide one of the following responses:\n" +
                "   - 'true'\n" +
                "   - 'false'\n" +
                "   - 'Invalid'\n" +
                "Example: Job: Cardiologist\r\nClinic: Suburb Care Center\r\nLocation: 456 Elm St, Suburbia\r\nDescription: Perform heart surgeries and consultations.\r\nResponsibilities:\r\nRequirements:\r\nCompensation and Benefits:\r\nAvailable Positions: FullTime" +
                "Query Parameters: Role: Doctor\r\nClinic Name: *\r\nLocation: *\r\nSpecialty: *" +
                "Should return true" +
                "Example: Job: Registered Nurse\r\nClinic: City Health Clinic\r\nLocation: *\r\nDescription: Provide patient care in pediatrics.\r\nResponsibilities:\r\nRequirements:\r\nCompensation and Benefits:\r\nAvailable Positions: FullTime" +
                "Query Parameters: Role: Doctor\r\nClinic Name: *\r\nLocation: GA\r\nSpecialty: *" +
                "Should return false" +
                $"Job Overview:\n{JobUtils.ToString(job)}\n" +
                $"Query Parameters:\n{filters}\n"
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("true"))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("false"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating job match with filters: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateJobNaturalLanguage(Job job, string prompt)
        {
            Console.WriteLine("EvaluateJobNaturalLanguage: " + JobUtils.ToString(job));
            var chatMessage = new UserChatMessage(
                "Evaluate whether the job matches the provided user query. " +
                "Use the job overview and natural language user search query to make your decision. " +
                "For example, if the user query is 'Nurse' you should return jobs that are nurse positions or similar to nurse roles and return true." +
                "If a user query is 'nurse' and the job is describing a role for doctor then return false." +
                "Do your best to match the user query with the job overview, even if there's not alot of information." +
                "Provide one of the following responses:\n" +
                "   - 'true': The job aligns with the query parameters.\n" +
                "   - 'false': The job does not align with the query parameters.\n" +
                "   - 'Invalid (Give brief explanation as to why you determined invalid)': The query lacks enough clarity or information.\n\n" +
                "Result: 'true'\n\n" +
                $"Job Overview:\n{JobUtils.ToString(job)}\n" +
                $"User Query:\n{prompt}\n"
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("true"))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase) || responseContent.Contains("false"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating job match with query: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateJobMatchWithCandidate(Candidate candidate, Job job)
        {
            Console.WriteLine("EvaluateJobMatchWithCandidate");
            var chatMessage = new UserChatMessage(
                "Evaluate if the following job is a good fit for the provided candidate. " +
                "Base your decision on the candidate overview and the job's details. " +
                "Provide one of the following responses: 'true' (if the job is a good match), " +
                "'false' (if the job is not a match), or 'Invalid' (if the query lacks enough clarity or information). " +
                "Respond with only 'true', 'false', or 'Invalid'.\n\n" +
                $"Candidate Overview:\n{CandidateUtils.ToString(candidate)}" +
                $"Job Overview:\n{JobUtils.ToString(job)}\n\n" 
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating job match with candidate: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateCandidateMatchWithJob(Job job, Candidate candidate)
        {
            Console.WriteLine("Evaluating candidate match with job");
            var chatMessage = new UserChatMessage(
                "Evaluate if the following candidate is a good fit for the provided job. " +
                "Base your decision on the job overview and the candidate's details. " +
                "Provide one of the following responses: 'true' (if the candidate is a good match), " +
                "'false' (if the candidate is not a match), or 'Invalid' (if the query lacks enough clarity or information). " +
                "Respond with only 'true', 'false', or 'Invalid'.\n\n" +
                $"Job Overview:\n{JobUtils.ToString(job)}\n\n" +
                $"Candidate Overview:\n{CandidateUtils.ToString(candidate)}"
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating candidate match with job: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateAvailability(string userQuery, List<Position> positions)
        {
            Console.WriteLine("Evaluating availability");
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's availability aligns with this query." +
                $"User Query: '{userQuery}'" +
                $"Willing to work the following position types: {PositionExtensions.ToStringRepresentation(positions)}" +
                "Respond with 'true' if the availability and/or desired positions align with the query, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating availability: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateOccupation(string userQuery, string occupation)
        {
            Console.WriteLine("Evaluating specialty");
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's occupation is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Specialty: '{occupation}'" +
                "Respond with 'true' if the occupation is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating specialty: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateSpecialty(string userQuery, string specialty)
        {
            Console.WriteLine("Evaluating specialty");
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's specialty is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Specialty: '{specialty}'" +
                "Respond with 'true' if the specialty is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating specialty: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateSkills(string userQuery, Skill skill)
        {
            Console.WriteLine("Evaluating experience");
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's skill is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Skill Name: '{skill.Name}'" +
                "Respond with 'true' if the experience is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });

                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();

                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");

                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating experience: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateTitle(string userQuery, string title)
        {
            Console.WriteLine("Evaluating title");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's title is relevant to this query. For example, if the user query is doctor and the job title is for a nurse position you should return false." +
                $"User Query: '{userQuery}'" +
                $"Title: '{title}'" +
                "Respond with 'true' if the title is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating title: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateDescription(string userQuery, string description)
        {
            Console.WriteLine("Evaluating title");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's description is relevant to this query. For example, if the user query is nurse and the description is for a doctor role you should return false." +
                $"User Query: '{userQuery}'" +
                $"Description: '{description}'" +
                "Respond with 'true' if the description is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating description: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateResponsibilities(string userQuery, string responsibilities)
        {
            Console.WriteLine("Evaluating Responsibilities");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's responsibilities are relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Responsibilities: '{responsibilities}'" +
                "Respond with 'true' if the responsibilities are relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating responsibilities: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateRequirements(string userQuery, string requirements)
        {
            Console.WriteLine("Evaluating requirements");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's requirements are relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Responsibilities: '{requirements}'" +
                "Respond with 'true' if the requirements are relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating requirements: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateCompensationAndBenefits(string userQuery, string compensationAndBenefits)
        {
            Console.WriteLine("Evaluating requirements");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's compensation and benefits are relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Compensation and Benefits: '{compensationAndBenefits}'" +
                "Respond with 'true' if the compensation and benefits are relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating compensation and benefits: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateLocation(string userQuery, string location)
        {
            Console.WriteLine("Evaluating title");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's location is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Location: '{location}'" +
                "Respond with 'true' if the location is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating location: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateClinicName(string userQuery, string clinicName)
        {
            Console.WriteLine("Evaluating title");
            var chatMessage = new UserChatMessage(
                "Evaluate if this job's clinic name is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Clinic Name: '{clinicName}'" +
                "Respond with 'true' if the clinic name is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );
            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });
                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();
                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");
                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating clinic name: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateExperience(string userQuery, Experience experience)
        {
            Console.WriteLine("Evaluating experience");
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's experience is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Experience Employer: '{experience.Employer}'" +
                $"Experience Role: '{experience.Role}'" +
                $"Experience Description: '{experience.Description}'" +
                $"Experience Start: '{experience.Start}'" +
                $"Experience End: '{experience.End}'" +
                "Respond with 'true' if the experience is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });

                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();

                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");

                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating experience: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<bool> EvaluateCredentialsAndCertifications(string userQuery, Credential credential)
        {
            Console.WriteLine("Evaluating credentials: " + credential.Name + " - " + userQuery);
            var chatMessage = new UserChatMessage(
                "Evaluate if this candidate's credential is relevant to this query." +
                $"User Query: '{userQuery}'" +
                $"Credential Name: '{credential.Name}'" +
                $"Credential Issuer: '{credential.Issuer}'" +
                $"Credential Issue Date: '{credential.IssueDate}'" +
                $"Credential Expiration Date: '{credential.ExpirationDate}'" +
                "Respond with 'true' if the experience is relevant, 'false' if it is not, or 'Invalid' if the query is unclear."
            );

            try
            {
                // Send the chat message to the AI service
                var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });

                // Extract the response content
                var responseContent = response.Value.Content[0].Text.Trim();

                Console.WriteLine($"Response Content: {responseContent}");
                responseContent = responseContent.Replace("'", "\"");

                // Handle the AI response
                if (responseContent.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (responseContent.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response received: " + responseContent);
                    return false;
                    //throw new InvalidOperationException("The AI service returned an invalid response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while evaluating experience: {ex.Message}");
                throw; // Re-throw the exception for higher-level handling
            }
        }

        public async Task<List<CandidateFilters>> GenerateCandidateFilters(string userQuery)
        { 
            Console.WriteLine("Generating candidate filters");
            var chatMessage = new UserChatMessage(
                $"Based on the user's response: '{userQuery}', determine the candidate filters to apply." +
                " Respond in JSON format as an array of objects with the fields: 'FirstName', 'LastName', 'Email', 'Phone', 'State', 'Specialty', 'Occupation', 'Availability', 'CredentialsAndCertifications', 'Skills', & 'Experience'." +
                " Use '*' as the value for any filters that are not specified in the query." +
                "Example: userQuery = 'Show all candidates that live in Georgia'" +
                "Result: [{'FirstName': '*', 'LastName': '*', 'Email': '*', 'Phone': '*', 'State': 'Georgia', 'Occupation': '*', 'Specialty': '*', 'Skills': '*', 'Experience': '*', 'Availability': '*', 'CredentialsAndCertifications': '*'}]." +
                "If the user's response suggests that the candidates Skills, Experience, Credentials & Certifications, and/or Availability need to be reviewed assign 'true' for each" +
                "Example: userQuery = 'Find locum tenens physicians in Georgia with experience in emergency medicine and certifications in Advanced Cardiovascular Life Support (ACLS). Let me know if their skills or availability need further review.'" +
                "Result: [{'FirstName': '*', 'LastName': '*', 'Email': '*', 'Phone': '*', 'State': 'Georgia', 'Occupation': '*', 'Specialty': 'true', 'Skills': 'true', 'Experience': 'true', 'Availability': '*', 'CredentialsAndCertifications': 'true'}]." +
                " If the query does not specify any filters or is unclear, respond with 'Invalid'."
            );

            var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });

            var responseContent = response.Value.Content[0].Text;

            Console.WriteLine($"Response Content: {responseContent}");
            responseContent = responseContent.Replace("'", "\"");

            List<CandidateFilters> filters = new List<CandidateFilters>();

            try
            {
                if (!string.IsNullOrEmpty(responseContent) && !responseContent.Equals("Invalid", StringComparison.OrdinalIgnoreCase))
                {
                    // Deserialize JSON response into CandidateFilters list
                    var parsedFilters = JsonSerializer.Deserialize<List<CandidateFilters>>(responseContent);

                    foreach (var item in parsedFilters)
                    {
                        // Validate the parsed filters
                        if (!string.IsNullOrEmpty(item.FirstName) &&
                            !string.IsNullOrEmpty(item.LastName) &&
                            !string.IsNullOrEmpty(item.Email) &&
                            !string.IsNullOrEmpty(item.Phone) &&
                            !string.IsNullOrEmpty(item.State) &&
                            !string.IsNullOrEmpty(item.Occupation) &&
                            !string.IsNullOrEmpty(item.Skills) &&
                            !string.IsNullOrEmpty(item.Experience) &&
                            !string.IsNullOrEmpty(item.Availability) &&
                            !string.IsNullOrEmpty(item.Specialty) &&
                            !string.IsNullOrEmpty(item.CredentialsAndCertifications))
                        {
                            filters.Add(new CandidateFilters
                            {
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Email = item.Email,
                                Phone = item.Phone,
                                State = item.State,
                                Occupation = item.Occupation,
                                Specialty = item.Specialty,
                                CredentialsAndCertifications = item.CredentialsAndCertifications,
                                Experience = item.Experience,
                                Skills = item.Skills,
                                Availability = item.Availability
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Invalid filter object: {JsonSerializer.Serialize(item)}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid or empty response content.");
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Parsing error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing query: {ex.Message}");
            }

            Console.WriteLine("Availabilities.length: " + filters.Count);
            return filters;
        }

        public async Task<List<JobFilters>> GenerateJobFilters(string userQuery)
        {
            Console.WriteLine("Generating job filters");
            var chatMessage = new UserChatMessage(
                $"Based on the user's response: '{userQuery}', determine the job filters to apply." +
                " Respond in JSON format as an array of objects with the fields: 'Title', 'Description', 'Responsibilities', 'Requirements', 'CompensationAndBenefits', 'Availability', 'Location'" +
                " Use '*' as the value for any filters that are not specified in the query." +
                "Example: userQuery = 'Show all jobs that are looking for part time nurses'" +
                "Result: [{'Title': 'true', 'Description': 'true', 'Responsibilities': '*', 'Requirements': '*', 'CompensationAndBenefits': '*', 'Availability': 'true', 'Location': '*'" +
                "If the user's response suggests that the candidates Skills, Experience, Credentials & Certifications, and/or Availability need to be reviewed assign 'true' for each" +
                " If the query does not specify any filters or is unclear, respond with 'Invalid'."
            );

            var response = await _chatClient.CompleteChatAsync(new[] { chatMessage });

            var responseContent = response.Value.Content[0].Text;

            Console.WriteLine($"Response Content: {responseContent}");
            responseContent = responseContent.Replace("'", "\"");

            List<JobFilters> filters = new List<JobFilters>();

            try
            {
                if (!string.IsNullOrEmpty(responseContent) && !responseContent.Equals("Invalid", StringComparison.OrdinalIgnoreCase))
                {
                    // Deserialize JSON response into JobFilters list
                    var parsedFilters = JsonSerializer.Deserialize<List<JobFilters>>(responseContent);

                    foreach (var item in parsedFilters)
                    {
                        // Validate the parsed filters
                        if (!string.IsNullOrEmpty(item.Title) &&
                            !string.IsNullOrEmpty(item.Description) &&
                            !string.IsNullOrEmpty(item.Responsibilities) &&
                            !string.IsNullOrEmpty(item.Requirements) &&
                            !string.IsNullOrEmpty(item.CompensationAndBenefits) &&
                            !string.IsNullOrEmpty(item.Availability) &&
                            !string.IsNullOrEmpty(item.Location))
                        {
                            filters.Add(new JobFilters
                            {
                                Title = item.Title,
                                Description = item.Description,
                                Responsibilities = item.Responsibilities,
                                Requirements = item.Requirements,
                                CompensationAndBenefits = item.CompensationAndBenefits,
                                Availability = item.Availability,
                                Location = item.Location
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Invalid filter object: {JsonSerializer.Serialize(item)}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid or empty response content.");
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Parsing error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing query: {ex.Message}");
            }

            Console.WriteLine("Availabilities.length: " + filters.Count);
            return filters;
        }

        public async Task<List<Availability>> GenerateAvailability(string availability)
        {
            Console.WriteLine("Determining availability");
            var chatMessage = new UserChatMessage(
                $"Based on the user's response '{availability}', determine the days of the week and times they are available." +
                "Respond in JSON format as an array of objects with 'day', 'start', and 'end' fields." +
                "For example: [{'Day': 'Monday', 'Start': '09:00:00', 'End': '17:00:00'}]." +
                "If you cannot determine availability, respond with 'Invalid'."
            );

            var response = _chatClient.CompleteChat(new[] { chatMessage });

            var responseContent = response.Value.Content[0].Text;

            Console.WriteLine($"Response Content: {responseContent}");
            responseContent = responseContent.Replace("'", "\"");

            List<Availability> availabilities = new List<Availability>();

            try
            {
                if (!string.IsNullOrEmpty(responseContent))
                {
                    // Deserialize the JSON response into a list of parsed availability objects
                    var parsedAvailability = JsonSerializer.Deserialize<List<ParsedAvailability>>(responseContent);

                    // Map the parsed data to the Availability model
                    foreach (var item in parsedAvailability)
                    {
                        if (Enum.TryParse<Weekday>(item.Day, true, out Weekday dayOfWeek))
                        {
                            availabilities.Add(new Availability
                            {
                                CandidateId = 0, // Ensure you pass a valid candidateId here
                                DayofWeek = dayOfWeek,
                                StartTime = TimeSpan.Parse(item.Start),
                                EndTime = TimeSpan.Parse(item.End)
                            });
                        }
                    }
                }
                else
                {
                    // Handle the case where no valid response content is returned
                    Console.WriteLine("Invalid or empty response content.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing availability: {ex.Message}");
            }

            //foreach (var item in availabilities)
            //{
            //    Console.WriteLine($"Availability Day: {item.DayofWeek}");
            //    Console.WriteLine($"Availability Start: {item.StartTime}");
            //    Console.WriteLine($"Availability End: {item.EndTime}");
            //}

            // Some type of parsing so that I can create a list of availabilities based on the GPT's response.
            Console.WriteLine("Availabilities.length: " + availabilities.Count);
            return availabilities;
        }

        public async Task<string> DetermineStateEntryAsync(string userResponse)
        {
            var chatMessage = new UserChatMessage(
                $"Based on the user's response '{userResponse}', what state (in the United States) does this person live in? " +
                "If you can determine a valid state from the input, simply respond with the name of the state. " +
                "For example, if the user response is 'GA', 'georgia', then you should respond with 'Georgia'. " +
                "If you can't determine a valid state based on the user's response, respond with 'Invalid'.",
                userResponse
            );

            var chatStream = _chatClient.CompleteChatStreamingAsync(new[] { chatMessage });

            string state = string.Empty;
            await foreach (var chatUpdate in chatStream)
            {
                foreach (var contentPart in chatUpdate.ContentUpdate)
                {
                    state += contentPart.Text;
                }
            }

            return state.Trim();
        }

        public void RunExample()
        {
            ChatCompletion completion = _chatClient.CompleteChat(
                [
                    new SystemChatMessage("You are a helpful assistant that talks like a Count Dracula."),
                    new UserChatMessage("Does Azure OpenAI support customer managed keys?"),
                    new AssistantChatMessage("Yes, customer managed keys are supported by Azure OpenAI"),
                    new UserChatMessage("Do other Azure AI services support this too?")
                ]);
            Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
            _logger.LogInformation($"{completion.Role}: {completion.Content[0].Text}");
           
        }
    }
}
