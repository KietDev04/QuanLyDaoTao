﻿{"metadata":{"kernel_spec":{"name":"SQL","language":"sql","display_name":"SQL"},"language_info":{"name":"sql","version":""}},"nbformat":4,"nbformat_minor":2,"cells":[{"cell_type":"markdown","source":["# [dbo].[__EFMigrationsHistory]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='__EFMigrationsHistory' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["USE [QuanLyDaoTao]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='__EFMigrationsHistory' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[__EFMigrationsHistory](\r\n\t[MigrationId] [nvarchar](150) NOT NULL,\r\n\t[ProductVersion] [nvarchar](32) NOT NULL,\r\n CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED \r\n(\r\n\t[MigrationId] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='__EFMigrationsHistory' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetRoleClaims]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoleClaims' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetRoleClaims](\r\n\t[Id] [int] IDENTITY(1,1) NOT NULL,\r\n\t[RoleId] [nvarchar](450) NOT NULL,\r\n\t[ClaimType] [nvarchar](max) NULL,\r\n\t[ClaimValue] [nvarchar](max) NULL,\r\n CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoleClaims' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetRoles]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoles' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetRoles](\r\n\t[Id] [nvarchar](450) NOT NULL,\r\n\t[Name] [nvarchar](256) NULL,\r\n\t[NormalizedName] [nvarchar](256) NULL,\r\n\t[ConcurrencyStamp] [nvarchar](max) NULL,\r\n CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoles' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUserClaims]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserClaims' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetUserClaims](\r\n\t[Id] [int] IDENTITY(1,1) NOT NULL,\r\n\t[UserId] [nvarchar](450) NOT NULL,\r\n\t[ClaimType] [nvarchar](max) NULL,\r\n\t[ClaimValue] [nvarchar](max) NULL,\r\n CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserClaims' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUserLogins]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserLogins' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetUserLogins](\r\n\t[LoginProvider] [nvarchar](450) NOT NULL,\r\n\t[ProviderKey] [nvarchar](450) NOT NULL,\r\n\t[ProviderDisplayName] [nvarchar](max) NULL,\r\n\t[UserId] [nvarchar](450) NOT NULL,\r\n CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED \r\n(\r\n\t[LoginProvider] ASC,\r\n\t[ProviderKey] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserLogins' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUserRoles]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetUserRoles](\r\n\t[UserId] [nvarchar](450) NOT NULL,\r\n\t[RoleId] [nvarchar](450) NOT NULL,\r\n CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED \r\n(\r\n\t[UserId] ASC,\r\n\t[RoleId] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUsers]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUsers' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetUsers](\r\n\t[Id] [nvarchar](450) NOT NULL,\r\n\t[UserName] [nvarchar](256) NULL,\r\n\t[NormalizedUserName] [nvarchar](256) NULL,\r\n\t[Email] [nvarchar](256) NULL,\r\n\t[NormalizedEmail] [nvarchar](256) NULL,\r\n\t[EmailConfirmed] [bit] NOT NULL,\r\n\t[PasswordHash] [nvarchar](max) NULL,\r\n\t[SecurityStamp] [nvarchar](max) NULL,\r\n\t[ConcurrencyStamp] [nvarchar](max) NULL,\r\n\t[PhoneNumber] [nvarchar](max) NULL,\r\n\t[PhoneNumberConfirmed] [bit] NOT NULL,\r\n\t[TwoFactorEnabled] [bit] NOT NULL,\r\n\t[LockoutEnd] [datetimeoffset](7) NULL,\r\n\t[LockoutEnabled] [bit] NOT NULL,\r\n\t[AccessFailedCount] [int] NOT NULL,\r\n CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUsers' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUserTokens]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserTokens' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[AspNetUserTokens](\r\n\t[UserId] [nvarchar](450) NOT NULL,\r\n\t[LoginProvider] [nvarchar](450) NOT NULL,\r\n\t[Name] [nvarchar](450) NOT NULL,\r\n\t[Value] [nvarchar](max) NULL,\r\n CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED \r\n(\r\n\t[UserId] ASC,\r\n\t[LoginProvider] ASC,\r\n\t[Name] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserTokens' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[BaiGiangs]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[BaiGiangs]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[BaiGiangs](\r\n\t[Id] [int] IDENTITY(1,1) NOT NULL,\r\n\t[TenBaiGiang] [nvarchar](max) NOT NULL,\r\n\t[NoiDung] [nvarchar](max) NOT NULL,\r\n\t[DeCuongId] [int] NOT NULL,\r\n\t[FilePDF] [nvarchar](max) NOT NULL,\r\n\t[VideoUrl] [nvarchar](max) NOT NULL,\r\n CONSTRAINT [PK_BaiGiangs] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[DeCuongs]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[DeCuongs]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[DeCuongs](\r\n\t[Id] [int] IDENTITY(1,1) NOT NULL,\r\n\t[TenDeCuong] [nvarchar](max) NOT NULL,\r\n\t[KhoaId] [int] NOT NULL,\r\n CONSTRAINT [PK_DeCuongs] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[Khoas]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["/****** Object:  Table [dbo].[Khoas]    Script Date: 3/13/2025 1:28:56 PM ******/\r\nSET ANSI_NULLS ON\r\n","GO\r\n","SET QUOTED_IDENTIFIER ON\r\n","GO\r\n","CREATE TABLE [dbo].[Khoas](\r\n\t[Id] [int] IDENTITY(1,1) NOT NULL,\r\n\t[TenKhoa] [nvarchar](max) NOT NULL,\r\n\t[MoTa] [nvarchar](max) NOT NULL,\r\n CONSTRAINT [PK_Khoas] PRIMARY KEY CLUSTERED \r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\r\nGO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[__EFMigrationsHistory]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='__EFMigrationsHistory' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250312155457_InitialCreate', N'9.0.3')\r\n","INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313031604_AddMoTaColumnToKhoas', N'9.0.3')\r\n","INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313035837_AddIdentityTables', N'9.0.3')\r\n","INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313035956_AddFilePDFAndVideoUrlToBaiGiang', N'9.0.3')\r\n","INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313043004_SyncModelWithDatabase', N'9.0.3')\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='__EFMigrationsHistory' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetRoles]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoles' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'81ee809c-c33a-4ce9-913d-4cae9f1c183f', N'Admin', N'ADMIN', NULL)\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoles' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUserRoles]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'499d5b3f-e852-4d85-883b-eed17c47b694', N'81ee809c-c33a-4ce9-913d-4cae9f1c183f')\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[AspNetUsers]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUsers' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'499d5b3f-e852-4d85-883b-eed17c47b694', N'admin@example.com', N'ADMIN@EXAMPLE.COM', N'admin@example.com', N'ADMIN@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEMiyZvb5GP1HhLT7xNu3jLs0h0EeWwKPMqWTwOqCAOIIMM5ciIik+ckaxwkpFo4ClQ==', N'F6QSJXSRUNF6FN6MOWVNKKTSNZJRGFEH', N'41e7b38e-0249-49a5-85ba-4f7fe3f88864', NULL, 0, 0, NULL, 1, 0)\r\n","INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd390ebea-ff86-4698-8192-bad5c33719b2', N'kietn4623@gmail.com', N'KIETN4623@GMAIL.COM', N'kietn4623@gmail.com', N'KIETN4623@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEOxsFbLi29mTwLa2k857vCrX9Q53a8fa2sV/iXrnUB2aBLn9AiRzSchfNtIFI51/dQ==', N'K4OWHVPF4QPWRZ7NIBSJVHH5W6IP2GRJ', N'a7cf6549-dc3d-4456-9a55-a8c338fd3a37', NULL, 0, 0, NULL, 1, 0)\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUsers' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[BaiGiangs]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["SET IDENTITY_INSERT [dbo].[BaiGiangs] ON \r\n","INSERT [dbo].[BaiGiangs] ([Id], [TenBaiGiang], [NoiDung], [DeCuongId], [FilePDF], [VideoUrl]) VALUES (4, N'Bài 1: Các giai đoạn trong quản trị thiết kế một CSDL', N'Nội dung bài học về các giai đoạn thiết kế CSDL', 3, N'https://i.pinimg.com/736x/87/d5/2f/87d52f42221de0c43a6c67c133004fc6.jpg', N'https://youtu.be/6CQWklzrUZQ?si=tiISTrBwkpEomZWA')\r\n","INSERT [dbo].[BaiGiangs] ([Id], [TenBaiGiang], [NoiDung], [DeCuongId], [FilePDF], [VideoUrl]) VALUES (5, N'Bài 2: Dẫn nhập', N'Nội dung bài học dẫn nhập', 3, N'https://i.pinimg.com/236x/58/24/87/582487c207e347fd9ffb80a590c9a6bb.jpg', N'https://youtu.be/Ds9ASJ4tM1I?si=qRT9evcIQ77uJIZk')\r\n","INSERT [dbo].[BaiGiangs] ([Id], [TenBaiGiang], [NoiDung], [DeCuongId], [FilePDF], [VideoUrl]) VALUES (6, N'Bài 3: Mức tiều chính trong công việc thiết kế có sự hỗ trợ', N'Nội dung bài học về mức tiêu chính', 3, N'https://i.pinimg.com/236x/84/e2/04/84e204ffaae2ff5d5ee4fcf6106429b7.jpg', N'https://youtu.be/wtbRapiOBrI?si=bTvFALXMsVbt3SAe')\r\n","SET IDENTITY_INSERT [dbo].[BaiGiangs] OFF\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[DeCuongs]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["SET IDENTITY_INSERT [dbo].[DeCuongs] ON \r\n","INSERT [dbo].[DeCuongs] ([Id], [TenDeCuong], [KhoaId]) VALUES (3, N'Đề cương Lập trình C#', 3)\r\n","INSERT [dbo].[DeCuongs] ([Id], [TenDeCuong], [KhoaId]) VALUES (4, N'Đề cương Cơ sở dữ liệu', 4)\r\n","SET IDENTITY_INSERT [dbo].[DeCuongs] OFF\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [dbo].[Khoas]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']","object_type":"Table"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["SET IDENTITY_INSERT [dbo].[Khoas] ON \r\n","INSERT [dbo].[Khoas] ([Id], [TenKhoa], [MoTa]) VALUES (3, N'Lập trình C#', N'Khóa học cơ bản về lập trình C#')\r\n","INSERT [dbo].[Khoas] ([Id], [TenKhoa], [MoTa]) VALUES (4, N'Cơ sở dữ liệu', N'Khóa học về quản lý cơ sở dữ liệu')\r\n","SET IDENTITY_INSERT [dbo].[Khoas] OFF\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']","object_type":"Table"}},{"cell_type":"markdown","source":["# [DF__BaiGiangs__FileP__52593CB8]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/Column[@Name='FilePDF']/Default[@Name='DF__BaiGiangs__FileP__52593CB8']","object_type":"Default"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[BaiGiangs] ADD  DEFAULT (N'') FOR [FilePDF]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/Column[@Name='FilePDF']/Default[@Name='DF__BaiGiangs__FileP__52593CB8']","object_type":"Default"}},{"cell_type":"markdown","source":["# [DF__BaiGiangs__Video__534D60F1]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/Column[@Name='VideoUrl']/Default[@Name='DF__BaiGiangs__Video__534D60F1']","object_type":"Default"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[BaiGiangs] ADD  DEFAULT (N'') FOR [VideoUrl]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/Column[@Name='VideoUrl']/Default[@Name='DF__BaiGiangs__Video__534D60F1']","object_type":"Default"}},{"cell_type":"markdown","source":["# [DF__Khoas__MoTa__5165187F]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']/Column[@Name='MoTa']/Default[@Name='DF__Khoas__MoTa__5165187F']","object_type":"Default"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[Khoas] ADD  DEFAULT (N'') FOR [MoTa]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='Khoas' and @Schema='dbo']/Column[@Name='MoTa']/Default[@Name='DF__Khoas__MoTa__5165187F']","object_type":"Default"}},{"cell_type":"markdown","source":["# [FK_AspNetRoleClaims_AspNetRoles_RoleId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoleClaims' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetRoleClaims_AspNetRoles_RoleId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])\r\nREFERENCES [dbo].[AspNetRoles] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetRoleClaims' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetRoleClaims_AspNetRoles_RoleId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_AspNetUserClaims_AspNetUsers_UserId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserClaims' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserClaims_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])\r\nREFERENCES [dbo].[AspNetUsers] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserClaims' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserClaims_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_AspNetUserLogins_AspNetUsers_UserId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserLogins' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserLogins_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])\r\nREFERENCES [dbo].[AspNetUsers] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserLogins' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserLogins_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_AspNetUserRoles_AspNetRoles_RoleId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserRoles_AspNetRoles_RoleId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])\r\nREFERENCES [dbo].[AspNetRoles] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserRoles_AspNetRoles_RoleId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_AspNetUserRoles_AspNetUsers_UserId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserRoles_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])\r\nREFERENCES [dbo].[AspNetUsers] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserRoles' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserRoles_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_AspNetUserTokens_AspNetUsers_UserId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserTokens' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserTokens_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])\r\nREFERENCES [dbo].[AspNetUsers] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='AspNetUserTokens' and @Schema='dbo']/ForeignKey[@Name='FK_AspNetUserTokens_AspNetUsers_UserId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_BaiGiangs_DeCuongs_DeCuongId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/ForeignKey[@Name='FK_BaiGiangs_DeCuongs_DeCuongId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[BaiGiangs]  WITH CHECK ADD  CONSTRAINT [FK_BaiGiangs_DeCuongs_DeCuongId] FOREIGN KEY([DeCuongId])\r\nREFERENCES [dbo].[DeCuongs] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[BaiGiangs] CHECK CONSTRAINT [FK_BaiGiangs_DeCuongs_DeCuongId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='BaiGiangs' and @Schema='dbo']/ForeignKey[@Name='FK_BaiGiangs_DeCuongs_DeCuongId']","object_type":"ForeignKey"}},{"cell_type":"markdown","source":["# [FK_DeCuongs_Khoas_KhoaId]"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']/ForeignKey[@Name='FK_DeCuongs_Khoas_KhoaId']","object_type":"ForeignKey"}},{"outputs":[],"execution_count":0,"cell_type":"code","source":["ALTER TABLE [dbo].[DeCuongs]  WITH CHECK ADD  CONSTRAINT [FK_DeCuongs_Khoas_KhoaId] FOREIGN KEY([KhoaId])\r\nREFERENCES [dbo].[Khoas] ([Id])\r\nON DELETE CASCADE\r\n","GO\r\n","ALTER TABLE [dbo].[DeCuongs] CHECK CONSTRAINT [FK_DeCuongs_Khoas_KhoaId]\r\n","GO\r\n"],"metadata":{"urn":"Server[@Name='DESKTOP-2C56F5J\\SQLEXPRESS02']/Database[@Name='QuanLyDaoTao']/Table[@Name='DeCuongs' and @Schema='dbo']/ForeignKey[@Name='FK_DeCuongs_Khoas_KhoaId']","object_type":"ForeignKey"}}]}