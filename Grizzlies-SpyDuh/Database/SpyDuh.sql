USE [master]
GO
IF db_id('SpyDuh') IS NULL
  CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [UserAssignment];
DROP TABLE IF EXISTS [Assignment];
DROP TABLE IF EXISTS [UserService];
DROP TABLE IF EXISTS [Service];
DROP TABLE IF EXISTS [UserSkill];
DROP TABLE IF EXISTS [Skill];
DROP TABLE IF EXISTS [SpyEnemy];
DROP TABLE IF EXISTS [SpyTeam];
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [Agency];

CREATE TABLE [User] (
  [Id] int PRIMARY KEY identity not null,
  [Name] nvarchar(255) not null,
  [Email] nvarchar(255),
  [AgencyId] int,
  [IsHandler] bit,
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
  [ServicePrice] float not null
)
GO

CREATE TABLE [Agency] (
  [Id] int PRIMARY KEY identity,
  [Name] nvarchar(255) not null
)
GO

CREATE TABLE [Assignment] (
  [Id] int PRIMARY KEY identity,
  [Description] nvarchar(255) not null,
  [AgencyId] int not null,
  [Fatal] bit not null,
  [StartMissionDateTime] datetime not null,
  [EndMissionDateTime] datetime 
)
GO

CREATE TABLE [UserAssignment] (
  [Id] int PRIMARY KEY identity,
  [RoleDescription] nvarchar(255) not null,
  [AssignmentId] int not null,
  [ServiceId] int not null,
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

ALTER TABLE [Assignment] ADD FOREIGN KEY (AgencyId) REFERENCES [Agency] ([Id])
GO

ALTER TABLE [UserAssignment] ADD FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment] ([Id])
GO

ALTER TABLE [UserAssignment] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [UserAssignment] ADD FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id])
GO

--Starter Data for Agency Table

INSERT INTO [dbo].[Agency]
           ([Name])
     VALUES('Defense Intelligence Agency'),
           ('National Security Agency'),
           ('Spies R Us'),
           ('CIA'),
		   ('Spies Inc.')
GO


--Starter Data for User Table
INSERT INTO [dbo].[User]
           ([Name]
           ,[Email]
           ,[AgencyId]
		   ,[IsHandler])
     VALUES
           ('Harry Palmer', 'hpalmer@Spyduh.org',2, 1),
           ('Tom Bishop', 'tpishop@Spyduh.org',1, 1),
           ('Roger Thornhill', 'rhornhill@Spyduh.org',5, 1),
           ('Annie Walker', 'aWalker@Spyduh.org',3, 1),
           ('Black Widow', 'bwidow@Spyduh.org',5, 0),
           ('William Brandt', 'wbrandt@Spyduh.org',2, 0),
           ('Felix Leiter', 'fleiterr@Spyduh.org',4, 1),
           ('Nathan Muir', 'nmuir@Spyduh.org',1, 0),
           ('Evelyn Salt', null,3, 0),
           ('Carrie Mathison', null, null, null),
           ('Jack Ryan', 'jryan@Spyduh.org', null, null),
           ('Nick Fury', 'nfury@Spyduh.org', null, null),
           ('Sarah Walker', 'swalker@Spyduh.org',2, 0),
           ('Austin Powers', 'apowers@Spyduh.org', null, null),
           ('Emma Peel', null,1, 0),
           ('Napoleon Solo', 'nsolo.org',4, 0),
           ('Harry Hart', 'hart@Spyduh.org', null, null),
           ('Maxwell Smart', null, null, null),
           ('Sydney Bristow', 'sbristow@Spyduh.org',5, 0),
           ('George Smiley', 'gsmiley@Spyduh.org', null, null),
           ('Ethan Hunt', null,3, 0),
           ('Illya Kuryakin', null,4, 0)
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



--Starter Data for Service Table
INSERT INTO [Service]
           (Service.Name)
     VALUES('Just regular spying'),
           ('Getting intel'),
           ('Mainframe hacking'),
           ('Assassination')
GO

--Starter Data for UserService Table
INSERT INTO [UserService]
           (ServiceId, UserId, ServicePrice)
     VALUES
		(1, 1, 500),
		(1, 2, 570),
		(2, 2, 2000),
		(1, 3, 600.30),
		(2, 4, 1055.65),
		(3, 4, 100),
		(1, 5, 499.99),
		(2, 5, 1500),
		(4, 5, 200000),
		(2, 6, 399.99),
		(3, 6, 9000),
		(1, 7, 999.99),
		(2, 7, 987.87),
		(1, 8, 1010.10),
		(4, 8, 50000),
		(3, 9, 55.50),
		(1, 10, 655.55),
		(2, 10, 3300.90),
		(2, 11, 2555.55),
		(3, 11, 999.99),
		(1, 12, 2.5),
		(2, 12, 2100),
		(3, 12, 2000),
		(3, 13, 6000),
		(3, 14, 500000),
		(4, 14, 4.5),
		(1, 15, 3.5),
		(2, 15, 2000),
		(1, 16, 988.88),
		(2, 16, 2000),
		(2, 17, 999.99),
		(3, 17, 687.65),
		(2, 18, 3500),
		(4, 18, 350000.06),
		(3, 19, 654.5),
		(4, 19, 102000.65),
		(1, 20, 456.36),
		(2, 20, 1900),
		(3, 21, 1800.01),
		(1, 22, 900.97),
		(2, 22, 1800.80),
		(4, 22, 240000)
GO



--Starter Data for UserSkills Table

insert into [dbo].[UserSkill]
			([UserId]
           ,[SkillId]
           ,[SkillLevel])

values	(1,2,5) 
		,(1,4,7)
		,(2,6,8)
		,(2,8,10)
		,(3,10,3)
		,(3,1,6)
		,(4,3,9)
		,(4,5,2)
		,(5,7,5)
		,(5,9,7)
		,(6,2,7)
		,(6,4,8)
		,(7,6,8)
		,(7,8,10)
		,(8,10,2)
		,(8,1,6)
		,(9,3,8)
		,(9,5,7)
		,(10,7,7)
		,(10,9,5)
		,(11,2,8)
		,(11,4,8)
		,(12,6,7)
		,(12,8,10)
		,(13,10,7)
		,(13,1,5)
		,(14,2,9)
		,(14,3,8)
		,(15,4,8)
		,(15,5,10)
		,(16,6,7)
		,(16,7,9)
		,(17,8,10)
		,(17,9,3)
		,(18,10,10)
		,(18,1,6)
		,(19,3,7)
		,(19,5,5)
		,(20,7,8)
		,(21,9,10)
		,(21,6,10)
		,(22,7,8)
		,(22,10,10)
GO

--Insert Starter Data for Assignments Table

INSERT INTO [dbo].[Assignment] 
			([Description], 
			 [AgencyId],
			 [Fatal],
			 [StartMissionDateTime],
			 [EndMissionDateTime])

		VALUES
			('There are rumors of a secret organization that is developing a weapon that turns all dogs into cats. The organization must be infilitrated and the plans and research for this weapon must be destroyed', 1, 1, '2022-12-23T14:25:10.000', '2023-04-12T18:30:00.00'),
			('A key witness in a murder trial is being harrassed by a group that call themselves ''The Bad Guys''. Discover and expose the group''s leader to save the witness', 2, 0, '2023-03-23T14:25:10.000', null),
			('A neighborhood villan is stealing everyone''s mail and replacing it with bannanas. Track down the perpetrator and bring them to justice', 3, 1, '2023-03-07T09:30:00.000', null),
			('An heiress believes her husband has only married her for her fortune and is planning an untimely demise for her. Befriend the husband and discover his true intentions.', 4, 1, '2023-01-11T09:30:00.000', '2023-03-29T09:30:00.000'),
			('A local charity is embezzling money. Sneak into their office and hack into their financial records to prove it.', 5, 0, '2023-02-11T09:30:00.000', '2023-04-11T09:30:00.000'), 
			('A suspicious death has occured on a remote billionaire''s island. Go undercover as new staff hires to discover more.', 5, 1, '2023-02-11T09:30:00.000', '2023-05-11T09:30:00.000')

--Insert Starter Data for UserAssignments Table

INSERT INTO [dbo].[UserAssignment]
			([RoleDescription],
			 [AssignmentId],
			 [ServiceId],
			 [UserId])
		VALUES
			('The Team Leader', 1, 1, 1),
			('The Muscle', 1, 1, 2),
			('The Hacker', 1,  3, 3),
			('The Team Leader', 2, 1, 4),
			('The Researcher', 2, 2, 5),
			('The Grifter', 2,  1, 6),
			('The Solo Agent', 3,  1, 7),
			('The Grifter', 4, 1, 8),
			('The Backup', 4,  1, 9),
			('The Hacker', 5, 3, 10),
			('The Muscle', 5, 1 , 11),
			('Undercover Agent 1', 6, 2, 12),
			('Undercover Agent 2', 6, 2, 13),
			('Undercover Agent 3', 6, 2, 14)