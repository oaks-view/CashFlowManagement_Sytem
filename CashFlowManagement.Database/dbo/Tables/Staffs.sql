CREATE TABLE [dbo].[Staffs] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Username]      NVARCHAR (30) NOT NULL,
    [Password]      NVARCHAR (50) NOT NULL,
    [FirstName]     NVARCHAR (20) NOT NULL,
    [LastName]      NVARCHAR (20) NOT NULL,
    [StaffCategory] INT           NOT NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Staffs] UNIQUE NONCLUSTERED ([Username] ASC)
);

