IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909112636_CreateTodoItem')
BEGIN
    CREATE TABLE [TodoItems] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [IsCompleted] bit NOT NULL,
        CONSTRAINT [PK_TodoItems] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909112636_CreateTodoItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200909112636_CreateTodoItem', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200912110359_AddTimeStamp')
BEGIN
    ALTER TABLE [TodoItems] ADD [RowVersion] rowversion NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200912110359_AddTimeStamp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200912110359_AddTimeStamp', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001015112_AddCategory')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TodoItems]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [TodoItems] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [TodoItems] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001015112_AddCategory')
BEGIN
    CREATE TABLE [Categories] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [RowVersion] rowversion NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001015112_AddCategory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201001015112_AddCategory', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001015409_AddCategoryIdToTodoItem')
BEGIN
    ALTER TABLE [TodoItems] ADD [CategoryId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001015409_AddCategoryIdToTodoItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201001015409_AddCategoryIdToTodoItem', N'3.1.8');
END;

GO

