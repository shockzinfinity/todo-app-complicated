SELECT *
FROM [dbo].[__EFMigrationsHistory]
SELECT *
FROM [dbo].[Users]
SELECT *
FROM [dbo].[Categories]
SELECT *
FROM [dbo].[TodoItems]
SELECT *
FROM [dbo].[Flows]

-- UPDATE [dbo].[Categories]
-- SET [BgColor] = 'rgb(191, 0, 121)'
-- WHERE 1 = 1
-- AND [Id] = 36

-- DELETE FROM [dbo].[Categories] WHERE [Id] > 2 

-- INSERT INTO [dbo].[Flows] ([Name], [Pos], [CreatedAt], [UpdatedAt], [CategoryId])
-- VALUES (N'BACKLOG', 65536, GETDATE(), GETDATE(), 36) 
-- INSERT INTO [dbo].[Flows] ([Name], [Pos], [CreatedAt], [UpdatedAt], [CategoryId])
-- VALUES (N'할일', 131072, GETDATE(), GETDATE(), 36)
-- INSERT INTO [dbo].[Flows] ([Name], [Pos], [CreatedAt], [UpdatedAt], [CategoryId])
-- VALUES (N'진행', 196608, GETDATE(), GETDATE(), 36)
-- INSERT INTO [dbo].[Flows] ([Name], [Pos], [CreatedAt], [UpdatedAt], [CategoryId])
-- VALUES (N'완료', 262144, GETDATE(), GETDATE(), 36)

SELECT *
FROM [dbo].[TodoItems]
SELECT *
FROM [dbo].[Flows]

65536
131072
196608

SELECT *
FROM [dbo].[TodoItems]
WHERE [FlowId] = 9


-- UPDATE [dbo].[TodoItems]
-- SET [Pos] = 65536 * 7
-- WHERE [Id] = 2

-- DELETE FROM [dbo].[Flows] WHERE [Id] IN (4, 5, 6)


-- DELETE FROM [dbo].[TodoItems] WHERE [Id] IN (24, 25, 26)
-- DELETE FROM [dbo].[Flows] WHERE [Id] IN (16, 17, 18, 19)
