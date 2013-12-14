CREATE TABLE [UserGroup]
(
	ID int identity,
	Name varchar(100) not null
)
GO
ALTER TABLE [UserGroup] ADD CONSTRAINT PK_UserGroup PRIMARY KEY (ID)
GO
CREATE TABLE [Users]
(
	ID int identity,
	FirstName varchar(100) not null,
	LastName varchar(100) not null
)
GO
ALTER TABLE [Users] ADD CONSTRAINT PK_User PRIMARY KEY (ID)
GO
CREATE TABLE [Membership]
(
	ID int identity,
	UserGroupID int not null,
	UserID int not null
)
GO
ALTER TABLE [Membership] ADD CONSTRAINT PK_Membership PRIMARY KEY (ID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_User FOREIGN KEY (UserID) REFERENCES [Users](ID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_UserGroup FOREIGN KEY (UserGroupID) REFERENCES [UserGroup](ID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT UC_UserUserGroup UNIQUE (UserID, UserGroupID)
GO
