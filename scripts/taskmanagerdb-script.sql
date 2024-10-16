USE [master]
GO
/****** Object:  Database [TaskManagerDB]    Script Date: 16/10/2024 4:29:56 p. m. ******/
CREATE DATABASE [TaskManagerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskManagerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskManagerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TaskManagerDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManagerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskManagerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskManagerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskManagerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskManagerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskManagerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskManagerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskManagerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskManagerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskManagerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskManagerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskManagerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskManagerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskManagerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskManagerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskManagerDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TaskManagerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskManagerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskManagerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskManagerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskManagerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskManagerDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TaskManagerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskManagerDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TaskManagerDB] SET  MULTI_USER 
GO
ALTER DATABASE [TaskManagerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskManagerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskManagerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskManagerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaskManagerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskManagerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TaskManagerDB', N'ON'
GO
ALTER DATABASE [TaskManagerDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TaskManagerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TaskManagerDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CommentDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusHistories]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskId] [uniqueidentifier] NOT NULL,
	[PreviousStatus] [nvarchar](100) NOT NULL,
	[NewStatus] [nvarchar](100) NOT NULL,
	[ChangeDate] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StatusHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Priority] [nvarchar](250) NOT NULL,
	[Status] [nvarchar](250) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[AssignedBy] [uniqueidentifier] NOT NULL,
	[AssignedTo] [uniqueidentifier] NOT NULL,
	[Tag] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/10/2024 4:29:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Firstname] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordSalt] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241015215715_InitDB', N'8.0.10')
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsDisabled]) VALUES (N'8053084e-a237-44c0-9d61-34307230f8a1', N'Supervisor', N'Supervisor', 0)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsDisabled]) VALUES (N'1ebcdf1a-e978-42bf-b713-98b47e48994d', N'Administrator', N'Administrator', 0)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsDisabled]) VALUES (N'35a549db-ef7e-4fb1-b0c2-cef3d6ee5c56', N'Employee', N'Employee', 0)
GO
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Priority], [Status], [CreationDate], [DueDate], [AssignedBy], [AssignedTo], [Tag]) VALUES (N'2c005c8f-83e0-4857-9db2-7ecfa88a3cd5', N'Task #3 - mod', N'Task #3', N'Low', N'Pending', CAST(N'2024-10-16T17:46:37.3602005' AS DateTime2), CAST(N'2024-11-16T17:46:37.3602005' AS DateTime2), N'0818c3b2-1fb8-4e2e-91ac-08ae57c9badd', N'21998813-1f61-46f1-9270-e7e64cdfbbda', N'string')
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Priority], [Status], [CreationDate], [DueDate], [AssignedBy], [AssignedTo], [Tag]) VALUES (N'6a1740c5-8c1c-4673-8e66-e2a7f22fc8de', N'Task #2', N'Task #2', N'Low', N'Pending', CAST(N'2024-10-16T17:46:24.3970868' AS DateTime2), CAST(N'2024-11-16T17:46:24.3971442' AS DateTime2), N'd6d3dea8-30c9-44bb-8b1f-21baac47d489', N'21998813-1f61-46f1-9270-e7e64cdfbbda', N'string')
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [Priority], [Status], [CreationDate], [DueDate], [AssignedBy], [AssignedTo], [Tag]) VALUES (N'cf679efa-0df1-483c-9e25-f92f3ad06aca', N'Task #1', N'Task #1', N'High', N'Pending', CAST(N'2024-10-16T16:26:31.8425085' AS DateTime2), CAST(N'2024-11-16T16:26:31.8425612' AS DateTime2), N'0818c3b2-1fb8-4e2e-91ac-08ae57c9badd', N'63a89f19-7d3b-44b9-8bdb-d424125b9a76', N'string')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'c5604547-76c6-4c46-95f6-9f4f84a904f9', N'8053084e-a237-44c0-9d61-34307230f8a1')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'0818c3b2-1fb8-4e2e-91ac-08ae57c9badd', N'1ebcdf1a-e978-42bf-b713-98b47e48994d')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'd6d3dea8-30c9-44bb-8b1f-21baac47d489', N'1ebcdf1a-e978-42bf-b713-98b47e48994d')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'21998813-1f61-46f1-9270-e7e64cdfbbda', N'35a549db-ef7e-4fb1-b0c2-cef3d6ee5c56')
GO
INSERT [dbo].[Users] ([Id], [Username], [Firstname], [Lastname], [Password], [PasswordSalt], [Email], [Enabled], [IsDeleted]) VALUES (N'0818c3b2-1fb8-4e2e-91ac-08ae57c9badd', N'alex1234', N'Alex', N'Rodriguez', N'YPqetF8nV8C3SVyl2ZFVv+1Do3UgY509YnYdIGOZbUE=', N'Md7Vq6VGhjLQEFhcL1ehyw==', N'alex1234@example.com', 1, 0)
INSERT [dbo].[Users] ([Id], [Username], [Firstname], [Lastname], [Password], [PasswordSalt], [Email], [Enabled], [IsDeleted]) VALUES (N'd6d3dea8-30c9-44bb-8b1f-21baac47d489', N'admin1234', N'admin', N'admin', N'lMzw8O43RQzasEadv++ocq0WgI6NVbOyZMcUJMlzj3g=', N'zGW+I5bFCvju7DN7f7atCg==', N'admin1234@example.com', 1, 0)
INSERT [dbo].[Users] ([Id], [Username], [Firstname], [Lastname], [Password], [PasswordSalt], [Email], [Enabled], [IsDeleted]) VALUES (N'c5604547-76c6-4c46-95f6-9f4f84a904f9', N'supervisor1234', N'supervisor', N'supervisor', N'/ODdCgGl9pykr59m7mXQ7Lx1pvtMrGLhBreufV4sMdE=', N'/005gm/gUfZ5DdLVLJIzmg==', N'supervisor1234@example.com', 1, 0)
INSERT [dbo].[Users] ([Id], [Username], [Firstname], [Lastname], [Password], [PasswordSalt], [Email], [Enabled], [IsDeleted]) VALUES (N'21998813-1f61-46f1-9270-e7e64cdfbbda', N'employee1234', N'employee', N'employee', N'0YHh49zDwBUMMRFXDgvv+isldnFzjszK0Jm/b+xoHyE=', N'yV79iEm97g5aBPmk4L3p1A==', N'employee1234@example.com', 1, 0)
GO
/****** Object:  Index [IX_Comments_TaskId]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Comments_TaskId] ON [dbo].[Comments]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_role_name]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_role_name] ON [dbo].[Roles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StatusHistories_TaskId]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_StatusHistories_TaskId] ON [dbo].[StatusHistories]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StatusHistories_UserId]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_StatusHistories_UserId] ON [dbo].[StatusHistories]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_username]    Script Date: 16/10/2024 4:29:57 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_username] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tasks_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tasks_TaskId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[StatusHistories]  WITH CHECK ADD  CONSTRAINT [FK_StatusHistories_Tasks_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatusHistories] CHECK CONSTRAINT [FK_StatusHistories_Tasks_TaskId]
GO
ALTER TABLE [dbo].[StatusHistories]  WITH CHECK ADD  CONSTRAINT [FK_StatusHistories_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatusHistories] CHECK CONSTRAINT [FK_StatusHistories_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [TaskManagerDB] SET  READ_WRITE 
GO
