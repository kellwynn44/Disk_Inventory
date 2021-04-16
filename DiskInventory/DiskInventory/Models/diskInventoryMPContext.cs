using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class diskInventoryMPContext : DbContext
    {
        public diskInventoryMPContext()
        {
        }

        public diskInventoryMPContext(DbContextOptions<diskInventoryMPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; }
        public virtual DbSet<MediaArtist> MediaArtists { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //    optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=diskInventoryMP;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.ArtistTypeId).HasColumnName("ArtistTypeID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Artist__ArtistTy__398D8EEE");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("ArtistType");

                entity.Property(e => e.ArtistTypeId).HasColumnName("ArtistTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("Borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("BorrowerID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.ToTable("DiskHasBorrower");

                entity.Property(e => e.DiskHasBorrowerId).HasColumnName("DiskHasBorrowerID");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BorrowerId).HasColumnName("BorrowerID");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate()+(14))");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.ReturnedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskHasBo__Borro__33D4B598");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskHasBo__Media__34C8D9D1");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.ToTable("LoanStatus");

                entity.Property(e => e.LoanStatusId).HasColumnName("LoanStatusID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MediaArtist>(entity =>
            {
                entity.ToTable("MediaArtist");

                entity.HasIndex(e => new { e.MediaId, e.ArtistId }, "UQ__MediaArt__A095B3192E101D41")
                    .IsUnique();

                entity.Property(e => e.MediaArtistId).HasColumnName("MediaArtistID");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.MediaArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MediaArti__Artis__3D5E1FD2");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.MediaArtists)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MediaArti__Media__3E52440B");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("MediaType");

                entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__Media__B2C2B5AFB4C7D9DB");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.LoanStatusId)
                    .HasColumnName("LoanStatusID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaTypeId)
                    .HasColumnName("MediaTypeID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Media__GenreID__2B3F6F97");

                entity.HasOne(d => d.LoanStatus)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.LoanStatusId)
                    .HasConstraintName("FK__Media__LoanStatu__2E1BDC42");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Media__MediaType__2C3393D0");
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ArtistID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
