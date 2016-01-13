CREATE TABLE [dbo].[CompetitorStages] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [PenaltyPoints] INT NOT NULL,
    [Competitor_Id] INT NULL,
    [Stage_Id]      INT NULL,
    [Match_Id]      INT NULL,
    CONSTRAINT [PK_dbo.CompetitorStages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CompetitorStages_dbo.Competitors_Competitor_Id] FOREIGN KEY ([Competitor_Id]) REFERENCES [dbo].[Competitors] ([Id]),
    CONSTRAINT [FK_dbo.CompetitorStages_dbo.Matches_Match_Id] FOREIGN KEY ([Match_Id]) REFERENCES [dbo].[Matches] ([Id]),
    CONSTRAINT [FK_dbo.CompetitorStages_dbo.Stages_Stage_Id] FOREIGN KEY ([Stage_Id]) REFERENCES [dbo].[Stages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Match_Id]
    ON [dbo].[CompetitorStages]([Match_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Stage_Id]
    ON [dbo].[CompetitorStages]([Stage_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Competitor_Id]
    ON [dbo].[CompetitorStages]([Competitor_Id] ASC);

