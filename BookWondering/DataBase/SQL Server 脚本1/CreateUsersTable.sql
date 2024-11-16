USE BookWonderingDB
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2022/3/15 16:19:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QQ] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[PassWord] [nvarchar](max) NULL,
	[NickName] [nvarchar](20) NULL,
	[RegDate] [datetime2](7) NULL,
	[LoginNum] [int] NULL,
	[LastLoginTime] [datetime2](7) NULL,
	[UserType] [tinyint] NULL,
	[UserSex] [nvarchar](max) NULL,
	[Status] [tinyint] NULL,
	[CreateTime] [datetime2](7) NULL,
	[CreatorId] [int] NULL,
	[LastModifyTime] [datetime2](7) NULL,
	[LastModifierId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO