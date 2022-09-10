CREATE TABLE [dbo].[Tweet] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Title]   NVARCHAR (MAX) NULL,
    [Body]    NVARCHAR (MAX) NULL,
    [Created] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Tweet] PRIMARY KEY CLUSTERED ([Id] ASC)
);

