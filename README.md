# Daklapack assessment

A small full-stack shipment monitoring application created with ASP.NET Core 10 Web API and Angular 19 with the Material UI library.

The API currently uses an in-memory list of shipments and has CORS configured, OpenAPI support and global exception handling for unhandled exceptions.

Paging and filtering of results are also provided.

As per the assessment wording, class types are used throughout, but in some cases a record type would also be suitable.

CORS is enabled to only allow the default port for an Angular application, and due to the nature of this assessment only GET requests are permitted.

No any types are used for shipment data or HTTP responses.

## Architecture

The application follows a layered architecture.

```
Angular Component
       |
       v
Angular Service
       |
       v
REST API Controller
       |
       v
IShipmentService
       |
       v
Shipment Service
       |
       v
In-memory shipment model / data list
```

The API is separated into

* Models — define the shipment domain model.
* Services — contain shipment-related business/data access logic.
* Controllers — expose the REST API and handle HTTP concerns.

The controller depends on IShipmentService through dependency injection rather than creating the service itself.

The Angular application is separated into

* Models — strongly typed TypeScript interfaces.
* Services — responsible for HTTP communication.
* Components — responsible for reusable UI components.
* Pages - responsible for the screens displayed to users.
* Environments - files specific to a particular environment

## Prerequisites and setup:

* .NET 10 SDK
* Node.JS
* Angular 19 CLI
* An IDE or code editor, e.g. VS Code, Visual Studio, JetBrains Rider

## Run the application:
  
  1. Clone the GitHub repository
  
  2. Navigate to the API

  ```bash
  cd Shipment-Api
  dotnet restore
  dotnet run
  ```

  3. Navigate to the Angular project

  ```bash
  cd shipment-client
  npm install
  ng serve -o
  ```
