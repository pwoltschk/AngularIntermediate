# Contributing to PragmaticCleanArchitecture

Thank you for your interest in contributing to PragmaticCleanArchitecture!  
We're excited to collaborate with you and see how, together, we can improve and evolve this project.

## Getting Started

If this is your first time here, a great way to begin is by tackling issues labeled "help wanted" or "good first issue." These issues are selected to help you get familiar with the project while making an impactful contribution right away.

### Before Raising Large PRs

For significant changes, please raise an issue first to discuss the proposal. This helps ensure that your idea aligns with the project's direction and saves time for both you and the maintainers.

## Requirements

To contribute to this project, make sure you have the following tools installed:

- **.NET SDK 8**
- **MSSQL**

## Setting Up the Development Environment

To set up the development environment, follow these steps:

1. **Database Setup**: 
   - This project uses Entity Framework Core migrations. When you run the application for the first time, migrations will be applied automatically to set up the database schema. This allows you to get started without manual database setup.

2. **Running the Application**:
   - Run the application with:
     ```bash
     dotnet run
     ```
   - This will start both the Blazor frontend and the backend API locally. You can access the app at:
     - [http://localhost:5000](http://localhost:5000) for HTTP
     - [https://localhost:5001](https://localhost:5001) for HTTPS

## Contribution Principles

When contributing, please keep these principles in mind to align with our project’s goals:

- **Best Practices**: We aim for PragmaticCleanArchitecture to be a reference example of best practices in .NET and clean architecture. Contributions should follow these established patterns.
- **Selective Tool and Library Use**: We strive to use a realistic set of tools and libraries, rather than showcasing every possible option. New tools should fit well with the existing architecture and goals.
- **Architectural Integrity**: We welcome refactoring and improvements, as long as they’re justified and enhance the overall design. Major architectural changes should be backed by clear reasons, such as improvements in maintainability or performance.
- **Reliability and Scalability**: Contributions that enhance reliability and scalability are greatly appreciated. These could include improvements to error handling, data access, or other aspects that make the project more robust.
- **Performance Enhancements**: We support contributions that enhance performance. Please include any relevant benchmarks or metrics to show the impact of your changes.

## Spot a Typo?

Typos and small fixes are valuable to us—no contribution is too small! If you find something minor, feel free to open a Pull Request directly. For these types of changes, there’s no need to open a separate issue.

## Have a Suggestion?

If you have an idea to enhance PragmaticCleanArchitecture, please open an issue with the following details:

- A clear title and description of your suggestion
- Any relevant examples, diagrams, or mockups
- Indicate if you’re interested in implementing the feature yourself

We’ll review your suggestion and discuss its potential inclusion in the project.

## Code of Conduct

To maintain a positive and welcoming environment for everyone, please follow our [Code of Conduct](CODE_OF_CONDUCT.md). Respectful collaboration is essential for a successful project.
