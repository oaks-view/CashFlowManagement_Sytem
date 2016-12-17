CREATE TABLE [dbo].[Expenses] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (30)  NOT NULL,
    [Cost]        INT            NOT NULL,
    [StaffId]     NVARCHAR (128) NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Expenses_Staffs] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[Staffs] ([Id])
);

