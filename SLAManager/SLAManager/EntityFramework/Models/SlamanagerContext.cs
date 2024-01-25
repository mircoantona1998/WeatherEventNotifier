using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SLAManagerdata.Configurations;

namespace SLAManagerdata.Models;

public partial class SlamanagerContext : DbContext
{
    public SlamanagerContext()
    {
    }

    public SlamanagerContext(DbContextOptions<SlamanagerContext> options)
     : base(options)
    {
        if (config.configuration == null)
            config.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.configuration["ConnectionStrings:SLAManager"]);

    public virtual DbSet<Heartbeat> Heartbeats { get; set; }

    public virtual DbSet<HeartbeatView> HeartbeatViews { get; set; }

    public virtual DbSet<MessageReceived> MessageReceiveds { get; set; }

    public virtual DbSet<MessageSent> MessageSents { get; set; }

    public virtual DbSet<MetricDatum> MetricData { get; set; }

    public virtual DbSet<MonitoringMetric> MonitoringMetrics { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Sla> Slas { get; set; }

    public virtual DbSet<SlaMetricStatus> SlaMetricStatuses { get; set; }

    public virtual DbSet<SlaMetricStatusView> SlaMetricStatusViews { get; set; }

    public virtual DbSet<SlaMetricViolation> SlaMetricViolations { get; set; }

    public virtual DbSet<SlaMetricViolationForecast> SlaMetricViolationForecasts { get; set; }

    public virtual DbSet<SlaMetricViolationForecastView> SlaMetricViolationForecastViews { get; set; }

    public virtual DbSet<SlaMetricViolationView> SlaMetricViolationViews { get; set; }

    public virtual DbSet<SlaView> SlaViews { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heartbeat>(entity =>
        {
            entity.ToTable("Heartbeat");

            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Heartbeats)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("FK_heartbeat_Services");
        });

        modelBuilder.Entity<HeartbeatView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Heartbeat_view");

            entity.Property(e => e.Servicename)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<MessageReceived>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageR__3213E83FD870A341");

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
            entity.HasKey(e => e.Id).HasName("PK__MessageS__3213E83F59B218A9");

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

        modelBuilder.Entity<MetricDatum>(entity =>
        {
            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MetricName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Metric_name");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.Value2)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MonitoringMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MetricMonitoring");

            entity.ToTable("MonitoringMetric");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Metric)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_Id");

            entity.Property(e => e.DateUpdate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isActive");
            entity.Property(e => e.IsBlocked)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isBlocked");
            entity.Property(e => e.Partition).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Password).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Service1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("Service");
            entity.Property(e => e.Servicename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Sla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SLA");

            entity.ToTable("Sla");

            entity.Property(e => e.Symbol)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDatetime).HasColumnType("datetime");
        });

        modelBuilder.Entity<SlaMetricStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StatusMetricSla");

            entity.ToTable("SlaMetricStatus");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaMetricStatusView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Sla_metric_status_view");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Metric)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MetricDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Symbol)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaMetricViolation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ViolationMetricSla");

            entity.ToTable("SlaMetricViolation");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Violation)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaMetricViolationForecast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ForecastViolationMetricSla");

            entity.ToTable("SlaMetricViolationForecast");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Violation)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaMetricViolationForecastView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Sla_metric_violation_forecast_view");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Metric)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MetricDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Symbol)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Violation)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaMetricViolationView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Sla_metric_violation_view");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Endpoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instance)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Metric)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MetricDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Symbol)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Violation)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlaView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Sla_view");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Metric)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Symbol)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDatetime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users__users_Id");

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
            entity.Property(e => e.DateUpdate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");
            entity.Property(e => e.LastAccess)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
