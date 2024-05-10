CREATE DATABASE CompanyDb

USE CompanyDb;


CREATE TABLE Department (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName VARCHAR(50) NOT NULL
);

CREATE TABLE Employee (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PIN VARCHAR(20) UNIQUE NOT NULL,
    EmployeeName VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    ActiveStatus BIT NOT NULL CHECK (ActiveStatus IN (0,1)),
    DepartmentID INT NOT NULL,
    CONSTRAINT FK_DepartmentID FOREIGN KEY (DepartmentID) REFERENCES Department(ID)
);

-- Inserting data into the Department table
INSERT INTO Department (DepartmentName)
VALUES 
    ('ICT'),
    ('Program');

-- Inserting data into the Employee table
INSERT INTO Employee (PIN, EmployeeName, DateOfBirth, ActiveStatus, DepartmentID)
VALUES
    ('R0001', 'Mr. Anis', '1980-05-20', 1, 1),  -- Active employee in ICT department
    ('R0002', 'Kobir Hossain', '1984-02-15', 1, 1),  -- Active employee in ICT department
    ('R0003', 'Abbas Ali', '1983-05-05', 1, 2),  -- Active employee in Program department
    ('P0004', 'Kazi Akbar', '1999-12-25', 0, 1),  -- Inactive employee in ICT department
    ('P0005', 'Abir Hossain', '1999-12-26', 1, 1);  -- Active employee in ICT department


