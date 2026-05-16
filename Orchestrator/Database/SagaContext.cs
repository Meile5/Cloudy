using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Orchestrator.Models;

namespace Orchestrator.Database;

public partial class SagaContext : DbContext
{
    public SagaContext(DbContextOptions<SagaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Saga> Sagas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Saga>(entity =>
        {
            entity.HasKey(e => e.SagaId).HasName("PK__saga__1ED2AFFBB46DA2EA");

            entity.ToTable("saga");

            entity.Property(e => e.SagaId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("saga_id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("booking_id");
            entity.Property(e => e.BookingProcessed).HasColumnName("booking_processed");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsCompleted).HasColumnName("is_completed");
            entity.Property(e => e.IsFailed).HasColumnName("is_failed");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("payment_id");
            entity.Property(e => e.PaymentProcessed).HasColumnName("payment_processed");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
