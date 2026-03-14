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
    INSERT INTO Products (Price, Category, CreatedBy, UpdatedBy)
    VALUES
    (10, 'PRODDOS', 1, NULL),
    (60, 'PRODUNO', 1, NULL),
    (5, 'PRODDOS', 1, NULL),
    (5, 'PRODUNO', 1, NULL),
    (15, 'PRODDOS', 1, NULL);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;