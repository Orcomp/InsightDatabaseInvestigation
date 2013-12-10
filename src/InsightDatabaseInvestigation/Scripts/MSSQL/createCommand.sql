CREATE TABLE [UserGroup]
(
	UserGroupID int identity,
	Name varchar(100) not null
)
GO
ALTER TABLE [UserGroup] ADD CONSTRAINT PK_UserGroup PRIMARY KEY (UserGroupID)
GO
CREATE TABLE [Users]
(
	UserID int identity,
	FirstName varchar(100) not null,
	LastName varchar(100) not null
)
GO
ALTER TABLE [Users] ADD CONSTRAINT PK_User PRIMARY KEY (UserID)
GO
CREATE TABLE [Membership]
(
	MembershipID int identity,
	UserGroupID int not null,
	UserID int not null
)
GO
ALTER TABLE [Membership] ADD CONSTRAINT PK_Membership PRIMARY KEY (MembershipID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_User FOREIGN KEY (UserID) REFERENCES [Users](UserID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT FK_UserGroup FOREIGN KEY (UserGroupID) REFERENCES [UserGroup](UserGroupID)
GO
ALTER TABLE [Membership] ADD CONSTRAINT UC_UserUserGroup UNIQUE (UserId, UserGroupId)
GO
