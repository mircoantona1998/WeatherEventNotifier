﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Userdata.Configurations;

namespace Userdata.Models;

public partial class UserdataContext : DbContext
{
    public UserdataContext()
    {
    }

    public UserdataContext(DbContextOptions<UserdataContext> options)
        : base(options)
    {
        if(config.configuration==null)
            config.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    public virtual DbSet<MessageReceived> MessageReceiveds { get; set; }

    public virtual DbSet<MessageSent> MessageSents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.configuration["ConnectionStrings:Userdata"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageReceived>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageR__3213E83FDFED8CFE");

            entity.ToTable("MessageReceived");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Creator)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("creator");
            entity.Property(e => e.IdOffsetResponse).HasColumnName("idOffsetResponse");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Offset).HasColumnName("offset");
            entity.Property(e => e.Partition).HasColumnName("partition");
            entity.Property(e => e.TagMessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tagMessage");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Topic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("topic");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<MessageSent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageS__3213E83F979BAACB");

            entity.ToTable("MessageSent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Creator)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("creator");
            entity.Property(e => e.IdOffsetResponse).HasColumnName("idOffsetResponse");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Offset).HasColumnName("offset");
            entity.Property(e => e.Partition).HasColumnName("partition");
            entity.Property(e => e.TagMessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tagMessage");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Topic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("topic");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_Id");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Cap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Cognome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.DateUpdate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isActive");
            entity.Property(e => e.IsBlocked)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isBlocked");
            entity.Property(e => e.LastAccess)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Partition).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Password).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
