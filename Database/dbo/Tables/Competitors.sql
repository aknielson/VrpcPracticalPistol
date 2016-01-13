CREATE TABLE [dbo].[Competitors] (
    [Id]                          INT            IDENTITY (1, 1) NOT NULL,
    [MagazineCapacity] INT NULL, 
    [Caliber_Id]                     INT NULL,
    [Match_Id]                    INT            NULL,
    [Member_Id]                   INT            NULL,
    CONSTRAINT [PK_dbo.Competitors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Competitors_dbo.Matches_Match_Id] FOREIGN KEY ([Match_Id]) REFERENCES [dbo].[Matches] ([Id]),
    CONSTRAINT [FK_dbo.Competitors_dbo.Members_Member_Id] FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Members] ([Id]), 
    CONSTRAINT [FK_dob.Competitors_dbo.Calibers_Caliber_id] FOREIGN KEY ([Caliber_Id]) REFERENCES [dbo].[Calibers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Member_Id]
    ON [dbo].[Competitors]([Member_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Match_Id]
    ON [dbo].[Competitors]([Match_Id] ASC);

