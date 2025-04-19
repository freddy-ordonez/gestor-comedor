CREATE DATABASE [comedor-infantil-01];
GO

USE [comedor-infantil-01];
GO

CREATE TABLE Beneficiaries(
	[BeneficiaryId] INT PRIMARY KEY IDENTITY(1,1),
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Status] VARCHAR(10) NOT NULL
);
GO

CREATE TABLE Inventory(
	[InventoryId] INT PRIMARY KEY IDENTITY(1,1),
	[ProductName] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[Quantity] INT NOT NULL,
	[EntryDate] DATE NOT NULL,
	[ExpiryDate] DATE NOT NULL
);
GO

CREATE TABLE Activities(
	[ActivityId] INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NOT NULL
);
GO

CREATE TABLE Donors(
	[DonorId] INT PRIMARY KEY IDENTITY(1,1),
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[DonorType] VARCHAR(50) NOT NULL,
	[Phone] VARCHAR(8) NOT NULL,
	[Address] VARCHAR(255) NOT NULL
);
GO

CREATE TABLE TypeIdentifications(
	[TypeIdentificationId]	INT PRIMARY KEY IDENTITY(1,1),
	[TypeIdentification]	VARCHAR(50) NOT NULL,
	[Status]				VARCHAR(20) NOT NULL
);
GO

CREATE TABLE Volunteers(
	[VolunteerId] INT PRIMARY KEY IDENTITY(1,1),
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[Identification] VARCHAR(20) NOT NULL,
	[TypeIdentification] int NOT NULL,
	[Phone] VARCHAR(8) NOT NULL,
	[Availability] VARCHAR(150) NOT NULL,
	[Status] VARCHAR(20) NOT NULL
);
GO

CREATE TABLE AssignmentActivities(
	[AssignmentId]		INT IDENTITY(1,1) PRIMARY KEY,
	[VolunteerId]		INT NOT NULL,
	[ActivityId]		INT NOT NULL,
	[AssignmentDate]	DATETIME NOT NULL
	UNIQUE		(VolunteerId, ActivityId),
	FOREIGN KEY (VolunteerId)	REFERENCES Volunteers(VolunteerId),
	FOREIGN KEY (ActivityId)	REFERENCES Activities(ActivityId)
);
GO

CREATE TABLE MoneyDonations(
	[MoneyDonationId]	INT PRIMARY KEY IDENTITY(1,1),
	[DonorId]			INT NOT NULL,
	[Amount]			DECIMAL(10,2) NOT NULL,
	[DonationDate]		DATE NOT NULL,
	[Porpuse]			VARCHAR(255) NOT NULL,
	FOREIGN KEY (DonorId) REFERENCES Donors(DonorId)
);
GO

CREATE TABLE InKindDonations(
	[InKindDonationId]	INT IDENTITY(1,1) PRIMARY KEY,
	[DonorId]			INT NOT NULL,
	[ProductId]			INT NOT NULL,
	[DonationDate]		DATE NOT NULL,
	UNIQUE (ProductId),
	FOREIGN KEY (DonorId)	REFERENCES Donors(DonorId),
	FOREIGN KEY (ProductId) REFERENCES Inventory(InventoryId)
);
GO

CREATE TABLE Users(
	[UserId]	INT PRIMARY KEY IDENTITY(1,1),
	[Email]		VARCHAR(100) NOT NULL,
	[Password]	NVARCHAR(MAX) NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName]	VARCHAR(50) NOT NULL,
	[Status]	VARCHAR(20) NOT NULL
);
GO

CREATE TABLE Audits(
	[AuditId] INT PRIMARY KEY IDENTITY(1,1),
	[UserId] INT NOT NULL,
	[Action] CHAR NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[ActionDate] DATETIME NOT NULL
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
 );
GO

CREATE TABLE Modules(
	[ModuleId] INT PRIMARY KEY IDENTITY(1,1),
	[ModuleName] VARCHAR(100) NOT NULL,
	[ClassCSS] VARCHAR(100) NOT NULL,
	[Link] VARCHAR(50) NOT NULL
);

CREATE TABLE ModulesForUser(
	[ModuleForUserId]	INT		IDENTITY(1,1)	PRIMARY KEY,
	[ModuleId]			INT		NOT NULL,
	[UserId]			INT		NOT NULL,
	UNIQUE (ModuleId, UserId),
	FOREIGN KEY (ModuleId) REFERENCES Modules(ModuleId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);


INSERT INTO TypeIdentifications (TypeIdentification, [Status]) 
VALUES	('Cédula Jurídica', 'Activo'),
		('DIMEX', 'Activo'),
		('NITE', 'Activo'),
		('Pasaporte', 'Activo'),
		('Cédula Física', 'Activo');