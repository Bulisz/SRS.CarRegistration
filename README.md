# SRS.CarRegistration

USE master;
IF (db_id('CarRegistration') is null)
  CREATE DATABASE CarRegistration;
  GO

USE CarRegistration;
CREATE TABLE Owners (
    OwnerID INT IDENTITY(1,1) PRIMARY KEY,
    LastName NVARCHAR(255),
    FirstName NVARCHAR(255),
    BirthDate DATE
);

CREATE TABLE Cars (
    CarID INT IDENTITY(1,1) PRIMARY KEY,
    Brand NVARCHAR(255),
    Model NVARCHAR(255),
    RegistrationNumber NVARCHAR(10),
    ProductionDate DATE
);

CREATE TABLE CarOwner (
    CarOwnerID INT IDENTITY(1,1) PRIMARY KEY,
    OwnerID INT FOREIGN KEY REFERENCES Owners(OwnerID),
    CarID INT FOREIGN KEY REFERENCES Cars(CarID)
);

#

CREATE PROCEDURE GetCarsByOwnerId
    @OwnerID INT
AS
BEGIN
    SELECT Cars.*
    FROM Cars
    INNER JOIN CarOwner ON Cars.CarID = CarOwner.CarID
    WHERE CarOwner.OwnerID = @OwnerID;
END;
