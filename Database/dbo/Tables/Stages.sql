CREATE TABLE [dbo].[Stages] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [StageName]              NVARCHAR (MAX) NOT NULL,	
    [StageOrder] INT NULL, 
    [IncludeInCombinedScore] BIT            NOT NULL,
    [IsVirginia]             BIT            NOT NULL,
    [NumberOfStrings]        INT            NOT NULL,
    [Designer_Id]            INT            NULL,
    [Match_Id]               INT            NULL,
    CONSTRAINT [PK_dbo.Stages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Stages_dbo.Matches_Match_Id] FOREIGN KEY ([Match_Id]) REFERENCES [dbo].[Matches] ([Id]),
    CONSTRAINT [FK_dbo.Stages_dbo.Members_Designer_Id] FOREIGN KEY ([Designer_Id]) REFERENCES [dbo].[Members] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Match_Id]
    ON [dbo].[Stages]([Match_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Designer_Id]
    ON [dbo].[Stages]([Designer_Id] ASC);

