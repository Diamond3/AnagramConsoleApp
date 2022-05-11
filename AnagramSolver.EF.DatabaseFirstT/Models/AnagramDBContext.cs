using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class AnagramDBContext : DbContext
    {
        public AnagramDBContext()
        {
        }

        public AnagramDBContext(DbContextOptions<AnagramDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=LT-LIT-SC-0597;initial catalog=AnagramDB;trusted_connection=true;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.HasKey(e => e.WordId)
                    .HasName("PK__CachedWo__2C20F066DE4BF75F");

                entity.ToTable("CachedWord");

                entity.Property(e => e.Anagram)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Word)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("Word");

                entity.HasIndex(e => e.SortedForm, "idx_sortedWordForm");

                entity.Property(e => e.FirstForm)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Form)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SecondForm)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SortedForm)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
