USE [master]
GO
IF db_id('SpyDuh') IS NULL
  CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [UserSkill];
DROP TABLE IF EXISTS [Service];
DROP TABLE IF EXISTS [Skill];
DROP TABLE IF EXISTS [UserService];
DROP TABLE IF EXISTS [Agency];
DROP TABLE IF EXISTS [SpyEnemy];
DROP TABLE IF EXISTS [SpyTeam];
DROP TABLE IF EXISTS [Assignment];
DROP TABLE IF EXISTS [UserAssignment];

CREATE TABLE [User] (
  [Id] int PRIMARY KEY identity not null,
  [Name] nvarchar(255) not null,
  [Email] nvarchar(255),
  [AgencyId] int default 0
)
GO

CREATE TABLE [UserSkill] (
  [Id] int PRIMARY KEY identity,
  [UserId] int not null,
  [SkillId] int not null,
  [SkillLevel] int 
)
GO

CREATE TABLE [Skill] (
  [Id] int PRIMARY KEY identity,
  [Name] nvarchar(255) not null
)
GO

CREATE TABLE [SpyTeam] (
  [Id] int PRIMARY KEY identity,
  [UserId1] int not null,
  [UserId2] int not null
)
GO

CREATE TABLE [SpyEnemy] (
  [Id] int PRIMARY KEY identity,
  [UserId1] int not null,
  [UserId2] int not null
)
GO

CREATE TABLE [Service] (
  [Id] int PRIMARY KEY identity,
  [Name] nvarchar(255)not null
)
GO

CREATE TABLE [UserService] (
  [Id] int PRIMARY KEY identity,
  [ServiceId] int not null,
  [UserId] int not null,
  [ServicePrice] money not null
)
GO

CREATE TABLE [Agency] (
  [Id] int PRIMARY KEY identity,
  [Handler] bit not null,
  [Name] nvarchar(255) not null
)
GO

CREATE TABLE [Assignment] (
  [Id] int PRIMARY KEY identity,
  [Description] nvarchar(255) not null,
  [UserService] int not null,
  [Fatal] bit not null,
  [StartMissionDate] datetime not null,
  [EndMissionDate] datetime 
)
GO

CREATE TABLE [UserAssignment] (
  [Id] int PRIMARY KEY identity,
  [AssignmentId] int not null,
  [UserId] int not null
)
GO

ALTER TABLE [User] ADD FOREIGN KEY ([AgencyId]) REFERENCES [Agency] ([Id])
GO

ALTER TABLE [UserSkill] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [UserSkill] ADD FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([Id])
GO

ALTER TABLE [UserService] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [UserService] ADD FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id])
GO

ALTER TABLE [SpyEnemy] ADD FOREIGN KEY ([UserId1]) REFERENCES [User] ([Id])
GO

ALTER TABLE [SpyEnemy] ADD FOREIGN KEY ([UserId2]) REFERENCES [User] ([Id])
GO

ALTER TABLE [SpyTeam] ADD FOREIGN KEY ([UserId1]) REFERENCES [User] ([Id])
GO

ALTER TABLE [SpyTeam] ADD FOREIGN KEY ([UserId2]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Assignment] ADD FOREIGN KEY ([UserService]) REFERENCES [UserService] ([Id])
GO

ALTER TABLE [UserAssignment] ADD FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment] ([Id])
GO

ALTER TABLE [UserAssignment] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO


--Starter Data for Agency Table

INSERT INTO [dbo].[Agency]
           ([Handler]
           ,[Name])
     VALUES(0,'Michael Westen'),
           (1,'John Drake'),
           (0,'Edgar Brodie'),
           (0,'Richard Hannay'),
           (1,'Alicia Huberman')    
GO


--Starter Data for User Table
INSERT INTO [dbo].[User]
           ([Name]
           ,[Email]
           ,[AgencyId])
     VALUES
           ('Harry Palmer', 'hpalmer@Spyduh.org',2),
           ('Tom Bishop', 'tpishop@Spyduh.org',1),
           ('Roger Thornhill', 'rhornhill@Spyduh.org',5),
           ('Annie Walker', 'aWalker@Spyduh.org',3),
           ('Black Widow', 'bwidow@Spyduh.org',5),
           ('William Brandt', 'wbrandt@Spyduh.org',2),
           ('Felix Leiter', 'fleiterr@Spyduh.org',4),
           ('Nathan Muir', 'nmuir@Spyduh.org',1),
           ('Evelyn Salt', null,3),
           ('Carrie Mathison', null,1),
           ('Jack Ryan', 'jryan@Spyduh.org',4),
           ('Nick Fury', 'nfury@Spyduh.org',2),
           ('Sarah Walker', 'swalker@Spyduh.org',5),
           ('Austin Powers', 'apowers@Spyduh.org',3),
           ('Emma Peel', null,4),
           ('Napoleon Solo', 'nsolo.org',3),
           ('Harry Hart', 'hart@Spyduh.org',2),
           ('Maxwell Smart', null,4),
           ('Sydney Bristow', 'sbristow@Spyduh.org',5),
           ('George Smiley', 'gsmiley@Spyduh.org',3),
           ('Ethan Hunt', null,4),
           ('Illya Kuryakin', null,4)
GO





--Starter Data for SpyTeam Table

INSERT INTO [SpyTeam] (UserId1, UserId2) VALUES (1,3), (1,5), (3,7), (9, 5), (15, 13), (15, 17), (11, 9), (17, 19), (17, 1), (3, 9)



--Starter Data for SpyEnemy Table
INSERT INTO [SpyEnemy] (UserId1, UserId2) VALUES (2,4), (2,6), (4,8), (10,6), (16, 14), (16,18), (12,10), (18,20), (18,2), (4,10)   




--Starter Data for Skills Table

insert into [dbo].[Skill]
        ([Name])

values ('Critical Thinking')
		,('Codebreaking Puzzles')
		,('Communication Skills')
		,('Logical Thinking')
		,('Observation')
		,('Deception Skills')
		,('Physical Fitness')
        ,('Computer Skills')
        ,('Martial Arts')
        ,('Boxing')
        ,('Professional Shooter')
        ,('Hacker')
