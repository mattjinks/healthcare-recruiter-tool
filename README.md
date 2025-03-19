# Healthcare Recruiter Tool

## **Demo**: https://youtu.be/orYaOjswD-g

## Description

Match healthcare professionals with clinician roles

## Key Features

**AI-Powered Matching:** Automatically match healthcare professionals to clinician roles based on skills, credentials, requirements, etc.

**Natural Language Search:** Search for candidates or positions using conversational queries for a more user-friendly experience.

## Technologies Used

**Microsoft Tech Stack**

- .NET
- C#
- Azure OpenAI for natural-language queries
- Azure SQL Database for data storage
- Azure App Service for hosting
- Visual Studio for development and debugging

## Areas for Improvement

**Performance Optimization:** Currently, every search triggers a database query, which can slow down the application. Implementing a caching mechanism (e.g., Redis or in-memory caching) could significantly improve search speed by reducing redundant database calls.

**Enhanced Front-End Development:** Transitioning to a modern front-end framework like React or Angular would enable a component-based architecture, making the front-end more organized, scalable, and easier to maintain.
