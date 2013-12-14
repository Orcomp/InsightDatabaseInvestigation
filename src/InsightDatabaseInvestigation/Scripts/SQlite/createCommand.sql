CREATE TABLE UserGroups
(
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name varchar(100) not null
);

CREATE TABLE [Users]
(
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
	FirstName varchar(100) not null,
	LastName varchar(100) not null
);

CREATE TABLE Memberships
(
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
	UserGroupID integer not null,
	UserID integer not null,
	FOREIGN KEY(UserGroupID) references UserGroups(ID),
	FOREIGN KEY(UserID) references [Users](ID)
);