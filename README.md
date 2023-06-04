# Lean Onion Architecture in ASP.NET Core

This repository demonstrates the complete implementation of the Onion Architecture in ASP.NET Core. It is one of the few example repositories in the .NET space that consistently follows this pattern. The Onion Architecture in ASP.NET Core promotes a clear separation of concerns, enhancing the maintainability and testability of the code. By using the Onion Architecture in ASP.NET Core, developers can create scalable and robust applications.

## Features

- **Best Practice Example of Lean and Efficient IdentityServer Authorization**: This project provides a lean and efficient example of authorization using IdentityServer. It showcases how IdentityServer can be integrated into ASP.NET Core to ensure secure and scalable authentication and authorization. The lean IdentityServer example demonstrates best practices for implementing IdentityServer in a .NET Core application.
- **Minimal Example of Role-Based Authorization in .NET**: A minimal example of role-based authorization in .NET Core. It demonstrates how claims and permissions can be assigned to roles to implement flexible and secure access control. This example of role-based authorization in .NET Core is designed to be simple yet effective, making it easy to understand and implement in your own projects.

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/pwoltschk/PragmaticCleanArchitecture
    ```
2. Navigate to the project directory:
    ```bash
    cd your-repository
    ```
3. Install the dependencies:
    ```bash
    dotnet restore
    ```

## Usage

1. Start the application:
    ```bash
    dotnet run
    ```
2. Open your browser and navigate to `https://localhost:5001`.

## Detailed Explanation

### Onion Architecture in ASP.NET Core

The Onion Architecture in ASP.NET Core is a powerful design pattern that helps in creating maintainable and testable applications. By structuring your application with the Onion Architecture, you can ensure that the core business logic is isolated from external dependencies. This repository serves as an excellent example of how to implement the Onion Architecture in ASP.NET Core, providing a clear and concise guide for developers.

### Role-Based Authorization in .NET Core

Role-based authorization in .NET Core is a crucial aspect of modern application security. This repository includes a minimal example of role-based authorization in .NET Core, demonstrating how to use IdentityRole and IdentityUser to manage roles and permissions. By following this example, you can implement a robust role-based authorization system in your own ASP.NET Core projects.

### Lean IdentityServer Example

Implementing IdentityServer in a .NET Core application can be complex, but this repository provides a lean IdentityServer example that simplifies the process. By following the best practices outlined in this example, you can integrate IdentityServer into your ASP.NET Core application efficiently and securely. This lean IdentityServer example is designed to be easy to follow and implement, making it an invaluable resource for developers.

## Inspiration

This project was inspired by:

- [AngularASPNetCoreBusinessApplications](https://github.com/KevinDockx/AngularASPNetCoreBusinessApplications/blob/master/Finished%20sample/TourManagement.API/Entities/AuditableEntity.cs) by Kevin Dockx
- [AspNetCoreSpa](https://github.com/fullstackproltd/AspNetCoreSpa/blob/e98a1494686e87b384a1d1b868af80f6dd2bd7df/src/Infrastructure/Infrastructure/ServicesExtensions.cs) by Fullstackpro Ltd
- [MediatR.Useful.Behaviors](https://github.com/EngRajabi/MediatR.Useful.Behaviors/blob/develop/src/MediatR.Useful.Behavior/Behavior/PerformanceBehaviour.cs) by Eng Rajabi
- [NDC London 2021 Workshop](https://github.com/jasontaylordev/ndc-london-2021-workshop) by Jason Taylor

## Contributing

Contributions are welcome! Please create a pull request or open an issue to suggest improvements. By contributing to this project, you can help improve the implementation of the Onion Architecture in ASP.NET Core and the examples of role-based authorization in .NET Core.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

I hope this version meets your needs! If you need further adjustments or additional content, just let me know. 😊