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

## What I learned
During this assessment, I learned the fundementals of blazor, this was my first time using blazor so there was a lot to learn, I am still not confident in my blazor knowledge, but thanks to this 
assessment, I have gained some exposure, and now that I have used it for the first time, I now have somewhere to start my journey and now I can improve from where I started off and build 
more projects in the future to gain more experience. I also had problems with MSSQL server management stuidio on my device, and this lead me to learn how to use SQL server directly on Visual Studio,
it was intersting, and a lot simpler than I expected it to be. Since this assessment was challengin, I can now take the opportunity to work on it further in my own time and further imporve my skills with blazor and .NET.


## What was hardest for me
The hardest thing for me was modifying the database with my blazor application. I managed to connect the database to the app, but when it came to adding new products and deleting products I really struggled.
I managed to to connect my form to the categories table(you can see this when you use the drop down, it displays the categories in the database), however when I tried to add new products, 
the products couldn't save to the database for some reason, same as when I tried to delete products, the products wouldn't delete from the database. I stil don't know why my form isn't working because when i manually add the product to the code, 
it saves to the database, however when i try to make it so that the form accepts user input, then it doesn't register as a string and pops up the error message saying that the the product fieled can't be empty even when the user inputs a product.
I tried everything I could but the problem persisted.

Another part that I struggled with was implementing Bootstrap/Mudblazor for design, I kept running into errors even after I instlled all the dependencies, hence I was unable to add some nice styling.
However even with all these problems, I will take them as a challenge to further develop my skills, since it showed me some new things to learn.

