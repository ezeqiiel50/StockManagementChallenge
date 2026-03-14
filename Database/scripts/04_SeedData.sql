BEGIN TRY
    BEGIN TRANSACTION;
    -- =============================================
    -- USERS
    -- =============================================

    INSERT INTO Users (Username, PasswordHash)
    VALUES 
    ('admin', '$2a$11$2B5JQHyOQwDluGiTmsTrPupl3WcBIpnWX7pSqmdbWlffbfBL86Iwm'),
    ('tester','$2a$11$T8llk7dtPFNb7c44ZZYWmOJyWsTBvqdzobUnfLFuzt1Wf9zbRz9oq');

    -- =============================================
    -- PRODUCTS
    -- =============================================
    -- Se asume que el usuario admin tiene Id = 1
    INSERT INTO Products (Price, Description, Category, CreatedBy)
    VALUES
    (10,'PRODUCTO A', 'PRODDOS', 1),
    (60,'PRODUCTO B','PRODUNO', 1),
    (5, 'PRODUCTO C', 'PRODDOS', 1),
    (5, 'PRODUCTO D','PRODUNO', 1),
    (15,'PRODUCTO E','PRODDOS', 1);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;