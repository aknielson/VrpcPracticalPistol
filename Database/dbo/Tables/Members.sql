CREATE TABLE [dbo].[Members] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (MAX) NULL,
    [LastName]     NVARCHAR (MAX) NULL,
    [EmailAddress] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Members] PRIMARY KEY CLUSTERED ([Id] ASC)
);

