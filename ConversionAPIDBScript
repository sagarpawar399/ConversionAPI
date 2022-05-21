USE [DB_A49191_pawarsagar399]
GO

/****** Object:  Table [dbo].[DataUnitFactor]    Script Date: 05/21/2022 10:10:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataUnitFactor](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Factor] [int] NOT NULL,
 CONSTRAINT [PK_DataUnitFactor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DataUnitFactor] ADD  CONSTRAINT [DF_DataUnitFactor_Id]  DEFAULT (newid()) FOR [Id]
GO

USE [DB_A49191_pawarsagar399]
GO

/****** Object:  Table [dbo].[LengthUnitFactor]    Script Date: 05/21/2022 9:55:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LengthUnitFactor](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Factor] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_LengthUnitFactor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LengthUnitFactor] ADD  CONSTRAINT [DF_LengthUnitFactor_Id]  DEFAULT (newid()) FOR [Id]
GO


USE [DB_A49191_pawarsagar399]
GO

/****** Object:  Table [dbo].[TempUnitFactor]    Script Date: 05/21/2022 9:48:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TempUnitFactor](
	[Id] [uniqueidentifier] NOT NULL,
	[Fac1] [decimal](18, 0) NOT NULL,
	[Fac2] [decimal](18, 0) NOT NULL,
	[Fac3] [decimal](18, 0) NOT NULL,
	[Fac4] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TempUnitFactor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TempUnitFactor] ADD  CONSTRAINT [DF_TempUnitFactor_Id]  DEFAULT (newid()) FOR [Id]
GO

USE [DB_A49191_pawarsagar399]
GO

INSERT INTO [dbo].[DataUnitFactor]([Name],[Factor])
     VALUES('bytetokb,kbtomb,mbtogb,gbtotb,kbtobyte,mbtokb,gbtomb,tbtogb',1);
INSERT INTO [dbo].[DataUnitFactor]([Name],[Factor])
     VALUES('bytetomb,kbtogb,mbtotb,mbtobyte,gbtokb,tbtomb',2)
INSERT INTO [dbo].[DataUnitFactor]([Name],[Factor])
     VALUES('bytetogb,kbtotb,gbtobyte,tbtokb',3)
INSERT INTO [dbo].[DataUnitFactor]([Name],[Factor])
     VALUES('bytetotb,tbtobyte',4)
GO


USE [DB_A49191_pawarsagar399]
GO

INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('cmtomm,mmtocm',10)
INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('mtocm,cmtom',100)
INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('kmtom,mtokm',1000)
INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('mtomm,mmtom',1000)
INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('kmtocm,cmtokm',100000)
INSERT INTO [dbo].[LengthUnitFactor]([Name],[Factor]) VALUES('kmtomm,mmtokm',1000000)
GO


USE [DB_A49191_pawarsagar399]
GO

INSERT INTO [dbo].[TempUnitFactor]([Fac1],[Fac2],[Fac3],[Fac4]) VALUES(32,5,9,273.15)
GO



