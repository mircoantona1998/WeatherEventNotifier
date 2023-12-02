using Microsoft.EntityFrameworkCore;

namespace Userdata.Models
{
    public partial class UserdataContext : DbContext
    {
        public UserdataContext()
        {
        }

        public UserdataContext(DbContextOptions<UserdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alert> Alerts { get; set; } = null!;
        public virtual DbSet<AlertCode> AlertCodes { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Logging> Loggings { get; set; } = null!;
        public virtual DbSet<Mail> Mail { get; set; } = null!;
        public virtual DbSet<MailConfiguration> MailConfigurations { get; set; } = null!;
        public virtual DbSet<MailUser> MailUsers { get; set; } = null!;
        public virtual DbSet<TelegramConfiguration> TelegramConfigurations { get; set; } = null!;
        public virtual DbSet<TelegramMessage> TelegramMessages { get; set; } = null!;
        public virtual DbSet<TelegramUser> TelegramUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=Userdata;User ID=sa;Password=RootRoot.1; Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.IdAlert)
                    .HasName("PK_alert_IdAlert");

                entity.ToTable("Alert");

                entity.Property(e => e.CodeAlert)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CodeRecog)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.DateVisited).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeAlertNavigation)
                    .WithMany(p => p.Alerts)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.CodeAlert)
                    .HasConstraintName("alert$fk_codeAlert");
            });

            modelBuilder.Entity<AlertCode>(entity =>
            {
                entity.ToTable("Alert_code");

                entity.HasIndex(e => e.Code, "alert_code$Code_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Variable)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Job1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job");

                entity.Property(e => e.LastTimestampEnd).HasColumnType("datetime");

                entity.Property(e => e.LastTimestampStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<Logging>(entity =>
            {
                entity.ToTable("Logging");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasColumnName("dateCreate");

                entity.Property(e => e.File)
                    .HasMaxLength(512)
                    .HasColumnName("file");

                entity.Property(e => e.Level)
                    .HasMaxLength(45)
                    .HasColumnName("level");

                entity.Property(e => e.LongMessage).HasColumnName("longMessage");

                entity.Property(e => e.Message)
                    .HasMaxLength(1024)
                    .HasColumnName("message");

                entity.Property(e => e.Module)
                    .HasMaxLength(512)
                    .HasColumnName("module");

                entity.Property(e => e.RecoveryAction).HasMaxLength(45);
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
                entity.HasNoKey();

                entity.ToTable("MailConfiguration");

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
                    .HasColumnName("mail")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TelegramConfiguration>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TelegramConfiguration");

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
                    .HasColumnName("chat_id")
                    .IsFixedLength();

                entity.Property(e => e.IdUser).HasColumnName("idUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cognome)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.LastAccess).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
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
}
