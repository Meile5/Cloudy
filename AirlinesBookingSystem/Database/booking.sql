-- original sql was a mix of mssql and postgres, I changed it to mssql bc thats easier (postgres doesn't have "create if not exists" for database)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'booking-db')
BEGIN
        CREATE DATABASE [booking-db];
END
GO

USE [booking-db];
GO

CREATE TABLE passengers (
                            id varchar(36) PRIMARY KEY,

                            first_name VARCHAR(100) NOT NULL,
                            last_name VARCHAR(100) NOT NULL,

                            email VARCHAR(255) UNIQUE NOT NULL,
                            phone VARCHAR(50),

                            date_of_birth DATE,
                            passport_number VARCHAR(50),

                            frequent_flyer_number VARCHAR(100),

                            created_at datetime DEFAULT GETDATE(),
                            updated_at datetime DEFAULT GETDATE()
);


CREATE TABLE flights (
                         id varchar(36) PRIMARY KEY,

                         flight_number VARCHAR(20) NOT NULL,

                         origin_airport CHAR(3) NOT NULL,
                         destination_airport CHAR(3) NOT NULL,

                         departure_time datetime NOT NULL,
                         arrival_time datetime NOT NULL,

                         aircraft_id VARCHAR(50),

                         status VARCHAR(30) DEFAULT 'scheduled',

                         base_fare NUMERIC(10,2) NOT NULL,
                         currency CHAR(3) NOT NULL DEFAULT 'USD',

                         created_at datetime DEFAULT GETDATE(),
                         updated_at datetime DEFAULT GETDATE()
);



-- =========================================
-- SEATS
-- =========================================

CREATE TABLE seats (
                       id varchar(36) PRIMARY KEY,

                       flight_id varchar(36) NOT NULL REFERENCES flights(id),

                       seat_number VARCHAR(10) NOT NULL,

                       cabin_class VARCHAR(20) NOT NULL,
                       fare_class VARCHAR(20),

                       status VARCHAR(20) NOT NULL DEFAULT 'available',

                       price NUMERIC(10,2) NOT NULL,

                       created_at datetime DEFAULT GETDATE(),
                       updated_at datetime DEFAULT GETDATE()

                       CONSTRAINT unique_flight_seat
                           UNIQUE (flight_id, seat_number)
);


CREATE TABLE bookings (
                          id varchar(36) PRIMARY KEY,

                          booking_reference VARCHAR(20) NOT NULL UNIQUE,

                          passenger_id varchar(36) NOT NULL
                              REFERENCES passengers(id),

                          flight_id varchar(36) NOT NULL
                              REFERENCES flights(id),

                          seat_id varchar(36) NOT NULL
                              REFERENCES seats(id),

);


CREATE INDEX idx_flights_route
    ON flights(origin_airport, destination_airport, departure_time);

CREATE INDEX idx_seats_flight
    ON seats(flight_id);

CREATE INDEX idx_bookings_passenger
    ON bookings(passenger_id);




-- =========================================
-- CONSTRAINTS
-- =========================================

ALTER TABLE seats
    ADD CONSTRAINT chk_seat_status
        CHECK (
            status IN (
                       'available',
                       'reserved',
                       'sold',
                       'blocked'
                )
            );



-- =========================================
-- SAMPLE BOOKING TRANSACTION
-- =========================================
/*
BEGIN;

-- 1. Lock seat row
SELECT *
FROM seats
WHERE id = 99122
    FOR UPDATE;

-- 2. Verify seat still available
-- application checks:
-- status = 'available'

-- 4. Create booking
INSERT INTO bookings (
    booking_reference,
    passenger_id,
    flight_id,
    seat_id,
    booking_status,
    total_amount,
    currency,
    confirmed_at
)
VALUES (
           'X7KD92',
           71,
           8821,
           99122,
           'confirmed',
           450.00,
           'USD',
           NOW()
       );

-- 5. Mark seat sold
UPDATE seats
SET status = 'sold',
    updated_at = NOW()
WHERE id = 99122;

COMMIT;
*/