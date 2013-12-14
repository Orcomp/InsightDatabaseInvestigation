	
/****** Object:  StoredProcedure [dbo].[GetModel]    Script Date: 11/27/2013 21:57:53 ******/

AUTOPROC All [UserGroup] Single=UserGroup Plural=UserGroups
GO
AUTOPROC All [Users] Single=User Plural=Users
GO
AUTOPROC All [Membership] Single=Membership Plural=Memberships
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
	select * from [Usergroup]
	select * from [Membership]
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
	from Membership
		inner join UserGroup 
			on Membership.UserGroupID = UserGroup.ID
		inner join [Users]
			on [Users].ID = Membership.ID
	where Membership.ID IN (Select mem.ID from Membership mem where mem.ID = @userID)

	select distinct UserGroup.*
	from Membership
		inner join UserGroup
			on Membership.ID = UserGroup.ID
	where Membership.ID = @userID

	select *
	from Membership
	where Membership.ID IN (Select mem.ID from Membership mem where mem.ID = @userID)
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
	select * from Usergroup
	select * from Membership
	
END

GO
