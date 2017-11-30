CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(128) NULL, 
    [Abstract] VARCHAR(128) NULL, 
    [Pages] INT NULL, 
    [AuthorId] INT NULL
)
