	
/****** Object:  StoredProcedure [dbo].[GetModel]    Script Date: 11/27/2013 21:57:53 ******/

AUTOPROC All [UserGroups] Single=UserGroup Plural=UserGroups
GO
AUTOPROC All [Users] Single=User Plural=Users
GO
AUTOPROC All [Memberships] Single=Membership Plural=Memberships
GO

-- =============================================
-- Author:		Pieter Van Parys
-- Create date: 29-11-2013
-- Description:	Gets all user, and related, info
-- =============================================
CREATE PROCEDURE [dbo].[GetModel]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * from [Users]
	select * from [Usergroups]
	select * from [Memberships]
END

GO
-- =============================================
-- Author:		Pieter Van Parys
-- Create date: 29-11-2013
-- Description:	Gets all user, and related, info
-- =============================================
CREATE PROCEDURE [dbo].[GetUserById]
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select  * 	
	from [Users]
	where ID = @userID
	UNION
	select distinct [Users].*
	from Memberships
		inner join UserGroups
			on Memberships.UserGroupID = UserGroups.ID
		inner join [Users]
			on [Users].ID = Memberships.ID
	where Memberships.ID IN (Select mem.ID from Memberships mem where mem.ID = @userID)

	select distinct UserGroups.*
	from Memberships
		inner join UserGroups
			on Memberships.ID = UserGroups.ID
	where Memberships.ID = @userID

	select *
	from Memberships
	where Memberships.ID IN (Select mem.ID from Memberships mem where mem.ID = @userID)
END

GO
-- =============================================
-- Author:		Pieter Van Parys
-- Create date: 29-11-2013
-- Description:	Gets all user, and related, info
-- =============================================
CREATE PROCEDURE [dbo].[GetTopUsers]
	@count int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select * from [Users]
	select * from Usergroups
	select * from Memberships
	
END

GO
