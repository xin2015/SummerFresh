CREATE TABLE [dbo].[SYS_Log](
	[ID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
	[Thread] [nvarchar](200) NULL,
	[Logger] [nvarchar](200) NULL,
	[Message] [nvarchar](4000) NULL,
 CONSTRAINT [PK_SYS_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SYS_Log] ADD  CONSTRAINT [DF_SYS_Log_ID]  DEFAULT (newid()) FOR [ID]
GO

