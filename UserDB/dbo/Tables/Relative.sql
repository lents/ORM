CREATE TABLE [dbo].[Relative]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Relationship] NVARCHAR(20) NOT NULL, 
    [FullName] NVARCHAR(250) NOT NULL, 
    [UserId] INT NOT NULL,
    CONSTRAINT FK_RelationUser FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
