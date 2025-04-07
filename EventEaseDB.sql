--DATABASE CREATION 
USE master;
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EventEaseDB')
DROP DATABASE EventEaseDB;
CREATE DATABASE EventEaseDB;

USE EventEaseDB;

--TABLE VENUE CREATION 
CREATE TABLE [Venue] (
	VenueId  INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	VenueName	NVARCHAR(250) NOT NULL,
	Location	NVARCHAR(255) NOT NULL,
	Capacity 	INT NOT NULL,
	ImageUrl	NVARCHAR(2083) UNIQUE NOT NULL
);

--CREATE EVENT TABLE 
CREATE TABLE [Event] (
	EventId INT IDENTITY (1,1) PRIMARY KEY,
	VenueId INT NOT NULL,
	EventName NVARCHAR(100) NOT NULL,
	EventDate DATE NOT NULL,
	Description NVARCHAR(100) NOT NULL,
	CONSTRAINT FK_Event_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueId)
);

--CREATE BOOKING TABLE
CREATE TABLE [Booking] (
	BookingId INT IDENTITY (1,1) PRIMARY KEY,
	VenueId INT NOT NULL,
	EventId INT NOT NULL,
	BookingDate DATE NOT NULL,
	CONSTRAINT FK_Booking_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueId),
	CONSTRAINT FK_Booking_Event FOREIGN KEY (EventId) REFERENCES [EVENT](EventId)
);

--Insert data into Venue
INSERT INTO [Venue] (VenueName, Location, Capacity, ImageUrl)
VALUES ('White Gardens', 'Ruimsig', 500, 'https://example.com/images/venue1.jpg'),
       ('Caesar Palace', 'Gauteng', 850, 'https://example.com/images/venue2.jpg');

--Insert data into EVENT 
INSERT INTO [Event] (VenueId, EventName, EventDate, Description)
VALUES (1, 'Whiter Wedding', '2025-06-05', 'A winter wonderland themed wedding'),
(2, 'Summer Music Fest', '2025-10-15', 'An open-air summer music festival');

--Insert data into Booking 
INSERT INTO [Booking] (VenueId, EventId, BookingDate)
VALUES (1, 1, '2025-01-01'),
(2, 2, '2025-11-01');

SELECT * FROM [Venue];
SELECT * FROM [Event];
SELECT * FROM [Booking];

--Drop Tables (if needed)
--DROP TABLE Venue;
--DROP TABLE [EVENT];
--DROP TABLE Booking;