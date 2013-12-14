CREATE TABLE [UserGroups]
(
	ID int identity,
	Name varchar(100) not null
)
GO
ALTER TABLE [UserGroups] ADD CONSTRAINT PK_UserGroup PRIMARY KEY (ID)
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
CREATE TABLE [Memberships]
(
	ID int identity,
	UserGroupID int not null,
	UserID int not null
)
GO
ALTER TABLE [Memberships] ADD CONSTRAINT PK_Membership PRIMARY KEY (ID)
GO
ALTER TABLE [Memberships] ADD CONSTRAINT FK_User FOREIGN KEY (UserID) REFERENCES [Users](ID)
GO
ALTER TABLE [Memberships] ADD CONSTRAINT FK_UserGroup FOREIGN KEY (UserGroupID) REFERENCES [UserGroups](ID)
GO
ALTER TABLE [Memberships] ADD CONSTRAINT UC_UserUserGroup UNIQUE (UserID, UserGroupID)
GO
