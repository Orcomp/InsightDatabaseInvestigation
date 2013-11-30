CREATE TABLE UserGroup
(
	GroupID int identity,
	Name varchar(100) not null,
	Comment varchar(1000)
)
GO
ALTER TABLE UserGroup ADD CONSTRAINT PK_UserGroup PRIMARY KEY (GroupID)
GO
CREATE TABLE [Users]
(
	UserID int identity,
	FirstName varchar(100) not null,
	LastName varchar(100) not null,
	Middle varchar(1) not null,
	Email varchar(100) not null,
	Phone varchar(16) not null,
	Comment varchar(1000)
)
GO
ALTER TABLE [Users] ADD CONSTRAINT PK_User PRIMARY KEY (UserID)
GO
CREATE TABLE Membership
(
	MembershipID int identity,
	GroupID int not null,
	UserID int not null
)
GO
ALTER TABLE [Membership] ADD CONSTRAINT PK_Membership PRIMARY KEY (MembershipID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_User FOREIGN KEY (UserID) REFERENCES [Users](UserID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_UserGroup FOREIGN KEY (GroupID) REFERENCES [UserGroup](GroupID)
GO