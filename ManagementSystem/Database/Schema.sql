CREATE DATABASE MunicipalityDB;

CREATE TABLE Citizen(
	CitizenId INT PRIMARY KEY,
	FullName VARCHAR(50),
	Address VARCHAR(50),
	PhoneNumber INT,
	Email VARCHAR(100) UNIQUE,
	DateOfBirth DATE,
	RegistrationDate DATE
);

CREATE TABLE Report(
	ReportId INT PRIMARY KEY,
	CitizenId INT,
	ReportType VARCHAR(50),
	Details VARCHAR,
	SubmissionDate DATE,
	Status VARCHAR DEFAULT 'Under Review',
	FOREIGN KEY (CitizenId) REFERENCES Citizen(CitizenID)
);

CREATE TABLE ServiceRequest(
	RequestId INT PRIMARY KEY,
	CitizenId INT,
	ServiceType VARCHAR(50),
	RequestDate DATE,
	Status VARCHAR(50) DEFAULT 'Pending'
	FOREIGN KEY (CitizenId) REFERENCES Citizen(CitizenID)
);

CREATE TABLE Staff (
	StaffId INT PRIMARY KEY,
	FullName VARCHAR(50),
	Position VARCHAR(50),
	Department VARCHAR(50),
	Email VARCHAR UNIQUE,
	PhoneNumber INT,
	HireDate DATE
);