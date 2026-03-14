-- =============================================
-- TABLE: Users
-- =============================================

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL
);

-- =============================================
-- TABLE: Products
-- =============================================

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Price INT NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    UpdatedAt DATETIME2 NULL,
    UpdatedBy INT NULL,
	
    CONSTRAINT FK_Products_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_Products_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);