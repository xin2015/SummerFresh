USE [SummerFreshData]
GO
/****** Object:  Table [dbo].[APP_PageFile]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_PageFile](
	[PageFileId] [varchar](38) NOT NULL,
	[PageId] [varchar](38) NULL,
	[FileId] [varchar](38) NULL,
 CONSTRAINT [PK_APP_PageFile] PRIMARY KEY CLUSTERED 
(
	[PageFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'07f6edb8-6855-4dae-8b41-376a634d8f58', N'7dc7b0d9-50eb-446b-9892-ba5c4dcc0415', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'2477c91e-482d-4e95-ac1c-589292e0c81f', N'8ea34ad1-602d-4c10-83c1-55611634af57', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'2982dbdb-8a9f-41bd-9203-66fc98d125fb', N'6cf17081-7549-459d-85ac-c0bf558150c5', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'45f2a9d5-9cfa-4b7e-8157-ab78a4bbdb2c', N'6cf17081-7549-459d-85ac-c0bf558150c5', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'5ec76a7f-0cd1-4137-8563-3e59bf38d8e9', N'7dc7b0d9-50eb-446b-9892-ba5c4dcc0415', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'6c5846a8-cb93-49d4-953d-5dd1c14f9549', N'390b18b4-ba94-44f7-ad71-33465cb9d78f', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'73101494-f06c-4f1b-a4d5-e7b517dbe71e', N'8ea34ad1-602d-4c10-83c1-55611634af57', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'bfc4f2c2-4a06-4350-8f62-8d8fada63b9f', N'8ea34ad1-602d-4c10-83c1-55611634af57', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'db3ae90a-7669-4495-be15-f1b4fe8afde4', N'd16b757a-eed5-41c4-8f3d-6e13d23f6e0e', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_PageFile] ([PageFileId], [PageId], [FileId]) VALUES (N'dcd1357a-2545-4133-8d1c-1139e19fcfb0', N'6cf17081-7549-459d-85ac-c0bf558150c5', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
/****** Object:  Table [dbo].[APP_Page]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_Page](
	[PageId] [varchar](38) NOT NULL,
	[PageName] [nvarchar](50) NULL,
	[PageTitle] [nvarchar](100) NULL,
	[PageStartUpScript] [nvarchar](1000) NULL,
	[LayoutId] [varchar](38) NULL,
	[PageService] [nvarchar](100) NULL,
	[ParentId] [varchar](38) NULL,
	[PageType] [varchar](20) NULL,
	[PageScriptBlock] [text] NULL,
	[LastUpdateTime] [datetime] NULL,
	[PageStyle] [nvarchar](500) NULL,
 CONSTRAINT [PK_APP_Page] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'382971ce-3157-436e-bde2-d7651383fe6a', N'AlarmPublishEdit', N'AlarmPublishasdfsafadsfsad', N'', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'', CAST(0x0000A5E501183BDB AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SYSPermissionRuleEdit', N'SYSPermissionRule', NULL, N'ffd20d14-0b22-4956-b0b3-4eebceca091b', NULL, N'', N'1', NULL, CAST(0x0000A5E50121E799 AS DateTime), NULL)
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'3cfe182d-bdf8-4152-8c86-c60cce2a709d', N'ccc', N'ddd', N'', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'', N'6df272dd-c7cc-469d-ac62-94296b7046ed', N'1', N'', CAST(0x0000A5E700BC0DCA AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'6df272dd-c7cc-469d-ac62-94296b7046ed', N'aaa', N'bbb', N'', N'', N'', N'', N'0', N'', CAST(0x0000A5E700BBFB92 AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'AllProvinceList', N'AllProvince', N'', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'', CAST(0x0000A5E601125159 AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'AllCityEdit', N'AllCity', NULL, N'ffd20d14-0b22-4956-b0b3-4eebceca091b', NULL, N'', N'1', NULL, CAST(0x0000A5DF00BEDF2A AS DateTime), NULL)
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'NMCCityEdit', N'NMCCity', N'', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'', CAST(0x0000A5E501087D56 AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'AlarmPublishList', N'AlarmPublish', N'', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'', CAST(0x0000A5E5010DF61B AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'c378640e-5338-4fda-a603-4bd387d40b13', N'AllCityList', N'AllCity', NULL, N'ffd20d14-0b22-4956-b0b3-4eebceca091b', NULL, N'', N'1', NULL, CAST(0x0000A5E000E49258 AS DateTime), NULL)
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'cf88193b-85dd-415c-91db-41732cf8072b', N'SYSPermissionRuleList', N'SYSPermissionRule', N'helloc();', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'function helloc(){alert("hello world");}', CAST(0x0000A5E7011677F4 AS DateTime), N'')
INSERT [dbo].[APP_Page] ([PageId], [PageName], [PageTitle], [PageStartUpScript], [LayoutId], [PageService], [ParentId], [PageType], [PageScriptBlock], [LastUpdateTime], [PageStyle]) VALUES (N'ea9bd482-7168-4950-a255-0749bff9b75f', N'AllProvinceEdit', N'AllProvince', N'', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'', N'', N'1', N'', CAST(0x0000A5E7010C460D AS DateTime), N'')
/****** Object:  Table [dbo].[APP_LayoutFile]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_LayoutFile](
	[LayoutFileId] [varchar](38) NOT NULL,
	[LayoutId] [varchar](38) NULL,
	[FileId] [varchar](38) NULL,
 CONSTRAINT [PK_APP_LayoutExternalFile] PRIMARY KEY CLUSTERED 
(
	[LayoutFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'0132763d-8bdb-496c-82fd-b0140f18075a', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'0bbb8511-12e5-48c4-8f0d-fcf77138b439', N'127fbebe-bd37-4495-864e-1515902436a6', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'0c6a317c-7eeb-4cc8-9c8f-0d53837efdbb', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'0dc2bb17-6bee-4384-b628-7bf9fe6ba6ec', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'0fab0941-ce4d-4062-988c-34033e8944ab', N'd277b38f-4158-47c9-8b94-b998756573dd', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'1059bf57-aa3e-473b-a891-5e7d48010338', N'd277b38f-4158-47c9-8b94-b998756573dd', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'11b43cb2-96d7-4599-b9f5-84999ee79570', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'1206b6f2-259b-4e07-975f-71192853c582', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'813805f1-aca4-4cfd-b742-20c12d73f01c')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'18e3a660-4f00-4e7f-b421-ad801501f269', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'191ac569-92eb-4218-8880-45d846c385f4', N'127fbebe-bd37-4495-864e-1515902436a6', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'1bd4b427-ea40-46fe-8c93-9f101fe04c1a', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'1d71f9ea-575c-466f-bf47-d4d3abf51689', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'1e36c288-00e2-4bae-98fe-11f317e34a41', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'21db7d78-7e08-4b25-bedf-d38de92ceb73', N'127fbebe-bd37-4495-864e-1515902436a6', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'299b2574-d7f2-4d87-9812-3544cdb729a2', N'd277b38f-4158-47c9-8b94-b998756573dd', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'29c25199-c5d0-4738-bf82-afb354ce80f8', N'127fbebe-bd37-4495-864e-1515902436a6', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'2ec98c2d-fe72-46f7-8c1b-fc03617056eb', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'2edd0be7-3bbe-4806-8ff3-5e4149689153', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'813805f1-aca4-4cfd-b742-20c12d73f01c')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'2f9c6dc3-9b73-4aa4-8f03-f329c192de51', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'308118d1-3cd4-4dff-81ef-90ff5f4f008e', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'33106e59-e9f5-4f7d-841b-9df698ff5daa', N'd277b38f-4158-47c9-8b94-b998756573dd', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'3e5632de-2068-4fac-bf1f-9953530929b2', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'408283ed-54dc-4752-9138-dd36524d282a', N'd277b38f-4158-47c9-8b94-b998756573dd', N'e4582507-ff0b-472b-a392-fdb9e73db29e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'43b9fd44-b014-46aa-b56d-c75465ddbfca', N'127fbebe-bd37-4495-864e-1515902436a6', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'57d10203-46b5-4ced-b1c9-168261464bdb', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'58cf94c6-109b-44d4-bc77-d4a8a017429a', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'5b21ab45-9fb6-4453-bdc1-008c8280d1c0', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'61912761-5db0-4f79-943d-0dd37b61b733', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'652abd2b-9367-4bff-8d16-68561195d166', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'67e79598-80c2-48c7-9fd0-d7d210dea1c8', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'688a1647-4f14-401d-a43e-9638c238c106', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'689edd6d-8aac-4e32-a783-f90cd178df21', N'd277b38f-4158-47c9-8b94-b998756573dd', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'6db33256-023c-44c6-9b56-b2b281c9423e', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'70889c46-14a6-4cf6-af25-f0aa401927ec', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'7de738e1-8966-4763-b915-de111862f85b', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'8040f8e2-a25e-4c39-80fe-a8a5912ea1cd', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'8334e4a2-0d97-4d11-9f1b-b03b380ce8f0', N'127fbebe-bd37-4495-864e-1515902436a6', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'85f33c9a-22bc-4f39-831d-b52a30b5a5fd', N'127fbebe-bd37-4495-864e-1515902436a6', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'87c1ce47-c4cf-4b18-af82-28ac7b7fc5b8', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'88edd9e7-33d7-4cc0-9cfd-b40fe83075ba', N'd277b38f-4158-47c9-8b94-b998756573dd', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'8acfb90a-554c-4ed6-8f54-13e59a4a05ac', N'd277b38f-4158-47c9-8b94-b998756573dd', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'928649ed-e954-4b87-9473-685329e3b21b', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'943f2d5a-3cfa-4101-8251-3c584742c70c', N'd277b38f-4158-47c9-8b94-b998756573dd', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'9448e738-4866-48a0-87b0-5bd3ac2c9129', N'127fbebe-bd37-4495-864e-1515902436a6', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'9b3cdb8d-234f-4596-b135-9e95f8b7d738', N'd277b38f-4158-47c9-8b94-b998756573dd', N'813805f1-aca4-4cfd-b742-20c12d73f01c')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'a04172a6-ae1c-4796-982e-b2bd61459a7f', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'a4a5dad4-40fd-4b31-8982-a622a4645766', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'e4582507-ff0b-472b-a392-fdb9e73db29e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'a99e3aaf-4d61-4bcc-9a26-48bf2e846ca6', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'b057b738-ee06-4679-90a0-a4cbee2b53aa', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'c3ac59f3-37e2-420d-923b-fdf888b9b74e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'b451cad4-601b-43c9-b452-011f80803baa', N'127fbebe-bd37-4495-864e-1515902436a6', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'b4707ace-4e0f-4bf5-9a12-0aceaf56e9ce', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'b88174af-7891-492a-b593-28cb8ca6c7cc', N'd277b38f-4158-47c9-8b94-b998756573dd', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c19186d2-b093-4631-a8c8-957dae740c42', N'127fbebe-bd37-4495-864e-1515902436a6', N'c634bfc0-a3b6-486c-8ffd-a6c901425585')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c2703854-cbeb-43be-986f-3ae3e91b3530', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c4ef2318-c783-494a-b6a0-6f2f66a59909', N'127fbebe-bd37-4495-864e-1515902436a6', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c59bce85-5209-4b86-a967-24e7e336597b', N'd277b38f-4158-47c9-8b94-b998756573dd', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c63c1467-d637-49c7-bbde-487ba9d3e40d', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'972daf5b-1c43-441c-9543-81a127874df2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c6b37e64-e02f-4e63-aaeb-35e2b7a9b3cb', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c95ab582-00f2-471a-8bd6-8d5cf2060ea5', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'c9927941-97c0-4d70-a962-ed2a4f06f5c7', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'cb2cb48d-805c-4c1a-a801-204acc6f9590', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'0493fcab-50c5-4a02-a87e-bee6efbd4257')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'd448b087-eec7-4ac9-bf49-f7ec395b107d', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'cde87f14-7098-47e1-bfe2-090384985481')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'd52c1c7b-c272-41c7-931f-79b79de0e020', N'127fbebe-bd37-4495-864e-1515902436a6', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'd57c175e-6d5a-4a35-8df0-e58088b7d58e', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'e087578d-1e5d-4c50-9729-72e1f644f3f6', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'eccd5a1c-ab78-423b-8f96-22d6bd73e115', N'd277b38f-4158-47c9-8b94-b998756573dd', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'edd513bc-273d-40f6-9cbb-504114d5462f', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'eec3ffe1-5253-45d9-bc0c-a2021abfdc8a', N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'f4cb0f29-d39a-454b-a86d-a7c2f2aa4535', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'e4582507-ff0b-472b-a392-fdb9e73db29e')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'f53b21e8-f344-47d4-ab5c-350f1c6def81', N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'adfb5379-5213-4365-b9f5-2709bae736e8')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'f624de40-0307-499b-a9dc-6979d7793403', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'0a795132-b675-40b1-ac98-f2ac922a0071')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'f73b9817-2c28-4a3c-8168-cc19a65eedd2', N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'fab097c5-1e5a-47f8-8e83-4ced70898dfe', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f')
INSERT [dbo].[APP_LayoutFile] ([LayoutFileId], [LayoutId], [FileId]) VALUES (N'fd0e2615-5b81-42d9-a3d6-3bde0773713a', N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0')
/****** Object:  Table [dbo].[APP_Layout]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_Layout](
	[LayoutId] [varchar](38) NOT NULL,
	[LayoutHtml] [text] NULL,
	[LayoutStartUpScript] [nvarchar](2000) NULL,
	[LayoutName] [nvarchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
	[LayoutPageStyle] [nvarchar](500) NULL,
 CONSTRAINT [PK_APP_Layout] PRIMARY KEY CLUSTERED 
(
	[LayoutId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_Layout] ([LayoutId], [LayoutHtml], [LayoutStartUpScript], [LayoutName], [LastUpdateTime], [LayoutPageStyle]) VALUES (N'83586863-C2CF-40A2-B0E5-2B9D3E87E7E7', N'    <div class="treeform-left" data-options="region:''west'',split:true" title="树型菜单" style="width: 220px;" layout="Left">$Left$</div>    <div data-options="region:''center''" style="overflow-y:auto;" class="main-container" layout="Right">$Right$</div>', N'summerFresh.widget.init();', N'左窄右宽布局', CAST(0x0000A5B100F2AD0B AS DateTime), NULL)
INSERT [dbo].[APP_Layout] ([LayoutId], [LayoutHtml], [LayoutStartUpScript], [LayoutName], [LastUpdateTime], [LayoutPageStyle]) VALUES (N'8fd7e502-e753-4c6d-be7d-141a1b0fe01d', N'<div data-options="region:''north''" layout="Top" style="height:100px">$Top$</div> <div data-options="region:''center''" layout="Left" style="overflow-y:scroll;">$Left$</div>    <div data-options="region:''east'',split:true" style="width:220px;" layout="Right">$Right$</div>', N'summerFresh.widget.init();', N'上下-左宽右窄布局', CAST(0x0000A5D1010E210D AS DateTime), NULL)
INSERT [dbo].[APP_Layout] ([LayoutId], [LayoutHtml], [LayoutStartUpScript], [LayoutName], [LastUpdateTime], [LayoutPageStyle]) VALUES (N'f3f8d6b7-d51c-41cd-b226-e2d217cfe9ac', N'    <div data-options="region:''center''" layout="Left" style="overflow-y:scroll;">$Left$</div>    <div data-options="region:''east'',split:true" style="width:220px;" layout="Right">$Right$</div>', N'summerFresh.widget.init();', N'左宽右窄布局', CAST(0x0000A5B100F29C68 AS DateTime), NULL)
INSERT [dbo].[APP_Layout] ([LayoutId], [LayoutHtml], [LayoutStartUpScript], [LayoutName], [LastUpdateTime], [LayoutPageStyle]) VALUES (N'ffd20d14-0b22-4956-b0b3-4eebceca091b', N'<div layout="Right">$Right$</div>', N'summerFresh.widget.init();', N'普通布局', CAST(0x0000A5D800EF64DE AS DateTime), N'.blue{color:blue;}')
/****** Object:  Table [dbo].[APP_ExternalFile]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_ExternalFile](
	[FileId] [varchar](38) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[FilePath] [varchar](100) NULL,
	[Rank] [int] NULL,
	[FileType] [varchar](10) NULL,
 CONSTRAINT [PK_APP_ExternalFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'0493fcab-50c5-4a02-a87e-bee6efbd4257', N'easyui', N'/content/js/layout/jquery.easyui.min.js', 7, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'0a795132-b675-40b1-ac98-f2ac922a0071', N'reset', N'/content/css/reset.css', 1, N'CSS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'5cfa9182-ce16-4c89-ac38-13d9d37a4de2', N'blockui', N'/content/js/jquery.blockui.min.js', 5, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'5ebf15f1-d728-40f3-a13f-ea2562cc82e0', N'jquery', N'/content/js/jquery-1.9.1.min.js', 1, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'7379f121-0ae3-4ea4-ac79-fcd2a9d329b9', N'hightchart', N'/content/js/highcharts/highcharts.js', 10, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'7a00dc17-cc5a-440e-8785-a1aef198ca5c', N'xheditor', N'/content/js/xheditor-1.1.14-zh-cn.min.js', 3, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'813805f1-aca4-4cfd-b742-20c12d73f01c', N'tree', N'/content/js/zTree_v3/js/jquery.ztree.all-3.5.min.js', 4, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'8c85a8e6-8e2c-4c8e-aad4-c581bb69b07b', N'easyuicss', N'/Content/js/layout/css/easyui.css', 5, N'CSS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'972daf5b-1c43-441c-9543-81a127874df2', N'commoncss', N'/content/css/common.css', 2, N'CSS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'adfb5379-5213-4365-b9f5-2709bae736e8', N'commonjs', N'/content/js/common.js', 2, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'c3ac59f3-37e2-420d-923b-fdf888b9b74e', N'default', N'/content/themes/default/default.css', 4, N'CSS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'c634bfc0-a3b6-486c-8ffd-a6c901425585', N'datepicker', N'/content/js/datepicker/WdatePicker.js', 5, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'cd5d3dd6-0f5e-4c75-81fe-bc87f676b65f', N'showModalDialog', N'/content/js/showModalDialog.js', 6, N'JS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'cde87f14-7098-47e1-bfe2-090384985481', N'icon', N'/content/css/icon.css', 3, N'CSS')
INSERT [dbo].[APP_ExternalFile] ([FileId], [FileName], [FilePath], [Rank], [FileType]) VALUES (N'e4582507-ff0b-472b-a392-fdb9e73db29e', N'treecss', N'/content/js/zTree_v3/css/zTreeStyle/zTreeStyle.css', 4, N'CSS')
/****** Object:  Table [dbo].[APP_CRUD]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_CRUD](
	[CRUDId] [varchar](38) NOT NULL,
	[CRUDName] [nvarchar](50) NULL,
	[PKName] [varchar](50) NULL,
	[TableName] [varchar](50) NULL,
	[TitleFieldName] [varchar](50) NULL,
	[ValueFieldName] [varchar](50) NULL,
	[ParentFieldName] [varchar](50) NULL,
	[InsertSQL] [text] NULL,
	[UpdateSQL] [text] NULL,
	[DeleteSQL] [text] NULL,
	[GetOneSQL] [text] NULL,
	[SelectSQL] [text] NULL,
	[DefaultSortExpression] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_APP_CRUD] PRIMARY KEY CLUSTERED 
(
	[CRUDId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_CRUD] ([CRUDId], [CRUDName], [PKName], [TableName], [TitleFieldName], [ValueFieldName], [ParentFieldName], [InsertSQL], [UpdateSQL], [DeleteSQL], [GetOneSQL], [SelectSQL], [DefaultSortExpression], [LastUpdateTime]) VALUES (N'03be9e1d-d75e-41d3-b739-d106f35b6d79', N'AllProvince', N'Id', N'AllProvince', N'ProvinceName', N'Id', N'', N'INSERT INTO AllProvince 
([Id],[ProvinceCode],[ProvinceName],[ProvinceJC]) 
 VALUES 
 (#Id#,#ProvinceCode#,#ProvinceName#,#ProvinceJC#)', N'UPDATE AllProvince SET [Id]=#Id#
{?? ,[ProvinceCode]=#ProvinceCode# }
{?? ,[ProvinceName]=#ProvinceName# }
{?? ,[ProvinceJC]=#ProvinceJC# }
 WHERE [Id]=#Id#', N'DELETE FROM [AllProvince]  WHERE Id IN ($Id$)', N'SELECT [Id],
[ProvinceCode],
[ProvinceName],
[ProvinceJC] 
 FROM AllProvince WITH(NOLOCK) WHERE [Id]=#Id#', N'SELECT [Id],
[ProvinceCode],
[ProvinceName],
[ProvinceJC] 
 FROM [AllProvince] WITH(NOLOCK) WHERE 1=1 
{? AND [Id]=#Id# } 
{? AND [ProvinceCode]=#ProvinceCode# } 
{? AND [ProvinceName] like ''%$ProvinceName$%'' } 
{? AND [ProvinceJC] like ''%$ProvinceJC$%'' }', N'[Id] ASC', CAST(0x0000A5E70111E2F0 AS DateTime))
INSERT [dbo].[APP_CRUD] ([CRUDId], [CRUDName], [PKName], [TableName], [TitleFieldName], [ValueFieldName], [ParentFieldName], [InsertSQL], [UpdateSQL], [DeleteSQL], [GetOneSQL], [SelectSQL], [DefaultSortExpression], [LastUpdateTime]) VALUES (N'2b0beb0c-0e82-4415-8c26-2055f2d86b3e', N'NMCCity', N'StationCode', N'NMCCity', N'CityName', N'StationCode', N'', N'INSERT INTO NMCCity 
([StationCode],[CityCode],[CityName],[CityNamePY],[ProvinceAJC]) 
 VALUES 
 (#StationCode#,#CityCode#,#CityName#,#CityNamePY#,#ProvinceAJC#)', N'UPDATE NMCCity SET [StationCode]=#StationCode#
{?? ,[CityName]=#CityName# }
{?? ,[CityNamePY]=#CityNamePY# }
{?? ,[ProvinceAJC]=#ProvinceAJC# }
 WHERE [StationCode]=#StationCode# AND [CityCode]=#CityCode#', N'DELETE FROM [NMCCity]  WHERE [StationCode]=#StationCode# AND [CityCode]=#CityCode#', N'SELECT [StationCode],
[CityCode],
[CityName],
[CityNamePY],
[ProvinceAJC] 
 FROM NMCCity WITH(NOLOCK) WHERE [StationCode]=#StationCode# AND [CityCode]=#CityCode#', N'SELECT [StationCode],
[CityCode],
[CityName],
[CityNamePY],
[ProvinceAJC] 
 FROM [NMCCity] WITH(NOLOCK) WHERE 1=1 
{? AND [StationCode] like ''%$StationCode$%'' } 
{? AND [CityCode] like ''%$CityCode$%'' } 
{? AND [CityName] like ''%$CityName$%'' } 
{? AND [CityNamePY] like ''%$CityNamePY$%'' } 
{? AND [ProvinceAJC] like ''%$ProvinceAJC$%'' }', N'[StationCode] ASC', CAST(0x0000A5E7011E9582 AS DateTime))
INSERT [dbo].[APP_CRUD] ([CRUDId], [CRUDName], [PKName], [TableName], [TitleFieldName], [ValueFieldName], [ParentFieldName], [InsertSQL], [UpdateSQL], [DeleteSQL], [GetOneSQL], [SelectSQL], [DefaultSortExpression], [LastUpdateTime]) VALUES (N'500f1bec-f187-432d-948e-7ff01e548228', N'SYSPermissionRule', N'PermissionRuleId', N'SYS_PermissionRule', N'RuleName', N'PermissionRuleId', NULL, N'INSERT INTO SYS_PermissionRule 
([PermissionRuleId],[PermissionId],[RuleName],[Priority],[RuleContent],[Description]) 
 VALUES 
 (#PermissionRuleId#,#PermissionId#,#RuleName#,#Priority#,#RuleContent#,#Description#)', N'UPDATE SYS_PermissionRule SET [PermissionRuleId]=#PermissionRuleId#
{?? ,[PermissionId]=#PermissionId# }
{?? ,[RuleName]=#RuleName# }
{?? ,[Priority]=#Priority# }
{?? ,[RuleContent]=#RuleContent# }
{?? ,[Description]=#Description# }
 WHERE [PermissionRuleId]=#PermissionRuleId#', N'DELETE FROM [SYS_PermissionRule]  WHERE PermissionRuleId IN ($PermissionRuleId$)', N'SELECT [PermissionRuleId],
[PermissionId],
[RuleName],
[Priority],
[RuleContent],
[Description] 
 FROM SYS_PermissionRule WITH(NOLOCK) WHERE [PermissionRuleId]=#PermissionRuleId#', N'SELECT [PermissionRuleId],
[PermissionId],
[RuleName],
[Priority],
[RuleContent],
[Description] 
 FROM [SYS_PermissionRule] WITH(NOLOCK) WHERE 1=1 
{? AND [PermissionRuleId] like ''%$PermissionRuleId$%'' } 
{? AND [PermissionId] like ''%$PermissionId$%'' } 
{? AND [RuleName] like ''%$RuleName$%'' } 
{? AND [Priority]=#Priority# } 
{? AND [RuleContent] like ''%$RuleContent$%'' } 
{? AND [Description] like ''%$Description$%'' }', N'[PermissionRuleId] ASC', CAST(0x0000A5E50121E4B6 AS DateTime))
INSERT [dbo].[APP_CRUD] ([CRUDId], [CRUDName], [PKName], [TableName], [TitleFieldName], [ValueFieldName], [ParentFieldName], [InsertSQL], [UpdateSQL], [DeleteSQL], [GetOneSQL], [SelectSQL], [DefaultSortExpression], [LastUpdateTime]) VALUES (N'b8c9e320-1370-46a7-b1da-123bf3357f30', N'AllCity', N'Id', N'AllCity', N'CityName', N'Id', NULL, N'INSERT INTO AllCity 
([Id],[CityCode],[CityName],[ProvinceId],[CityJC]) 
 VALUES 
 (#Id#,#CityCode#,#CityName#,#ProvinceId#,#CityJC#)', N'UPDATE AllCity SET [Id]=#Id#
{?? ,[CityCode]=#CityCode# }
{?? ,[CityName]=#CityName# }
{?? ,[ProvinceId]=#ProvinceId# }
{?? ,[CityJC]=#CityJC# }
 WHERE [Id]=#Id#', N'DELETE FROM [AllCity]  WHERE Id IN ($Id$)', N'SELECT [Id],
[CityCode],
[CityName],
[ProvinceId],
[CityJC] 
 FROM AllCity WITH(NOLOCK) WHERE [Id]=#Id#', N'SELECT [Id],
[CityCode],
[CityName],
[ProvinceId],
[CityJC] 
 FROM [AllCity] WITH(NOLOCK) WHERE 1=1 
{? AND [Id]=#Id# } 
{? AND [CityCode]=#CityCode# } 
{? AND [CityName] like ''%$CityName$%'' } 
{? AND [ProvinceId]=#ProvinceId# } 
{? AND [CityJC] like ''%$CityJC$%'' }', N'[Id] ASC', CAST(0x0000A5DF00BEDB3C AS DateTime))
/****** Object:  Table [dbo].[APP_Component]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_Component](
	[ComponentId] [varchar](38) NOT NULL,
	[ComponentName] [varchar](300) NULL,
	[ParentId] [varchar](38) NULL,
	[PageId] [varchar](38) NULL,
	[ComponentType] [varchar](300) NULL,
	[ComponentXML] [text] NULL,
	[Rank] [int] NULL,
	[TargetId] [varchar](50) NULL,
	[ComponentDataType] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_APP_Compoment] PRIMARY KEY CLUSTERED 
(
	[ComponentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'02c15296-e743-4db3-b438-4bc4cb068963', N'btnSearchSubmit', N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSearchSubmit","Name":null,"Label":"查询","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary search-button","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDDCB AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'05e8aaec-5650-4804-a86e-132847ee00dd', N'columnPermissionId', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnPermissionId","CssClass":null,"Visiable":true,"Rank":6,"FieldName":"PermissionId","ColumnName":"PermissionId","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 6, N'Columns', N'Control', CAST(0x0000A5E50121E53A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'AlarmPublishSearch', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Search', N'{"TargetId":"AlarmPublishTable","SearchFormTemplate":null,"FormCssClass":"form-inline","ID":"AlarmPublishSearch","Rank":1,"CssClass":"search","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 1, N'Right', N'Component', CAST(0x0000A5DF00C02740 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'0bdc25b8-68b2-411c-b393-19c25e8899bd', N'Description', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Description","Name":"Description","Label":"Description","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":6,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 6, N'Fields', N'Control', CAST(0x0000A5E50121E76B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'0c0cef0c-38f7-4112-beca-f23b7e4d37dd', N'columnAlarmLevel', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmLevel","CssClass":null,"Visiable":true,"Rank":9,"FieldName":"AlarmLevel","ColumnName":"AlarmLevel","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 9, N'Columns', N'Control', CAST(0x0000A5DF00C02538 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'0d479b6d-cc15-43b0-b7a3-4b4f2cdc7b67', N'PermissionRuleId', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"PermissionRuleId","Name":"PermissionRuleId","Label":"PermissionRuleId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5E50121E64E AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'0f01db47-ffaa-4c13-97a6-5978ef722596', N'columnPermissionRuleId', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnPermissionRuleId","CssClass":null,"Visiable":true,"Rank":3,"FieldName":"PermissionRuleId","ColumnName":"PermissionRuleId","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":true}', 3, N'Columns', N'Control', CAST(0x0000A5E50121E522 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'13e36bdc-6629-4e80-a4c8-1ce2073e051d', N'Id', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Id","Name":"Id","Label":"Id","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5DF00BEDE75 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'146fbb42-37c2-458d-a8fa-58ffed42311c', N'dddd', N'98bcdf3c-f8d4-49d9-a498-56cc37137b99', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Business.DataFormatColumnConverter', N'{"DataFormatString":"{0:yyyy-MM-dd}","DataFormatFields":"","ColumnName":"LastUpdateTime","AppendColumnName":"","Rank":0,"ConverterType":1,"ID":"dddd"}', 0, N'ColumnConverters', N'DataSource', CAST(0x0000A5E601048C09 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'157b90d5-57fb-4727-af47-37d48b451d77', N'btnSearchSubmit', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSearchSubmit","Name":null,"Label":"查询","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary search-button","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E6C3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'171ca04f-5c58-485f-af1e-77386685e348', N'columnFileName', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnFileName","CssClass":null,"Visiable":true,"Rank":36,"FieldName":"FileName","ColumnName":"FileName","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 36, N'Columns', N'Control', CAST(0x0000A5DF00C02648 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'19289d95-95fd-4781-b82d-5f5cf768aa19', N'ProvinceAJC', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceAJC","Name":"ProvinceAJC","Label":"ProvinceAJC","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5D400ECD8E1 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'19ddc420-7146-4610-93d3-bb64a173f651', N'RuleContent', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RuleContent","Name":"RuleContent","Label":"RuleContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5E50121E74F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'1d8794de-7917-4224-9f74-2e4df701d57f', N'AllCityToolbar', N'c378640e-5338-4fda-a603-4bd387d40b13', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Toolbar', N'{"TargetId":"AllCityTable","PageStartUpScript":"","PageScriptBlock":"\r\n\r\n","ID":"AllCityToolbar","Rank":2,"CssClass":"toolbar","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 2, N'Right', N'Component', CAST(0x0000A5DF00BEDCC1 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'1f7447bc-bdf7-4f9f-a4e7-221c7ee45d22', N'columnAlarmContent', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmContent","CssClass":null,"Visiable":true,"Rank":21,"FieldName":"AlarmContent","ColumnName":"AlarmContent","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 21, N'Columns', N'Control', CAST(0x0000A5DF00C025A6 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'217f4b38-096e-4cac-b8fa-7edc762f337a', N'AlarmLeader', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmLeader","Name":"AlarmLeader","Label":"AlarmLeader","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5DF00E63CC0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'22b38ed0-530b-42e9-9b4a-94b5b213a4eb', N'FilePath', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"FilePath","Name":"FilePath","Label":"FilePath","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":13,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 13, N'Fields', N'Control', CAST(0x0000A5DF00C028B5 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'24ab1c1e-7bec-4c67-84ee-88f52eb94986', N'RelieveContent', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RelieveContent","Name":"RelieveContent","Label":"RelieveContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":9,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 9, N'Fields', N'Control', CAST(0x0000A5DF00E63D06 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'25676343-90a8-4b42-90f4-f171433323cc', N'Status', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.DropDownList', N'{"RelateControlID":null,"AppendEmptyOption":true,"EmptyOptionText":"==请选择==","ID":"Status","Name":"Status","Label":"Status","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":10,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 10, N'Fields', N'Control', CAST(0x0000A5DF00E63D1A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'2579b746-c2a9-4861-85bb-f551980d4a5d', N'btnSearchSubmit', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSearchSubmit","Name":null,"Label":"查询","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary search-button","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00C028CF AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'266c8521-aadc-4fc7-bba5-05ba286dc391', N'CityName', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityName","Name":"CityName","Label":"CityName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5DF00BEDEAF AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'26f8b7ec-fa77-4c59-9e70-c119c997a7db', N'columnStatus', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnStatus","CssClass":null,"Visiable":true,"Rank":30,"FieldName":"Status","ColumnName":"Status","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 30, N'Columns', N'Control', CAST(0x0000A5DF00C025F5 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'27582db4-f46c-4ed6-9994-c498cc8f7aeb', N'btnInsert', N'a7c0b9ae-e952-4d51-8914-ecf0272bef9d', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnInsert","Name":null,"Label":"新增","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00C0270C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'2c03c804-31fd-4cdc-b0b2-106c35998c53', N'UpdateTime', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"UpdateTime","Name":"UpdateTime","Label":"UpdateTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":11,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 11, N'Fields', N'Control', CAST(0x0000A5DF00C02881 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'AlarmPublishForm', N'382971ce-3157-436e-bde2-d7651383fe6a', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.Form', N'{"FormMode":0,"Data":null,"AppendQueryString":true,"PostUrl":"/CRUD/Save","FormTemplate":null,"FormCssClass":"form form-horizontal","ID":"AlarmPublishForm","Rank":0,"CssClass":null,"AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Right', N'Component', CAST(0x0000A5DF00E63C6A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'2fc48e58-83fc-4ce5-a81e-4387b30acea2', N'Id', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Id","Name":"Id","Label":"Id","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5DF00E63C7F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'2fe32ebb-6128-4906-889d-1f0d945023b6', N'columnCityJC', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnCityJC","CssClass":null,"Visiable":true,"Rank":26,"FieldName":"CityJC","ColumnName":"CityJC","ShowLength":0,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":false,"IsKey":false}', 26, N'Columns', N'Control', CAST(0x0000A5E000E49254 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3171c126-a99a-4aec-aef4-50acdcaefe33', N'AllCityTablePager', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Pager', N'{"PageSize":10,"RecordCount":0,"CurrentPageIndex":1,"PrePageSize":"10,20,50,100","DisplayIndexCount":10,"TargetId":"AllCityTable","PageCount":0,"ID":"AllCityTablePager","Rank":0,"CssClass":"data-pager","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Pager', N'Component', CAST(0x0000A5DF00BEDC86 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'32e30349-2f19-4a8d-ad56-03df121ad5c5', N'columnAlarmStartTime', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmStartTime","CssClass":null,"Visiable":true,"Rank":18,"FieldName":"AlarmStartTime","ColumnName":"AlarmStartTime","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 18, N'Columns', N'Control', CAST(0x0000A5DF00C0258A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3386b8e9-2e13-4591-b0b6-45c2a4fe7bfe', N'StationCode', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"StationCode","Name":"StationCode","Label":"StationCode","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5D400ECD88B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'33ae2234-05aa-4e6e-8a8f-b97fe4503ffc', N'btnSubmit', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSubmit","Name":null,"Label":"保存","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-success btn-big","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BF3C2E AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3597f7e3-7eba-4b7b-8cbc-4c0658c8a3ea', N'ProvinceJC', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceJC","Name":"ProvinceJC","Label":"ProvinceJC","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5DF00BF3C15 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'39ec8328-c177-4664-95e6-5bfbc1aa4ab6', N'btnSubmit', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSubmit","Name":null,"Label":"保存","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-success btn-big","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDF08 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3b031c41-49c6-453f-813a-ea800dedce20', N'AlarmPerson', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmPerson","Name":"AlarmPerson","Label":"AlarmPerson","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5DF00E63CAF AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3d27da1e-5b58-4a5b-adb1-e58f627f7396', N'AlarmPublishTablePager', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Pager', N'{"PageSize":10,"RecordCount":0,"CurrentPageIndex":1,"PrePageSize":"10,20,50,100","DisplayIndexCount":10,"TargetId":"AlarmPublishTable","PageCount":0,"ID":"AlarmPublishTablePager","Rank":0,"CssClass":"data-pager","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Pager', N'Component', CAST(0x0000A5DF00C026B7 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3df8bc8a-0655-4cbf-9ba1-970a57cefa72', N'AlarmPublishTableDs', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"AlarmPublish","Parameter":null,"SortExpression":null,"ID":"AlarmPublishTableDs","KeyFieldName":null}', 0, N'DataSource', N'DataSource', CAST(0x0000A5DF00C026D5 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3e9722aa-e804-4e46-9ea9-888139bfe040', N'columnAlarmLeader', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmLeader","CssClass":null,"Visiable":true,"Rank":15,"FieldName":"AlarmLeader","ColumnName":"AlarmLeader","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 15, N'Columns', N'Control', CAST(0x0000A5DF00C02570 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'3f43229c-a573-4b5e-8d27-2be6527b8152', N'UpdateTime', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"UpdateTime","Name":"UpdateTime","Label":"UpdateTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":11,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 11, N'Fields', N'Control', CAST(0x0000A5DF00E63D2B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'40d7d0b8-efcb-41be-b99e-af9989cf66cb', N'ccc', N'98bcdf3c-f8d4-49d9-a498-56cc37137b99', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Business.DataFormatColumnConverter', N'{"DataFormatString":"{0}-{1}","DataFormatFields":"PageName,LayoutId","ColumnName":"PageName,PageTitle","AppendColumnName":"","Rank":0,"ConverterType":1,"ID":"ccc"}', 0, N'ColumnConverters', N'DataSource', CAST(0x0000A5E601059223 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'41b35d85-b7d5-4cc8-8e12-4ea89172be6b', N'FileName', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"FileName","Name":"FileName","Label":"FileName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":12,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 12, N'Fields', N'Control', CAST(0x0000A5DF00C0289C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'4272b939-9056-493b-8ef5-cce4e7304422', N'FilePath', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"FilePath","Name":"FilePath","Label":"FilePath","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":13,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 13, N'Fields', N'Control', CAST(0x0000A5DF00E63D4C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'48a9ece4-475a-4e88-8856-4f9e3b6d0756', N'btnSubmit', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSubmit","Name":null,"Label":"保存","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-success btn-big","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00E63C34 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'4d4fe8b0-bdfe-40f0-9edf-6e78c29a0280', N'Id', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Id","Name":"Id","Label":"Id","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5DF00BF3BCB AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'5427d8a0-414c-4414-9658-567ec89a3acd', N'columnDescription', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnDescription","CssClass":null,"Visiable":true,"Rank":18,"FieldName":"Description","ColumnName":"Description","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 18, N'Columns', N'Control', CAST(0x0000A5E50121E58E AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'54df3b6e-4177-4426-9b27-b90fb03f865d', N'CityJC', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityJC","Name":"CityJC","Label":"CityJC","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5DF00BEDEEA AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'54fca9b2-d058-43ad-bb52-2e21166ae91a', N'AlarmLevel', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmLevel","Name":"AlarmLevel","Label":"AlarmLevel","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5DF00E63C9F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'5669ada4-33c8-40be-9d28-3c0546540d21', N'NMCCityFormDs', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"NMCCity","Parameter":null,"SortExpression":null,"ID":"NMCCityFormDs","KeyFieldName":"StationCode"}', 0, N'FormService', N'DataSource', CAST(0x0000A5D400ECD90F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'5bdc055d-0b0e-4aaf-ba38-5b8b606a04aa', N'columnCityCode', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnCityCode","CssClass":null,"Visiable":true,"Rank":11,"FieldName":"CityCode","ColumnName":"CityCode","ShowLength":0,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":false,"IsKey":false}', 11, N'Columns', N'Control', CAST(0x0000A5E000E4920B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'5ee15240-af5d-48f1-9e73-e72fe012781a', N'columnRuleName', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnRuleName","CssClass":null,"Visiable":true,"Rank":9,"FieldName":"RuleName","ColumnName":"RuleName","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 9, N'Columns', N'Control', CAST(0x0000A5E50121E54F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'634c272e-8fbe-4a6a-b7a9-ca8388da2245', N'columnRelieveContent', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnRelieveContent","CssClass":null,"Visiable":true,"Rank":27,"FieldName":"RelieveContent","ColumnName":"RelieveContent","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 27, N'Columns', N'Control', CAST(0x0000A5DF00C025DA AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'644ad47f-27e0-412b-b038-ca8ef480a83d', N'SYSPermissionRuleTableDs', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"SYSPermissionRule","Parameter":null,"SortExpression":null,"ID":"SYSPermissionRuleTableDs","KeyFieldName":"PermissionRuleId"}', 0, N'DataSource', N'DataSource', CAST(0x0000A5E50121E5E2 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'67dab101-5b48-4055-ae4f-da262da56205', N'RuleName', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RuleName","Name":"RuleName","Label":"RuleName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5E50121E72A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'6a7bfa0d-0e15-44ec-a733-de32fb82fb57', N'CityName', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityName","Name":"CityName","Label":"CityName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5D400ECD8B6 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'6ed28809-a54f-4878-bd8e-041c34f73e9e', N'Id', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Id","Name":"Id","Label":"Id","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5DF00C0275A AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7071c720-c322-4555-9a61-66f9bd5bbb68', N'AlarmPerson', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmPerson","Name":"AlarmPerson","Label":"AlarmPerson","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5DF00C027A8 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7280509a-c99e-4c20-8c8d-b5a16ce35298', N'AlarmPublishTime', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmPublishTime","Name":"AlarmPublishTime","Label":"AlarmPublishTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5DF00C02774 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'73f42e6e-f2bd-4e55-aa00-5ea52fc7abd6', N'btnDelete', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableButton', N'{"ID":"btnDelete","Name":"删除","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDC69 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'783d5b99-0178-4c8f-806f-5e4def80a050', N'AlarmStartTime', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmStartTime","Name":"AlarmStartTime","Label":"AlarmStartTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":6,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 6, N'Fields', N'Control', CAST(0x0000A5DF00C027E0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'790551b0-62a4-4bc9-8734-4fa7557b6527', N'btnBatchDelete', N'a7c0b9ae-e952-4d51-8914-ecf0272bef9d', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnBatchDelete","Name":null,"Label":"批量删除","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-default","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00C02726 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7c06817f-9aee-4b8a-903f-5892c0e3528e', N'columnId', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnId","CssClass":null,"Visiable":true,"Rank":3,"FieldName":"Id","ColumnName":"Id","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 3, N'Columns', N'Control', CAST(0x0000A5DF00C024FF AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7c3ab85b-0b03-495c-95c1-fdbc5fa74310', N'AlarmPublishTime', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmPublishTime","Name":"AlarmPublishTime","Label":"AlarmPublishTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5DF00E63C90 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7d2d14ac-7eb0-4fc3-bfad-e71d95560b27', N'columnAlarmEndTime', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmEndTime","CssClass":null,"Visiable":true,"Rank":24,"FieldName":"AlarmEndTime","ColumnName":"AlarmEndTime","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 24, N'Columns', N'Control', CAST(0x0000A5DF00C025C0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7ef9f544-2692-4e4b-82dc-4d3aeb2e1254', N'PermissionId', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"PermissionId","Name":"PermissionId","Label":"PermissionId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5E50121E717 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'7fa963ba-531f-48c9-872c-f23dfb769a3e', N'columnRuleContent', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnRuleContent","CssClass":null,"Visiable":true,"Rank":15,"FieldName":"RuleContent","ColumnName":"RuleContent","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 15, N'Columns', N'Control', CAST(0x0000A5E50121E57C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'819cba12-3857-4122-a1b0-a8168808b950', N'CityCode', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityCode","Name":"CityCode","Label":"CityCode","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5D400ECD8A0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'86fd5e98-3fee-471f-890f-b35e14710cc9', N'AllCityTableDs', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"AllCity","Parameter":null,"SortExpression":null,"ID":"AllCityTableDs","KeyFieldName":"Id"}', 0, N'DataSource', N'DataSource', CAST(0x0000A5DF00BEDCA2 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'SYSPermissionRuleForm', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.Form', N'{"FormMode":0,"Data":null,"AppendQueryString":true,"PostUrl":"/CRUD/Save","FormTemplate":null,"FormCssClass":"form form-horizontal","ID":"SYSPermissionRuleForm","Rank":0,"CssClass":null,"AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Right', N'Component', CAST(0x0000A5E50121E6E4 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'8f464a23-e010-4bfe-937c-303a5ea5cba4', N'PermissionRuleId', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"PermissionRuleId","Name":"PermissionRuleId","Label":"PermissionRuleId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5E50121E6FB AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'8f482580-8e72-42a1-bec9-7dadf034244e', N'AllProvinceListTable', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Controls.Table', N'{"AutoGenerateColum":true,"ShowCheckBox":false,"ShowIndex":false,"KeyFieldName":"","GroupByField":"","InsertUrl":"","EditUrl":"","DeleteUrl":"","TableCssClass":"table table-bordered table-striped table-hover","AllowPaging":true,"PagerPosition":1,"ID":"AllProvinceListTable","Rank":3,"CssClass":"data-list","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":""}', 3, N'Right', N'Component', CAST(0x0000A5E601125155 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'911f3295-a1dc-4003-9ba2-27ecd14d0164', N'AlarmEndTime', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmEndTime","Name":"AlarmEndTime","Label":"AlarmEndTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":8,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 8, N'Fields', N'Control', CAST(0x0000A5DF00E63CF3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'91e469f9-0bdf-49df-9100-f777ed02f261', N'btnEdit', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableButton', N'{"ID":"btnEdit","Name":"编辑","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00C02680 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'93a8b6f9-dba6-4410-9d08-5efcea576856', N'Description', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Description","Name":"Description","Label":"Description","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":6,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 6, N'Fields', N'Control', CAST(0x0000A5E50121E6B0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'98bcdf3c-f8d4-49d9-a498-56cc37137b99', N'AllProvinceListTableSqlIdDataSource', N'8f482580-8e72-42a1-bec9-7dadf034244e', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Business.SqlIdDataSource', N'{"SqlId":"testPage","Parameter":null,"SortExpression":"PageId","DataFilters":[],"ColumnConverters":[],"ID":"AllProvinceListTableSqlIdDataSource"}', 0, N'DataSource', N'DataSource', CAST(0x0000A5E601044FF7 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'9cfbb4f0-bfa7-450c-9992-b8d97dab6a0c', N'columnPriority', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnPriority","CssClass":null,"Visiable":true,"Rank":12,"FieldName":"Priority","ColumnName":"Priority","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 12, N'Columns', N'Control', CAST(0x0000A5E50121E564 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'9e72f1c1-9243-4cac-917a-7ab512704bde', N'columnProvinceId', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnProvinceId","CssClass":null,"Visiable":true,"Rank":21,"FieldName":"ProvinceId","ColumnName":"ProvinceId","ShowLength":0,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":false,"IsKey":false}', 21, N'Columns', N'Control', CAST(0x0000A5E000E4923C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a316acbf-a340-44d7-8b8a-7b06cbef5d6b', NULL, N'26f8b7ec-fa77-4c59-9e70-c119c997a7db', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Business.DictionaryDataSource', N'{"DictionaryCode":"YesOrNo","ID":null}', 0, N'ColumnConverter', N'DataSource', CAST(0x0000A5DF00C0260F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a31f4a6e-737f-4e9b-b26b-97755d2eb541', N'SYSPermissionRuleToolbar', N'cf88193b-85dd-415c-91db-41732cf8072b', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Toolbar', N'{"TargetId":"SYSPermissionRuleTable","PageStartUpScript":"","PageScriptBlock":"\r\n\r\n","ID":"SYSPermissionRuleToolbar","Rank":2,"CssClass":"toolbar","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 2, N'Right', N'Component', CAST(0x0000A5E50121E5FA AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'AlarmPublishTable', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Table', N'{"AutoGenerateColum":false,"ShowCheckBox":false,"ShowIndex":false,"KeyFieldName":null,"GroupByField":null,"InsertUrl":"/Page/AlarmPublishEdit","EditUrl":"/Page/AlarmPublishEdit","DeleteUrl":"/CRUD/Delete","TableCssClass":"table table-bordered table-striped table-hover","AllowPaging":true,"PagerPosition":1,"ID":"AlarmPublishTable","Rank":3,"CssClass":"data-list","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 3, N'Right', N'Component', CAST(0x0000A5DF00C024E0 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a66ee0ae-515e-417d-b0df-8e7903bbd322', N'btnBatchDelete', N'1d8794de-7917-4224-9f74-2e4df701d57f', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnBatchDelete","Name":null,"Label":"批量删除","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-default","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDCFB AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a6712b2a-b855-4ae5-803a-010deedc1565', N'btnSubmit', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSubmit","Name":null,"Label":"保存","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-success btn-big","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5D400ECD8F8 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a7c0b9ae-e952-4d51-8914-ecf0272bef9d', N'AlarmPublishToolbar', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.Toolbar', N'{"TargetId":"AlarmPublishTable","PageStartUpScript":"","PageScriptBlock":"\r\n\r\n","ID":"AlarmPublishToolbar","Rank":2,"CssClass":"toolbar","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 2, N'Right', N'Component', CAST(0x0000A5DF00C026F1 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'a80c7184-2889-40f0-b005-40ecc8901835', N'CityCode', N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityCode","Name":"CityCode","Label":"CityCode","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5DF00BEDD55 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ae663ad7-b5ce-44f8-ab60-a830bdaffb0c', N'columnAlarmPublishTime', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmPublishTime","CssClass":null,"Visiable":true,"Rank":6,"FieldName":"AlarmPublishTime","ColumnName":"AlarmPublishTime","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 6, N'Columns', N'Control', CAST(0x0000A5DF00C0251C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b1f9b883-3151-4030-ad3f-371d53c095bf', N'RelieveContent', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RelieveContent","Name":"RelieveContent","Label":"RelieveContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":9,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 9, N'Fields', N'Control', CAST(0x0000A5DF00C0282F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b2531e69-e0db-4eb7-aa18-99fc7024d362', N'AlarmContent', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmContent","Name":"AlarmContent","Label":"AlarmContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":7,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 7, N'Fields', N'Control', CAST(0x0000A5DF00E63CE3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'SYSPermissionRuleTable', N'cf88193b-85dd-415c-91db-41732cf8072b', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Table', N'{"AutoGenerateColum":false,"ShowCheckBox":false,"ShowIndex":false,"KeyFieldName":"PermissionRuleId","GroupByField":null,"InsertUrl":"/Page/SYSPermissionRuleEdit","EditUrl":"/Page/SYSPermissionRuleEdit","DeleteUrl":"/CRUD/Delete","TableCssClass":"table table-bordered table-striped table-hover","AllowPaging":true,"PagerPosition":1,"ID":"SYSPermissionRuleTable","Rank":3,"CssClass":"data-list","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 3, N'Right', N'Component', CAST(0x0000A5E50121E505 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b2e66a7c-9eae-48dc-a83e-a54a71e54ee2', NULL, N'25676343-90a8-4b42-90f4-f171433323cc', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Business.DictionaryDataSource', N'{"DictionaryCode":"YesOrNo","ID":null}', 0, N'DataSource', N'DataSource', CAST(0x0000A5DF00E63C48 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b32b50a8-c3e1-4229-994c-550e183e5990', N'columnUpdateTime', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnUpdateTime","CssClass":null,"Visiable":true,"Rank":33,"FieldName":"UpdateTime","ColumnName":"UpdateTime","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 33, N'Columns', N'Control', CAST(0x0000A5DF00C0262C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b386732b-ec8e-4e63-89d1-02711afeab18', N'SYSPermissionRuleFormDs', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"SYSPermissionRule","Parameter":null,"SortExpression":null,"ID":"SYSPermissionRuleFormDs","KeyFieldName":"PermissionRuleId"}', 0, N'FormService', N'DataSource', CAST(0x0000A5E50121E795 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b5e1ca23-da22-40ab-89fb-1a2f33295746', N'btnDelete', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableButton', N'{"ID":"btnDelete","Name":"删除","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E5B8 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b64eb1e2-b83c-4be4-a433-03570fd79e77', N'SYSPermissionRuleTablePager', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Pager', N'{"PageSize":10,"RecordCount":0,"CurrentPageIndex":1,"PrePageSize":"10,20,50,100","DisplayIndexCount":10,"TargetId":"SYSPermissionRuleTable","PageCount":0,"ID":"SYSPermissionRuleTablePager","Rank":0,"CssClass":"data-pager","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Pager', N'Component', CAST(0x0000A5E50121E5CD AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'AllCitySearch', N'c378640e-5338-4fda-a603-4bd387d40b13', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Search', N'{"TargetId":"AllCityTable","SearchFormTemplate":"","FormCssClass":"form-inline","ID":"AllCitySearch","Rank":1,"CssClass":"search pd10","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":""}', 1, N'Right', N'Component', CAST(0x0000A5DF00C092A3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b8a21afa-4e2c-41f3-9781-68250ee9c9e5', N'btnEdit', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableButton', N'{"ID":"btnEdit","Name":"编辑","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDC4B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'b9f2a5ef-2678-4054-af73-c18921360d2c', N'AllProvinceListIFrame', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Controls.IFrame', N'{"Src":"","Height":"99%","Width":"100%","Scrolling":false,"ID":"AllProvinceListIFrame","Rank":3,"CssClass":"","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":""}', 3, N'', N'Component', CAST(0x0000A5E601124739 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'bb600932-e190-4a98-b4e9-a1aed97e8f2b', N'FileName', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"FileName","Name":"FileName","Label":"FileName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":12,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 12, N'Fields', N'Control', CAST(0x0000A5DF00E63D3C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'bc60a5a1-db43-4b58-8f5f-49b36de74e43', N'RuleName', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RuleName","Name":"RuleName","Label":"RuleName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5E50121E678 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'bd64e0f8-45e1-4df9-8bb4-78d3469dd335', N'AlarmLevel', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmLevel","Name":"AlarmLevel","Label":"AlarmLevel","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5DF00C0278E AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'bf460dd7-c7f8-47ae-996e-3936c71b785a', N'AlarmPublishFormDs', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"AlarmPublish","Parameter":null,"SortExpression":null,"ID":"AlarmPublishFormDs","KeyFieldName":null}', 0, N'FormService', N'DataSource', CAST(0x0000A5DF00E63C59 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c02d09ba-967e-40fb-b469-c798be5889ac', N'btnInsert', N'1d8794de-7917-4224-9f74-2e4df701d57f', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnInsert","Name":null,"Label":"新增","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00BEDCDE AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c4776b88-9a01-4daf-8e4f-5820a1934e2b', N'ProvinceCode', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceCode","Name":"ProvinceCode","Label":"ProvinceCode","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5DF00BF3BE5 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c47fc141-dbe1-4ef6-b120-c7777f49f127', N'Status', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.DropDownList', N'{"RelateControlID":null,"AppendEmptyOption":true,"EmptyOptionText":"==请选择==","ID":"Status","Name":"Status","Label":"Status","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":10,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 10, N'Fields', N'Control', CAST(0x0000A5DF00C0284C AS DateTime))
GO
print 'Processed 100 total records'
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'AllCityTable', N'c378640e-5338-4fda-a603-4bd387d40b13', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.Table', N'{"AutoGenerateColum":false,"ShowCheckBox":false,"ShowIndex":false,"KeyFieldName":"Id","GroupByField":null,"InsertUrl":"/Page/AllCityEdit","EditUrl":"/Page/AllCityEdit","DeleteUrl":"/CRUD/Delete","TableCssClass":"table table-bordered table-striped table-hover","AllowPaging":true,"PagerPosition":1,"ID":"AllCityTable","Rank":3,"CssClass":"data-list","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 3, N'Right', N'Component', CAST(0x0000A5DF00BEDB77 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c4f35372-d812-4763-9afb-9ec93be96243', N'AlarmLeader', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmLeader","Name":"AlarmLeader","Label":"AlarmLeader","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5DF00C027C3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c817d2de-d706-44aa-9377-901e4237d700', N'AllProvinceForm', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.Form', N'{"FormMode":0,"Data":null,"AppendQueryString":true,"PostUrl":"/CRUD/Save","FormTemplate":null,"FormCssClass":"form form-horizontal","ID":"AllProvinceForm","Rank":0,"CssClass":null,"AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Right', N'Component', CAST(0x0000A5DF00BF3BAF AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'c8dfb572-0175-4566-9cc0-3d14fc1d1c88', N'AlarmContent', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmContent","Name":"AlarmContent","Label":"AlarmContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":7,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 7, N'Fields', N'Control', CAST(0x0000A5DF00C027FA AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'AllCityForm', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.Form', N'{"FormMode":0,"Data":null,"AppendQueryString":true,"PostUrl":"/CRUD/Save","FormTemplate":null,"FormCssClass":"form form-horizontal","ID":"AllCityForm","Rank":0,"CssClass":null,"AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Right', N'Component', CAST(0x0000A5DF00BEDE4F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'cb71745a-3366-40b5-9d0b-1b3e596dc246', N'columnAlarmPerson', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnAlarmPerson","CssClass":null,"Visiable":true,"Rank":12,"FieldName":"AlarmPerson","ColumnName":"AlarmPerson","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 12, N'Columns', N'Control', CAST(0x0000A5DF00C02554 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'd0747dc5-8c2f-4195-8438-16e8db796ff2', N'ProvinceId', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceId","Name":"ProvinceId","Label":"ProvinceId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5DF00BEDECC AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'd38535d2-de57-42d5-92dd-fa6cdab9ca03', N'btnInsert', N'a31f4a6e-737f-4e9b-b26b-97755d2eb541', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnInsert","Name":null,"Label":"新增","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-primary","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E60F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'd825e12b-540b-43ba-80f4-de72a5ec4e9e', N'btnSubmit', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.Button', N'{"ButtonType":1,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnSubmit","Name":null,"Label":"保存","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-success btn-big","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E783 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'SYSPermissionRuleSearch', N'cf88193b-85dd-415c-91db-41732cf8072b', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Search', N'{"TargetId":"SYSPermissionRuleTable","FormLayoutType":0,"SearchFormTemplate":null,"FormCssClass":"form-inline","ID":"SYSPermissionRuleSearch","Rank":1,"CssClass":"search pt10 pl10","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 1, N'Right', N'Component', CAST(0x0000A5E50121E63B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'd96b5410-a5f3-4268-856a-e943a048d143', N'', N'98bcdf3c-f8d4-49d9-a498-56cc37137b99', N'6e0a16f6-2c2f-4ddc-9743-060b61922184', N'SummerFresh.Business.DataFormatColumnConverter', N'{"DataFormatString":"","DataFormatFields":"","ColumnName":"PageStyle,ParentId","AppendColumnName":"","Rank":0,"ConverterType":3,"ID":""}', 0, N'ColumnConverters', N'DataSource', CAST(0x0000A5E60105E705 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'daad9b56-54fb-437b-8dc5-12e2b66757da', N'RuleContent', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"RuleContent","Name":"RuleContent","Label":"RuleContent","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":5,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 5, N'Fields', N'Control', CAST(0x0000A5E50121E69D AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'dad925b0-4b76-49fe-882f-cbf989d808cc', N'columnCityName', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnCityName","CssClass":null,"Visiable":true,"Rank":16,"FieldName":"CityName","ColumnName":"CityName","ShowLength":0,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":false,"IsKey":false}', 16, N'Columns', N'Control', CAST(0x0000A5E000E49224 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'dc510f67-0591-47d5-9faa-d2c2699eed45', N'AllCityFormDs', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"AllCity","Parameter":null,"SortExpression":null,"ID":"AllCityFormDs","KeyFieldName":"Id"}', 0, N'FormService', N'DataSource', CAST(0x0000A5DF00BEDF26 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'dd7ead7d-d661-47ea-a2f7-76d418579243', N'AlarmEndTime', N'08e0f322-0b5a-4e1d-8c3e-c8952fc6a275', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmEndTime","Name":"AlarmEndTime","Label":"AlarmEndTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":8,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 8, N'Fields', N'Control', CAST(0x0000A5DF00C02815 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'dda87ab1-a84f-4884-b6b5-f88c5801010c', N'btnDelete', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableButton', N'{"ID":"btnDelete","Name":"删除","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5DF00C0269C AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e2205f61-7e32-4964-bb4f-b9216fc99333', N'PermissionId', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"PermissionId","Name":"PermissionId","Label":"PermissionId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5E50121E661 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e25d8aa4-b846-4ba5-b1ad-235ecb82c4f0', N'ProvinceId', N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceId","Name":"ProvinceId","Label":"ProvinceId","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5DF00BEDD8F AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e2dcd66b-a371-49db-99a9-1cf497c7a411', N'Id', N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Id","Name":"Id","Label":"Id","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":1,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 1, N'Fields', N'Control', CAST(0x0000A5DF00BEDD39 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e3bd9ebb-2903-45a4-9377-b153078eb2d3', NULL, N'c47fc141-dbe1-4ef6-b120-c7777f49f127', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Business.DictionaryDataSource', N'{"DictionaryCode":"YesOrNo","ID":null}', 0, N'DataSource', N'DataSource', CAST(0x0000A5DF00C02867 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'NMCCityForm', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.Form', N'{"FormMode":0,"Data":null,"PostUrl":"/CRUD/Save","FormTemplate":null,"ID":"NMCCityForm","Rank":0,"CssClass":"form","AutoHeight":false,"AutoHeightOffset":0,"Visiable":true,"AttributeString":null}', 0, N'Right', N'Component', CAST(0x0000A5D400ECD875 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e4bccc86-1361-46ff-9154-240f1b2b15fd', N'Priority', N'd8391439-8fc2-4e5b-8bf7-5c21542f6480', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Priority","Name":"Priority","Label":"Priority","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5E50121E68B AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'e4be106d-b2c0-4520-9b7d-28b15d10f9a5', N'CityNamePY', N'e3cdba51-dd13-43f0-b429-7cb14e7f2309', N'7ea4d3d1-5275-45a0-8d4f-4a6cc777eb2d', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityNamePY","Name":"CityNamePY","Label":"CityNamePY","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5D400ECD8CB AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ea9828c4-83db-4e98-b14b-3f54072b1908', N'btnBatchDelete', N'a31f4a6e-737f-4e9b-b26b-97755d2eb541', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.Button', N'{"ButtonType":0,"OnClick":null,"PageStartUpScript":"","PageScriptBlock":"","ID":"btnBatchDelete","Name":null,"Label":"批量删除","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":0,"AttributeString":null,"CssClass":"btn btn-default","Validator":null,"Value":null,"Visiable":true}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E621 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ecd8afe0-d5dc-4aa5-9231-96b4f2630bba', N'AlarmStartTime', N'2ce1bba6-b01a-4f75-9d9a-93bfc33b2b0f', N'382971ce-3157-436e-bde2-d7651383fe6a', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"AlarmStartTime","Name":"AlarmStartTime","Label":"AlarmStartTime","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":6,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 6, N'Fields', N'Control', CAST(0x0000A5DF00E63CD4 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'edc79674-9777-416a-adf6-ead7f63ee9cd', N'columnId', N'c4eaf7e6-0203-47cd-a923-110f468d425e', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnId","CssClass":null,"Visiable":true,"Rank":6,"FieldName":"Id","ColumnName":"Id","ShowLength":0,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":false,"IsKey":false}', 6, N'Columns', N'Control', CAST(0x0000A5E000E491E1 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ee91d73d-2806-4752-98b8-5229636ea1d0', N'columnFilePath', N'a5fa394b-3fea-4ed6-b303-3d12eaba7540', N'ac7efba5-7bd8-4489-a794-c7de0cf3b191', N'SummerFresh.Controls.TableColumn', N'{"ID":"columnFilePath","CssClass":null,"Visiable":true,"Rank":39,"FieldName":"FilePath","ColumnName":"FilePath","ShowLength":10,"ColumnWidth":null,"DefaultValue":null,"Colspan":0,"TextAlign":1,"DataFormatString":null,"DataFormatFields":null,"HtmlEncode":false,"ReferenceField":null,"Sortable":true,"IsKey":false}', 39, N'Columns', N'Control', CAST(0x0000A5DF00C02664 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'f3543ef2-bdc9-4a4b-bf27-fb5e69026f76', N'ProvinceName', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"ProvinceName","Name":"ProvinceName","Label":"ProvinceName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5DF00BF3BFD AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'f7d3a171-2b56-4327-9dd9-327d2fde56d5', N'Priority', N'89abbc46-e1d4-4af2-9bcf-68d5959072b8', N'39965b01-c70c-44a2-a60c-3404040a8cf5', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"Priority","Name":"Priority","Label":"Priority","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":4,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 4, N'Fields', N'Control', CAST(0x0000A5E50121E73D AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'f91e0321-f870-4359-b43a-c2ecc7ea4ef2', N'btnEdit', N'b26731fd-58ef-40f2-9b5e-d8b9f4883403', N'cf88193b-85dd-415c-91db-41732cf8072b', N'SummerFresh.Controls.TableButton', N'{"ID":"btnEdit","Name":"编辑","CssClass":"btn btn-default btn-sm","Href":null,"Target":2,"Visiable":true,"Rank":0,"OnClick":null,"TableButtonType":0,"DataFields":null}', 0, N'Buttons', N'Control', CAST(0x0000A5E50121E5A3 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'fb92b791-43ce-4e67-812d-2ddb970956db', N'CityName', N'b7997f14-bfd6-4147-b264-0d3fefe1d7c0', N'c378640e-5338-4fda-a603-4bd387d40b13', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityName","Name":"CityName","Label":"CityName","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":3,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 3, N'Fields', N'Control', CAST(0x0000A5DF00BEDD71 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'fbb186c5-4969-4b26-9ba4-6159a4011b06', N'AllProvinceFormDs', N'c817d2de-d706-44aa-9377-901e4237d700', N'ea9bd482-7168-4950-a255-0749bff9b75f', N'SummerFresh.Business.CRUDService', N'{"CRUDName":"AllProvince","Parameter":null,"SortExpression":null,"ID":"AllProvinceFormDs","KeyFieldName":"Id"}', 0, N'FormService', N'DataSource', CAST(0x0000A5DF00BF3C48 AS DateTime))
INSERT [dbo].[APP_Component] ([ComponentId], [ComponentName], [ParentId], [PageId], [ComponentType], [ComponentXML], [Rank], [TargetId], [ComponentDataType], [LastUpdateTime]) VALUES (N'ff76d8ce-146c-4de5-a0f7-a82dac4c700c', N'CityCode', N'ca61e277-c4ad-48b3-8358-508eca7be1f9', N'7c33d2fe-a6b2-450e-8321-c6edbe170447', N'SummerFresh.Controls.TextBox', N'{"AutoCompleteSqlId":null,"TextMode":0,"ID":"CityCode","Name":"CityCode","Label":"CityCode","Description":null,"Enable":true,"ChangeTiggerSearch":false,"ContainerTemplate":null,"Rank":2,"AttributeString":null,"CssClass":"form-control","Validator":null,"Value":null,"Visiable":true}', 2, N'Fields', N'Control', CAST(0x0000A5DF00BEDE94 AS DateTime))
/****** Object:  Table [dbo].[SYS_UserRole]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_UserRole](
	[UserRoleId] [varchar](38) NOT NULL,
	[UserId] [varchar](38) NOT NULL,
	[RoleId] [varchar](38) NOT NULL,
 CONSTRAINT [PK_SYS_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (N'b0a481b8-bae2-4256-a5a1-4a5aed0157ae', N'4a786d06-0382-4c1a-85a0-165747283a66', N'61f024a7-9c0f-495a-b748-83038b0ec77d')
INSERT [dbo].[SYS_UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (N'e1aa3c3d-c89b-436a-ac16-1f9f1c6b9c9a', N'4a786d06-0382-4c1a-85a0-165747283a66', N'1ee75600-5109-4eab-bcba-f54c18b3acfb')
/****** Object:  Table [dbo].[SYS_User]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_User](
	[UserId] [varchar](38) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserCode] [nvarchar](50) NULL,
	[LoginId] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[DepartmentId] [varchar](38) NULL,
	[Status] [varchar](10) NULL,
	[Rank] [int] NULL,
 CONSTRAINT [PK_SYS_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
INSERT [dbo].[SYS_User] ([UserId], [UserName], [UserCode], [LoginId], [Password], [DepartmentId], [Status], [Rank]) VALUES (N'4a786d06-0382-4c1a-85a0-165747283a66', N'刘海峰', N'A011', N'liuhf', N'aY1RoZ2KEhzlgUmde3AWaA==', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', N'Enabled', 111)
/****** Object:  Table [dbo].[SYS_RoleTypePermission]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_RoleTypePermission](
	[RoleTypePermissionId] [varchar](38) NOT NULL,
	[RoleTypeId] [varchar](38) NULL,
	[PermissionId] [varchar](38) NULL,
	[PermissionRuleId] [varchar](38) NULL,
 CONSTRAINT [PK_SYS_RoleTypePermission] PRIMARY KEY CLUSTERED 
(
	[RoleTypePermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'0127C58E-E1C8-4B11-8B4B-B3D88CFDFEB0', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'a564bf5c-cc6e-4276-9501-7291ac70141b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'029B5BF9-975B-4C92-8607-9250E1EDB5A7', NULL, N'a5102c85-8713-48c3-add4-1a01f983101e', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'038B95DC-0ED0-4533-91B1-B43B41278258', NULL, N'6d5abcdb-7fae-4925-afbb-d3fb596463c1', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'0BA79125-5007-4520-BD90-69003A084539', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'147E8E11-656E-4025-8B8D-DDDFAB4D2887', NULL, N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'14BB0727-8552-467A-B4CA-1ED259951098', NULL, N'50849f51-0967-4d23-b752-16f1c88a18e6', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'1678CD5B-AC04-47E9-B7E4-E080A13C9DAF', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'50849f51-0967-4d23-b752-16f1c88a18e6', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'177DF9A4-2B27-41A2-883B-8D50D700EE6B', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'18376DE5-150A-4B25-94BF-FC6D62308CFF', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'd0865f8e-351b-4fc9-848c-40fffcd13bd9', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'1AE5253D-0274-4119-B750-A164208DE2A8', NULL, N'296551e5-0f6d-4d02-91cd-096a005f5b4b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'1B929BC6-31B1-4167-AAB6-09A52182E9C8', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'203C3219-5BFC-4900-8AD9-A87EE54D240C', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'46c3e6f4-4cf6-41eb-b24e-570323e25f9c', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'23A68B65-AB9C-4C11-95BC-FD15E9B16253', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'2455B5E3-C387-4274-8F2D-7A1FFDD15276', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'1b5466f3-6ced-47a9-86f7-6d7ef2a88efb', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'24F80264-5918-4EE5-8B4B-1E1CAD100AFC', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', N'acd9b66b-66e2-4098-b5b8-06477f6e1a6b')
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'25D26D3E-CF55-485E-9CC2-75A1C1D6D962', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'296551e5-0f6d-4d02-91cd-096a005f5b4b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'2622437F-2B00-440E-A746-59444AEA4D7A', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'3473F43B-A47D-4DCD-9339-A8F8AFD2A29B', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'50849f51-0967-4d23-b752-16f1c88a18e6', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'356B1CDC-68BC-4EDF-8E8D-153444A3B1C2', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'37CBB101-42AB-4999-B349-468CDB82650B', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'a5102c85-8713-48c3-add4-1a01f983101e', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'38B7E1D8-35CD-44EA-A3EE-6F63B53FEEB8', NULL, N'1b5466f3-6ced-47a9-86f7-6d7ef2a88efb', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'3DC44993-CEFF-4DDB-B98B-FBEBD4DAD88A', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'1b5466f3-6ced-47a9-86f7-6d7ef2a88efb', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'3E0E56CF-AD94-4C93-AF92-173EAE34D00A', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'420ACA62-3C89-4FB2-B9CD-781E0B666AF7', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'6d5abcdb-7fae-4925-afbb-d3fb596463c1', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'42B926A7-D4F5-4AA0-978B-8BD17D3B6AC2', NULL, N'd0865f8e-351b-4fc9-848c-40fffcd13bd9', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'437848C0-03FF-4473-A7E4-5E338D7556FF', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'a564bf5c-cc6e-4276-9501-7291ac70141b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'45FC5A59-93E4-46D0-92DB-D473863EE1E2', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'e206e45c-b9f6-4cc7-a35b-fa64ed98946c', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'4BB78262-6409-458C-AB98-EB9B14FD5822', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'4359f9d6-4a80-4256-aaa6-19bd53e167f6', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'56D53B5E-3602-4B98-8D9F-38C43616DF9B', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'62B8E4C2-38BC-410D-8B3B-1A9BC79AAAD2', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'162b46d7-eb2f-4888-9149-776438854693', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'688BBECC-938A-411E-8B61-3C26C8CB1377', NULL, N'a564bf5c-cc6e-4276-9501-7291ac70141b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'68E0A536-20DD-4B97-8F7C-5B412A4D398F', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'69E2265E-E6E6-4D9E-9D72-4193FD58F788', NULL, N'50849f51-0967-4d23-b752-16f1c88a18e6', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'6D02528D-4D09-46ED-96F4-C1F6AEEF136F', NULL, N'9cd37ca6-40c9-4255-b144-7ac267a8571f', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'7C236706-5342-4565-9D92-34546AA6A55A', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', N'1450ce6e-4799-419e-9cdf-b2039a12ea56')
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'810EDF04-BA78-47C0-8700-830927BBAA6E', NULL, N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'95FE9C17-444F-49B3-BBB1-1E9A5F75855A', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'BB9C0B77-E467-4B59-A8F6-8526427B5F0B', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'BC256A92-7045-41DB-95CD-F752731DCEA4', NULL, N'd0865f8e-351b-4fc9-848c-40fffcd13bd9', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'BCAE410F-6984-4E55-9440-2930F1233390', N'594693e6-06b7-4637-94c0-1c38bd57a3be', NULL, NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'C2A28247-A4C2-4458-A02D-9FAB12706285', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'd0865f8e-351b-4fc9-848c-40fffcd13bd9', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'CEA2E057-5587-49BD-A005-686F31281CFD', NULL, N'a564bf5c-cc6e-4276-9501-7291ac70141b', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'D24E75C4-D1D8-4A23-9E55-AC690985337E', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'e57634a1-fa61-418e-b080-bf25f35ba141', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'D860C711-8795-45DE-AABB-5BB285EED965', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'2044e2df-5ff2-4919-b0f2-8dc5a641bdc9', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'DB8E3357-D24B-490F-B5C8-0F5E855019C1', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'6d5abcdb-7fae-4925-afbb-d3fb596463c1', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'E6FC6B26-F1BB-4FFA-98F0-4C6CB2A2326D', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'9cd37ca6-40c9-4255-b144-7ac267a8571f', NULL)
INSERT [dbo].[SYS_RoleTypePermission] ([RoleTypePermissionId], [RoleTypeId], [PermissionId], [PermissionRuleId]) VALUES (N'F4FC552C-4273-4C9E-9A77-F9FABA2E769B', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'9cd37ca6-40c9-4255-b144-7ac267a8571f', NULL)
/****** Object:  Table [dbo].[SYS_RoleType]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_RoleType](
	[RoleTypeId] [varchar](38) NOT NULL,
	[RoleTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SYS_RoleType] PRIMARY KEY CLUSTERED 
(
	[RoleTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_RoleType] ([RoleTypeId], [RoleTypeName]) VALUES (N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'系统管理员')
INSERT [dbo].[SYS_RoleType] ([RoleTypeId], [RoleTypeName]) VALUES (N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'普通管理员')
/****** Object:  Table [dbo].[SYS_RolePermission]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_RolePermission](
	[RolePermissionId] [varchar](38) NOT NULL,
	[RoleId] [varchar](38) NULL,
	[PermissionId] [varchar](38) NULL,
	[PermissionRuleId] [varchar](38) NULL,
 CONSTRAINT [PK_SYS_RolePermission] PRIMARY KEY CLUSTERED 
(
	[RolePermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Role]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Role](
	[RoleId] [varchar](38) NOT NULL,
	[RoleTypeId] [varchar](38) NOT NULL,
	[DepartmentId] [varchar](38) NOT NULL,
 CONSTRAINT [PK_SYS_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_Role] ([RoleId], [RoleTypeId], [DepartmentId]) VALUES (N'1ee75600-5109-4eab-bcba-f54c18b3acfb', N'594693e6-06b7-4637-94c0-1c38bd57a3be', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C')
INSERT [dbo].[SYS_Role] ([RoleId], [RoleTypeId], [DepartmentId]) VALUES (N'54288018-acc8-4a98-b989-4719ae8517dd', N'737ea06d-abcd-46e7-8557-0fd6919d8dc7', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C')
/****** Object:  Table [dbo].[SYS_PermissionRule]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_PermissionRule](
	[PermissionRuleId] [varchar](38) NOT NULL,
	[PermissionId] [varchar](38) NULL,
	[RuleName] [varchar](150) NULL,
	[Priority] [int] NULL,
	[RuleContent] [varchar](1000) NULL,
	[Description] [varchar](1000) NULL,
 CONSTRAINT [PK_SYS_PermissionRule] PRIMARY KEY CLUSTERED 
(
	[PermissionRuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_PermissionRule] ([PermissionRuleId], [PermissionId], [RuleName], [Priority], [RuleContent], [Description]) VALUES (N'1450ce6e-4799-419e-9cdf-b2039a12ea56', N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', N'查看当前用户信息', 10, N'and UserId=#Env:User.UserId#', N'')
INSERT [dbo].[SYS_PermissionRule] ([PermissionRuleId], [PermissionId], [RuleName], [Priority], [RuleContent], [Description]) VALUES (N'acd9b66b-66e2-4098-b5b8-06477f6e1a6b', N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', N'查看所有用户信息', 1, N'and 1=1', N'')
/****** Object:  Table [dbo].[SYS_Permission]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Permission](
	[PermissionId] [varchar](38) NOT NULL,
	[PermissionName] [nvarchar](50) NULL,
	[PermissionCode] [nvarchar](50) NULL,
	[Url] [varchar](300) NULL,
	[ParentId] [varchar](38) NULL,
	[Rank] [int] NULL,
	[ElementId] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[PermissionType] [varchar](10) NULL,
	[Icon] [nvarchar](200) NULL,
	[Status] [varchar](10) NULL,
 CONSTRAINT [PK_SYS_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'0a9b9a93-f699-4315-be00-11092e7078cd', N'系统管理', N'System', N'', N'8a374ea9-fcb2-4607-96a9-71d808f57ed6', 0, NULL, N'', N'1', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', N'用户管理', N'user', N'/Entity/TreeList/User', N'0a9b9a93-f699-4315-be00-11092e7078cd', 2, NULL, N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'162b46d7-eb2f-4888-9149-776438854693', N'页面设计', N'PageDesigner', N'/PageDesigner/Index', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 1, N'', N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'1b5466f3-6ced-47a9-86f7-6d7ef2a88efb', N'角色类型管理', N'RoleType01', N'/Entity/List/RoleType', N'0a9b9a93-f699-4315-be00-11092e7078cd', 3, NULL, N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'2044e2df-5ff2-4919-b0f2-8dc5a641bdc9', N'增删改查', N'CRUD', N'/Entity/List/CRUD', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 10, N'', N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'296551e5-0f6d-4d02-91cd-096a005f5b4b', N'角色管理', N'Role01', N'/Entity/List/Role', N'0a9b9a93-f699-4315-be00-11092e7078cd', 3, NULL, N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'4359f9d6-4a80-4256-aaa6-19bd53e167f6', N'缓存管理', N'CacheManagement', N'/Entity/List/AllCache', N'0a9b9a93-f699-4315-be00-11092e7078cd', 10, N'', N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'46c3e6f4-4cf6-41eb-b24e-570323e25f9c', N'布局管理', N'LayoutManagement', N'/Entity/List/Layout', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 2, N'', N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'4c03bd1a-eb84-44ce-ac74-6857dfde7246', N'查看用户信息', N'SelectUser', N'', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', 4, N'', N'', N'3', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'50849f51-0967-4d23-b752-16f1c88a18e6', N'组织管理', N'Department01', N'/Entity/TreeList/Department', N'0a9b9a93-f699-4315-be00-11092e7078cd', 1, N'', N'', N'2', N'/Content/icons/root.gif', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'6d5abcdb-7fae-4925-afbb-d3fb596463c1', N'功能权限管理', N'Role', N'/Entity/TreeList/Permission', N'0a9b9a93-f699-4315-be00-11092e7078cd', 2, NULL, N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'8d640bba-184d-42a9-9cd4-4dd7322851a3', N'应用设计', N'AppDesign', N'', N'', 1, N'', N'', N'1', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'9cd37ca6-40c9-4255-b144-7ac267a8571f', N'删除用户', N'DeleteUser', N'', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', 3, N'btnDelete', N'', N'3', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'9eae6852-9fc9-4a82-b629-50112487c8b0', N'外置文件', N'ExternalFile', N'/Entity/List/ExternalFile', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 3, N'', N'', N'', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'a5102c85-8713-48c3-add4-1a01f983101e', N'数据字典', N'DataDictionary01', N'/Entity/List/DataDictionary', N'0a9b9a93-f699-4315-be00-11092e7078cd', 6, NULL, N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'a564bf5c-cc6e-4276-9501-7291ac70141b', N'编辑用户', N'ModifyUser', N'', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', 2, N'btnEdit', N'', N'3', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'd0865f8e-351b-4fc9-848c-40fffcd13bd9', N'新增用户', N'CreateUser', N'', N'144e62ec-097a-4cf8-95c4-6ce6e4a8470b', 1, N'btnInsert', N'', N'3', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'e206e45c-b9f6-4cc7-a35b-fa64ed98946c', N'数据库表', N'DataBaseTable', N'/Entity/TreeList/DataField', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 10, N'', N'', N'2', N'', N'Enabled')
INSERT [dbo].[SYS_Permission] ([PermissionId], [PermissionName], [PermissionCode], [Url], [ParentId], [Rank], [ElementId], [Description], [PermissionType], [Icon], [Status]) VALUES (N'e57634a1-fa61-418e-b080-bf25f35ba141', N'外置文件', N'ExternalFile', N'/Entity/List/ExternalFile', N'8d640bba-184d-42a9-9cd4-4dd7322851a3', 1, N'', N'', N'2', N'', N'Enabled')
/****** Object:  Table [dbo].[SYS_Department]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Department](
	[DepartmentId] [varchar](38) NOT NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[DepartmentCode] [nvarchar](50) NULL,
	[ParentId] [varchar](38) NULL,
	[Rank] [int] NULL,
	[Status] [varchar](10) NULL,
 CONSTRAINT [PK_SYS_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'42037c18-f416-4ad0-be55-0e4c32115273', N'财务部', N'CWB', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', 34, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', N'总公司', N'ROOT', N'', 1001, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'b3852a1c-126c-4f26-9d7e-b959fcce257b', N'总经办', N'ZJB', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', 1009, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'bcbe1bb7-a91f-47fd-b35c-41a16ac7f194', N'综合部', N'ZHB', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', 1003, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'f90654db-5e81-4535-9224-30e470ebe006', N'办公室', N'BGS1', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', 1002, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'f9a6f74f-4d9f-41d5-a64e-922cdd53cd96', N'后勤部', N'HQB', N'b3852a1c-126c-4f26-9d7e-b959fcce257b', 12, N'Enabled')
INSERT [dbo].[SYS_Department] ([DepartmentId], [DepartmentName], [DepartmentCode], [ParentId], [Rank], [Status]) VALUES (N'fe22505f-2871-4b6b-bc6c-d67e8d88385d', N'信息部', N'XXB', N'AEE1543A-B042-47EB-AFD4-D9BED111DF9C', 1005, N'Enabled')
/****** Object:  Table [dbo].[SYS_DataDictionaryItems]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_DataDictionaryItems](
	[DictionaryItemId] [varchar](38) NOT NULL,
	[DictionaryItemCode] [nvarchar](50) NOT NULL,
	[DictionaryItemText] [nvarchar](50) NOT NULL,
	[DictionaryId] [varchar](38) NOT NULL,
	[Rank] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_SYS_DataDictionaryItems] PRIMARY KEY CLUSTERED 
(
	[DictionaryItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'06b49acb-6657-4b9a-9818-a88b40a6c771', N'False', N'否', N'e9edffb3-184a-4c89-b833-36ceb7c3f92e', 2, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'1995deb0-9441-4842-ab69-a83856ff64d2', N'True', N'是', N'e9edffb3-184a-4c89-b833-36ceb7c3f92e', 1, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'3fe325df-8a83-4aa4-a068-648f43bee232', N'1', N'常规', N'13ba004e-9d37-4758-bee2-6f6dee7c3055', 1, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'4257156b-307c-4888-95cb-8df2455b01ac', N'JS', N'JS', N'847bb55f-f50e-40c4-a021-1921f867256d', 1, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'4bf542cf-984b-4eb4-a726-12b01e63b1e0', N'Enabled', N'启用', N'e9b15eb5-3d98-4732-86b3-c5248710dc17', 1, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'51a88bd7-b113-4b57-a59d-fbc1cbc3c8f4', N'Disabled', N'禁用', N'e9b15eb5-3d98-4732-86b3-c5248710dc17', 2, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'6c8ab19a-28cf-4455-beb3-d12cd08144d7', N'2', N'非常规', N'13ba004e-9d37-4758-bee2-6f6dee7c3055', 2, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'81b4719f-9b2a-4c31-8299-5c50ab5e70e0', N'CSS', N'CSS', N'847bb55f-f50e-40c4-a021-1921f867256d', 2, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'87d48865-fda4-4021-ac2e-40f0ff746a08', N'1', N'页面', N'6d3b2015-95a2-4e4e-ac50-fcaf2353aaf7', 2, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'9646c869-3699-4ee0-a3b4-684487aeceea', N'1', N'模块', N'25a53b18-dc05-45ec-974d-07e8dfc63cf8', 1, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'aaf4a63c-0f7e-49e1-b658-f109cf0726e5', N'3', N'功能', N'25a53b18-dc05-45ec-974d-07e8dfc63cf8', 3, 0)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'c849047f-fe52-4f91-a660-789867e1ae99', N'0', N'分类', N'6d3b2015-95a2-4e4e-ac50-fcaf2353aaf7', 1, 1)
INSERT [dbo].[SYS_DataDictionaryItems] ([DictionaryItemId], [DictionaryItemCode], [DictionaryItemText], [DictionaryId], [Rank], [Status]) VALUES (N'd7525b54-203e-40be-bd08-816ea02a68ed', N'2', N'页面', N'25a53b18-dc05-45ec-974d-07e8dfc63cf8', 2, 0)
/****** Object:  Table [dbo].[SYS_DataDictionary]    Script Date: 04/15/2016 15:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_DataDictionary](
	[DictionaryId] [varchar](38) NOT NULL,
	[DictionaryCode] [nvarchar](50) NULL,
	[DictionaryName] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_SYS_Dictionary] PRIMARY KEY CLUSTERED 
(
	[DictionaryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'13ba004e-9d37-4758-bee2-6f6dee7c3055', N'PollutantTypeId', N'污染物类型', N'')
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'25a53b18-dc05-45ec-974d-07e8dfc63cf8', N'PermissionType', N'功能权限类型', N'')
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'6d3b2015-95a2-4e4e-ac50-fcaf2353aaf7', N'PageType', N'页面类型', N'')
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'847bb55f-f50e-40c4-a021-1921f867256d', N'ExternalFileType', N'外置文件类型', N'')
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'e9b15eb5-3d98-4732-86b3-c5248710dc17', N'Status', N'启用禁用', N'')
INSERT [dbo].[SYS_DataDictionary] ([DictionaryId], [DictionaryCode], [DictionaryName], [Description]) VALUES (N'e9edffb3-184a-4c89-b833-36ceb7c3f92e', N'YesOrNo', N'是否', N'这是描述，描述很长怎么办，哈哈哈，会不会自动截断？')
/****** Object:  UserDefinedFunction [dbo].[fn_get_sub_me_component]    Script Date: 04/15/2016 15:31:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fn_get_sub_me_component] (@parent_component_id varchar(300))
RETURNS TABLE
AS
RETURN 
(
    WITH F_USF_COMPONENT 
AS 
( 
--递归开始
SELECT *
	           FROM APP_Component CO
	           where CO.ComponentId = @parent_component_id    
UNION ALL 
-- 递归成员 (RM): 
-- 使用现有数据往下一层展开 
SELECT CO.*
	           FROM F_USF_COMPONENT FCO	  
INNER JOIN APP_Component CO ON FCO.ComponentId = CO.ParentId
) 
--从递归函数获取数据
SELECT *
FROM F_USF_COMPONENT
);
GO
