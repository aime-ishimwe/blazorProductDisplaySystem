## Product Display System
This is a simple Blazor Server application that shows a list of products from a database. 

Github repo: https://github.com/aime-ishimwe/blazorProductDisplaySystem
## How to run application

## Prerequisites
Make sure the following are installed on your machine:

.NET 7.0 SDK or later

SQL Server (or SQL Server Express)

Visual Studio 2022+ with:
  ASP.NET and web development workload
  SQL Server Data Tools

## Instructions
 1. Download the zipped folder, and extract all the files.
 2. Open the project with Visual Studio. 
 3. Open SQL Server Management Studio (SSMS), and run the following SQL to create and populate your tables:
    
     CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

4. Connect you Database to the project with a connection string e.g:"Server=YOUR_SERVER_NAME;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;".
   
6. Run the app in visual studio.



