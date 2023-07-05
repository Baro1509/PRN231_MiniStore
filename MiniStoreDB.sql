--CREATE DATABASE MiniStore
USE MiniStore

CREATE TABLE [dbo].Staff(
	StaffID varchar(10) NOT NULL,
	RoleID varchar(2) NOT NULL,
	[StaffName] [varchar](180) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[Category](
	[CategoryID] varchar(10) NOT NULL,
	[CategoryName] [varchar](40) NOT NULL,
	[CategoryDescription] [nvarchar](150) NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (CategoryID)
)
-------------------------------------------------------------------------------
CREATE TABLE [dbo].Product(
	ProductID [int] identity NOT NULL,
	[CategoryID] varchar(10) NOT NULL,
	ProductName [varchar](40) NOT NULL,
	[Description] [varchar](220) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	ProductStatus [tinyint] NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (ProductID),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
)
-------------------------------------------------------------------------------
CREATE TABLE [dbo].Invoice(
	InvoiceID [int] identity NOT NULL,
	StaffID varchar(10) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Total] [money] NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (InvoiceID),
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceID] [int] NOT NULL,
	ProductID [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [float] NOT NULL,
	PRIMARY KEY (InvoiceID, ProductID),
    FOREIGN KEY ([InvoiceID]) REFERENCES Invoice([InvoiceID]),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
)
-------------------------------------------------------------------------------
CREATE TABLE WorkShift(
	ShiftID int identity not null,
	StartTime datetime not null,
	EndTime datetime not null,
	Coefficient [float] default 1,
	Bonus money default 0,
	CreatedBy varchar(10) NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (ShiftID),
    FOREIGN KEY (CreatedBy) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE Duty(
	DutyID int identity not null,
	ShiftID int not null,
	RoleID int NOT NULL,
	AssignedTo varchar(10) NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (DutyID),
    FOREIGN KEY (ShiftID) REFERENCES WorkShift(ShiftID),
    FOREIGN KEY (AssignedTo) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE ShiftSalary(
	ShiftSalaryID int identity not null,
	[AssignedTo] varchar(10) NOT NULL,
	[ApprovedBy] varchar(10) NOT NULL,
	Salary money NOT NULL,
	CreatedTime datetime NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (ShiftSalaryID),
    FOREIGN KEY (AssignedTo) REFERENCES Staff(StaffID),
    FOREIGN KEY (ApprovedBy) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE MonthSalary(
	MonthSalaryID int identity not null,
	[AssignedTo] varchar(10) NOT NULL,
	[ApprovedBy] varchar(10) NOT NULL,
	Salary money NOT NULL,
	StartTime datetime NOT NULL,
	EndTime datetime NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (MonthSalaryID),
    FOREIGN KEY (AssignedTo) REFERENCES Staff(StaffID),
    FOREIGN KEY (ApprovedBy) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE MonthlyBonus(
	MonthlyBonusID int identity not null,
	[AssignedTo] varchar(10) NOT NULL,
	[ApprovedBy] varchar(10) NOT NULL,
	Bonus money NOT NULL,
	StartTime datetime NOT NULL,
	EndTime datetime NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (MonthlyBonusID),
    FOREIGN KEY (AssignedTo) REFERENCES Staff(StaffID),
    FOREIGN KEY (ApprovedBy) REFERENCES Staff(StaffID)
)
-------------------------------------------------------------------------------
CREATE TABLE LeaveRequest(
	LeaveRequestID int identity not null,
	RequestedBy varchar(10) NOT NULL,
	ApprovedBy varchar(10) NOT NULL,
	StartTime datetime NOT NULL,
	EndTime datetime NOT NULL,
	[Status] tinyint default 1,
	PRIMARY KEY (LeaveRequestID),
    FOREIGN KEY (RequestedBy) REFERENCES Staff(StaffID),
    FOREIGN KEY (ApprovedBy) REFERENCES Staff(StaffID)
)
------------------------------------------------------------------------------
CREATE TABLE Attendance(
	AttendanceID int identity not null,
	StaffID varchar(10) not null,
	CreatedTime datetime not null,
	IsCheckIn bit default 0 not null,
	[Status] bit default 1 not null,
	PRIMARY KEY (AttendanceID),
	FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
)

--drop table Attendance
--drop table LeaveRequest
--drop table MonthlyBonus
--drop table MonthSalary
drop table ShiftSalary
--drop table Duty
--drop table WorkShift
--drop table InvoiceDetail
--drop table Invoice
--drop table Product
--drop table Category
--drop table Staff

INSERT INTO Staff (StaffID, StaffName, RoleID, Password, Status) values ('VyLT', N'Luân Tường Vy', 'SM', '1', 1)
INSERT INTO Staff (StaffID, StaffName, RoleID, Password, Status) values ('VyLT1', N'Luân Tường Vy', 'MA', '1', 1)