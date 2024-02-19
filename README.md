## SQL rész:

USE master;<br/>
IF (db_id('CarRegistration') is null)<br/>
  &emsp;CREATE DATABASE CarRegistration;<br/>
  &emsp;GO<br/>
<br/>
USE CarRegistration;<br/>
CREATE TABLE Owners (<br/>
    &emsp;OwnerID INT IDENTITY(1,1) PRIMARY KEY,<br/>
    &emsp;LastName NVARCHAR(255),<br/>
    &emsp;FirstName NVARCHAR(255),<br/>
    &emsp;BirthDate DATE<br/>
);<br/>
<br/>
CREATE TABLE Cars (<br/>
    &emsp;CarID INT IDENTITY(1,1) PRIMARY KEY,<br/>
    &emsp;Brand NVARCHAR(255),<br/>
    &emsp;Model NVARCHAR(255),<br/>
    &emsp;RegistrationNumber NVARCHAR(10),<br/>
    &emsp;ProductionDate DATE<br/>
);<br/>
<br/>
CREATE TABLE CarOwner (<br/>
    &emsp;CarOwnerID INT IDENTITY(1,1) PRIMARY KEY,<br/>
    &emsp;OwnerID INT FOREIGN KEY REFERENCES Owners(OwnerID),<br/>
    &emsp;CarID INT FOREIGN KEY REFERENCES Cars(CarID)<br/>
);

#

CREATE PROCEDURE GetCarsByOwnerId<br/>
    &emsp;@OwnerID INT<br/>
AS<br/>
BEGIN<br/>
    &emsp;SELECT Cars.*<br/>
    &emsp;FROM Cars<br/>
    &emsp;INNER JOIN CarOwner ON Cars.CarID = CarOwner.CarID<br/>
    &emsp;WHERE CarOwner.OwnerID = @OwnerID;<br/>
END;
