CREATE TABLE [dbo].[Incomes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (30)  NOT NULL,
    [Amount]      INT            NOT NULL,
    [StaffId]     NVARCHAR (128) NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    CONSTRAINT [PK_Incomes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Incomes_Staffs] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[Staffs] ([Id])
);

