/****** Object:  Table [dbo].[UserGroup]    Script Date: 11/29/2013 07:50:28 ******/
CREATE TABLE [dbo].[UserGroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Comment] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User]    Script Date: 11/29/2013 07:50:28 ******/
CREATE TABLE [dbo].[User](
	[UserID] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Middle] [varchar](1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](16) NOT NULL,
	[Comment] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membership]    Script Date: 11/29/2013 07:50:28 ******/
CREATE TABLE [dbo].[Membership](
	[MembershipID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembershipID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  ForeignKey [FK__Membershi__UserI__173876EA]    Script Date: 11/29/2013 07:50:28 ******/
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD FOREIGN KEY([GroupID])
REFERENCES [dbo].[UserGroup] ([GroupID])
GO

/****** Object:  ForeignKey [FK__Membershi__UserI__182C9B23]    Script Date: 11/29/2013 07:50:28 ******/
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
