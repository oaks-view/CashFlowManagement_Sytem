CREATE TABLE [dbo].[Staffs] (
    [Id]            NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (120) NOT NULL,
    [StaffCategory] INT            NOT NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Staffs_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

