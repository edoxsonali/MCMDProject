USE [master]
GO
/****** Object:  Database [MCMD]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetViewDoctor]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[GetViewDoctor]
(	
     @LoginId      INT=0,	
	 @SpecialityID  INT=0, 
	 @RoleId		INT=0,
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
		LEFT OUTER JOIN dbo.[DoctorClinicInformation] C  With (NOLOCk)
				ON C.LoginId=L.LoginId
		LEFT OUTER JOIN dbo.[UpgradeService] U With (NOLOCk)
				ON U.LoginId = L.LoginId
		LEFT OUTER JOIN dbo.[Membership] M  With (NOLOCk)
				ON M.MembershipId = U.MembershipId
 		WHERE (R.RoleId = @RoleId OR  @RoleId=0 )
			AND (L.LoginId= @EmployeeId  OR  @EmployeeId =0)
			AND (L.LoginId= @LoginId OR  @LoginId=0)
			AND (S.SpecialityID=@SpecialityID OR @SpecialityID=0 )			
			AND (L.FirstName= @FirstName OR @FirstName='')
			AND (L.LastName=@LastName OR @LastName='')
			AND (L.EmailID=@EmailID OR @EmailID='')
			AND (L.UserPhone=@UserPhone OR @UserPhone='')			
			AND (C.ClinicInfoId= @ClinicInfoId OR  @ClinicInfoId=0)



END

 --EXEC GetViewDoctor @RoleId=4 ,@EmployeeId=1016,@FirstName='gauri',@LastName='rakshe',@EmailID='gauri@gmail.com',@UserPhone='9656895856' 
--EXEC GetViewDoctor @LoginId=0,@SpecialityID=0,@RoleId=0,@EmployeeId=0 ,@FirstName='',@LastName='',@EmailID='',@UserPhone='',@ClinicInfoId=0

GO
/****** Object:  StoredProcedure [dbo].[GetViewUsers]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[AppointmentDiary]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentDiary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[SomeImportantKey] [int] NOT NULL,
	[DateTimeScheduled] [datetime] NOT NULL,
	[AppointmentLength] [int] NOT NULL,
	[StatusENUM] [int] NOT NULL,
 CONSTRAINT [PK_ConsultantBookings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AutoRenaval]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[City]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](max) NULL,
	[StateId] [int] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClinicTime]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClinicTime](
	[ClinicTimeId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[Day] [nvarchar](50) NULL,
	[FirstSetting] [bit] NULL,
	[IsWorkingDay] [bit] NULL,
	[StartTime] [time](7) NULL,
	[StartSlot] [nvarchar](50) NULL,
	[EndTime] [time](7) NULL,
	[EndSlot] [nvarchar](50) NULL,
	[CreatedById] [int] NULL,
	[CreatedOnDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedOnDate] [datetime] NULL,
 CONSTRAINT [PK_ClinicTime] PRIMARY KEY CLUSTERED 
(
	[ClinicTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[DoctorClinicInformation]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[DoctorPersonalInformation]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Duration]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Help]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Login]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Login_Role]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Login_Speciality]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[MCMDRoles]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Media]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[Membership]    Script Date: 6/22/2015 10:31:26 AM ******/
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
	[CheckedStatus] [bit] NULL,
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
/****** Object:  Table [dbo].[Patient]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](30) NULL,
	[EmailID] [varchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[ConfirmPassword] [nvarchar](max) NULL,
	[PasswordSalt] [nvarchar](max) NULL,
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
	[InactiveFlag] [char](1) NULL,
	[CreatedByID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulingDiary]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchedulingDiary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[SomeImportantKey] [int] NULL,
	[DateTimeScheduled] [datetime] NULL,
	[DateTimeScheduledEnd] [datetime] NULL,
	[AppointmentLength] [int] NULL,
	[StatusENUM] [int] NULL,
	[StartTime] [time](7) NULL,
	[StartSlot] [nvarchar](10) NULL,
	[EndTime] [time](7) NULL,
	[EndSlot] [nvarchar](10) NULL,
 CONSTRAINT [PK_SchedulingDiary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[State]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](max) NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Title]    Script Date: 6/22/2015 10:31:26 AM ******/
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
/****** Object:  Table [dbo].[UpgradeService]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpgradeService](
	[UpgradeServiceId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[MembershipId] [int] NULL,
	[Durations] [int] NULL,
	[AutoRenaval] [int] NULL,
	[CheckedStatus] [bit] NULL,
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
/****** Object:  Table [dbo].[UpgradeServiceLog]    Script Date: 6/22/2015 10:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpgradeServiceLog](
	[UpgradeServLogId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[MembershipId] [int] NULL,
	[Durations] [int] NULL,
	[AutoRenaval] [int] NULL,
	[CheckedStatus] [bit] NULL,
	[CreatedById] [int] NULL,
	[InactiveFlag] [char](1) NULL,
	[CreatedOnDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedOnDate] [datetime] NULL,
 CONSTRAINT [PK_UpgradeServiceLog] PRIMARY KEY CLUSTERED 
(
	[UpgradeServLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AppointmentDiary] ON 

GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (1, N'sonali', 0, CAST(0x0000A4B600A4CB80 AS DateTime), 25, 0)
GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (2, N'Neha', 0, CAST(0x0000A4C300000000 AS DateTime), 30, 0)
GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (3, N'Madhura', 0, CAST(0x0000A4C200000000 AS DateTime), 30, 0)
GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (4, N'aaa', 0, CAST(0x0000A4C101499700 AS DateTime), 30, 0)
GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (5, N'zzzz', 0, CAST(0x0000A4BB00F73140 AS DateTime), 30, 0)
GO
INSERT [dbo].[AppointmentDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [AppointmentLength], [StatusENUM]) VALUES (6, N'sona', 0, CAST(0x0000A4BF00986F70 AS DateTime), 30, 0)
GO
SET IDENTITY_INSERT [dbo].[AppointmentDiary] OFF
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
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (1, N'Nicobar', 1)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (2, N'North and Middle Andaman', 1)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (3, N'South Andaman', 1)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (4, N'Anantapur', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (5, N'Chittoor', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (6, N'Cuddapah', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (7, N'East Godavari', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (8, N'Guntur', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (9, N'Krishna', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (10, N'Kurnool', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (11, N'Nellore', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (12, N'Prakasam', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (13, N'Srikakulam', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (14, N'Visakhapatnam', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (15, N'Vizianagaram', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (16, N'West Godavari', 2)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (17, N'Anjaw', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (18, N'Changlang', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (19, N'Dibang Valley', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (20, N'East Kameng', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (21, N'East Siang', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (22, N'Kurung Kumey', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (23, N'Lohit', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (24, N'Longding', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (25, N'Lower Dibang Valley', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (26, N'Lower Subansiri', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (27, N'Papum Pare', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (28, N'Tawang', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (29, N'Tirap', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (30, N'Upper Siang', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (31, N'Upper Subansiri', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (32, N'West Kameng', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (33, N'West Siang', 3)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (34, N'Baksa', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (35, N'Barpeta', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (36, N'Bongaigaon', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (37, N'Cachar', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (38, N'Chirang', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (39, N'Darrang', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (40, N'Dhemaji', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (41, N'Dhubri', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (42, N'Dibrugarh', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (43, N'Dima Hasao', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (44, N'Goalpara', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (45, N'Golaghat', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (46, N'Hailakandi', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (47, N'Jorhat', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (48, N'Kamrup Metropolitan', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (49, N'Kamrup', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (50, N'Karbi Anglong', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (51, N'Karimganj', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (52, N'Kokrajhar', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (53, N'Lakhimpur', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (54, N'Morigaon', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (55, N'Nagaon', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (56, N'Nalbari', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (57, N'Sivasagar', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (58, N'Sonitpur', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (59, N'Tinsukia', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (60, N'Udalguri', 4)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (61, N'Araria', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (62, N'Arwal', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (63, N'Aurangabad', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (64, N'Banka', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (65, N'Begusarai', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (66, N'Bhagalpur', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (67, N'Bhojpur', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (68, N'Buxar', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (69, N'Darbhanga', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (70, N'East Champaran (Motihari)', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (71, N'Gaya', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (72, N'Gopalganj', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (73, N'Jamui', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (74, N'Jehanabad', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (75, N'Kaimur (Bhabua)', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (76, N'Katihar', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (77, N'Khagaria', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (78, N'Kishanganj', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (79, N'Lakhisarai', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (80, N'Madhepura', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (81, N'Madhubani', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (82, N'Munger (Monghyr)', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (83, N'Muzaffarpur', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (84, N'Nalanda', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (85, N'Nawada', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (86, N'Patna', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (87, N'Purnia (Purnea)', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (88, N'Rohtas', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (89, N'Saharsa', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (90, N'Samastipur', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (91, N'Saran', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (92, N'Sheikhpura', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (93, N'Sheohar', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (94, N'Sitamarhi', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (95, N'Siwan', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (96, N'Supaul', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (97, N'Vaishali', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (98, N'West Champaran', 5)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (99, N'Chandigarh', 6)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (100, N'Balod', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (101, N'Baloda Bazar', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (102, N'Balrampur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (103, N'Bastar', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (104, N'Bemetara', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (105, N'Bijapur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (106, N'Bilaspur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (107, N'Dantewada (South Bastar)', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (108, N'Dhamtari', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (109, N'Durg', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (110, N'Gariaband', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (111, N'Janjgir-Champa', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (112, N'Jashpur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (113, N'Kabirdham (Kawardha)', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (114, N'Kanker (North Bastar)', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (115, N'Kondagaon', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (116, N'Korba', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (117, N'Korea (Koriya)', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (118, N'Mahasamund', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (119, N'Mungeli', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (120, N'Narayanpur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (121, N'Raigarh', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (122, N'Raipur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (123, N'Rajnandgaon', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (124, N'Sukma', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (125, N'Surajpur', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (126, N'Surguja', 7)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (127, N'Dadra & Nagar Haveli', 8)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (128, N'Daman', 9)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (129, N'Diu', 9)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (130, N'Central Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (131, N'East Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (132, N'New Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (133, N'North Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (134, N'North East Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (135, N'North West Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (136, N'South Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (137, N'South West Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (138, N'West Delhi', 10)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (139, N'North Goa', 11)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (140, N'South Goa', 11)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (141, N'Ahmedabad', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (142, N'Amreli', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (143, N'Anand', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (144, N'Aravalli', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (145, N'Banaskantha (Palanpur)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (146, N'Bharuch', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (147, N'Bhavnagar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (148, N'Botad', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (149, N'Chhota Udepur', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (150, N'Dahod', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (151, N'Dangs (Ahwa)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (152, N'Devbhoomi Dwarka', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (153, N'Gandhinagar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (154, N'Gir Somnath', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (155, N'Jamnagar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (156, N'Junagadh', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (157, N'Kachchh', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (158, N'Kheda (Nadiad)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (159, N'Mahisagar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (160, N'Mehsana', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (161, N'Morbi', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (162, N'Narmada (Rajpipla)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (163, N'Navsari', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (164, N'Panchmahal (Godhra)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (165, N'Patan', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (166, N'Porbandar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (167, N'Rajkot', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (168, N'Sabarkantha (Himmatnagar)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (169, N'Surat', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (170, N'Surendranagar', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (171, N'Tapi (Vyara)', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (172, N'Vadodara', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (173, N'Valsad', 12)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (174, N'Ambala', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (175, N'Bhiwani', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (176, N'Faridabad', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (177, N'Fatehabad', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (178, N'Gurgaon', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (179, N'Hisar', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (180, N'Jhajjar', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (181, N'Jind', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (182, N'Kaithal', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (183, N'Karnal', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (184, N'Kurukshetra', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (185, N'Mahendragarh', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (186, N'Mewat', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (187, N'Palwal', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (188, N'Panchkula', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (189, N'Panipat', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (190, N'Rewari', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (191, N'Rohtak', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (192, N'Sirsa', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (193, N'Sonipat', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (194, N'Yamunanagar', 13)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (195, N'Bilaspur', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (196, N'Chamba', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (197, N'Hamirpur', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (198, N'Kangra', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (199, N'Kinnaur', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (200, N'Kullu', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (201, N'Lahaul & Spiti', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (202, N'Mandi', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (203, N'Shimla', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (204, N'Sirmaur (Sirmour)', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (205, N'Solan', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (206, N'Una', 14)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (207, N'Anantnag', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (208, N'Bandipora', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (209, N'Baramulla', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (210, N'Budgam', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (211, N'Doda', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (212, N'Ganderbal', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (213, N'Jammu', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (214, N'Kargil', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (215, N'Kathua', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (216, N'Kishtwar', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (217, N'Kulgam', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (218, N'Kupwara', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (219, N'Leh', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (220, N'Poonch', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (221, N'Pulwama', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (222, N'Rajouri', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (223, N'Ramban', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (224, N'Reasi', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (225, N'Samba', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (226, N'Shopian', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (227, N'Srinagar', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (228, N'Udhampur', 15)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (229, N'Bokaro', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (230, N'Chatra', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (231, N'Deoghar', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (232, N'Dhanbad', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (233, N'Dumka', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (234, N'East Singhbhum', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (235, N'Garhwa', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (236, N'Giridih', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (237, N'Godda', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (238, N'Gumla', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (239, N'Hazaribag', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (240, N'Jamtara', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (241, N'Khunti', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (242, N'Koderma', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (243, N'Latehar', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (244, N'Lohardaga', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (245, N'Pakur', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (246, N'Palamu', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (247, N'Ramgarh', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (248, N'Ranchi', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (249, N'Sahibganj', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (250, N'Seraikela-Kharsawan', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (251, N'Simdega', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (252, N'West Singhbhum', 16)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (253, N'Bagalkot', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (254, N'Bangalore Rural', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (255, N'Bangalore Urban', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (256, N'Belgaum', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (257, N'Bellary', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (258, N'Bidar', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (259, N'Bijapur', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (260, N'Chamarajanagar', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (261, N'Chickmagalur', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (262, N'Chikballapur', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (263, N'Chitradurga', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (264, N'Dakshina Kannada', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (265, N'Davangere', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (266, N'Dharwad', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (267, N'Gadag', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (268, N'Gulbarga', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (269, N'Hassan', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (270, N'Haveri', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (271, N'Kodagu', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (272, N'Kolar', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (273, N'Koppal', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (274, N'Mandya', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (275, N'Mysore', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (276, N'Raichur', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (277, N'Ramnagara', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (278, N'Shimoga', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (279, N'Tumkur', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (280, N'Udupi', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (281, N'Uttara Kannada (Karwar)', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (282, N'Yadgir', 17)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (283, N'Alappuzha', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (284, N'Ernakulam', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (285, N'Idukki', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (286, N'Kannur', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (287, N'Kasaragod', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (288, N'Kollam', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (289, N'Kottayam', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (290, N'Kozhikode', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (291, N'Malappuram', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (292, N'Palakkad', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (293, N'Pathanamthitta', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (294, N'Thiruvananthapuram', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (295, N'Thrissur', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (296, N'Wayanad', 18)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (297, N'Lakshadweep', 19)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (298, N'Alirajpur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (299, N'Anuppur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (300, N'Ashoknagar', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (301, N'Balaghat', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (302, N'Barwani', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (303, N'Betul', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (304, N'Bhind', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (305, N'Bhopal', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (306, N'Burhanpur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (307, N'Chhatarpur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (308, N'Chhindwara', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (309, N'Damoh', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (310, N'Datia', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (311, N'Dewas', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (312, N'Dhar', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (313, N'Dindori', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (314, N'Guna', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (315, N'Gwalior', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (316, N'Harda', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (317, N'Hoshangabad', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (318, N'Indore', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (319, N'Jabalpur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (320, N'Jhabua', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (321, N'Katni', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (322, N'Khandwa', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (323, N'Khargone', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (324, N'Mandla', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (325, N'Mandsaur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (326, N'Morena', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (327, N'Narsinghpur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (328, N'Neemuch', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (329, N'Panna', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (330, N'Raisen', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (331, N'Rajgarh', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (332, N'Ratlam', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (333, N'Rewa', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (334, N'Sagar', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (335, N'Satna', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (336, N'Sehore', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (337, N'Seoni', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (338, N'Shahdol', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (339, N'Shajapur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (340, N'Sheopur', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (341, N'Shivpuri', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (342, N'Sidhi', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (343, N'Singrauli', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (344, N'Tikamgarh', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (345, N'Ujjain', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (346, N'Umaria', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (347, N'Vidisha', 20)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (348, N'Ahmednagar', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (349, N'Akola', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (350, N'Amravati', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (351, N'Aurangabad', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (352, N'Beed', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (353, N'Bhandara', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (354, N'Buldhana', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (355, N'Chandrapur', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (356, N'Dhule', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (357, N'Gadchiroli', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (358, N'Gondia', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (359, N'Hingoli', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (360, N'Jalgaon', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (361, N'Jalna', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (362, N'Kolhapur', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (363, N'Latur', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (364, N'Mumbai City', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (365, N'Mumbai Suburban', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (366, N'Nagpur', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (367, N'Nanded', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (368, N'Nandurbar', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (369, N'Nashik', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (370, N'Osmanabad', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (371, N'Parbhani', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (372, N'Pune', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (373, N'Raigad', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (374, N'Ratnagiri', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (375, N'Sangli', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (376, N'Satara', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (377, N'Sindhudurg', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (378, N'Solapur', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (379, N'Thane', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (380, N'Wardha', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (381, N'Washim', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (382, N'Yavatmal', 21)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (383, N'Bishnupur', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (384, N'Chandel', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (385, N'Churachandpur', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (386, N'Imphal East', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (387, N'Imphal West', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (388, N'Senapati', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (389, N'Tamenglong', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (390, N'Thoubal', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (391, N'Ukhrul', 22)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (392, N'East Garo Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (393, N'East Jaintia Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (394, N'East Khasi Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (395, N'North Garo Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (396, N'Ri Bhoi', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (397, N'South Garo Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (398, N'South West Garo Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (399, N'South West Khasi Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (400, N'West Garo Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (401, N'West Jaintia Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (402, N'West Khasi Hills', 23)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (403, N'Aizawl', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (404, N'Champhai', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (405, N'Kolasib', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (406, N'Lawngtlai', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (407, N'Lunglei', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (408, N'Mamit', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (409, N'Saiha', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (410, N'Serchhip', 24)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (411, N'Dimapur', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (412, N'Kiphire', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (413, N'Kohima', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (414, N'Longleng', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (415, N'Mokokchung', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (416, N'Mon', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (417, N'Peren', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (418, N'Phek', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (419, N'Tuensang', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (420, N'Wokha', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (421, N'Zunheboto', 25)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (422, N'Angul', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (423, N'Balangir', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (424, N'Balasore', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (425, N'Bargarh', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (426, N'Bhadrak', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (427, N'Boudh', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (428, N'Cuttack', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (429, N'Deogarh', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (430, N'Dhenkanal', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (431, N'Gajapati', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (432, N'Ganjam', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (433, N'Jagatsinghapur', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (434, N'Jajpur', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (435, N'Jharsuguda', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (436, N'Kalahandi', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (437, N'Kandhamal', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (438, N'Kendrapara', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (439, N'Kendujhar (Keonjhar)', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (440, N'Khordha', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (441, N'Koraput', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (442, N'Malkangiri', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (443, N'Mayurbhanj', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (444, N'Nabarangpur', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (445, N'Nayagarh', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (446, N'Nuapada', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (447, N'Puri', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (448, N'Rayagada', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (449, N'Sambalpur', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (450, N'Sonepur', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (451, N'Sundargarh', 26)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (452, N'Karaikal', 27)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (453, N'Mahe', 27)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (454, N'Pondicherry', 27)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (455, N'Yanam', 27)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (456, N'Amritsar', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (457, N'Barnala', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (458, N'Bathinda', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (459, N'Faridkot', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (460, N'Fatehgarh Sahib', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (461, N'Fazilka', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (462, N'Ferozepur', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (463, N'Gurdaspur', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (464, N'Hoshiarpur', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (465, N'Jalandhar', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (466, N'Kapurthala', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (467, N'Ludhiana', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (468, N'Mansa', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (469, N'Moga', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (470, N'Muktsar', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (471, N'Nawanshahr', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (472, N'Pathankot', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (473, N'Patiala', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (474, N'Rupnagar', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (475, N'Sangrur', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (476, N'SAS Nagar (Mohali)', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (477, N'Tarn Taran', 28)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (478, N'Ajmer', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (479, N'Alwar', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (480, N'Banswara', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (481, N'Baran', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (482, N'Barmer', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (483, N'Bharatpur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (484, N'Bhilwara', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (485, N'Bikaner', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (486, N'Bundi', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (487, N'Chittorgarh', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (488, N'Churu', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (489, N'Dausa', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (490, N'Dholpur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (491, N'Dungarpur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (492, N'Hanumangarh', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (493, N'Jaipur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (494, N'Jaisalmer', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (495, N'Jalore', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (496, N'Jhalawar', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (497, N'Jhunjhunu', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (498, N'Jodhpur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (499, N'Karauli', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (500, N'Kota', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (501, N'Nagaur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (502, N'Pali', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (503, N'Pratapgarh', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (504, N'Rajsamand', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (505, N'Sawai Madhopur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (506, N'Sikar', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (507, N'Sirohi', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (508, N'Sri Ganganagar', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (509, N'Tonk', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (510, N'Udaipur', 29)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (511, N'East Sikkim', 30)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (512, N'North Sikkim', 30)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (513, N'South Sikkim', 30)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (514, N'West Sikkim', 30)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (515, N'Ariyalur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (516, N'Chennai', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (517, N'Coimbatore', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (518, N'Cuddalore', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (519, N'Dharmapuri', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (520, N'Dindigul', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (521, N'Erode', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (522, N'Kanchipuram', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (523, N'Kanyakumari', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (524, N'Karur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (525, N'Krishnagiri', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (526, N'Madurai', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (527, N'Nagapattinam', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (528, N'Namakkal', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (529, N'Nilgiris', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (530, N'Perambalur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (531, N'Pudukkottai', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (532, N'Ramanathapuram', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (533, N'Salem', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (534, N'Sivaganga', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (535, N'Thanjavur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (536, N'Theni', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (537, N'Thoothukudi (Tuticorin)', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (538, N'Tiruchirappalli', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (539, N'Tirunelveli', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (540, N'Tiruppur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (541, N'Tiruvallur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (542, N'Tiruvannamalai', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (543, N'Tiruvarur', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (544, N'Vellore', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (545, N'Viluppuram', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (546, N'Virudhunagar', 31)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (547, N'Adilabad', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (548, N'Hyderabad', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (549, N'Karimnagar', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (550, N'Khammam', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (551, N'Mahabubnagar', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (552, N'Medak', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (553, N'Nalgonda', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (554, N'Nizamabad', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (555, N'Rangareddy', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (556, N'Warangal', 32)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (557, N'Dhalai', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (558, N'Gomati', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (559, N'Khowai', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (560, N'North Tripura', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (561, N'Sepahijala', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (562, N'South Tripura', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (563, N'Unakoti', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (564, N'West Tripura', 33)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (565, N'Agra', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (566, N'Aligarh', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (567, N'Allahabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (568, N'Ambedkar Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (569, N'Auraiya', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (570, N'Azamgarh', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (571, N'Baghpat', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (572, N'Bahraich', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (573, N'Ballia', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (574, N'Balrampur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (575, N'Banda', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (576, N'Barabanki', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (577, N'Bareilly', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (578, N'Basti', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (579, N'Bhim Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (580, N'Bijnor', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (581, N'Budaun', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (582, N'Bulandshahr', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (583, N'Chandauli', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (584, N'Chatrapati Sahuji Mahraj Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (585, N'Chitrakoot', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (586, N'Deoria', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (587, N'Etah', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (588, N'Etawah', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (589, N'Faizabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (590, N'Farrukhabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (591, N'Fatehpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (592, N'Firozabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (593, N'Gautam Buddha Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (594, N'Ghaziabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (595, N'Ghazipur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (596, N'Gonda', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (597, N'Gorakhpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (598, N'Hamirpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (599, N'Hardoi', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (600, N'Hathras', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (601, N'Jalaun', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (602, N'Jaunpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (603, N'Jhansi', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (604, N'Jyotiba Phule Nagar (J.P. Nagar)', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (605, N'Kannauj', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (606, N'Kanpur Dehat', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (607, N'Kanpur Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (608, N'Kanshiram Nagar (Kasganj)', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (609, N'Kaushambi', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (610, N'Kushinagar (Padrauna)', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (611, N'Lakhimpur - Kheri', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (612, N'Lalitpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (613, N'Lucknow', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (614, N'Maharajganj', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (615, N'Mahoba', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (616, N'Mainpuri', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (617, N'Mathura', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (618, N'Mau', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (619, N'Meerut', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (620, N'Mirzapur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (621, N'Moradabad', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (622, N'Muzaffarnagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (623, N'Panchsheel Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (624, N'Pilibhit', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (625, N'Prabuddh Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (626, N'Pratapgarh', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (627, N'RaeBareli', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (628, N'Rampur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (629, N'Saharanpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (630, N'Sant Kabir Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (631, N'Sant Ravidas Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (632, N'Shahjahanpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (633, N'Shravasti', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (634, N'Siddharth Nagar', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (635, N'Sitapur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (636, N'Sonbhadra', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (637, N'Sultanpur', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (638, N'Unnao', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (639, N'Varanasi', 34)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (640, N'Almora', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (641, N'Bageshwar', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (642, N'Chamoli', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (643, N'Champawat', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (644, N'Dehradun', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (645, N'Haridwar', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (646, N'Nainital', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (647, N'Pauri Garhwal', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (648, N'Pithoragarh', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (649, N'Rudraprayag', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (650, N'Tehri Garhwal', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (651, N'Udham Singh Nagar', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (652, N'Uttarkashi', 35)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (653, N'Bankura', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (654, N'Birbhum', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (655, N'Burdwan (Bardhaman)', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (656, N'Cooch Behar', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (657, N'Dakshin Dinajpur (South Dinajpur)', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (658, N'Darjeeling', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (659, N'Hooghly', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (660, N'Howrah', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (661, N'Jalpaiguri', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (662, N'Kolkata', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (663, N'Malda', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (664, N'Murshidabad', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (665, N'Nadia', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (666, N'North 24 Parganas', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (667, N'Paschim Medinipur (West Medinipur)', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (668, N'Purba Medinipur (East Medinipur)', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (669, N'Purulia', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (670, N'South 24 Parganas', 36)
GO
INSERT [dbo].[City] ([CityId], [CityName], [StateId]) VALUES (671, N'Uttar Dinajpur (North Dinajpur)', 36)
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[ClinicTime] ON 

GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (1, 4, N'Monday', 1, 1, CAST(0x07005A9426450000 AS Time), N'AM', CAST(0x0700D85EAC3A0000 AS Time), N'PM', 1, CAST(0x0000A4B10126FA9A AS DateTime), 1, CAST(0x0000A4B10126FB4C AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (2, 4, N'Tuesday', 1, 1, CAST(0x070040230E430000 AS Time), N'AM', CAST(0x0700D85EAC3A0000 AS Time), N'AM', 1, CAST(0x0000A4B10126FDEB AS DateTime), 1, CAST(0x0000A4B10126FDEB AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (3, 4, N'Wednesday', 1, 1, CAST(0x070010ACD1530000 AS Time), N'AM', CAST(0x0700709A4A320000 AS Time), N'PM', 1, CAST(0x0000A4B10126FDFA AS DateTime), 1, CAST(0x0000A4B10126FDFA AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (4, 4, N'Thursday', 1, 1, CAST(0x070010ACD1530000 AS Time), N'AM', CAST(0x0700D85EAC3A0000 AS Time), N'PM', 1, CAST(0x0000A4B10126FE01 AS DateTime), 1, CAST(0x0000A4B10126FE01 AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (5, 4, N'Friday', 1, 1, CAST(0x070040230E430000 AS Time), N'PM', CAST(0x07003CB8192E0000 AS Time), N'PM', 1, CAST(0x0000A4B10126FE07 AS DateTime), 1, CAST(0x0000A4B10126FE07 AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (6, 4, N'Saturday', 1, 1, CAST(0x07003CB8192E0000 AS Time), N'PM', CAST(0x07003CB8192E0000 AS Time), N'PM', 1, CAST(0x0000A4B10126FE0C AS DateTime), 1, CAST(0x0000A4B10126FE0C AS DateTime))
GO
INSERT [dbo].[ClinicTime] ([ClinicTimeId], [LoginId], [Day], [FirstSetting], [IsWorkingDay], [StartTime], [StartSlot], [EndTime], [EndSlot], [CreatedById], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (7, 4, N'Sunday', 1, 1, CAST(0x07003CB8192E0000 AS Time), N'PM', CAST(0x07003CB8192E0000 AS Time), N'PM', 1, CAST(0x0000A4B10126FE11 AS DateTime), 1, CAST(0x0000A4B10126FE11 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ClinicTime] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

GO
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (1, N'India')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorClinicInformation] ON 

GO
INSERT [dbo].[DoctorClinicInformation] ([ClinicInfoId], [LoginId], [ClinicName], [ClinicAddress], [ClinicPhoneNo], [ClinicFees], [Country], [State], [City], [ZipCode], [ClinicServices], [AwardsAndRecognization], [AboutClinic], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 4, N'Smita clinic', N'at Pashan', N'9689787458', 500, 1, 15, 41, 411059, N'service', N'awards and recognization', N'About Clinic', N'N', 1, CAST(0x0000A4BE01301F74 AS DateTime), 1, CAST(0x0000A4BE01301F74 AS DateTime))
GO
INSERT [dbo].[DoctorClinicInformation] ([ClinicInfoId], [LoginId], [ClinicName], [ClinicAddress], [ClinicPhoneNo], [ClinicFees], [Country], [State], [City], [ZipCode], [ClinicServices], [AwardsAndRecognization], [AboutClinic], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 7, N'Apeksha Clinic', N'At wakad', N'6666666666', 500, 1, 15, 41, 411051, N'service', N'award ', N'about me', N'N', 1, CAST(0x0000A4B200FDCE10 AS DateTime), 1, CAST(0x0000A4B200FDCE10 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DoctorClinicInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[DoctorPersonalInformation] ON 

GO
INSERT [dbo].[DoctorPersonalInformation] ([PersonalInfoId], [LoginId], [MiddleName], [Qualification], [RegistrationNo], [Affiliation], [AboutMe], [AboutExperience], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 4, N'MiddleName', N'MCS', 1234, N'Affiliation', N'About me', N'experience', N'N', 1, CAST(0x0000A4BF00A4C441 AS DateTime), 1, CAST(0x0000A4BF00A4C441 AS DateTime))
GO
INSERT [dbo].[DoctorPersonalInformation] ([PersonalInfoId], [LoginId], [MiddleName], [Qualification], [RegistrationNo], [Affiliation], [AboutMe], [AboutExperience], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 7, N'akash', N'MCB', 2345, N'Affiliation', N'About me', N'Experience', N'N', 1, CAST(0x0000A4B300C4928D AS DateTime), 1, CAST(0x0000A4B300C4928D AS DateTime))
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
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, N'SystemAdmin', N'System', N'Admin', N'Admin@edoxhealthcare.com', 1000, N'ZqWxMHC/yPAD689h32IvoAQlTNZ8MA9S3mtIf04sVdnxiQtd53UzlZiQBLth2+4IM3kABQnu9N4YhC+wNhLkaQ==', N'ZqWxMHC/yPAD689h32IvoAQlTNZ8MA9S3mtIf04sVdnxiQtd53UzlZiQBLth2+4IM3kABQnu9N4YhC+wNhLkaQ==', N'100000.EJF9i5/2kz7fAw87+nuZSOPZEJAAqNR3YujzX4oQ4ZGfOA==', 0, NULL, NULL, NULL, NULL, NULL, N'On8IxP+d5egs22ETXBqD24YeYf84sCAlgjKRi4K4gPw+axux9maPFeyvq/6jvQRon3Ecao6g94I6fXxdQ7fDCA==', NULL, NULL, N'9689856985', N'N', 1, CAST(0x0000A486012AD400 AS DateTime), 1, CAST(0x0000A486012AD400 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'pranita', N'pranita', N'gurav', N'pranita@g.com', 0, N'EqK2VUHT8OvesN90oHqUNpe2j1w7ZGKgDobdu+XIFpJMY/9wL+ON4lzR+Ur+K+fVsAiSzwTUT+3UZV4rnurTQw==', N'EqK2VUHT8OvesN90oHqUNpe2j1w7ZGKgDobdu+XIFpJMY/9wL+ON4lzR+Ur+K+fVsAiSzwTUT+3UZV4rnurTQw==', N'100000.2jwRQGnqWkL0OfkeeZ39PI7PuNi4TNapMhk/7tjLrMNUxw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689858985', N'N', 1, CAST(0x0000A4AC010CE896 AS DateTime), 1, CAST(0x0000A4AC010CE918 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'sonali', N'sonali', N'borude', N'sonali@g.com', 0, N'bGiqveYKrIhRzMEHU5S3FhwCKcNkCZGkiEh9yFaIej056rGx/ZeyXDqX+IV9q4hhkCDb6f3X+I7XuxhPhMo3Sw==', N'bGiqveYKrIhRzMEHU5S3FhwCKcNkCZGkiEh9yFaIej056rGx/ZeyXDqX+IV9q4hhkCDb6f3X+I7XuxhPhMo3Sw==', N'100000.Pk8POJ4Br5DOsRDPT0kgQBCb1MM4gaykL9xUUbNlAEbfQw==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689856985', N'N', 1, CAST(0x0000A4AC012A6299 AS DateTime), 1, CAST(0x0000A4AC012A6299 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (4, N'smita', N'smita', N'godse', N'smita@gmail.com', 0, N'PLIZ3AgDcSX0lSVCMaEJi/93JGwEo2tMEgYNTDH1Ya0XYr2kqIue6r/GxGfWq4zUVr8uyRyodX6iQ6AS7u1QLw==', N'PLIZ3AgDcSX0lSVCMaEJi/93JGwEo2tMEgYNTDH1Ya0XYr2kqIue6r/GxGfWq4zUVr8uyRyodX6iQ6AS7u1QLw==', N'100000.QKeNrn5K/xTqulSp9ugedrMkeJfNyOolgwI8gtzZsl+nqA==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689859999', N'N', 1, CAST(0x0000A4AE013593A2 AS DateTime), 1, CAST(0x0000A4AE013593A3 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (5, N'madhura', N'madhura', N'joshi', N'madhura@g.com', 1002, N'AsazhRgx+T5GTmtP0b+7hp90Y6km6x/zokWAuyV8OQjsES/9njkQAsShurujnwLywLAWN01moQY1O9fRWyrkpw==', N'AsazhRgx+T5GTmtP0b+7hp90Y6km6x/zokWAuyV8OQjsES/9njkQAsShurujnwLywLAWN01moQY1O9fRWyrkpw==', N'100000.xOTIPz+kERBJUiEulAlF3p8ji9lSE+kNBobq3gV0X/c7qQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9656854585', N'N', 1, CAST(0x0000A4AF00B0D37D AS DateTime), 1, CAST(0x0000A4AF00B0D37D AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (6, N'Trupti', N'Trupti', N'Kanse', N'trupti@g.com', 1003, N'SYjd3GnzsU/OtkH72SyA/lVFfiGLycGKdKjmJ11ZHk1kTRlu/B2QOIU2X8xiUYVc3qmIZkZfBwGqhgvNQ8w9aw==', N'SYjd3GnzsU/OtkH72SyA/lVFfiGLycGKdKjmJ11ZHk1kTRlu/B2QOIU2X8xiUYVc3qmIZkZfBwGqhgvNQ8w9aw==', N'100000.yCHshiNP6hX+Pn8apWOHMkm2o4N1lJDNEF2MSSxK6hSJ2w==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689858985', N'N', 1, CAST(0x0000A4B100C25260 AS DateTime), 1, CAST(0x0000A4B100C25260 AS DateTime))
GO
INSERT [dbo].[Login] ([LoginId], [UserName], [FirstName], [LastName], [EmailID], [EmployeeId], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (7, N'Apeksh', N'apeksh', N'navale', N'borudesonali2@gmail.com', 0, N'JiGixO2iddd/PLBP5j+sgFu6bBZ2izKF1MDFfkHdnSvEkB9CpD0PRBLAs5dZSPTRBU75G+kPcGIphluLahjFXA==', N'JiGixO2iddd/PLBP5j+sgFu6bBZ2izKF1MDFfkHdnSvEkB9CpD0PRBLAs5dZSPTRBU75G+kPcGIphluLahjFXA==', N'100000.BCmVxucyh1vPzZZnW6qMGPyg5s9a3lopRk3B0pHFYEPnAA==', 0, NULL, NULL, NULL, NULL, NULL, N'SFAqLBtktGcR1/K8h4VO1+Gc+kpan5vHteZ42G8qt3skzDN0R57luXCAglNYYOa5poAN5glpVdDKZpBxR814gQ==', CAST(0x0000A4C00117D724 AS DateTime), NULL, N'2356458578', N'N', 1, CAST(0x0000A4B100C56C4E AS DateTime), 1, CAST(0x0000A4B100C56C4E AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[Login_Role] ON 

GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (2, 4, 2)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (3, 4, 3)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (4, 4, 4)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (5, 2, 5)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (6, 2, 6)
GO
INSERT [dbo].[Login_Role] ([LoginRoleId], [RoleId], [LoginId]) VALUES (7, 4, 7)
GO
SET IDENTITY_INSERT [dbo].[Login_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Login_Speciality] ON 

GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (1, 184, 2)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (2, 25, 3)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (3, 25, 4)
GO
INSERT [dbo].[Login_Speciality] ([LoginSpecialityId], [SpecialityID], [LoginId]) VALUES (4, 25, 7)
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
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, 4, N'~/Media/drawing.jpg', N'image/jpeg', N'N', 1, CAST(0x0000A4B101279EBF AS DateTime), 1, CAST(0x0000A4B101279EBF AS DateTime))
GO
INSERT [dbo].[Media] ([MediaId], [LoginId], [FolderFilePath], [UploadType], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, 4, N'~/Media/Hemerocallis_lilioasphodelus_flower.jpg', N'image/jpeg', N'N', 1, CAST(0x0000A4B10127AF39 AS DateTime), 1, CAST(0x0000A4B10127AF39 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Media] OFF
GO
SET IDENTITY_INSERT [dbo].[Membership] ON 

GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [CheckedStatus], [InactiveFlag], [ModifiedDate]) VALUES (1, N'Directory Listing', 0, 0, N'N', CAST(0x0000A4B700C1E546 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [CheckedStatus], [InactiveFlag], [ModifiedDate]) VALUES (2, N'Online Appointment Scheduling', 1000, 0, N'N', CAST(0x0000A4B700C1FBD6 AS DateTime))
GO
INSERT [dbo].[Membership] ([MembershipId], [MembershipType], [Fees], [CheckedStatus], [InactiveFlag], [ModifiedDate]) VALUES (3, N'Medical Answering Service', 1000, 0, N'N', CAST(0x0000A4B700C20BE6 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Membership] OFF
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

GO
INSERT [dbo].[Patient] ([PatientId], [FirstName], [LastName], [EmailID], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (1, N'chaitali', N'Doke', N'chaitali@g.com', N'mfHQk1awneiwmHlu5Q/O4YPzyng2o2t0/pMNG2GK9TdTsm6BmFj0IeWuVd6MJSWkupNp4X23cNXKs2il8G4+/Q==', N'mfHQk1awneiwmHlu5Q/O4YPzyng2o2t0/pMNG2GK9TdTsm6BmFj0IeWuVd6MJSWkupNp4X23cNXKs2il8G4+/Q==', N'100000.sWRRrMylD3XunnhRQcHRsGp9tQGIA7zOcy2JaHdD3qwQiA==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689854585', N'N', 1, CAST(0x0000A4AE00CC4F11 AS DateTime), 1, CAST(0x0000A4AE00CC505D AS DateTime))
GO
INSERT [dbo].[Patient] ([PatientId], [FirstName], [LastName], [EmailID], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (2, N'Neha', N'Kasar', N'neha@g.com', N'YCkWkAQ2ZsT/bSLlHTiuSmycWtiOcwJu3vQU7Dl1Nq4waDe/MTgu8Ylmn9jLJ80Whz7Y8OorFonnowysWq017A==', N'YCkWkAQ2ZsT/bSLlHTiuSmycWtiOcwJu3vQU7Dl1Nq4waDe/MTgu8Ylmn9jLJ80Whz7Y8OorFonnowysWq017A==', N'100000.Oefrp+pUDDPNdotc4Tq4Hj4Zs+2QG1D/gPqMuvgDmyuawQ==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689787485', N'N', 1, CAST(0x0000A4AE010C7DD7 AS DateTime), 1, CAST(0x0000A4AE010C7DD7 AS DateTime))
GO
INSERT [dbo].[Patient] ([PatientId], [FirstName], [LastName], [EmailID], [Password], [ConfirmPassword], [PasswordSalt], [IslockedOut], [LastLockoutDate], [LastLoginDate], [LastLogOutDate], [IPAddress], [LastPasswordChangedDate], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate], [FailedPasswordAttemptCount], [UserPhone], [InactiveFlag], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate]) VALUES (3, N'sneha', N'adya', N'neha@gmail.com', N'OvZVU1mXFv4SH5w03O6i9OjGPS+emK2sjSOmi3LzyoUShEqZIsJfaYDAQE8OMU7MgdaYOiVJsod1UzQtmQmZqQ==', N'OvZVU1mXFv4SH5w03O6i9OjGPS+emK2sjSOmi3LzyoUShEqZIsJfaYDAQE8OMU7MgdaYOiVJsod1UzQtmQmZqQ==', N'100000.Qg9c1TZFk7SvJGYy3LP1apfgEyvo74ajMBAMRVdJiaO5gg==', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'9689858985', N'N', 1, CAST(0x0000A4AF0125105F AS DateTime), 1, CAST(0x0000A4AF0125105F AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET IDENTITY_INSERT [dbo].[SchedulingDiary] ON 

GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (11, N'Sunday', 0, CAST(0x0000A4BF00A4CB80 AS DateTime), CAST(0x0000A4BF00AA49C0 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x070068C461080000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (12, N'Monday', 0, CAST(0x0000A4B800B54640 AS DateTime), CAST(0x0000A4BC00BD83A0 AS DateTime), 5790, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x070068C461080000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (13, N'Monday', 0, CAST(0x0000A4B800317040 AS DateTime), CAST(0x0000A4BB00317040 AS DateTime), 4350, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x070040230E430000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (15, N'Sunday', 0, CAST(0x0000A4C000A4CB80 AS DateTime), CAST(0x0000A4C200AA49C0 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x070068C461080000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (16, N'Sunday', 0, CAST(0x0000A4C100A4CB80 AS DateTime), CAST(0x0000A4C200AA49C0 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x070068C461080000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (17, N'Sunday', 0, CAST(0x0000A4C200A4CB80 AS DateTime), CAST(0x0000A4C200AA49C0 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x070068C461080000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (19, N'Monday', 0, CAST(0x0000A4B900317040 AS DateTime), CAST(0x0000A4B80039ADA0 AS DateTime), 30, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x070040230E430000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (20, N'sss', 0, CAST(0x0000A4C600C5C100 AS DateTime), NULL, 20, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x0700A8E76F4B0000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (21, N'sss', 0, CAST(0x0000A4C700C5C100 AS DateTime), CAST(0x0000A4C900CB3F40 AS DateTime), 20, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x0700A8E76F4B0000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (22, N'sss', 0, CAST(0x0000A4C800C5C100 AS DateTime), CAST(0x0000A4C900CB3F40 AS DateTime), 20, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x0700A8E76F4B0000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (23, N'sss', 0, CAST(0x0000A4C900C5C100 AS DateTime), CAST(0x0000A4C900CB3F40 AS DateTime), 20, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x0700A8E76F4B0000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (24, N'sss', 0, CAST(0x0000A4CA00C5C100 AS DateTime), CAST(0x0000A4C900CB3F40 AS DateTime), 20, 0, CAST(0x0700709A4A320000 AS Time), N'PM', CAST(0x0700A8E76F4B0000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (25, N'aaa', 0, CAST(0x0000A4CC00C5C100 AS DateTime), NULL, 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x0700D088C3100000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (26, N'aaa', 0, CAST(0x0000A4CD00C5C100 AS DateTime), CAST(0x0000A4CF00CB3F40 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x0700D088C3100000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (27, N'aaa', 0, CAST(0x0000A4CE00C5C100 AS DateTime), CAST(0x0000A4CF00CB3F40 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x0700D088C3100000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (28, N'aaa', 0, CAST(0x0000A4CF00C5C100 AS DateTime), CAST(0x0000A4CF00CB3F40 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x0700D088C3100000 AS Time), N'PM')
GO
INSERT [dbo].[SchedulingDiary] ([ID], [Title], [SomeImportantKey], [DateTimeScheduled], [DateTimeScheduledEnd], [AppointmentLength], [StatusENUM], [StartTime], [StartSlot], [EndTime], [EndSlot]) VALUES (29, N'aaa', 0, CAST(0x0000A4D000C5C100 AS DateTime), CAST(0x0000A4CF00CB3F40 AS DateTime), 20, 0, CAST(0x0700A8E76F4B0000 AS Time), N'AM', CAST(0x0700D088C3100000 AS Time), N'PM')
GO
SET IDENTITY_INSERT [dbo].[SchedulingDiary] OFF
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (1, N'Accupressur', N'N', CAST(0x0000A4B100DDF56A AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (2, N'Acute and Chronic Diseases', N'N', CAST(0x0000A4AB00AFF2B8 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (3, N'AIDS/HIV Infectious Diseases', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (4, N'Allergy/Asthma and Immunology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (5, N'Anaesthesiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (6, N'Anaesthesiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (7, N'Anaesthesiology, Critical Care & Pain Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (8, N'Andrology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (9, N'Aroma Theraphy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (10, N'Audiometry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (11, N'Ayurveda', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (12, N'Ayurveda - Medicine Cardiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (13, N'Ayurvedic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (14, N'Ayurvedic Consultant and Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (15, N'Ayurvedic Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (16, N'Ayurvedic Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (17, N'Ayurvedic Siddha', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (18, N'Ayurvedic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (19, N'B T M Layout', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (20, N'Body Focus', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (21, N'Brain & Spine Surgery Neurosurgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (22, N'Cancer', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (23, N'Cardio Thoracic and Vascular Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (24, N'Cardiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (25, N'Cardiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (26, N'Cardiothoracic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (27, N'Chest and TB', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (28, N'Clinical Biochemistry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (29, N'Clinical Psychology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (30, N'Conservative & Endodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (31, N'Conservative Dentistry & Endodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (32, N'Consultant Dermatologist and Cosmetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (33, N'Consultant For Women and Children', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (34, N'Consultant Pediatrician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (35, N'Consultant Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (36, N'Consultant Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (37, N'Consultant Radiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (38, N'Consultant Urology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (39, N'Cosmetic & Plastic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (40, N'Cosmetic and Plastic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (41, N'Cosmetic and Plastic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (42, N'Cosmetic Dentist & Endodontist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (43, N'Cosmetic Dentist Implantalogist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (44, N'Cosmetic Dentistry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (45, N'Cosmetic Plastic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (46, N'Cosmetic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (47, N'Cosmo', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (48, N'Dental Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (49, N'Dental, Oral & Maxillofacial Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (50, N'Dentist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (51, N'Dentist, Oral and Maxillofacial Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (52, N'Dentist, Periodontics and Implantologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (53, N'Dermatolight', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (54, N'Dermatologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (55, N'Dermatologist & Cosmetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (56, N'Dermatologist And Cosmetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (57, N'Dermatology, Venereology, Leprosy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (58, N'Diabetes', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (59, N'Diabetes And Cardiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (60, N'Diabetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (61, N'Diabetologist & Neurologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (62, N'Diabetologist and Cardiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (63, N'Diabetology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (64, N'Dietician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (65, N'E.N.T.', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (66, N'Emergency Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (67, N'Endocrinologist and Diabetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (68, N'Endocrinology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (69, N'Endodontia (Root Canal)', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (70, N'Endodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (71, N'Endodontist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (72, N'Endodontist & Aesthetic Dentistry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (73, N'ENT', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (74, N'ENT and Head Neck Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (75, N'ENT Head and Neck Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (76, N'ENT Micro Surgery Ears and Other Routine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (77, N'ENT Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (78, N'ENT Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (79, N'Eye Consultant and Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (80, N'Eye Specialist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (81, N'Family Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (82, N'Family Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (83, N'Female Urology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (84, N'Gastro Intestinal Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (85, N'Gastroentero and Endoscopy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (86, N'Gastroentero Hepatology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (87, N'Gastroenterology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (88, N'Gastroenterology and Hepatology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (89, N'Gastroenterology/General Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (90, N'General and Laparoscopic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (91, N'General Clinic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (92, N'General Dentistry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (93, N'General Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (94, N'General Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (95, N'General Practice', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (96, N'General Practitioner', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (97, N'General Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (98, N'General Surgeon Laparoscopic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (99, N'General Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (100, N'Genetics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (101, N'Gynaecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (102, N'Gynaecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (103, N'Gynaecology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (104, N'Gynecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (105, N'Gynecology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (106, N'Hand and Micro Vascular Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (107, N'Head and Neck Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (108, N'Hear Ailments', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (109, N'Heart clinic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (110, N'Hematologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (111, N'Hematology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (112, N'Homeoapathy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (113, N'Homeopathic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (114, N'Homeopathic Consultant', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (115, N'Homeopathy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (116, N'Homoeo Physician - Consultant', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (117, N'Homoeopathic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (118, N'Homoeopathic Consultant', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (119, N'Homoeopathy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (120, N'Hormone Problems', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (121, N'Hospitaladmn', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (122, N'Implantalogist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (123, N'Implantologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (124, N'Industrial Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (125, N'Infertility', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (126, N'Infertility Clinic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (127, N'Infertility Specialist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (128, N'Internal Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (129, N'Interventional Cardiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (130, N'J.P.Nagar', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (131, N'Laparoscopic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (132, N'Laparoscopic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (133, N'Laparoscopic Urology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (134, N'Laproscopic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (135, N'Laproscopic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (136, N'Maxillofacial Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (137, N'Maxillofacial Surgeon & Implantologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (138, N'Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (139, N'Neonatologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (140, N'Nephrology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (141, N'Neuro Psychiatry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (142, N'Neuro Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (143, N'Neuro Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (144, N'Neurological Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (145, N'Neurologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (146, N'Neurology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (147, N'Neuropsychiatry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (148, N'Neurosurgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (149, N'Obstetric and Gynecology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (150, N'Obstetrician ', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (151, N'Obstetrician & Gynaecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (152, N'Obstetrician &Gynaecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (153, N'Obstetrician and Gynaecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (154, N'Obstetrician and Gynecologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (155, N'Obstetrician and Gynecologists', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (156, N'Obstetrics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (157, N'Obstetrics & Gynecology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (158, N'Obstetrics and Gynaecology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (159, N'Obstetrics, Gynecology and Infertility', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (160, N'Oncologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (161, N'Oncology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (162, N'Onocology and Laparoscopic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (163, N'Ophthalmologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (164, N'Ophthalmology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (165, N'Oral & Maxillofacial Pathology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (166, N'Oral & Maxillofacial Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (167, N'Oral & Maxillofacial Surgery, Orthodentist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (168, N'Oral and Maxillo Facial Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (169, N'Oral and Maxillofacial Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (170, N'Oral And Maxillofacial Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (171, N'Oral Implantology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (172, N'Oral Medicine & Radiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (173, N'Oral Pathology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (174, N'Orthodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (175, N'Orthodontist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (176, N'Orthodontist And Dentofacial Orthopedician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (177, N'Orthopaedic', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (178, N'Orthopaedic and Joint Replacement Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (179, N'Orthopaedic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (180, N'Orthopaedic Surgeon & Arthroscopy Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (181, N'Orthopaedic Surgeon, Joint Replacement, Trauma', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (182, N'Orthopaedic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (183, N'Orthopaedics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (184, N'Orthopeadics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (185, N'Orthopedic Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (186, N'Orthopedics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (187, N'Otolaryngology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (188, N'Paediatrician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (189, N'Paediatrics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (190, N'Paediatrics, Neonatology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (191, N'Paedratrician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (192, N'Pathalogy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (193, N'Pathology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (194, N'Peadiatrics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (195, N'Pediatrician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (196, N'Pediatrics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (197, N'Pedodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (198, N'Periodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (199, N'Periodontics and Implant Dentistry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (200, N'Physician', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (201, N'Physician & Diabetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (202, N'Physician and Diabetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (203, N'Physician and Sonologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (204, N'Physician Of Unani Medicine', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (205, N'Physiotheraphy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (206, N'Physiotherapy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (207, N'Piles and Fistula', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (208, N'Plastic surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (209, N'Podiatric Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (210, N'Proctology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (211, N'Prosthetics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (212, N'Prosthodontics', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (213, N'Prosthodontics & Implantology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (214, N'Prosthodontist & Implantologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (215, N'Psychiatry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (216, N'Psychiatry & Neurology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (217, N'Psychologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (218, N'Psychotherapy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (219, N'Pulmonologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (220, N'Qastroenterologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (221, N'Radiologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (222, N'Radiology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (223, N'Radiotherapy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (224, N'RCT Crowns and Bridges Implants', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (225, N'Rheumatology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (226, N'Sexologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (227, N'Sexual Medecine and Marital Therapy', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (228, N'Sikn Vd and Leprosy Specialist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (229, N'Skin Allergy and Cosmeosologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (230, N'Skin and VD', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (231, N'Skin Specialist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (232, N'Sonologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (233, N'Sonology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (234, N'Soul Mind and Body Develop', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (235, N'Specialist Physician , Diabetologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (236, N'Speech and Hearing', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (237, N'Speech Hearing', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (238, N'Spine Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (239, N'Spondilits', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (240, N'Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (241, N'Surgical Oncology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (242, N'Thoracic Surgery', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (243, N'Transplant Surgeon', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (244, N'Tympanometry', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (245, N'Ultra Sonologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (246, N'Urologist', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (247, N'Urology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (248, N'Urology - Oncology', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (249, N'Veterinary', N'N', CAST(0x0000A4A100000000 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (250, N'aaaa', N'N', CAST(0x0000A4B100E0D585 AS DateTime))
GO
INSERT [dbo].[Speciality] ([SpecialityID], [SpecialityName], [InactiveFlag], [ModifiedDate]) VALUES (251, N'bbbb', N'N', CAST(0x0000A4B100E1252B AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 

GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (1, N'Andaman and Nicobar Island', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (2, N'Andhra Pradesh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (3, N'Arunachal Pradesh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (4, N'Assam', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (5, N'Bihar', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (6, N'Chandigarh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (7, N'Chhattisgarh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (8, N'Dadra and Nagar Haveli', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (9, N'Daman and Diu', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (10, N'Delhi', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (11, N'Goa', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (12, N'Gujarat', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (13, N'Haryana', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (14, N'Himachal Pradesh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (15, N'Jammu and Kashmir', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (16, N'Jharkhand', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (17, N'Karnataka', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (18, N'Kerala', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (19, N'Lakshadweep', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (20, N'Madhya Pradesh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (21, N'Maharashtra', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (22, N'Manipur', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (23, N'Meghalaya', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (24, N'Mizoram', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (25, N'Nagaland', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (26, N'Odisha', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (27, N'Puducherry', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (28, N'Punjab', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (29, N'Rajasthan', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (30, N'Sikkim', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (31, N'Tamil Nadu', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (32, N'Telangana', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (33, N'Tripura', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (34, N'Uttar Pradesh', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (35, N'Uttarakhand', 1)
GO
INSERT [dbo].[State] ([StateId], [StateName], [CountryId]) VALUES (36, N'West Bengal', 1)
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
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (1, 4, 1, 4, 1, 1, 1, N'N', CAST(0x0000A4B200A83DB0 AS DateTime), 1, CAST(0x0000A4B200A83DB0 AS DateTime))
GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (2, 4, 2, 4, 1, 1, 1, N'N', CAST(0x0000A4B200A83DBE AS DateTime), 1, CAST(0x0000A4B200A83DBE AS DateTime))
GO
INSERT [dbo].[UpgradeService] ([UpgradeServiceId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (3, 7, 2, 1, 1, 1, 1, N'N', CAST(0x0000A4B200CA3CC1 AS DateTime), 1, CAST(0x0000A4B200CA3CC1 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UpgradeService] OFF
GO
SET IDENTITY_INSERT [dbo].[UpgradeServiceLog] ON 

GO
INSERT [dbo].[UpgradeServiceLog] ([UpgradeServLogId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (1, 4, 1, 4, 1, 1, 1, N'N', CAST(0x0000A4B200A83DB8 AS DateTime), 1, CAST(0x0000A4B200A83DB8 AS DateTime))
GO
INSERT [dbo].[UpgradeServiceLog] ([UpgradeServLogId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (2, 4, 2, 4, 1, 1, 1, N'N', CAST(0x0000A4B200A83DC3 AS DateTime), 1, CAST(0x0000A4B200A83DC3 AS DateTime))
GO
INSERT [dbo].[UpgradeServiceLog] ([UpgradeServLogId], [LoginId], [MembershipId], [Durations], [AutoRenaval], [CheckedStatus], [CreatedById], [InactiveFlag], [CreatedOnDate], [ModifiedById], [ModifiedOnDate]) VALUES (3, 7, 2, 1, 1, 1, 1, N'N', CAST(0x0000A4B200CA3CD9 AS DateTime), 1, CAST(0x0000A4B200CA3CD9 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UpgradeServiceLog] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Login_UserName]    Script Date: 6/22/2015 10:31:26 AM ******/
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [UQ_Login_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/22/2015 10:31:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[MCMDRoles]
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentDiary] ADD  CONSTRAINT [DF_ConsultantBookings_Status]  DEFAULT ((0)) FOR [StatusENUM]
GO
USE [master]
GO
ALTER DATABASE [MCMD] SET  READ_WRITE 
GO
