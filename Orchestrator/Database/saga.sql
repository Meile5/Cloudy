IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'booking-db')
    BEGIN
        CREATE DATABASE [saga-db];
    END
GO

USE [saga-db];
GO

CREATE TABLE saga (
                            saga_id varchar(36) PRIMARY KEY,

                            booking_id varchar(36) ,
                            payment_id varchar(36) ,

                            -- 0 = false, 1 = true
                            payment_processed BIT NOT NULL DEFAULT 0,
                            booking_processed BIT NOT NULL DEFAULT 0,
                            is_failed BIT NOT NULL DEFAULT 0,
                            is_completed BIT NOT NULL DEFAULT 0,

                            created_at datetime DEFAULT GETDATE(),
                            completed_at datetime DEFAULT GETDATE()
);