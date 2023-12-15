using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Userdata.Models;

public partial class UserdataContext : DbContext
{
    public UserdataContext()
    {
    }

    public UserdataContext(DbContextOptions<UserdataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<AlertCode> AlertCodes { get; set; }

    public virtual DbSet<Logging> Loggings { get; set; }

    public virtual DbSet<Mail> Mail { get; set; }

    public virtual DbSet<MailConfiguration> MailConfigurations { get; set; }

    public virtual DbSet<MailUser> MailUsers { get; set; }

    public virtual DbSet<MessageReceived> MessageReceiveds { get; set; }

    public virtual DbSet<MessageSent> MessageSents { get; set; }

    public virtual DbSet<TelegramConfiguration> TelegramConfigurations { get; set; }

    public virtual DbSet<TelegramMessage> TelegramMessages { get; set; }

    public virtual DbSet<TelegramUser> TelegramUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=Userdata;User ID=sa;Password=RootRoot.1; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.IdAlert).HasName("PK_alert_IdAlert");

            entity.ToTable("Alert");

            entity.Property(e => e.CodeAlert)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.CodeRecog)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.DateVisited)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.IdCe).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IdPod).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IdUserVisited).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsInvestigated).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsResolved).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsVisited).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.NeedInvestigation).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Priority).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.CodeAlertNavigation).WithMany(p => p.Alerts)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.CodeAlert)
                .HasConstraintName("alert$fk_codeAlert");
        });

        modelBuilder.Entity<AlertCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_alert_code_Id");

            entity.ToTable("Alert_code");

            entity.HasIndex(e => e.Code, "alert_code$Code_UNIQUE").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsEnable).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.MinValue).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Variable)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Logging>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_logging_id");

            entity.ToTable("Logging");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("dateCreate");
            entity.Property(e => e.File)
                .HasMaxLength(512)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("file");
            entity.Property(e => e.Level)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("level");
            entity.Property(e => e.LongMessage)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("longMessage");
            entity.Property(e => e.Message)
                .HasMaxLength(1024)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("message");
            entity.Property(e => e.Module)
                .HasMaxLength(512)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("module");
            entity.Property(e => e.RecoveryAction)
                .HasMaxLength(45)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Mail>(entity =>
        {
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.DateSent).HasColumnType("datetime");
            entity.Property(e => e.DateUpdate).HasColumnType("datetime");
            entity.Property(e => e.Destinatario).IsUnicode(false);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Mittente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Oggetto).IsUnicode(false);
            entity.Property(e => e.Result).IsUnicode(false);
            entity.Property(e => e.Testo).IsUnicode(false);
        });

        modelBuilder.Entity<MailConfiguration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MailConfiguration");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<MailUser>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Mail)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("mail");
        });

        modelBuilder.Entity<MessageReceived>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageR__3213E83F5620B82B");

            entity.ToTable("MessageReceived");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdOffsetResponse).HasColumnName("idOffsetResponse");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Offset).HasColumnName("offset");
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
            entity.HasKey(e => e.Id).HasName("PK__MessageS__3213E83FEAA8B022");

            entity.ToTable("MessageSent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdOffsetResponse).HasColumnName("idOffsetResponse");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Offset).HasColumnName("offset");
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

        modelBuilder.Entity<TelegramConfiguration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TelegramConfiguration");

            entity.Property(e => e.Bot)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bot");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        modelBuilder.Entity<TelegramMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Telegram");

            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.DateSent).HasColumnType("datetime");
            entity.Property(e => e.DateUpdate).HasColumnType("datetime");
            entity.Property(e => e.IdChat).IsUnicode(false);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Result).IsUnicode(false);
            entity.Property(e => e.Testo).IsUnicode(false);
        });

        modelBuilder.Entity<TelegramUser>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChatId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("chat_id");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
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
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
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
            entity.Property(e => e.Password).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
