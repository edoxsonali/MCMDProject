USE [master]
GO
/****** Object:  Database [MCMD]    Script Date: 5/20/2015 6:11:32 PM ******/
CREATE DATABASE [MCMD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MCMD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MCMD.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MCMD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MCMD_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MCMD] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MCMD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MCMD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MCMD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MCMD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MCMD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MCMD] SET ARITHABORT OFF 
GO
ALTER DATABASE [MCMD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MCMD] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MCMD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MCMD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MCMD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MCMD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MCMD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MCMD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MCMD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MCMD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MCMD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MCMD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MCMD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MCMD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MCMD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MCMD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MCMD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MCMD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MCMD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MCMD] SET  MULTI_USER 
GO
ALTER DATABASE [MCMD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MCMD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MCMD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MCMD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MCMD', N'ON'
GO
USE [MCMD]
GO
/****** Object:  StoredProcedure [dbo].[GetViewDoctor]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetViewDoctor]
(	
	 @RoleId		INT=0,
	 @SpecialityID  INT=0, 
	 @EmployeeId    INT=0,
	 @FirstName		varchar(100)='',
     @LastName		varchar(100)='',
     @EmailID		varchar(100)='',
     @UserPhone		NVARCHAR(100)='',
	 @ClinicInfoId	INT=0
	
)

AS
BEGIN 


Select  L.LoginId,
        L.UserName,		
		L.FirstName,
		L.LastName,
		S.SpecialityName,
		L.EmailID,	
		L.UserPhone,
		R.RoleName,
		C.ClinicName,
		M.MembershipType
	from dbo.[Login] L
		INNER JOIN dbo.[Login_Role] LR WITH (NOLOCK)
				ON L.LoginId=LR.LoginId
				AND L.InactiveFlag='N'
		INNER JOIN dbo.[MCMDRoles] R WITH (NOLOCK)
				ON R.RoleId=LR.RoleId
		INNER JOIN dbo.[Login_Speciality] LS With (NOLOCK)
				ON LS.LoginId=L.LoginId
		INNER JOIN dbo.[Speciality] S With (NOLOCk)
				ON S.SpecialityID=LS.SpecialityID
		INNER JOIN dbo.[DoctorClinicInformation] C  With (NOLOCk)
				ON C.LoginId=L.LoginId
		INNER JOIN dbo.[UpgradeService] U With (NOLOCk)
				ON U.LoginId = L.LoginId
		INNER JOIN dbo.[Membership] M  With (NOLOCk)
				ON M.MembershipId = U.MembershipId
 		WHERE (R.RoleId = @RoleId OR  @RoleId=0 )
			AND (L.LoginId= @EmployeeId OR  @EmployeeId=0)
			AND (L.FirstName= @FirstName OR @FirstName='')
			AND (L.LastName=@LastName OR @LastName='')
			AND (L.EmailID=@EmailID OR @EmailID='')
			AND (L.UserPhone=@UserPhone OR @UserPhone='')
			AND (S.SpecialityID=@SpecialityID OR @SpecialityID=0 )
			AND (C.ClinicInfoId= @ClinicInfoId OR  @ClinicInfoId=0)



END

 --EXEC GetViewDoctor @RoleId=4 ,@EmployeeId=1016,@FirstName='gauri',@LastName='rakshe',@EmailID='gauri@gmail.com',@UserPhone='9656895856' 
--EXEC GetViewDoctor @RoleId=4 ,@EmployeeId=22, @SpecialityID =2,@FirstName='',@LastName='',@EmailID='',@UserPhone='' 
GO
/****** Object:  StoredProcedure [dbo].[GetViewUsers]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetViewUsers]
(	
	 @RoleId		INT=0,
	 @EmployeeId    INT=0,
	 @FirstName		varchar(100)='',
     @LastName		varchar(100)='',
     @EmailID		varchar(100)='',
     @UserPhone		NVARCHAR(100)=''
)

AS
BEGIN 


Select  L.LoginId,
        L.UserName,
		L.FirstName,
		L.LastName,
		L.EmailID,
		L.EmployeeId,
		L.UserPhone,
		R.RoleName
	from dbo.[Login] L
		INNER JOIN dbo.[Login_Role] LR WITH (NOLOCK)
				ON L.LoginId=LR.LoginId
				AND L.InactiveFlag='N'
		INNER JOIN dbo.[MCMDRoles] R WITH (NOLOCK)
				ON R.RoleId=LR.RoleId
		WHERE (R.RoleId = @RoleId OR  @RoleId=0 )
			AND (L.EmployeeId= @EmployeeId OR  @EmployeeId=0)
			AND (L.FirstName= @FirstName OR @FirstName='')
			AND (L.LastName=@LastName OR @LastName='')
			AND (L.EmailID=@EmailID OR @EmailID='')
			AND (L.UserPhone=@UserPhone OR @UserPhone='')


END

 --EXEC GetViewUsers @RoleId=2 ,@EmployeeId=1006,@FirstName='sneha',@LastName='adya',@EmailID='sneha@g.com',@UserPhone='968958789' 

 --EXEC GetViewUsers @RoleId=4 ,@EmployeeId=0,@FirstName='sneha',@LastName='',@EmailID='',@UserPhone='' 
GO
/****** Object:  Table [dbo].[AutoRenaval]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoRenaval](
	[AutoRenavalId] [int] IDENTITY(1,1) NOT NULL,
	[Renaval] [nvarchar](50) NULL,
 CONSTRAINT [PK_AutoRenaval] PRIMARY KEY CLUSTERED 
(
	[AutoRenavalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](max) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorClinicInformation]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DoctorClinicInformation](
	[ClinicInfoId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[ClinicName] [nvarchar](50) NULL,
	[ClinicAddress] [nvarchar](max) NULL,
	[ClinicPhoneNo] [nvarchar](50) NULL,
	[ClinicFees] [int] NULL,
	[ClinicTimeFrom] [time](7) NULL,
	[ClinicTimeTo] [time](7) NOT NULL,
	[ClinicLunchbreakFrom] [time](7) NULL,
	[ClinicLunchbreakTo] [time](7) NULL,
	[Country] [int] NULL,
	[State] [int] NULL,
	[City] [int] NULL,
	[ZipCode] [int] NULL,
	[ClinicServices] [nvarchar](max) NULL,
	[AwardsAndRecognization] [nvarchar](max) NULL,
	[AboutClinic] [nvarchar](max) NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedByID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ClinicInformation] PRIMARY KEY CLUSTERED 
(
	[ClinicInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DoctorPersonalInformation]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DoctorPersonalInformation](
	[PersonalInfoId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Qualification] [nvarchar](50) NULL,
	[RegistrationNo] [int] NULL,
	[Affiliation] [nvarchar](50) NULL,
	[AboutMe] [nvarchar](max) NULL,
	[AboutExperience] [nvarchar](max) NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedByID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DoctorPersonalInformation] PRIMARY KEY CLUSTERED 
(
	[PersonalInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Duration]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Duration](
	[DurationId] [int] IDENTITY(1,1) NOT NULL,
	[Durations] [nvarchar](50) NULL,
 CONSTRAINT [PK_Duration] PRIMARY KEY CLUSTERED 
(
	[DurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Help]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Help](
	[HelpId] [int] IDENTITY(1,1) NOT NULL,
	[docId] [int] NULL,
	[DocName] [nvarchar](50) NULL,
	[Subject] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Help] PRIMARY KEY CLUSTERED 
(
	[HelpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[LoginId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](30) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[EmailID] [varchar](50) NOT NULL,
	[EmployeeId] [int] NULL,
	[Password] [nvarchar](max) NOT NULL,
	[ConfirmPassword] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[IslockedOut] [bit] NULL,
	[LastLockoutDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastLogOutDate] [datetime] NULL,
	[IPAddress] [varchar](20) NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[PasswordVerificationToken] [nvarchar](max) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NULL,
	[UserPhone] [nvarchar](20) NULL,
	[InactiveFlag] [char](1) NOT NULL,
	[CreatedByID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Login_LoginId] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login_Role]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login_Role](
	[LoginRoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[LoginId] [int] NULL,
 CONSTRAINT [PK_Login_Role] PRIMARY KEY CLUSTERED 
(
	[LoginRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login_Speciality]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login_Speciality](
	[LoginSpecialityId] [int] IDENTITY(1,1) NOT NULL,
	[SpecialityID] [int] NULL,
	[LoginId] [int] NULL,
 CONSTRAINT [PK_Login_Speciality] PRIMARY KEY CLUSTERED 
(
	[LoginSpecialityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MCMDRoles]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MCMDRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RoleName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.MCMDRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Media]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Media](
	[MediaId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[FolderFilePath] [varchar](max) NULL,
	[UploadType] [varchar](20) NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedByID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Membership](
	[MembershipId] [int] IDENTITY(1,1) NOT NULL,
	[MembershipType] [nvarchar](50) NULL,
	[Fees] [int] NULL,
	[Duration] [int] NULL,
	[AutoRenaval] [int] NULL,
	[InactiveFlag] [char](1) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[MembershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Speciality](
	[SpecialityID] [int] IDENTITY(1,1) NOT NULL,
	[SpecialityName] [nvarchar](50) NULL,
	[InactiveFlag] [char](1) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[SpecialityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[State]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](max) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Title]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Title](
	[TitleId] [int] IDENTITY(1,1) NOT NULL,
	[TitleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[TitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UpgradeService]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpgradeService](
	[UpgradeServiceId] [int] IDENTITY(1,1) NOT NULL,
	[MembershipId] [int] NULL,
	[LoginId] [int] NULL,
	[Durations] [int] NULL,
	[AutoRenaval] [int] NULL,
	[CreatedById] [int] NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedOnDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedOnDate] [datetime] NULL,
 CONSTRAINT [PK_UpgradeService_1] PRIMARY KEY CLUSTERED 
(
	[UpgradeServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UpgradeServiceLog]    Script Date: 5/20/2015 6:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpgradeServiceLog](
	[UpgradeServLogId] [int] NOT NULL,
	[MembershipId] [int] NULL,
	[LoginId] [int] NULL,
	[Durations] [int] NULL,
	[AutoRenaval] [int] NULL,
	[CreatedById] [int] NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedOnDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedOnDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AutoRenaval] ON 

GO
INSERT [dbo].[AutoRenaval] ([AutoRenavalId], [Renaval]) VALUES (1, N'Yes')
GO
INSERT [dbo].[AutoRenaval] ([AutoRenavalId], [Renaval]) VALUES (2, N'No')
GO
SET IDENTITY_INSERT [dbo].[AutoRenaval] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 

GO
INSERT [dbo].[City] ([CityId], [CityName]) VALUES (1, N'Pune')
GO
INSERT [dbo].[City] ([CityId], [CityName]) VALUES (2, N'Mumbai')
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

GO
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (1, N'India')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorClinicInformation] ON 

GO
INSERT [dbo].[DoctorClinicInformation] ([ClinicInfoId], [LoginId], [ClinicName], [ClinicAddress], [ClinicPhoneNo], [ClinicFees], [ClinicTimeFrom], [ClinicTimeTo], [ClinicLunchbreakFrom], [ClinicLunchbreakTo], [Country], [State], [City], [ZipCode], [ClinicServices], [AwardsAndRecognization], [AboutClinic], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 17, N'Test', N'at pune', N'6598565263', 1200, CAST(0x070040230E430000 AS Time), CAST(0x0700D85EAC3A0000 AS Time), CAST(0x070068C461080000 AS Time), CAST(0x0700D088C3100000 AS Time), 1, 1, 1, 41105, N' Services', N'Awards And Recognization', N'AboutClinic', N'N', 1, CAST(0x0000A4990120FFF1 AS DateTime), 1, CAST(0x0000A499012100EB AS DateTime))
GO
INSERT [dbo].[DoctorClinicInformation] ([ClinicInfoId], [LoginId], [ClinicName], [ClinicAddress], [ClinicPhoneNo], [ClinicFees], [ClinicTimeFrom], [ClinicTimeTo], [ClinicLunchbreakFrom], [ClinicLunchbreakTo], [Country], [State], [City], [ZipCode], [ClinicServices], [AwardsAndRecognization], [AboutClinic], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 17, N'Test', N'at pune', N'9656236598', 1200, CAST(0x070040230E430000 AS Time), CAST(0x0700D85EAC3A0000 AS Time), CAST(0x070068C461080000 AS Time), CAST(0x0700D088C3100000 AS Time), 1, 1, 1, 41105, N'Services', N'Awards And Recognization', N'AboutClinic', N'N', 1, CAST(0x0000A4990123717D AS DateTime), 1, CAST(0x0000A4990123717D AS DateTime))
GO
INSERT [dbo].[DoctorClinicInformation] ([ClinicInfoId], [LoginId], [ClinicName], [ClinicAddress], [ClinicPhoneNo], [ClinicFees], [ClinicTimeFrom], [ClinicTimeTo], [ClinicLunchbreakFrom], [ClinicLunchbreakTo], [Country], [State], [City], [ZipCode], [ClinicServices], [AwardsAndRecognization], [AboutClinic], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, 17, N'Sonali', N'at pune', N'06598565263', 1000, CAST(0x070074053F470000 AS Time), CAST(0x0700D85EAC3A0000 AS Time), CAST(0x070068C461080000 AS Time), CAST(0x0700D088C3100000 AS Time), 1, 1, 1, 411057, N'Servicess', N'award', N'about', N'N', 1, CAST(0x0000A49E01153321 AS DateTime), 1, CAST(0x0000A49E01153321 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DoctorClinicInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorPersonalInformation] ON 

GO
INSERT [dbo].[DoctorPersonalInformation] ([PersonalInfoId], [LoginId], [MiddleName], [Qualification], [RegistrationNo], [Affiliation], [AboutMe], [AboutExperience], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 1, N'test', N'MCS', 9656, N'test', N'test', N'test', N'N', 1, CAST(0x0000A49A012CB3BE AS DateTime), 1, CAST(0x0000A49A012CB3BE AS DateTime))
GO
INSERT [dbo].[DoctorPersonalInformation] ([PersonalInfoId], [LoginId], [MiddleName], [Qualification], [RegistrationNo], [Affiliation], [AboutMe], [AboutExperience], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 1, N'test', N'MCS', 96569, N'Affiliation', N'about', N'erer', N'N', 1, CAST(0x0000A49B00FF44CF AS DateTime), 1, CAST(0x0000A49B00FF44CF AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DoctorPersonalInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[Duration] ON 

GO
INSERT [dbo].[Duration] ([DurationId], [Durations]) VALUES (1, N'Monthly')
GO
INSERT [dbo].[Duration] ([DurationId], [Durations]) VALUES (2, N'Quarterly')
GO
INSERT [dbo].[Duration] ([DurationId], [Durations]) VALUES (3, N'Half yearly')
GO
INSERT [dbo].[Duration] ([DurationId], [Durations]) VALUES (4, N'Annually')
GO
SET IDENTITY_INSERT [dbo].[Duration] OFF
GO
SET IDENTITY_INSERT [dbo].[Help] ON 

GO
INSERT [dbo].[Help] ([HelpId], [docId], [DocName], [Subject], [Description]) VALUES (1, 17, N'gauri rakshe', N'test', N'xyzfgdfgdfgdfgdffgdgdgdgggdgdgdgdgdgdgdffgfgf')
GO
SET IDENTITY_INSERT [dbo].[Help] OFF
GO
SET IDENTITY_INSERT [dbo].[Login] ON 

GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, N'SystemAdmin', N'Admin', N'kshetriy', N'admin@edoxhealthcare.com', 1000, N'ZqWxMHC/yPAD689h32IvoAQlTNZ8MA9S3mtIf04sVdnxiQtd53UzlZiQBLth2+4IM3kABQnu9N4YhC+wNhLkaQ==', N'ZqWxMHC/yPAD689h32IvoAQlTNZ8MA9S3mtIf04sVdnxiQtd53UzlZiQBLth2+4IM3kABQnu9N4YhC+wNhLkaQ==', N'100000.EJF9i5/2kz7fAw87+nuZSOPZEJAAqNR3YujzX4oQ4ZGfOA==', 0, NULL, NULL, NULL, NULL, NULL, N'On8IxP+d5egs22ETXBqD24YeYf84sCAlgjKRi4K4gPw+axux9maPFeyvq/6jvQRon3Ecao6g94I6fXxdQ7fDCA==', CAST(0x0000A48D00F5F6B9 AS DateTime), NULL, N'5696569856', N'N', 1, CAST(0x0000A486012AF186 AS DateTime), 1, CAST(0x0000A486012AF26E AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'Neha', N'neha', N'kasar', N'sonal@edoxhealthcare.com', 1001, N'mfiUXU7IYyxUIOtdWGU2XG2VLesI8plws9FKG6Jgo5wb0PqOFYLFz83OUVmrbbLhZFPc3ge/fDBplKJEfky3FQ==', N'mfiUXU7IYyxUIOtdWGU2XG2VLesI8plws9FKG6Jgo5wb0PqOFYLFz83OUVmrbbLhZFPc3ge/fDBplKJEfky3FQ==', N'100000.zKjKsRwUp51A72yuCwpo+wpeUakeBZpxQSZ3NXaLO0x7Kg==', 0, NULL, NULL, NULL, NULL, NULL, N'7MQgoMRve3xJ4U8ADESzDHhIBfYi84uWtfDI6wLTr8IoBxhkE1GrEDnuWxLB+PQmktZrPAdkyUrgmpDpnpKQ3Q==', CAST(0x0000A48D00F0564F AS DateTime), NULL, N'1234567890', N'Y', 1, CAST(0x0000A486012C280A AS DateTime), 2, CAST(0x0000A48E0105643E AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'edoxhealthcare', N'edox', N'healthcare', N'sominath.zambare@edoxhealthcare.com', 1002, N'bl2E4oLUyMmuQbpq5bD6jpr9dsNRMwHSfY+lnmPXNDkv4wOwWa1Ar/3z2oSQHA1wuQaBbeucKhbh078s50K1ig==', N'bl2E4oLUyMmuQbpq5bD6jpr9dsNRMwHSfY+lnmPXNDkv4wOwWa1Ar/3z2oSQHA1wuQaBbeucKhbh078s50K1ig==', N'100000.XA19pKbVT8jkTS5O45jwssZeLf9dM4lJQd2+dVbjHEUTKA==', 0, NULL, NULL, NULL, NULL, NULL, N'LS1wCoVHcSPN+3AvA+R4ghYnlVhM8fk+SqsiFtWdLlgdYOQDLwcKj4608Uax5j9oND/QLmqZ5fvz97t/5vszRQ==', CAST(0x0000A48D00F4125A AS DateTime), NULL, N'1234567890', N'N', 1, CAST(0x0000A486012DCD33 AS DateTime), 1, CAST(0x0000A486012DCD33 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, N'uday', N'uday', N'rathod', N'uday@edoxhealthcare.com', 1003, N'mr7A6Qe/iecpT3caLF2GNgcT5ToreR1cspw6w6Vq9zmotA8x9ZMQ9V98GZpYtjSZ/KG5qIlsd3JA6zfNsHl3Tg==', N'mr7A6Qe/iecpT3caLF2GNgcT5ToreR1cspw6w6Vq9zmotA8x9ZMQ9V98GZpYtjSZ/KG5qIlsd3JA6zfNsHl3Tg==', N'100000.ur9B1PMMQ+2IZavQqMguhDmdxc+qY5LYemMxyBeR5KeWnw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1234567890', N'N', 1, CAST(0x0000A48601347FCA AS DateTime), 1, CAST(0x0000A48601347FCA AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (5, N'pranita', N'pranita', N'gurav', N'prani@g.com', 1004, N'S7VYV0Ul4dxh7Rpa8cEuVO6BCTt+vv1nOMjhVqq6ttD1Wok/rRqHldBWr5QPyU7NbL4I4Z0HEYgdyLKgpM0d4A==', N'S7VYV0Ul4dxh7Rpa8cEuVO6BCTt+vv1nOMjhVqq6ttD1Wok/rRqHldBWr5QPyU7NbL4I4Z0HEYgdyLKgpM0d4A==', N'100000.v3WtIjC7trsrgqBraAFQOGjBQgoxU/d6XCW3WaKMELjmAg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'36562365', N'N', 1, CAST(0x0000A4860138A6A0 AS DateTime), 1, CAST(0x0000A4860138A742 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (6, N'madhu', N'madhu', N'joshi', N'borudesonal@gmail.com', 1005, N'hd9jr145EoZNA0EgOc2jq2BXk/rx2Pep/N4SPIoHsaFNnd6INOp4blYOwAlhFX67Uywm42PqznKcdAc7Qu1AzQ==', N'hd9jr145EoZNA0EgOc2jq2BXk/rx2Pep/N4SPIoHsaFNnd6INOp4blYOwAlhFX67Uywm42PqznKcdAc7Qu1AzQ==', N'100000.ZU0RqOqNburj6Z3FnrAfFlIMudOtevrZd4drfK7v2ihIVA==', 0, NULL, NULL, NULL, NULL, NULL, N'HKcb033U1rzetV3oXCp4jpFOGWYcZ+5jG6gV4xQ09HFGm/HuNeBlwFf0Yw+C95OY0Sc7CfXpmUbbScP461i4ag==', CAST(0x0000A48D00FC46CB AS DateTime), NULL, N'96898589', N'N', 1, CAST(0x0000A4860139D496 AS DateTime), 1, CAST(0x0000A4860139D496 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (7, N'snehaAdya', N'sneha', N'adya', N'snehaady@gmail.com', 1031, N'TwRWrO8LujPfpcokzWXrPtbdYUu7sv4ZIBhtinkg2oVGlpiDRzIPSeEWn1YHurffXTNz3wxCoVcFUpSHncB5SQ==', N'TwRWrO8LujPfpcokzWXrPtbdYUu7sv4ZIBhtinkg2oVGlpiDRzIPSeEWn1YHurffXTNz3wxCoVcFUpSHncB5SQ==', N'100000.CTTGmyxpzkDO7OA1Qrd2I41ym32W4FKf3/CVQy2zXyY9nw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9999999999', N'N', 1, CAST(0x0000A48601414FF6 AS DateTime), 1, CAST(0x0000A48601415086 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (8, N'Chaitali', N'chaitali', N'doke', N'chaita@g.com', 1007, N'v/iZ+3WYdQdTRsu7WvRKhZKqUZ9J56X2/KvPjFJxa3MfqrVrSWRwikTzKvP/9e0S+IudzGKg5caZ8DMpToAD/A==', N'v/iZ+3WYdQdTRsu7WvRKhZKqUZ9J56X2/KvPjFJxa3MfqrVrSWRwikTzKvP/9e0S+IudzGKg5caZ8DMpToAD/A==', N'100000.jaakqLH5YvazWed7sPrvIVSdeDYivBQBV2ceIFsFdo0o+w==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'789656896', N'Y', 1, CAST(0x0000A48700B188E6 AS DateTime), 2, CAST(0x0000A48E01071862 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (9, N'trupti', N'trupti', N'kanse', N'tupi@gmail.com', 1008, N'YXaDvSSbuyKZUC9h1Y76qVTfYDH5W2tV73q/4pwoM9A+h30z/3ToFS1gs30XopXBaWI2xcuVsqR9wFKmeKLefQ==', N'YXaDvSSbuyKZUC9h1Y76qVTfYDH5W2tV73q/4pwoM9A+h30z/3ToFS1gs30XopXBaWI2xcuVsqR9wFKmeKLefQ==', N'100000.iE9oro0XFmCVstWIaoXsAKXBbqgBQnTcCDQrKmUwqqIflg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'6666666666', N'N', 1, CAST(0x0000A48700B1F28F AS DateTime), 1, CAST(0x0000A48700B1F28F AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (10, N'Rashi', N'raji', N'bhandare', N'raji@gmail.com', 1009, N'srNsMyqyvYXjOz8KcAbGuWBod+y3mFVmtsiZEcIbl/FDpVfPwkyWcN0SqBAkvTaKzuQ36lXiyfsmvcInymz36g==', N'srNsMyqyvYXjOz8KcAbGuWBod+y3mFVmtsiZEcIbl/FDpVfPwkyWcN0SqBAkvTaKzuQ36lXiyfsmvcInymz36g==', N'100000.diy9e2POivh1riNbu42EJpb68P0g+7fX2qSsVVYxveKRcQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1234567891', N'N', 1, CAST(0x0000A48700B2460C AS DateTime), 1, CAST(0x0000A48700B2460C AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (11, N'Angarika', N'ands', N'joshi', N'angs@g.com', 1010, N'FsqcwQpyfqIG8cMy0vuFENPhnE/KQ/gN6l6O1oZ3zpyRfoDihiIxMcVlISOQT8t7VqIfKDGEWOXREQJbkgHSiA==', N'FsqcwQpyfqIG8cMy0vuFENPhnE/KQ/gN6l6O1oZ3zpyRfoDihiIxMcVlISOQT8t7VqIfKDGEWOXREQJbkgHSiA==', N'100000.9V0L9YJI5lIkIfRUGWu89AgjBSHiz390KtPCX2UKz2R2gQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1234567890', N'Y', 1, CAST(0x0000A48700BBAAE0 AS DateTime), 2, CAST(0x0000A49700A5D27C AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (12, N'shital', N'shital', N'bhosle', N'shital@g.com', 1011, N'y08fb9HDR9ksemeyMbLdW+NNqeVu57J5XZbc+kO1afYV8L2hm1yWBRo+wJNtJYeqmeSCJIquUo1elJl7FWWgmQ==', N'y08fb9HDR9ksemeyMbLdW+NNqeVu57J5XZbc+kO1afYV8L2hm1yWBRo+wJNtJYeqmeSCJIquUo1elJl7FWWgmQ==', N'100000.pAR0cRSY8cEaMbHu5aA5VXqDHYhz/5pL01HjTUCsB6DKZQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1234567892', N'Y', 1, CAST(0x0000A48700BC0A21 AS DateTime), 2, CAST(0x0000A49D013B0C5F AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (13, N'shrutika', N'shruti', N'thorat', N'shruti@g.com', 1012, N'z8UmKqhIcVnA0B5FZsFAp4dlVKlPIgJ5hwCAN2pkQBnlSKOVEi3RzVcAdHmdSJbNUsD0WmhbGvHYdXHsF3+3wQ==', N'z8UmKqhIcVnA0B5FZsFAp4dlVKlPIgJ5hwCAN2pkQBnlSKOVEi3RzVcAdHmdSJbNUsD0WmhbGvHYdXHsF3+3wQ==', N'100000.39oG6D8QxB74OYgixyCki68VpSc0Lic2n76s3gYm2PBXAw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656896598', N'Y', 1, CAST(0x0000A48700BD46E2 AS DateTime), 2, CAST(0x0000A49A0124E601 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (14, N'aarti', N'navale', N'aarti', N'AARTI@G.COM', 1013, N'eHUnSqfYmaB5fSJDV7FO8xT+lttPRhb3TfM2EGUWpaucjDZYqSeD1tP86sFLkGlrYpxJm3VOJKRmQfXW+pynbQ==', N'eHUnSqfYmaB5fSJDV7FO8xT+lttPRhb3TfM2EGUWpaucjDZYqSeD1tP86sFLkGlrYpxJm3VOJKRmQfXW+pynbQ==', N'100000.5hzV/Sl+0ZZEsjqftKoyuk4sAtyOFgcynzvKLOMkhMBeSw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'8589659856', N'Y', 1, CAST(0x0000A487011B1ECD AS DateTime), 2, CAST(0x0000A49C00EEDA30 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (15, N'apeksha', N'apeksha', N'navale', N'apeksha@g.com', 1014, N'UWZBfHiFN5VakiX9EajzkKBQ56wcWp9mFPx2qGCAx5+r3zaQzRGYfbd5lle1TWoPN3rckoTmCECWf9vT8ZvyiA==', N'UWZBfHiFN5VakiX9EajzkKBQ56wcWp9mFPx2qGCAx5+r3zaQzRGYfbd5lle1TWoPN3rckoTmCECWf9vT8ZvyiA==', N'100000.qxstdA6kSGz48sGg9jYfZGIuasYYS23RHnJ8Urq4CrL7Bg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656985256', N'N', 1, CAST(0x0000A487012EEB2A AS DateTime), 1, CAST(0x0000A487012EEB2A AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (16, N'ishu', N'ishu', N'paul', N'ishu@g.com', 1015, N'rwsZMNKxGza4tLh161sY1v5SWE0ZbjHDD9MFdBre6A+Bw6exhdUUZ5Av/aUht3S3ZWsWftwwj2qOmERAg0iQVQ==', N'rwsZMNKxGza4tLh161sY1v5SWE0ZbjHDD9MFdBre6A+Bw6exhdUUZ5Av/aUht3S3ZWsWftwwj2qOmERAg0iQVQ==', N'100000.20OWnAE42U/TblbvQ4Po70fA0dcKNORAHcPwgzBLMEnJTQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895256', N'N', 1, CAST(0x0000A488009FD60A AS DateTime), 1, CAST(0x0000A488009FD60A AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (17, N'gauri', N'gauri', N'rakshe', N'gauri@gmail.com', 1016, N'xuhgYogiJx+4VZSQmUUFaZi005QTBBjvzamEqT+VD9OIzieOCrpde0oAAJmJVddZz6Yv3w3n0uJeybJ+vDCE/A==', N'xuhgYogiJx+4VZSQmUUFaZi005QTBBjvzamEqT+VD9OIzieOCrpde0oAAJmJVddZz6Yv3w3n0uJeybJ+vDCE/A==', N'100000.c8Y4qMMUIblaHfEQPnJzV6fhBm7FaXa55D6iqwQJMv4nKg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895856', N'N', 1, CAST(0x0000A48800A3EF83 AS DateTime), 1, CAST(0x0000A48800A3F02F AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (18, N'teju', N'teju', N'thorat', N'teju@g.com', 1017, N'FwOCUlV6KGFEApczN6dE2BCVZPom0O2kuwXTjNe4rMFhQmN7LwWAdETjpnEtToy7fPUqixQ9lismKt73tu6M+w==', N'FwOCUlV6KGFEApczN6dE2BCVZPom0O2kuwXTjNe4rMFhQmN7LwWAdETjpnEtToy7fPUqixQ9lismKt73tu6M+w==', N'100000.Jupr17aWjBtGtBxXFormlFk740EGfNlEW1P3CjQBIncASw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9856968985', N'Y', 1, CAST(0x0000A48800AA4F87 AS DateTime), 2, CAST(0x0000A48E0108A382 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (19, N'aakash', N'aakash', N'navale', N'akash@gmail.com', 1018, N'j7+RQNz4QAnwUaiTvlOsrN/CS0AaVC78LYLNqsnKij9/gP1FqQc32ZTn4SWUjSFg1WM0AoYNM2kXByxVTCmoww==', N'j7+RQNz4QAnwUaiTvlOsrN/CS0AaVC78LYLNqsnKij9/gP1FqQc32ZTn4SWUjSFg1WM0AoYNM2kXByxVTCmoww==', N'100000.n6aP6fF0cnoLgy7uE6LPVPfKiKXUgjmy3YaML4oSJ628Fg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'6956895869', N'N', 1, CAST(0x0000A48800E72DF0 AS DateTime), 1, CAST(0x0000A48800E72DF0 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (20, N'om', N'om', N'gaur', N'OM@G.COM', 1018, N'qNmNw+2aTWy1/TP+/6A+XVL6BKzVAE4viATp7TQ+3wa2qBRs+qx9U8IDKZVnCXUucoQI371d5G2LtayfaKpcKg==', N'qNmNw+2aTWy1/TP+/6A+XVL6BKzVAE4viATp7TQ+3wa2qBRs+qx9U8IDKZVnCXUucoQI371d5G2LtayfaKpcKg==', N'100000.EuCZRgr2C4jpPbQlafXRXbKt1B8/Wkk7U2rYEQlxXRBXuQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895845', N'Y', 1, CAST(0x0000A48800E9CBCD AS DateTime), 2, CAST(0x0000A48E01090DB6 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (21, N'Harshad ', N'Harshad', N'rakshe', N'devedox1@gmail.com', 1019, N'zzc/c7LF1vtoke8N1EyRbr00YK2vw0+gOxBJf5lLDEH/JO0JO0mw8rWupjrHc0ZhLYvUVZRWU3OR63a3mSkELw==', N'zzc/c7LF1vtoke8N1EyRbr00YK2vw0+gOxBJf5lLDEH/JO0JO0mw8rWupjrHc0ZhLYvUVZRWU3OR63a3mSkELw==', N'100000.f9lXGakc17SOTqm9z6VHJHArhz+/4KIWk9/D6KaaF6ND2A==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895865', N'N', 1, CAST(0x0000A48B0119E453 AS DateTime), 1, CAST(0x0000A48B0119E453 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (22, N'ganesh', N'ganesh', N'borude', N'sonali.b@edoxhealthcare.com', 1020, N'7aj3EVcRNfrZxyYuNZq2soqu5pNetGYL0BO1xvqyhVsZc/OR36AUncNk/GDKceuzCy3CGlEA1KIWZxSTlu9Eng==', N'7aj3EVcRNfrZxyYuNZq2soqu5pNetGYL0BO1xvqyhVsZc/OR36AUncNk/GDKceuzCy3CGlEA1KIWZxSTlu9Eng==', N'100000.825DzhOYuTdo1HNmthp7hvjVNn+vutFYqN4IxNufdJUEwQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895696', N'N', 1, CAST(0x0000A48B01247FF2 AS DateTime), 1, CAST(0x0000A48B01247FF2 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (23, N'santosh ', N'santosh', N'borude', N'santosh@edoxhealthcare.com', 1021, N'Sfs/tHCJ3gN37Hd9QCtsYzltUwPlPUs431S9+3g4kj49u1Px5FgeOH9Cb4hsZt1PvFLFS6uFDS87ws+bLHrUPw==', N'Sfs/tHCJ3gN37Hd9QCtsYzltUwPlPUs431S9+3g4kj49u1Px5FgeOH9Cb4hsZt1PvFLFS6uFDS87ws+bLHrUPw==', N'100000.1OZMlKzrLNGWmuj824kiVJvtvndZSbKPaiVBMRt1vtD1WA==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2145857858', N'N', 1, CAST(0x0000A48B0126792D AS DateTime), 1, CAST(0x0000A48B0126792D AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (24, N'ramdas', N'ramdsa', N'borude', N'sonali.borude@edoxhealthcare.com', 1022, N'IOPeMJ8uD4DMdKfV2jul5tgM6O9tH0trMMjPrVz3uhuDQrZ+Ii7EkVycrMmLefT8qijNM0AvWTvftXNCEt7cgg==', N'IOPeMJ8uD4DMdKfV2jul5tgM6O9tH0trMMjPrVz3uhuDQrZ+Ii7EkVycrMmLefT8qijNM0AvWTvftXNCEt7cgg==', N'100000.zGnTgtcPs0qWjPn4oWCjWfofi4Bn7JUhg0gvWDhZRGCwSg==', 0, NULL, NULL, NULL, NULL, NULL, N'6QAnRObnhUZ0IlZKwOseV4BKF03+gWJqUwz7FFxoLwJ8QFGnwi47YySvuivUlEvsE0v4ypWtbO99b3pY2wOu4g==', CAST(0x0000A48E00CFE138 AS DateTime), NULL, N'9656895845', N'N', 1, CAST(0x0000A48C00CE4357 AS DateTime), 1, CAST(0x0000A48C00CE4357 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (25, N'sonaliborudesonali', N'sonali', N'borude', N'bor@gmail.com', 1023, N'X6vmzrX/M2rSFa6Gojv3EAtGjLzO8REqBYk+tVt9jAtYWoSUnRcBh50psh0rhPmPfRYfLxo36mH5TN15kdGWpg==', N'X6vmzrX/M2rSFa6Gojv3EAtGjLzO8REqBYk+tVt9jAtYWoSUnRcBh50psh0rhPmPfRYfLxo36mH5TN15kdGWpg==', N'100000.CPelJ6ddkkbgE1QuBjR7k6qgmswQITwYyKNREtTIMFFcHw==', 0, NULL, NULL, NULL, NULL, NULL, N'Ykv8yKKCVy0ps3JElmUtKgyTtxeyth2KI1KTxouWV8LnaaDQug+qzj1SGrxRuq2NgA0emUecLBRC/Afc5Wm3Uw==', CAST(0x0000A48E00D4FF95 AS DateTime), NULL, N'6598569856', N'Y', 1, CAST(0x0000A48C00D421F9 AS DateTime), 2, CAST(0x0000A48E0108157B AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (26, N'supriya ', N'supriya', N'tathe', N'sup@gmail.com', 1024, N'U7zhHyT5GGKCIUV7lSUQDBWbuGSH8ir+Nej+HJ+PTN2LrOPOq1ij5rTNmt4Tzg0f8FtfmFJ3V+vP1m+dDycWFw==', N'U7zhHyT5GGKCIUV7lSUQDBWbuGSH8ir+Nej+HJ+PTN2LrOPOq1ij5rTNmt4Tzg0f8FtfmFJ3V+vP1m+dDycWFw==', N'100000.PlzGgaR93P/c+guJjn9jTtqKp1DdHes56ovVTA//K1adjw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656985699', N'Y', 1, CAST(0x0000A48C00F85A12 AS DateTime), 2, CAST(0x0000A49C011584BE AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (27, N'Parag', N'parag', N'gadgil', N'parag@gmail.com', 1025, N'PAv8Zi+9HelN0MiDzYjz8eDOXuJbUXT3nwIwjtuJC3UyxO5SZNvRPXc3NfbfufCn8eXAjEq5oPoJ977nrVjtoA==', N'PAv8Zi+9HelN0MiDzYjz8eDOXuJbUXT3nwIwjtuJC3UyxO5SZNvRPXc3NfbfufCn8eXAjEq5oPoJ977nrVjtoA==', N'100000.tZ+byB758NGF+FyjpKytrxn9s1S3a0w7q3n9lUdBUju5xg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656895696', N'N', 1, CAST(0x0000A48C00FF5B3D AS DateTime), 1, CAST(0x0000A48C00FF5B3D AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (28, N'Mitesh', N'Mitesh', N'Yadav', N'mitesh@gmail.com', 1026, N'amxa16cf5sSwC2uOsNYse3ygnDvyYPdYMGVnrYBRfpSaiRdbvz28j4h/t8LwHgaG7gSGwu1Y+4CgssdQS93CVQ==', N'amxa16cf5sSwC2uOsNYse3ygnDvyYPdYMGVnrYBRfpSaiRdbvz28j4h/t8LwHgaG7gSGwu1Y+4CgssdQS93CVQ==', N'100000.b8gzYQhBgF7CZ4OJODVHGVTf12+MNs/OnIgi7AEp9et3vg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'5689569685', N'N', 1, CAST(0x0000A48C01074492 AS DateTime), 1, CAST(0x0000A48C010744F6 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (29, N'gita', N'gita', N'bhandare', N'gita@gmail.com', 1027, N'lTOP4r2QA7JuTCv+7RL7qVZ3tdoPWAZLORtNUmOiS/y1cF0UC7trKjBUA9MRzShCxIevkMKnaBwx9e2CKKCygw==', N'lTOP4r2QA7JuTCv+7RL7qVZ3tdoPWAZLORtNUmOiS/y1cF0UC7trKjBUA9MRzShCxIevkMKnaBwx9e2CKKCygw==', N'100000.7vthZPphFUMXwwa/iLqHJRwAXwgrWgkiOp9F3Y2RG5aSGg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656985698', N'N', 1, CAST(0x0000A48C010C9727 AS DateTime), 1, CAST(0x0000A48C010C9727 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (30, N'shivanjali', N'shivanjali', N'bhandare', N'borudesonali@gmail.com', 1028, N'5UaZVyEO4+ipocpyx0LZNAhPeYLz4xYTQC4TujnZ5QxbXoyRUH7kH2fsf9Mmu7YTy2IIfOHfSLDtSuYJpjpl7w==', N'5UaZVyEO4+ipocpyx0LZNAhPeYLz4xYTQC4TujnZ5QxbXoyRUH7kH2fsf9Mmu7YTy2IIfOHfSLDtSuYJpjpl7w==', N'100000.f+AqsSk/t1F1uribkeqC1U8REMIiGcUGA4NF53BhM47B8A==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656985696', N'N', 1, CAST(0x0000A48C0127DE37 AS DateTime), 1, CAST(0x0000A48C0127DE37 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (31, N'shivraj', N'shiv', N'kshetriy', N'siv@g.com', 1029, N'gmi5ut5kpimE9sXPY4sWT+wnsM8/tQkhhtbHzBlDzWPgoZZT1tnrICXaep45peDCPlVdLH7RNPyXYsyTeRlqhg==', N'gmi5ut5kpimE9sXPY4sWT+wnsM8/tQkhhtbHzBlDzWPgoZZT1tnrICXaep45peDCPlVdLH7RNPyXYsyTeRlqhg==', N'100000.3J9S7iLqgYSzr0H/Zxb/lOeL1/gmfOKM5hO3smjen1hJ+A==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'5696569856', N'N', 1, CAST(0x0000A49200C1033C AS DateTime), 1, CAST(0x0000A49200C1033C AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (32, N'testEdit', N'testtestEdit', N'testEditEdit', N'borudesonali2Edit@gmail.com', 1032, N'SYyztzlu4/mi0KT0Y2DKKUi26uTmrBYLQHjYeX7Z8ZI4F0X6zbAoeJ/K13FYQW3AXKuvWXypUBdABDNhtnAxxw==', N'SYyztzlu4/mi0KT0Y2DKKUi26uTmrBYLQHjYeX7Z8ZI4F0X6zbAoeJ/K13FYQW3AXKuvWXypUBdABDNhtnAxxw==', N'100000.N43CZk10ccj/FOQ86coSfsE8G545ykjWgTvs5Q1RiDhong==', 0, NULL, NULL, NULL, NULL, NULL, N'BQKTG4pE1XhI2K3wC/OuY+Eb4+pxD1ligvN6xvzAD5uZDGBA5y6zIAuwZl7DChUlSroVOy0v/tJea1stczSrWQ==', CAST(0x0000A49C01273553 AS DateTime), NULL, N'9656856588', N'N', 1, CAST(0x0000A49A0126841F AS DateTime), 1, CAST(0x0000A49A0126841F AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (33, N'Vandana', N'vandana', N'bhupkar', N'xyz@gmail.com', 1033, N'dWHk7s3ndlxxZbLITe7K93M6L+kW8nauZBsOJl/XtLJZHSVe1DK+UwbXBJ8Z6ru2rATWFoFDaz7gaXctbAFeOw==', N'dWHk7s3ndlxxZbLITe7K93M6L+kW8nauZBsOJl/XtLJZHSVe1DK+UwbXBJ8Z6ru2rATWFoFDaz7gaXctbAFeOw==', N'100000.jN705H1g7RzyVkcerg70uQw7TmVKDNHgqdxv7ScWyS0Kvg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'8888888888', N'Y', 1, CAST(0x0000A49C010A3174 AS DateTime), 2, CAST(0x0000A49E00BFCFC0 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (34, N'TestDoctor', N'TestDoc', N'Doc', N'borudesonali2@gmail.com', 0, N'9/ScPVdmM1r8OUjLDZAJDM1GyzNmZkacYeSO/qxgd03hNUW/U7GN3RmhT6HASKLJtg9za4OeAzGklfpTOvzbsw==', N'9/ScPVdmM1r8OUjLDZAJDM1GyzNmZkacYeSO/qxgd03hNUW/U7GN3RmhT6HASKLJtg9za4OeAzGklfpTOvzbsw==', N'100000.cVEFyE080N0BgjP8fxUZ7Skzs8mzU5HHV23kUtRebqqBCg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'7777777777', N'N', 1, CAST(0x0000A49C01136160 AS DateTime), 1, CAST(0x0000A49C01136160 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[Login_Role] ON 

GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (4, 1, 4)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (5, 2, 5)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (6, 3, 6)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (7, 3, 7)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (8, 2, 8)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (9, 3, 9)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (10, 2, 10)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (11, 2, 11)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (12, 3, 12)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (13, 2, 13)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (14, 2, 14)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (17, 4, 17)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (18, 4, 18)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (19, 4, 19)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (20, 4, 20)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (21, 4, 21)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (22, 4, 22)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (23, 4, 23)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (24, 4, 24)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (25, 4, 25)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (26, 2, 26)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (27, 4, 27)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (28, 4, 28)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (29, 4, 29)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (30, 4, 30)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (31, 4, 31)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (32, 2, 32)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (33, 3, 33)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (34, 4, 34)
GO
SET IDENTITY_INSERT [dbo].[Login_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Login_Speciality] ON 

GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (1, 15, 1)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (2, 5, 2)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (3, 10, 3)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (4, 9, 4)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (5, 5, 5)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (6, 11, 6)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (7, 7, 7)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (8, 7, 8)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (9, 9, 9)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (10, 11, 10)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (11, 10, 11)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (12, 9, 12)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (13, 10, 13)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (14, 7, 14)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (15, 7, 15)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (16, 7, 16)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (17, 8, 17)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (18, 1, 18)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (19, 9, 19)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (20, 7, 20)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (21, 15, 21)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (22, 2, 22)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (23, 15, 23)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (24, 2, 24)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (25, 5, 25)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (26, 11, 26)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (27, 10, 27)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (28, 7, 28)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (29, 7, 29)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (30, 7, 30)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (31, 15, 31)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (32, 1, 32)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (33, 0, 33)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (34, 10, 34)
GO
SET IDENTITY_INSERT [dbo].[Login_Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[MCMDRoles] ON 

GO
INSERT [dbo].[MCMDRoles] ([RoleId], [Description], [RoleName]) VALUES (1, N'Complete Access', N'SuperAdmin')
GO
INSERT [dbo].[MCMDRoles] ([RoleId], [Description], [RoleName]) VALUES (2, N'Logical Delete Access', N'Admin')
GO
INSERT [dbo].[MCMDRoles] ([RoleId], [Description], [RoleName]) VALUES (3, N'NULLUser Level Access', N'COE')
GO
INSERT [dbo].[MCMDRoles] ([RoleId], [Description], [RoleName]) VALUES (4, N'Doctor', N'Doctor')
GO
SET IDENTITY_INSERT [dbo].[MCMDRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Media] ON 

GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 1, N'~/Media/drawing.jpg', N'image/jpeg', N'N', 1, CAST(0x0000A49E00BFF706 AS DateTime), 1, CAST(0x0000A49E00BFF706 AS DateTime))
GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 1, N'D:\Project\MCMDProject\MCMD.Web\Media\images.jpg', N'image/jpeg', N'Y', 1, CAST(0x0000A49E00C131A7 AS DateTime), 1, CAST(0x0000A49E00C16632 AS DateTime))
GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, 1, N'D:\Project\MCMDProject\MCMD.Web\Media\Vivai.jpg', N'image/jpeg', N'Y', 1, CAST(0x0000A49E00C17F65 AS DateTime), 1, CAST(0x0000A49E00C17F65 AS DateTime))
GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, 1, N'~/Media/flower.jpg', N'image/jpeg', N'N', 1, CAST(0x0000A49E00FE8392 AS DateTime), 1, CAST(0x0000A49E00FE8392 AS DateTime))
GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (5, 1, N'D:\Project\MCMDProject\MCMD.Web\Media\Day1WelcometoDotNetConf.mp4', N'video/mp4', N'N', 1, CAST(0x0000A49E012AD40C AS DateTime), 1, CAST(0x0000A49E012AD40C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Media] OFF
GO
SET IDENTITY_INSERT [dbo].[Membership] ON 

GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [Duration], [AutoRenaval], [InactiveFlag], [ModifiedDate]) VALUES (1, N'Online Appointment', 1000, 0, 0, N'N', CAST(0x0000A49E00E04501 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [Duration], [AutoRenaval], [InactiveFlag], [ModifiedDate]) VALUES (2, N'Patient Portal', 2000, 0, 0, N'N', CAST(0x0000A49E00E066E4 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [Duration], [AutoRenaval], [InactiveFlag], [ModifiedDate]) VALUES (3, N'Electronic Medical Record', 1000, 0, 0, N'N', CAST(0x0000A49E00E07234 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [Duration], [AutoRenaval], [InactiveFlag], [ModifiedDate]) VALUES (4, N'Practice Management', 2000, 0, 0, N'N', CAST(0x0000A49E00E080E5 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [Duration], [AutoRenaval], [InactiveFlag], [ModifiedDate]) VALUES (5, N'Integrated PM and EMR', 1100, 0, 0, N'N', CAST(0x0000A49E00E09AD1 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Membership] OFF
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (1, N'Dentist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (2, N'Ophthalmologist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (3, N'Homeopath', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (4, N'Physiotherapist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (5, N'Ayurveda', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (6, N'Beautician', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (7, N'Neurologist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (8, N'Geneticist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (9, N'Cosmetologist', N'N', CAST(0x0000A489009B7E55 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (10, N'Diabetes', N'Y', CAST(0x0000A48900B2DD0C AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (11, N'Hypnotherapist', N'Y', CAST(0x0000A48900B1F23E AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (12, N'Neurosurgeon', N'Y', CAST(0x0000A48900B1EE80 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (15, N'Optician', N'Y', CAST(0x0000A48900ADA79F AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (16, N'Orthopedist', N'Y', CAST(0x0000A48900B429FA AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (17, N'Dermatologist', N'Y', CAST(0x0000A49B0105A3F7 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 

GO
INSERT [dbo].[State] ([StateId], [StateName]) VALUES (1, N'Maharashtra')
GO
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[Title] ON 

GO
INSERT [dbo].[Title] ([TitleId], [TitleName]) VALUES (1, N'Dr')
GO
SET IDENTITY_INSERT [dbo].[Title] OFF
GO
SET IDENTITY_INSERT [dbo].[UpgradeService] ON 

GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [MembershipId], [LoginId], [Durations], [AutoRenaval], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (1, 1, 17, 1, 1, 1, N'N', CAST(0x0000A49E00C500F0 AS DateTime), 1, CAST(0x0000A49E00C500F0 AS DateTime))
GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [MembershipId], [LoginId], [Durations], [AutoRenaval], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (2, 2, 1, 1, 1, 1, N'N', CAST(0x0000A49E00C50148 AS DateTime), 1, CAST(0x0000A49E00C50148 AS DateTime))
GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [MembershipId], [LoginId], [Durations], [AutoRenaval], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (3, 3, 1, 2, 2, 1, N'N', CAST(0x0000A49E00C5177C AS DateTime), 1, CAST(0x0000A49E00C5177C AS DateTime))
GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [MembershipId], [LoginId], [Durations], [AutoRenaval], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (4, 4, 1, 2, 2, 1, N'N', CAST(0x0000A49E00C5178B AS DateTime), 1, CAST(0x0000A49E00C5178B AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UpgradeService] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Login_UserName]    Script Date: 5/20/2015 6:11:32 PM ******/
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [UQ_Login_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 5/20/2015 6:11:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[MCMDRoles]
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [MCMD] SET  READ_WRITE 
GO
