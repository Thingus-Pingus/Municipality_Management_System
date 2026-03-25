# Municipality_Management_System

## Overview
The Municipality Management System is a web-based application developed using ASP.NET Core MVC to streamline the management of municipal operations. The system enables efficient handling of citizen data, service requests, staff administration, and report tracking.

It provides a centralized platform that improves transparency, reduces manual processes, and enhances communication between citizens and municipal staff.

## Project Timeline
- Developed: March 2025 – April 2025  

## Updates
- Refactored and prepared for portfolio use in 2026 

---

## Technologies Used
- ASP.NET Core MVC  
- Entity Framework Core (Code-First)  
- SQL Server  
- C#  
- Razor Views  

---

## Features

### Citizen Management
- Register and manage citizen information  
- Update and maintain records  

### Service Request Management
- Submit service requests  
- Track request status (Pending, In Progress, Resolved)  

### Staff Administration
- Manage staff profiles, roles, and departments  

### Report Management
- Submit and track citizen reports/complaints  
- Review and update report statuses  

### System Features
- Full CRUD operations across all entities  
- Input validation and error handling  
- Protection against SQL injection using EF Core  

---

## Database Design
The system is based on a relational database with the following core entities:

- **Citizens**
- **ServiceRequests**
- **Staff**
- **Reports**

### Relationships
- One Citizen → Many Service Requests  
- One Citizen → Many Reports  

The database is fully normalized with proper primary and foreign key constraints.

---

## Entity Relationship Diagram (ERD)
The ERD illustrating the database design and relationships can be found in the `/Docs` folder.

---

## Database Setup (Migrations)
This project uses **Entity Framework Core Code-First Migrations** to generate the database schema.

## Setup DB Connection String
1. Open the project in Visual Studio
2. Navigate and open the `appsettings.json` file
3. Change the `Server=ServerName` name to your SQL Server

<img width="1040" height="72" alt="image" src="https://github.com/user-attachments/assets/884c5eff-0e34-4097-88f1-71b4ea02ab72" />

### Steps to create the database:

1. Open the project in Visual Studio  
2. Open **Package Manager Console**  
3. Run the following command:

```bash
Update-Database
```

<img width="918" height="258" alt="image" src="https://github.com/user-attachments/assets/e5a211cc-4c65-410c-b2db-d927ce95465c" />

This will:
- Create the database
- Apply all migrations
- Generate all tables and relationships automatically

---

## Testing & Debugging
The system was tested for:
- Functional correctness (CRUD operations)
- Usability and navigation
- Performance under multiple requests
- Security (including SQL injection prevention)

## Issues Resolved
- Form submission errors (400 Bad Request)
- Foreign key relationship issues
- Null reference exceptions
- Incorrect routing and view loading
- Data persistence issues
