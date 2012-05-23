use master
drop database [OnlineSPKT]
go
create database [OnlineSPKT]
go

USE [OnlineSPKT]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Languages] ([ID], [Name], [Description]) VALUES (1, N'C++', NULL)
INSERT [dbo].[Languages] ([ID], [Name], [Description]) VALUES (2, N'VB', NULL)
/****** Object:  Table [dbo].[File]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[File](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Type] [nvarchar](50) NULL,
	[Content] [varbinary](max) NULL,
	[Length] [varchar](max) NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Facuties]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Facuties](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Facuties] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comparers]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comparers](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Descripsion] [nvarchar](255) NULL,
	[ClassName] [nvarchar](255) NULL,
	[DllPath] [ntext] NULL,
 CONSTRAINT [PK_CompareTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Comparers] ([ID], [Name], [Descripsion], [ClassName], [DllPath]) VALUES (1, N'So sánh số nguyên', NULL, N'ChamDiem.SoSanh.SoSanhSoNguyen', N'SoSanh.dll')
INSERT [dbo].[Comparers] ([ID], [Name], [Descripsion], [ClassName], [DllPath]) VALUES (2, N'So sánh tùy chọn', NULL, N'ChamDiem.SoSanh.SoSanhExternal', N'SoSanh.dll')
/****** Object:  Table [dbo].[Roles]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Descripsion] [nvarchar](255) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Roles] ([ID], [Name], [Descripsion]) VALUES (1, N'Admin', N'Full Permission')
INSERT [dbo].[Roles] ([ID], [Name], [Descripsion]) VALUES (2, N'Lecturer', NULL)
INSERT [dbo].[Roles] ([ID], [Name], [Descripsion]) VALUES (3, N'Student', NULL)
/****** Object:  Table [dbo].[Difficulties]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Difficulties](
	[DifficultyID] [int] NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [nchar](10) NULL,
 CONSTRAINT [PK_Difficulty] PRIMARY KEY CLUSTERED 
(
	[DifficultyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Difficulties] ([DifficultyID], [Name], [Description]) VALUES (1, N'Dễ                                                ', NULL)
INSERT [dbo].[Difficulties] ([DifficultyID], [Name], [Description]) VALUES (2, N'Trung bình                                        ', NULL)
INSERT [dbo].[Difficulties] ([DifficultyID], [Name], [Description]) VALUES (3, N'Khó                                               ', NULL)
/****** Object:  Table [dbo].[Users]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](255) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[IsActived] [bit] NULL,
	[IsLocked] [bit] NULL,
	[LastLoginTime] [datetime] NULL,
	[PasswordInvalidCount] [int] NULL,
	[SecurityCode] [varchar](50) NULL,
	[SecurityCodeEndTime] [datetime] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'08110036', N'1b0571a6b55b9cef216c4f8a0ab7231f', N'Hải', N'Võ Trường', N'ocrenaka@gmail.com', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'08110052', N'757093c4abdbb2e351d431a7003040c2', N'Hùng', N'Trần Văn', N'vanhungcntt08@gmail.com', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'08110103', N'ca078e21dcf2a75cbe771d085e414338', N'Tâm', N'Lữ Thị Tố', N'08110103@student.hcmute.edu.vn', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'08110104', N'1e5c810d5d45f6de90ca1ecb3f4dcc48', N'Tâm', N'Nguyễn Công', N'nguyencongtam642@yahoo.com', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'08110142', N'1b48093393c3a875b9b121b9506f54b7', N'Vân', N'Dương Thị Thu', N'thuvan_081102@yahoo.com', 1, 0, CAST(0x0000A05A017A6B05 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'giangvien1', N'4297f44b13955235245b2497399d7a93', N'Một', N'Giảng Viên', N'nguyenhuutrung@gmail.com', NULL, 0, CAST(0x0000A05A017DA4E4 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Users] ([Username], [Password], [FirstName], [LastName], [Email], [IsActived], [IsLocked], [LastLoginTime], [PasswordInvalidCount], [SecurityCode], [SecurityCodeEndTime]) VALUES (N'sinhvien', N'c4ca4238a0b923820dcc509a6f75849b', N'Sinh', N'Viên', N'sv@gmail.com', NULL, 0, CAST(0x0000A05A0181B5E2 AS DateTime), NULL, NULL, NULL)
/****** Object:  Table [dbo].[Subjects]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subjects](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Subjects] ([ID], [Name], [IsDeleted]) VALUES (N'0112001', N'Cấu trúc dữ liệu 1', 0)
INSERT [dbo].[Subjects] ([ID], [Name], [IsDeleted]) VALUES (N'1179011', N'Cơ sở lập trình 1', 0)
INSERT [dbo].[Subjects] ([ID], [Name], [IsDeleted]) VALUES (N'1179012', N'Cơ sở lập trình 2', 0)
/****** Object:  Table [dbo].[Lecturer_Subject]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lecturer_Subject](
	[MaGV] [varchar](50) NOT NULL,
	[MaMH] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GiaoVien_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC,
	[MaMH] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Lecturer_Subject] ([MaGV], [MaMH]) VALUES (N'giangvien1', N'1179011')
/****** Object:  Table [dbo].[User_Role]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_Role](
	[Username] [varchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[Username] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110036', 3)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110052', 3)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110103', 3)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110104', 3)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110142', 1)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'08110142', 3)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'giangvien1', 1)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'giangvien1', 2)
INSERT [dbo].[User_Role] ([Username], [RoleID]) VALUES (N'sinhvien', 3)
/****** Object:  Table [dbo].[Classes]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Classes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectID] [varchar](50) NOT NULL,
	[Group] [nvarchar](50) NOT NULL,
	[Term] [nvarchar](50) NOT NULL,
	[SchoolYear] [nvarchar](100) NOT NULL,
	[LecturerID] [varchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON
INSERT [dbo].[Classes] ([ID], [SubjectID], [Group], [Term], [SchoolYear], [LecturerID], [IsDeleted]) VALUES (2, N'1179011', N'1', N'HK II', N'2011-2012', N'giangvien1', 0)
SET IDENTITY_INSERT [dbo].[Classes] OFF
/****** Object:  Table [dbo].[Exam]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exam](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[LecturerID] [varchar](50) NULL,
	[ClassID] [int] NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Class_Student]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Class_Student](
	[ClassID] [int] NOT NULL,
	[StudentID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Class_Student] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC,
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contests]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NOT NULL,
	[ExamID] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[TotalScore] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_NewContest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Problems]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Problems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LecturerID] [varchar](50) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Content] [ntext] NULL,
	[MemoryLimit] [int] NOT NULL,
	[TimeLimit] [int] NOT NULL,
	[ComparerID] [int] NOT NULL,
	[DifficultyID] [int] NOT NULL,
	[IsHiden] [bit] NOT NULL,
	[AvailableTime] [datetime] NULL,
	[TimeStamp] [timestamp] NULL,
	[ComparerParameter] [ntext] NULL,
	[FileID] [int] NULL,
	[Score] [float] NOT NULL,
	[SubjectID] [varchar](50) NOT NULL,
	[ExamID] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DeBai] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student_Submit]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student_Submit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProblemID] [int] NOT NULL,
	[StudentID] [varchar](50) NOT NULL,
	[ContestID] [int] NULL,
	[SourceCode] [ntext] NOT NULL,
	[TrangThaiBienDich] [int] NOT NULL,
	[TrangThaiCham] [int] NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[LanguageID] [int] NULL,
	[SubmitTime] [datetime] NOT NULL,
	[CompilerError] [nvarchar](1000) NULL,
 CONSTRAINT [PK_BaiThi_ThiSinh] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Problem_Class]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Problem_Class](
	[ProblemID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
 CONSTRAINT [PK_Problem_Class_1] PRIMARY KEY CLUSTERED 
(
	[ProblemID] ASC,
	[ClassID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contest_Student]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contest_Student](
	[StudentID] [varchar](50) NOT NULL,
	[ContestID] [int] NOT NULL,
	[Score] [float] NULL,
 CONSTRAINT [PK_Contest_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC,
	[ContestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TestCases]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCases](
	[MaTestCase] [int] IDENTITY(1,1) NOT NULL,
	[MaDB] [int] NOT NULL,
	[Input] [ntext] NOT NULL,
	[Output] [ntext] NOT NULL,
	[MoTa] [ntext] NULL,
	[Diem] [float] NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_TestCase] PRIMARY KEY CLUSTERED 
(
	[MaTestCase] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestCaseResult]    Script Date: 05/23/2012 23:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCaseResult](
	[StudentSubmitID] [int] NOT NULL,
	[TestCaseID] [int] NOT NULL,
	[ExecutionTime] [int] NULL,
	[ExecutionMemory] [int] NULL,
	[Score] [float] NULL,
	[Comment] [nvarchar](255) NULL,
 CONSTRAINT [PK_TestCaseResult] PRIMARY KEY CLUSTERED 
(
	[StudentSubmitID] ASC,
	[TestCaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Classes_IsDeleted]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Classes] ADD  CONSTRAINT [DF_Classes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Contests_IsDeleted]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Contests] ADD  CONSTRAINT [DF_Contests_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Exam_IsDeleted]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Exam] ADD  CONSTRAINT [DF_Exam_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Problems_IsDeleted]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems] ADD  CONSTRAINT [DF_Problems_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Subjects_IsDeleted]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Subjects] ADD  CONSTRAINT [DF_Subjects_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Users_IsActived]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActived]  DEFAULT ((0)) FOR [IsActived]
GO
/****** Object:  Default [DF_Users_IsLocked]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsLocked]  DEFAULT ((1)) FOR [IsLocked]
GO
/****** Object:  ForeignKey [FK_Class_Student_Classes]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Class_Student]  WITH CHECK ADD  CONSTRAINT [FK_Class_Student_Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ID])
GO
ALTER TABLE [dbo].[Class_Student] CHECK CONSTRAINT [FK_Class_Student_Classes]
GO
/****** Object:  ForeignKey [FK_Class_Student_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Class_Student]  WITH CHECK ADD  CONSTRAINT [FK_Class_Student_Users] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Class_Student] CHECK CONSTRAINT [FK_Class_Student_Users]
GO
/****** Object:  ForeignKey [FK_Classes_Subjects]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Subjects] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([ID])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Subjects]
GO
/****** Object:  ForeignKey [FK_Classes_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Users] FOREIGN KEY([LecturerID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Users]
GO
/****** Object:  ForeignKey [FK_Contest_Student_Contests]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Contest_Student]  WITH CHECK ADD  CONSTRAINT [FK_Contest_Student_Contests] FOREIGN KEY([ContestID])
REFERENCES [dbo].[Contests] ([ID])
GO
ALTER TABLE [dbo].[Contest_Student] CHECK CONSTRAINT [FK_Contest_Student_Contests]
GO
/****** Object:  ForeignKey [FK_Contest_Student_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Contest_Student]  WITH CHECK ADD  CONSTRAINT [FK_Contest_Student_Users] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Contest_Student] CHECK CONSTRAINT [FK_Contest_Student_Users]
GO
/****** Object:  ForeignKey [FK_NewContest_Classes]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Contests]  WITH CHECK ADD  CONSTRAINT [FK_NewContest_Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ID])
GO
ALTER TABLE [dbo].[Contests] CHECK CONSTRAINT [FK_NewContest_Classes]
GO
/****** Object:  ForeignKey [FK_NewContest_Exam]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Contests]  WITH CHECK ADD  CONSTRAINT [FK_NewContest_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ID])
GO
ALTER TABLE [dbo].[Contests] CHECK CONSTRAINT [FK_NewContest_Exam]
GO
/****** Object:  ForeignKey [FK_Exam_Classes]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ID])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Exam_Classes]
GO
/****** Object:  ForeignKey [FK_Exam_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_Users] FOREIGN KEY([LecturerID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Exam_Users]
GO
/****** Object:  ForeignKey [FK_Lecturer_Subject_Subjects]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Lecturer_Subject]  WITH CHECK ADD  CONSTRAINT [FK_Lecturer_Subject_Subjects] FOREIGN KEY([MaMH])
REFERENCES [dbo].[Subjects] ([ID])
GO
ALTER TABLE [dbo].[Lecturer_Subject] CHECK CONSTRAINT [FK_Lecturer_Subject_Subjects]
GO
/****** Object:  ForeignKey [FK_Lecturer_Subject_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Lecturer_Subject]  WITH CHECK ADD  CONSTRAINT [FK_Lecturer_Subject_Users] FOREIGN KEY([MaGV])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Lecturer_Subject] CHECK CONSTRAINT [FK_Lecturer_Subject_Users]
GO
/****** Object:  ForeignKey [FK_Problem_Class_Classes]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problem_Class]  WITH CHECK ADD  CONSTRAINT [FK_Problem_Class_Classes] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ID])
GO
ALTER TABLE [dbo].[Problem_Class] CHECK CONSTRAINT [FK_Problem_Class_Classes]
GO
/****** Object:  ForeignKey [FK_Problem_Class_Problems]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problem_Class]  WITH CHECK ADD  CONSTRAINT [FK_Problem_Class_Problems] FOREIGN KEY([ProblemID])
REFERENCES [dbo].[Problems] ([ID])
GO
ALTER TABLE [dbo].[Problem_Class] CHECK CONSTRAINT [FK_Problem_Class_Problems]
GO
/****** Object:  ForeignKey [FK_Problems_Comparers]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_Comparers] FOREIGN KEY([ComparerID])
REFERENCES [dbo].[Comparers] ([ID])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_Comparers]
GO
/****** Object:  ForeignKey [FK_Problems_Difficulties]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_Difficulties] FOREIGN KEY([DifficultyID])
REFERENCES [dbo].[Difficulties] ([DifficultyID])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_Difficulties]
GO
/****** Object:  ForeignKey [FK_Problems_Exam]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ID])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_Exam]
GO
/****** Object:  ForeignKey [FK_Problems_File]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_File] FOREIGN KEY([FileID])
REFERENCES [dbo].[File] ([ID])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_File]
GO
/****** Object:  ForeignKey [FK_Problems_Subjects]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_Subjects] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([ID])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_Subjects]
GO
/****** Object:  ForeignKey [FK_Problems_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Problems]  WITH CHECK ADD  CONSTRAINT [FK_Problems_Users] FOREIGN KEY([LecturerID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Problems] CHECK CONSTRAINT [FK_Problems_Users]
GO
/****** Object:  ForeignKey [FK_Student_Summit_Contests]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Student_Submit]  WITH CHECK ADD  CONSTRAINT [FK_Student_Summit_Contests] FOREIGN KEY([ContestID])
REFERENCES [dbo].[Contests] ([ID])
GO
ALTER TABLE [dbo].[Student_Submit] CHECK CONSTRAINT [FK_Student_Summit_Contests]
GO
/****** Object:  ForeignKey [FK_Student_Summit_Languages]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Student_Submit]  WITH CHECK ADD  CONSTRAINT [FK_Student_Summit_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([ID])
GO
ALTER TABLE [dbo].[Student_Submit] CHECK CONSTRAINT [FK_Student_Summit_Languages]
GO
/****** Object:  ForeignKey [FK_Student_Summit_Problems]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Student_Submit]  WITH CHECK ADD  CONSTRAINT [FK_Student_Summit_Problems] FOREIGN KEY([ProblemID])
REFERENCES [dbo].[Problems] ([ID])
GO
ALTER TABLE [dbo].[Student_Submit] CHECK CONSTRAINT [FK_Student_Summit_Problems]
GO
/****** Object:  ForeignKey [FK_Student_Summit_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[Student_Submit]  WITH CHECK ADD  CONSTRAINT [FK_Student_Summit_Users] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Student_Submit] CHECK CONSTRAINT [FK_Student_Summit_Users]
GO
/****** Object:  ForeignKey [FK_TestCaseResult_Student_Summit]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[TestCaseResult]  WITH CHECK ADD  CONSTRAINT [FK_TestCaseResult_Student_Summit] FOREIGN KEY([StudentSubmitID])
REFERENCES [dbo].[Student_Submit] ([ID])
GO
ALTER TABLE [dbo].[TestCaseResult] CHECK CONSTRAINT [FK_TestCaseResult_Student_Summit]
GO
/****** Object:  ForeignKey [FK_TestCaseResult_TestCases]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[TestCaseResult]  WITH CHECK ADD  CONSTRAINT [FK_TestCaseResult_TestCases] FOREIGN KEY([TestCaseID])
REFERENCES [dbo].[TestCases] ([MaTestCase])
GO
ALTER TABLE [dbo].[TestCaseResult] CHECK CONSTRAINT [FK_TestCaseResult_TestCases]
GO
/****** Object:  ForeignKey [FK_TestCases_Problems]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[TestCases]  WITH CHECK ADD  CONSTRAINT [FK_TestCases_Problems] FOREIGN KEY([MaDB])
REFERENCES [dbo].[Problems] ([ID])
GO
ALTER TABLE [dbo].[TestCases] CHECK CONSTRAINT [FK_TestCases_Problems]
GO
/****** Object:  ForeignKey [FK_User_Role_Roles]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_Roles]
GO
/****** Object:  ForeignKey [FK_User_Role_Users]    Script Date: 05/23/2012 23:43:35 ******/
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Users] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_Users]
GO
