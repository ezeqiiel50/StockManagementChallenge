-- =============================================
-- stored procedure para insertar producto
-- =============================================

CREATE PROCEDURE sp_CreateProduct
    @Price       INT,
    @Description NVARCHAR(50),
    @Category    NVARCHAR(50),
    @CreatedBy   INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            IF EXISTS (
                SELECT 1 FROM Products WITH (UPDLOCK, HOLDLOCK)
                WHERE Description = @Description
            )
            BEGIN
                COMMIT TRANSACTION
                SELECT -1 AS Id
                RETURN
            END

            INSERT INTO Products (Price, Description, Category, CreatedAt, CreatedBy)
            VALUES (@Price, @Description, @Category, GETUTCDATE(), @CreatedBy)

            SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO
-- =============================================
-- stored procedure para obtener productos
-- =============================================

CREATE PROCEDURE sp_GetProducts
AS
BEGIN
 SELECT Id,Price,Category,CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
 FROM Products;
END
GO
-- =============================================
-- stored procedure para obtener un producto
-- =============================================
CREATE PROCEDURE sp_GetProductById
    @Id INT
AS
BEGIN
SELECT Id,Price,Description,Category,CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
FROM Products
WHERE Id = @Id;
END
GO
-- =============================================
-- stored procedure para obtener un producto
-- =============================================

CREATE PROCEDURE [dbo].[sp_UpdateProduct]
    @Id          INT,
    @Price       INT,
    @Description NVARCHAR(50),
    @Category    NVARCHAR(50),
    @UpdatedBy   INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
			-- Verifica si el producto existe
           IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @Id)
			BEGIN
			    COMMIT TRANSACTION
			    SELECT -1 AS AffectedRows
			    RETURN
			END
		    -- Verifica si la descripcion existe
			IF EXISTS (SELECT 1 FROM Products WITH (UPDLOCK, HOLDLOCK) WHERE Description = @Description AND Id != @Id)
			BEGIN
			    COMMIT TRANSACTION
			    SELECT -2 AS AffectedRows
			    RETURN
			END
            UPDATE Products SET  Price = @Price, Description = @Description, Category = @Category, UpdatedAt = GETUTCDATE(), UpdatedBy = @UpdatedBy WHERE Id = @Id
            SELECT CAST(@@ROWCOUNT AS INT) AS AffectedRows

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
    END CATCH
END

GO
-- =============================================
-- stored procedure para eliminar un producto
-- =============================================

CREATE PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @Id)
            BEGIN
				COMMIT TRANSACTION
                SELECT -1 AS AffectedRows
                RETURN
            END
            DELETE FROM Products WHERE Id = @Id
            SELECT CAST(@@ROWCOUNT AS INT) AS AffectedRows 

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO
-- =============================================
-- stored procedure para obtener un usuario
-- =============================================
CREATE PROCEDURE sp_GetUserByUsername
    @Username NVARCHAR(100)
AS
BEGIN
    SELECT Id,UserName,PasswordHash
    FROM Users
    WHERE Username = @Username;
END
GO