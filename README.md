InsightDatabaseInvestigation
----------------------------------------------

This project is used to investigate the [Insight.Database](https://github.com/jonwagner/Insight.Database) and [Insight.Database.Schema](https://github.com/jonwagner/Insight.Database.Schema) libraries.

Insight.Database.Schema has been [around since 2003](https://github.com/jonwagner/Insight.Database.Schema/wiki/Insight.Database.Schema-v-EF-Code-Migrations).

# Purpose

Explore Many-to-Many relationships with Insight.Database as well as other features.

# Database Structure

We are using a very simple database structure to investigate these libraries. 
Just 3 tables:

- UserGroup
- User
- Membership


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

# Features Discovered

- When using Insight.Database.Schema all CRUD stored procedures get created automatically. (OK I re-read the [Insight.Database.Schema](https://github.com/jonwagner/Insight.Database.Schema) home page carefully and this feature is indeed mentioned towards the bottom. Very useful feature.)
- Insight.Database.Schema created a table called Insight_SchemaRegistry in the database to keep track of various things.
- Insight.Database.Schema will automatically create the database if it does not exist. (Would be nice to have an option to re-create a database if it already exists, so we know we always have a clean database for testing purposes.)

# Still to discover

- A nice way to deal with Many-To-Many relationships. (Currently we are doing all this by hand, which does work quite well.)
- Database backups.

# Notes for this project

- Edit the app.config file in the test project to modify the database connection string if required.

# SQLite

Works fine with System.Data.SQLite. (NOTE: Need to be careful as SQLite does not have stored procedures.)

There is no need to use the ODBC connection.