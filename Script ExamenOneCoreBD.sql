USE [master]
GO

/****** Object:  Database [ExamenOneCoreBD]    Script Date: 28/10/2019 10:10:04 a. m. ******/
CREATE DATABASE [ExamenOneCoreBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExamenOneCoreBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExamenOneCoreBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ExamenOneCoreBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExamenOneCoreBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [ExamenOneCoreBD] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExamenOneCoreBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ExamenOneCoreBD] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET ARITHABORT OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ExamenOneCoreBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ExamenOneCoreBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ExamenOneCoreBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ExamenOneCoreBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET RECOVERY FULL 
GO

ALTER DATABASE [ExamenOneCoreBD] SET  MULTI_USER 
GO

ALTER DATABASE [ExamenOneCoreBD] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ExamenOneCoreBD] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ExamenOneCoreBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ExamenOneCoreBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ExamenOneCoreBD] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ExamenOneCoreBD] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ExamenOneCoreBD] SET  READ_WRITE 
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Sexo] [nvarchar](1) NOT NULL,
	[Estatus] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaActualizacion] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spCreateUsuario]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateUsuario]  
 @Username NVARCHAR(50),   
 @Password NVARCHAR(200),   
 @Email NVARCHAR(200),   
 @Sexo NVARCHAR(1),
 @Estatus BIT  
AS  
BEGIN  
 SET NOCOUNT ON;  

 INSERT INTO Usuario(Username,Password,Email,Sexo,Estatus,FechaCreacion)  
 VALUES(@Username,@Password,@Email,@Sexo,@Estatus,CURRENT_TIMESTAMP)  
     
END  
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePassword]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdatePassword]
	@UserId INT,	
	@Password NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Usuario
	SET 
		Password = @Password
	WHERE 
		UserId = @UserId
    
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateUsuario]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateUsuario]
	@UserId INT,
	@Username NVARCHAR(50),	
	@Email NVARCHAR(200),
	@Sexo NVARCHAR(2),
	@Estatus BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Usuario
	SET 
		Username = @Username,
		Email = @Email,
		Sexo = @Sexo,
		Estatus = @Estatus
	WHERE 
		UserId = @UserId
    
END
GO
/****** Object:  StoredProcedure [dbo].[spValidaEmail]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spValidaEmail]  
 @Email NVARCHAR(50)
AS  
BEGIN  
 SET NOCOUNT ON;  

 SELECT * FROM Usuario WHERE Email = @Email;
      
END  
GO
/****** Object:  StoredProcedure [dbo].[spValidaUsuario]    Script Date: 28/10/2019 10:09:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spValidaUsuario]  
 @Username NVARCHAR(50)
AS  
BEGIN  
 SET NOCOUNT ON;  

 SELECT * FROM Usuario WHERE Username = @Username;
      
END  
GO
