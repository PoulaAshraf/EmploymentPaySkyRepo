PaySky Employment System
Overview
This project is a .NET Core solution with a SQL Server database. The project uses the Code First approach to generate the database schema from C# classes.

Prerequisites
Before running the project, ensure that you have the following installed on your system:

.NET Core SDK 
SQL Server 

Getting Started
To get started with the project, follow these steps:

1- Clone the repository from GitHub:
  https://github.com/PoulaAshraf/EmploymentPaySkyRepo
2- Open the project in Visual Studio 2022
3- Update the connection string in the appsettings.json file to point to your SQL Server instance:
  {
  "ConnectionStrings": {
    "MyDbContext": "Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
  }

  Replace (localdb)\\mssqllocaldb with the name of your SQL Server instance and MyDatabase with the name of your database.
4- Run the following command in the Nuget Package Console to create the database 
  update-database 
  Note: Migration files attached in project
5- In Project we have Script folder which has a sql query to archive expired vacancies so, you can execute it in your database and create daily job for it 
6- Project has Postman collection which has Request samples and its response 
