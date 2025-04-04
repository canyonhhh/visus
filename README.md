# Visus
QR code-based attendance tracking for youth centers.

## Overview
Visus is a full-stack application built with .NET 9 Aspire and React. It provides QR code-based attendance tracking for youth centers, allowing for efficient management of participant attendance.

## Technology Stack
- **.NET 9**: C# backend with Aspire for cloud-native application development
- **PostgreSQL**: Database for persistent storage
- **Entity Framework Core**: ORM for database operations
- **ASP.NET Identity**: Authentication and authorization
- **JWT**: Token-based authentication
- **Swagger**: API documentation
- **React 19**: Frontend UI library
- **Webpack**: Frontend build tool
- **Docker**: Containerization for deployment

## Project Structure
The solution consists of multiple projects:
- **visus.ApiService**: Backend API service with auth endpoints
- **visus.AppHost**: Aspire project orchestrator 
- **visus.Data**: Data access layer and identity models
- **visus.Frontend**: React frontend
- **visus.MigrationService**: Database migration service
- **visus.Models**: Shared models and DTOs
- **visus.ServiceDefaults**: Shared service configuration
- **visus.Tests**: Test project

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (version 20.12 or later)
- [Docker](https://www.docker.com/products/docker-desktop) for running PostgreSQL and containerized applications

## Getting Started
Follow these steps to get the application running locally:

### 1. Clone the Repository
```bash
git clone https://github.com/canyonhhh/visus
cd visus
```

### 2. Configure Secrets (Development)
Set up JWT secrets for development:
```bash
dotnet user-secrets init --project visus.ApiService
dotnet user-secrets set "JWT:Secret" "YourDevelopmentSecretKey1234567890123456789012" --project visus.ApiService
dotnet user-secrets set "JWT:ValidIssuer" "https://localhost" --project visus.ApiService
dotnet user-secrets set "JWT:ValidAudience" "https://localhost" --project visus.ApiService
dotnet user-secrets set "JWT:ExpirationInDays" "7" --project visus.ApiService
```

### 3. Set Up the Frontend
Navigate to the frontend directory and install dependencies:
```bash
cd visus.Frontend
npm install
```

### 4. Run with .NET Aspire
The application is designed to run with .NET Aspire, which coordinates all services, including:
- PostgreSQL database
- API service
- Migration service
- Frontend application

From the root directory, run:
```bash
dotnet run --project visus.AppHost
```

This command will:
- Start the PostgreSQL container
- Apply database migrations
- Start the API service
- Build and run the frontend

The Aspire dashboard will open in your browser, showing the status of all services. You can access the frontend application and API from there.

## Database Migrations
Migrations are handled automatically by the MigrationService when running with Aspire.

To manually create a new migration after modifying entity models:
```bash
dotnet ef migrations add <MigrationName> --project visus.Data --startup-project visus.MigrationService
```

When adding ASP.NET Identity or making significant schema changes, you may need to create a fresh migration:

```bash
# Remove existing migrations (if starting over)
dotnet ef migrations remove --project visus.Data

# Add new migration
dotnet ef migrations add InitialCreate --project visus.Data
```

## Testing
Run the tests using:
```bash
dotnet test
```

## Git Hooks
The project includes Git hooks for code formatting:
- Pre-commit hook: Runs `dotnet format` to ensure consistent code style

To install Husky:
```bash
dotnet tool restore
```
