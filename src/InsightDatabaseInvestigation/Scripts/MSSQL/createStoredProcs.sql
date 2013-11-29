
/****** Object:  StoredProcedure [dbo].[GetUsersGraph]    Script Date: 11/27/2013 21:57:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pieter Van Parys
-- Create date: 29-11-2013
-- Description:	Gets all user, and related, info
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersGraph]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * from [User]
	select * from Usergroup
	select * from Membership
END

GO
-- =============================================
-- Author:		Pieter Van Parys
-- Create date: 29-11-2013
-- Description:	Gets all user, and related, info
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersGraph]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * from [User]
	select * from Usergroup
	select * from Membership
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
	from [user]
	where UserID = @userID
	UNION
	select distinct [USER].*
	from Membership
		inner join UserGroup 
			on Membership.GroupID = UserGroup.GroupID
		inner join [User]
			on [User].UserID = Membership.UserID
	where Membership.GroupID IN (Select mem.GroupID from Membership mem where mem.UserID = @userID)

	select distinct UserGroup.*
	from Membership
		inner join UserGroup
			on Membership.GroupID = UserGroup.GroupID
	where Membership.UserID = @userID

	select *
	from Membership
	where Membership.GroupID IN (Select mem.GroupID from Membership mem where mem.UserID = @userID)
END

GO
