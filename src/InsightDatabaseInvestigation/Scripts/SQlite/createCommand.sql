CREATE TABLE UserGroup
(
    UserGroupID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name varchar(100) not null,
    Comment varchar(1000)
);

CREATE TABLE [Users]
(
	UserID INTEGER PRIMARY KEY AUTOINCREMENT,
	FirstName varchar(100) not null,
	LastName varchar(100) not null
);

CREATE TABLE Membership
(
	MembershipID INTEGER PRIMARY KEY AUTOINCREMENT,
	UserGroupID integer not null,
	UserID integer not null,
	FOREIGN KEY(UserGroupID) references UserGroup(UserGroupID),
	FOREIGN KEY(UserID) references [Users](SomeID)
);