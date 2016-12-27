
if not exists (select * from [Calibers])
begin
	SET IDENTITY_INSERT [dbo].[Calibers] ON 

	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (1, N'9MM', N'9MM')
	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (2, N'45 ACP', N'45 ACP')
	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (3, N'40 SW', N'40 SW')
	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (4, N'38 Special', N'38 Special')
	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (5, N'38 Super', N'38 Super')
	
	INSERT [dbo].[Calibers] ([Id], [Name], [Description]) VALUES (6, N'380 ACP', N'380 ACP')
	
	SET IDENTITY_INSERT [dbo].[Calibers] OFF
	
end