-- Verificar si existe y eliminarla (opcional)
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'StockManagementChallenge')
BEGIN
    DROP DATABASE [StockManagementChallenge]
END
GO

-- Crear la base de datos
CREATE DATABASE [StockManagementChallenge]
GO

-- Usar la base de datos
USE [StockManagementChallenge]
GO


USE [master]
GO

-- Crear el login a nivel de servidor
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'stock_user')
BEGIN
    CREATE LOGIN [stock_user] 
    WITH PASSWORD    = 'Stock@2024!',
         CHECK_POLICY  = ON,   
         CHECK_EXPIRATION = OFF 
END
GO

USE [StockManagementChallenge]
GO

-- Crear el usuario vinculado al login
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'stock_user')
BEGIN
    CREATE USER [stock_user] FOR LOGIN [stock_user]
END
GO

-- Asignar permisos solo a lo necesario
GRANT EXECUTE ON SCHEMA::dbo TO [stock_user]  
GRANT SELECT ON SCHEMA::dbo TO [stock_user]  
GO