# HNG12 Backend - Public API

Welcome to the Stage 1 Backend task for HNG12! This project provides a public API that returns information about a number in JSON format. The API responds with some  information about a given number.

## Project Description

This is a simple API developed in C# using Asp.Net core Minimal Api workload that does the following when accessed:

🔥 **Number**: Returns the number

🔥 **Properties**: Returns properties of the number in an array, if they are even or odd, and an armstrong number

🔥 **Prime**: Returns a boolean value indicating if the number is a prime number or not

🔥 **Perfect**: Returns a boolean value indicating if the number is a perfect number or not

🔥 **Digit Sum**: Returns the sum of the digits of the number



### Features
- **CORS Handling**: The API is configured to allow cross-origin requests.
- **Response Format**: All responses are returned in JSON format.
- **Fast Response Time**: The response time for the API is under 500ms.

## Setup Instructions

### Prerequisites
- [.NET SDK 9.0 or higher](https://dotnet.microsoft.com/download/dotnet) should be installed.
- A code editor such as [Visual Studio Code](https://code.visualstudio.com/) is recommended.

### Running Locally
```bash
      1. Clone the repository to your local machine:
         `git clone https://github.com/lankiman/hng-stage-1.git`
         `cd hng-stage-1`
      
      2. Restore project dependencies:
         `dotnet restore`
      
      3. Run the application locally:
         `dotnet run`
      
         This will run the API on your local machine. By default, it will be accessible at `http://localhost:5000`.
      
      4. Test the API by sending a GET request to the following endpoint:
         `http://localhost:5800`
```
You should receive a JSON response with the following data:
```json
      {
        "email": "user@example.com",
        "current_datetime": "2025-01-30T09:30:00Z",
        "github_url": "https://github.com/user/project"
     }
```
## API Documentation

### Endpoint: `GET /base-url/classify-number?number=371`


This endpoint returns the following data:
Status Code 200
```json
{
  "number": 371,
  "is_prime": false,
  "is_perfect": false,
  "properties": ["armstrong", "odd"],
  "digit_sum": 11,  
  "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371" 
}
```

The endpoint also returns the following data if number is not provided:
Status Code 400
```json
 {
  "message" :"required endpoint parameter number not provided, please provide one",
  "error" :true
}
```

The endpoint also returns the following data if number provided is not a valid integer:
Status Code 400
```json
 {
  "number" :"alphabet",
  "error" :true
}
```

To get the response, simply send a GET request to the root endpoint (`/classify-number?number=371`).

[Stage-1 Task](https://hng-stage-1-lankiman.up.railway.app/api/classify-number?number=28)

## Backlink

This project was developed using C# as the backend language. If you are interested in hiring C# developers, check out the following link:
[HNG Tech - Hire C# Developers](https://hng.tech/hire/csharp-developers)

## Documentation

This repository contains the code for the Stage 0 Backend task of the HNG12 program. The goal of this project is to build a simple public API that returns basic information, such as your registered email, the current datetime in UTC format, and the GitHub URL for the project repository.

### Project Structure

- **Program.cs**: The main entry point for the application. It sets up the web API with routes and CORS handling.
- **appsettings.json**: Configuration file (if applicable in future updates).
- **README.md**: This documentation file with setup instructions and details about the API.

### Technologies Used

- **C#**: The programming language used to build the API.
- **.NET 9.0**: The runtime used for building the application.
- **ASP.NET: The .NET web framework used in building the application
- **JSON**: The response format for the API.

## Deployment

This API is deployed to Railway using a docker image and is accessible at the following publicly available endpoint:

[Stage-1 Task](https://hng-stage-1-lankiman.up.railway.app/api/classify-number?number=28)

You can access the live version of the API by visiting the provided URL.

The API is configured to have a fast response time (less than 500ms), and it is CORS-compliant for cross-origin requests.



