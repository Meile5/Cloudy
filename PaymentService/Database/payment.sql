-- original sql was a mix of mssql and postgres, I changed it to mssql bc thats easier (postgres doesn't have "create if not exists" for database)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'payment-db')
BEGIN
        CREATE DATABASE [payment-db];
END
GO

USE [payment-db];
GO


CREATE TABLE payments (
                         id varchar(36) PRIMARY KEY,

                         --I know we would probably store this in a more secure way, but this is a small example program made in like a week
                         card_number VARCHAR(20) NOT NULL,

                         amount DECIMAL NOT NULL,
                         -- "completed", "reserved" (payment not completed) or "cancelled"
                         status VARCHAR(20) NOT NULL DEFAULT 'reserved',

                         currency CHAR(3) NOT NULL DEFAULT 'USD',

                         created_at datetime DEFAULT GETDATE(),
                         updated_at datetime DEFAULT GETDATE()
);