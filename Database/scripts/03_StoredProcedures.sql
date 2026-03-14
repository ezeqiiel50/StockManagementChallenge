-- =============================================
-- stored procedure para insertar producto
-- =============================================

CREATE PROCEDURE sp_CreateProduct
    @Price INT,
    @Category NVARCHAR(50),
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Products ( Price,Category,CreatedAt, CreatedBy )
    VALUES(@Price,@Category, GETUTCDATE(), @CreatedBy );
	SELECT SCOPE_IDENTITY() AS Id;
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
SELECT Id,Price,Category,CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
FROM Products
WHERE Id = @Id;
END
GO
-- =============================================
-- stored procedure para obtener un producto
-- =============================================

CREATE PROCEDURE sp_UpdateProduct
    @Id INT,
    @Price INT,
    @Category NVARCHAR(50),
    @UpdatedBy INT
AS
BEGIN
    UPDATE Products
    SET Price = @Price, Category = @Category, UpdatedAt = GETUTCDATE(),UpdatedBy = @UpdatedBy
    WHERE Id = @Id;
	SELECT @@ROWCOUNT AS AffectedRows;
END
GO
-- =============================================
-- stored procedure para eliminar un producto
-- =============================================

CREATE PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM Products
    WHERE Id = @Id;
	SELECT @@ROWCOUNT AS AffectedRows;
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