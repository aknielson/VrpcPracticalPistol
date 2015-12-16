CREATE TABLE [dbo].[Matches] (
    [Id]   INT      IDENTITY (1, 1) NOT NULL,
    [Date] DATETIME NOT NULL,
    CONSTRAINT [PK_dbo.Matches] PRIMARY KEY CLUSTERED ([Id] ASC)
);

