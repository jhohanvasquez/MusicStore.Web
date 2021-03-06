USE [BdMusicStore]
GO
/****** Object:  Table [dbo].[AlbumSet]    Script Date: 25/01/2021 10:04:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AlbunSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 25/01/2021 10:04:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Mail] [nvarchar](50) NULL,
	[Direction] [nvarchar](500) NULL,
	[Phone] [nvarchar](20) NULL,
	[Photo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 25/01/2021 10:04:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Album_Id] [int] NOT NULL,
	[Client_Id] [nvarchar](10) NOT NULL,
	[Total] [real] NOT NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SongSet]    Script Date: 25/01/2021 10:04:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Album_Id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SongSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_AlbunSet] FOREIGN KEY([Album_Id])
REFERENCES [dbo].[AlbumSet] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_AlbunSet]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Client] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Client]
GO
ALTER TABLE [dbo].[SongSet]  WITH CHECK ADD  CONSTRAINT [FK_SongSet_AlbunSet] FOREIGN KEY([Album_Id])
REFERENCES [dbo].[AlbumSet] ([Id])
GO
ALTER TABLE [dbo].[SongSet] CHECK CONSTRAINT [FK_SongSet_AlbunSet]
GO

USE [BdMusicStore]
GO
/****** Object:  StoredProcedure [dbo].[RegisterOrSaveUserClient]    Script Date: 29/01/2021 02:08:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegisterOrSaveUserClient]	
	  @Id nvarchar(10),
	  @Name nvarchar(100),
      @Mail nvarchar(50),
      @Direction nvarchar(500),
      @Phone nvarchar(20),
      @Photo nvarchar(max)
AS
BEGIN

	declare @ClientId int	
	
	SET NOCOUNT ON;
    
	IF  NOT EXISTS (SELECT * FROM Client 
WHERE Id=@Id)
	begin
		
		SET @ClientId = 0;

		insert into Client(
				  Id,
				  Name,
				  Mail,
				  Direction,
				  Phone,
				  Photo)
		values(
				  @Id,
				  @Name,
				  @Mail,
				  @Direction,
				  @Phone,
				  @Photo)
		
	end
	else
	begin

		SET @ClientId = @Id
		
		update Client
		set 			
			Name		= @Name,			
			Mail	= @Mail,
			Direction	= @Direction,
			Phone		= @Phone,
			Photo	= @Photo
		where Id = @ClientId

	end

	SELECT top 1 * from Client where Id = @ClientId

END


-----------------
/****** Object:  StoredProcedure [dbo].[ListarCertificados]    Script Date: 28/01/2021 10:16:40 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ListorDetailClient]
	-- Add the parameters for the stored procedure here
	 @Id nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	if (@Id = 0)
	begin
    -- Insert statements for procedure here
SELECT *
  FROM [dbo].[Client]
END
	else
	begin
	SELECT *
     FROM [dbo].[Client] where Id=@Id;
  END
   END
------------------------------------------
/****** Object:  StoredProcedure [dbo].[EliminarCertificado]    Script Date: 28/01/2021 10:15:51 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteClient]
	-- Add the parameters for the stored procedure here
	@Id nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	delete from PurchaseDetail where Id=@Id
	delete from Client where Id=@Id

END