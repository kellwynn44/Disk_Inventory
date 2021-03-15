/************************************************************************/
/*   Date         Programmer        Description						    */
/*  03-03-2021   Michelle Petit		created db and first 4 tables		*/                                  
/*  03-05-2021	Michelle Petit		created remaining tables for db		*/
/*	03-13-2021	Michelle Petit		added data to all the tables		*/
/*							                                            */		
/************************************************************************/

--best practice
use master;
go

--create new database named diskInventoryMP
drop database if exists diskInventoryMP;
go
create database diskInventoryMP;
go

--switch to new database
use diskInventoryMP;
go 

--create a new user
if SUSER_ID('diskUserMP') is null
	create login diskUserMP with password = 'sesam_123',
	default_database = diskInventoryMP;
create user diskUserMP;

--grant read permission to all tables to user
alter role db_datareader add member diskUserMP;
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
	(LoanStatusID int not null identity primary key,
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

--create media table with 3 required columns 
	--plus 3 foreign key columns
create table Media
	(MediaID int not null identity primary key,
	Title varchar(60) not null,
	ReleaseDate date not null,
	GenreID int not null references Genre(GenreID),
	MediaTypeID int not null references MediaType(MediaTypeID) default 1,
	LoanStatusID int null references LoanStatus(LoanStatusID) default 1
	);

--create media loan status table 3 required columns and 1 'optional' column (value to be entered at a later date)
		--plus 2 foreign key columns
create table DiskHasBorrower
	(DiskHasBorrowerID int not null identity primary key,
	BorrowedDate datetime not null default getdate(),
	DueDate datetime not null default (getdate() +14),
	ReturnedDate datetime null,
	BorrowerID int not null references Borrower(BorrowerID),
	MediaID int not null references Media(MediaID)
	);

--create artist type table with 2 required columns
create table ArtistType
	(ArtistTypeID int not null identity primary key,
	Description varchar(100) not null
	);

--create artist table with 1 required columns and 2 optional columns (so the user can choose between entering a lead singer's name or the band name)
		--plus 1 foreign key column
create table Artist
	(ArtistID int not null identity primary key,
	FirstName varchar(60) null,
	LastName varchar(60) null,
	ArtistTypeID int not null references ArtistType(ArtistTypeID)
	);

--create media artist table with 1 required column
	--plus 2 foreign key columns that have unique values
create table MediaArtist
	(MediaArtistID int not null identity primary key,
	ArtistID int not null references Artist(ArtistID),
	MediaID int not null references Media(MediaID)
	unique(MediaID, ArtistID)
	);


----add 20 rows of data to Media table (need to add data to 3 other tables first)----
--adding values to the LoanStatus table (5 rows)
insert into LoanStatus
	(Description)
values	('Available'),
		('Borrowed'),
		('Overdue'),
		('Damaged'),
		('Lost');

--adding values to the Genre table (36 rows)
insert into Genre
	 (Description)
values	('Rock'),
		('Jazz'),
		('Pop'),
		('Heavy Metal'),
		('Classical'),
		('Country'),
		('Celtic'),
		('Dance'),
		('Rap'),
		('Blues'),
		('Christmas'),
		('Religious'),
		('Childrens'),
		('Folk'),
		('Action'),
		('Comedy'),
		('Drama'),
		('Romance'),
		('Horror'),
		('Science Fiction'),
		('Documentary'),
		('Musical'),
		('Western'),
		('Family'),
		('Classic'),
		('Mystery'),
		('Non-fiction'),
		('Fantasy'),
		('Sci-Fi'),
		('Graphic Novel'),
		('Historical Fiction'),
		('Literary Fiction'),
		('Biographies/Autobiographies'),
		('Horror'),
		('Romance'),
		('Poetry');

--adding values to the MediaType table (3 rows)
insert into MediaType
	(Description)
values	('CD'),
		('DVD'),
		('Book');

--adding values to the Media table (22 rows)
insert into Media
	(Title, ReleaseDate, GenreID)
values	('Christmas Extraordinaire', '2001', 11),
		('Enchanted Christmas', '1993', 11),
		('Greatest Hits 22 Best-Loved Favorites', '1992', 35),  --should be 12
		('Born to Play', '2008', 13),
		('Smart Symphonies', '1999', 5),
		('Ultimate Mardi Gras', '2001', 2),
		('Mozart Flute Concerto No. 1 Flute and Harp Concerto', '1989', 5),
		('Mozart Collection', '2001', 5),
		('Passages', '2011', 14),
		('Folklore', '2020', 3),
		('The Princess Movie Song Collection', '2004', 13),
		('Ocean Eyes', '2009', 3),
		('Bach Sonatas', '1995', 5),
		('Enya', '1987', 7),
		('Ludwig van Beethoven Symphonien Nos. 1 & 2', '1985', 5),
		('Beethoven: Symphonies No. 3 and 8', '1994', 5),
		('The Joshua Tree', '1987', 1),
		('The Sweetest Gift', '2000', 11),
		('Rudolph the Red-Nosed Reindeer', '2003', 11),
		('21', '2011', 3),
		('Watermark', '1988', 3),
		('Watch the Throne', '2011', 9);

--updating one value in the media table
update Media
	set GenreID = 12
	where GenreID = 1;

----add 20 rows of data to the Borrower table----
--adding values to the Borrower table (20 rows) 
insert into Borrower
	(LastName, FirstName, PhoneNum)
	values	('Nielson', 'Michelle', '555-555-5550'),
			('Nielson', 'Anna', '555-555-5551'),
			('Nielson', 'Beth', '555-555-5552'),
			('Nielson', 'Wes', '555-555-5553'),
			('Nielson', 'Mark', '555-555-5554'),
			('Nielson', 'Cami', '555-555-5555'),
			('Nielson', 'Alder', '555-555-5556'),
			('Nielson', 'Kate', '555-555-5557'),
			('Nielson', 'Kai', '555-555-5558'),
			('Nielson', 'Monique', '555-555-5559'),
			('Nielson', 'Paul', '555-555-5560'),
			('Nielson', 'Erin', '555-555-5561'),
			('Nielson', 'Bella', '555-555-5562'),
			('Nielson', 'Lincoln', '555-555-5563'),
			('Nielson', 'Winston', '555-555-5564'),
			('Nielson', 'Hatch', '555-555-5565'),
			('Nielson', 'Liz', '555-555-5566'),
			('Nielson', 'Travis', '555-555-5567'),
			('Nielson', 'Duke', '555-555-5568'),
			('Nielson', 'Lenny', '555-555-5569');

--check that all 20 entries were created
select * from borrower
	order by FirstName;
--delete 1 row from the Borrower table
delete from borrower
	where FirstName = 'Lenny';
--check that there are now 19 rows
select * from borrower
	order by FirstName;

----add 20 rows of data to the Artist table (need to add values to the ArtistType table first)----
--adding values to the ArtistType table (8 rows)
insert into ArtistType
	(Description)
	values	('Singer'),
			('Musician'),
			('Band'),
			('Orchestra'),
			('Choir'),
			('Record Company'),
			('Actor'),
			('Author');

--adding values to the Artist table (22 rows)
insert into Artist
	(FirstName, LastName, ArtistTypeID)
	values	('Mannheim Steamroller', null, 4),
			('Anna Maria', 'Mendieta', 2),
			('Mormon Tabernacle Choir', null, 5),
			('The Backyardigans', null, 1),
			('Enfamil', null, 8),
			('Mardi Gras Records', null, 8),
			('Orpheus Chamber Orchestra', null, 4),
			('Classical Baby', null, 8),
			('C.G.', 'Ryche', 2),
			('Taylor', 'Swift', 1),
			('Koche Records', null, 8),
			('Owl City', null, 2),
			('James', 'Galway', 2),
			('Enya', null, 2),
			('Berliner Philharmoniker', null, 4),
			('The Met Orchestra', null, 4),
			('U2', null, 3),
			('Trisha', 'Yearwood', 1),
			('Destinys Child', null, 3),
			('Adele', null, 1),
			('Kanye', 'West', 1),
			('Jay-Z', null, 1);

----add 20 rows of data to the MediaLoanStatus table----
--adding values to the MediaLoanStatus table 

--past borrow values (1 row)
insert into DiskHasBorrower
	(BorrowedDate, DueDate, ReturnedDate, BorrowerID, MediaID)
	values	('02/15/2021', '02/28/2021', '02/27/2021', 5, 5);

--add current borrow values (19 rows)
insert into DiskHasBorrower
	(BorrowedDate, DueDate, ReturnedDate, BorrowerID, MediaID)
	values	('','', null, 3,2), --borrowerIDs = 1 and 2 do not have anything checked out; MediaID = 1 has never been checked out
			('','', null, 3,3),
			('','', null, 4,4),
			('','', null, 5,5), --BorrowerID = 5 borrowed MediaID = 5 twice
			('','', null, 6,6),
			('','', null, 7,7),
			('','', null, 8,8),
			('','', null, 9,9),
			('','', null, 10,10),
			('','', null, 11,11),
			('','', null, 12,12),
			('','', null, 13,13),
			('','', null, 14,14),
			('','', null, 15,15),
			('','', null, 16,16),
			('','', null, 17,17),
			('','', null, 18,18),
			('','', null, 19,19),
			('','', null, 19,20); --BorrowID = 19 has 2 Media items checked out

select * from DiskHasBorrower;  --20 rows

----add 20 rows of data to the MediaArtist table----
--adding values to the MediaArtist table (21 rows)
insert into MediaArtist
	( MediaID, ArtistID)
	values	(1,1),
			(2,2),
			(3,3),
			(4,4),
			(5,5),
			(6,6),
			(7,7),
			(8,8),
			(9,9),
			(10,10),
			(11,11),
			(12,12),
			(13,13),
			(14,14),
			(15,15),
			(16,16),
			(17,17),
			(18,18),
			(19,19),
			(20,20),
			(21,14), --ArtistID = 14 (Enya) has 2 media items
			(22,21), --MediaID = 22 has 2 artists
			(22,22);
			
--list of all media items that are on loan
select BorrowerID, MediaID, BorrowedDate, ReturnedDate
from DiskHasBorrower
	where ReturnedDate is null
	order by BorrowerID;

--added new line to DiskHasBorrower in class 3/15/2021