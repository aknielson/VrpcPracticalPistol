CREATE TABLE [dbo].[MatchStageTimes] (
    [Id]                 INT      IDENTITY (1, 1) NOT NULL,
    [StageTime]          TIME (7) NOT NULL,
    [CompetitorStage_Id] INT      NULL,
    CONSTRAINT [PK_dbo.MatchStageTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MatchStageTimes_dbo.CompetitorStages_CompetitorStage_Id] FOREIGN KEY ([CompetitorStage_Id]) REFERENCES [dbo].[CompetitorStages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CompetitorStage_Id]
    ON [dbo].[MatchStageTimes]([CompetitorStage_Id] ASC);

