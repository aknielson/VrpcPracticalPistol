if not exists (select * from [Members])
begin
	SET IDENTITY_INSERT [dbo].[Members] ON 

	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (1, N'Allen', N'Nielson', N'aknielson@hotmail.com')
	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (2, N'Don', N'Yash', N'Don.Yash@example.com')
	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (3, N'Parker', N'Leach', N'pleach@example.com')
	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (4, N'Dave', N'Saxon', N'dsaxon@example.com')
	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (5, N'Tim', N'Hay', N'thay@example.com')
	
	INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [EmailAddress]) VALUES (6, N'Joanne', N'Tagemeyer', N'jt@example.com')
	
	SET IDENTITY_INSERT [dbo].[Members] OFF
end