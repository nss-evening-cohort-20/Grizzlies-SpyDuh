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

--Starter Data for SpyTeam Table

INSERT INTO [SpyTeam] (UserId1, UserId2) VALUES (1,2)
INSERT INTO [SpyTeam] (UserId1, UserId2) VALUES (1,4)
INSERT INTO [SpyTeam] (UserId1, UserId2) VALUES (2,3)


--Starter Data for SpyEnemy Table
INSERT INTO [SpyEnemy] (UserId1, UserId2) VALUES (1,3)
INSERT INTO [SpyEnemy] (UserId1, UserId2) VALUES (2,4)
INSERT INTO [SpyTeam] (UserId1, UserId2) VALUES (3,4)