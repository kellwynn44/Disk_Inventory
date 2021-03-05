/*
*Original Author: Michelle Petit                                    *
*Date Created:  03-03-2021                                          *
*Version: 1.0                                                       *
*Date Last Modified: 03-05-2021                                     *
*Modified by: Michelle Petit                                        *
*Mod log:	created database and first 4 tables	            		*
*			created remaining tables for the database				*		
*/
use master;
go

--create new database named diskInventoryMP
drop database if exists diskInventoryMP;
go
create database diskInventoryMP;
go

use diskInventoryMP;
go 

--create borrower table with 4 required columns
create table Borrower
	(BorrowerID int not null identity primary key,
	LastName varchar(60) not null,
	FirstName varchar(60) not null,
	PhoneNum varchar(15) not null
	);

--create loan status table with 2 required columns
create table LoanStatus
	(MediaLoanID int not null identity primary key,
	Description varchar(200) not null
	);

--create genre table with 2 required columns
create table Genre
	(GenreID int not null identity primary key,
	Description varchar(200) not null
	);

--create media type table with 2 required columns
create table MediaType
	(MediaTypeID int not null identity primary key,
	Description varchar(100) not null
	);

--create media table with 3 required columns plus 3 foreign key columns
create table Media
	(MediaID int not null identity primary key,
	MediaName varchar(60) not null,
	ReleaseDate datetime not null,
	MediaLoanID int not null references LoanStatus(MediaLoanID),
	GenreID int not null references Genre(GenreID),
	MediaTypeID int not null references MediaType(MediaTypeID)
	);

--create media loan status table 2 required columns and 1 'optional' column (value to be entered at a later date)
		--plus 2 foreign key columns
create table MediaLoanStatus
	(MediaLoanStatusID int not null identity primary key,
	BorrowedDate datetime not null default getdate(),
	ReturnedDate datetime null,
	BorrowerID int not null references Borrower(BorrowerID),
	MediaID int not null references Media(MediaID)
	);

--create artist type table with 2 required columns
create table ArtistType
	(ArtistTypeID int not null identity primary key,
	Description varchar(100) not null
	);

--create artist table with 1 required columns and 3 option column (so the user can choose between entering a lead singer's name or the band name)
		--plus 1 foreign key column
create table Artist
	(ArtistID int not null identity primary key,
	FirstName varchar(60) null,
	LastName varchar(60) null,
	BandName varchar(60) null,
	ArtistTypeID int not null references ArtistType(ArtistTypeID)
	);
go

--create media artist table with 2 foreign key columns
create table MediaArtist
	(ArtistID int not null references Artist(ArtistID),
	MediaID int not null references Media(MediaID)
	);

--create a new user
if SUSER_ID('diskUserMP') is null
	create login diskUserMP with password = 'sesam_123',
	default_database = diskInventoryMP;
create user diskUserMP;

--grant read permission to all tables to user
alter role db_datareader add member diskUserMP;
go