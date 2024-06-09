## Introduction

This is an ASP.NET Core WebAPI project using .NET 8. This project provides a robust and scalable framework for building Web APIs and Micro services

# Getting Started

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later

## Installation

1. Clone the repository

```
git clone [clone_url]/_git/[your_repo]
```
2. Navigate to the repository directory

```
cd [repository_directory]
```

3. Restore the packages
```
dotnet restore
```
# Build and Test

1. To build the project, navigate to the project directory and run the following command:

```
dotnet build
```

2. To run the tests, use the following command. Path to the test project is needed if you are running the tests from outside the test project directory.

```
 dotnet test .\tests\UnitTests\Microservice.UnitTests\ --no-build

 dotnet test .\tests\IntegrationTests\Microservice.IntegrationTests\ --no-build
```

3. To run the application, use the following command:

```
dotnet run --project .\src\Startup\Microservice\
```

if this project is part of .NET Aspire based development, include this in the .NET Aspire AppHost project
and run the host app.

```
dotnet run --project .\src\AppHost\Microservice.AppHost\
```