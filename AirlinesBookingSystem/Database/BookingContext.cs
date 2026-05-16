using System;
using System.Collections.Generic;
using AirlinesBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesBookingSystem.Database;

public partial class BookingContext : DbContext
{
    public BookingContext(DbContextOptions<BookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F10F6B972");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.BookingReference, "UQ__bookings__BADA4559E7B295EC").IsUnique();

            entity.HasIndex(e => e.PassengerId, "idx_bookings_passenger");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BookingReference)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("booking_reference");
            entity.Property(e => e.FlightId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("flight_id");
            entity.Property(e => e.PassengerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("passenger_id");
            entity.Property(e => e.SeatId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("seat_id");

            entity.HasOne(d => d.Flight).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__flight__5DCAEF64");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__passen__5CD6CB2B");

            entity.HasOne(d => d.Seat).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__seat_i__5EBF139D");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flights__3213E83FF5023ABF");

            entity.ToTable("flights");

            entity.HasIndex(e => new { e.OriginAirport, e.DestinationAirport, e.DepartureTime }, "idx_flights_route");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.AircraftId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aircraft_id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrival_time");
            entity.Property(e => e.BaseFare)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("base_fare");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("USD")
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departure_time");
            entity.Property(e => e.DestinationAirport)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("destination_airport");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("flight_number");
            entity.Property(e => e.OriginAirport)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("origin_airport");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("scheduled")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__passenge__3213E83F299BEBCB");

            entity.ToTable("passengers");

            entity.HasIndex(e => e.Email, "UQ__passenge__AB6E6164638B35C0").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.FrequentFlyerNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("frequent_flyer_number");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("passport_number");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__seats__3213E83FA12F7722");

            entity.ToTable("seats");

            entity.HasIndex(e => e.FlightId, "idx_seats_flight");

            entity.HasIndex(e => new { e.FlightId, e.SeatNumber }, "unique_flight_seat").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CabinClass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cabin_class");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FareClass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fare_class");
            entity.Property(e => e.FlightId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("flight_id");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("seat_number");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("available")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Flight).WithMany(p => p.Seats)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__seats__flight_id__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
