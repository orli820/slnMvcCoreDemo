﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace prjMvcCoreDemo.Models
{
    public partial class dbDemoContext : DbContext
    {
        public dbDemoContext()
        {
        }

        public dbDemoContext(DbContextOptions<dbDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCustomer> TCustomers { get; set; }
        public virtual DbSet<TProduct> TProducts { get; set; }
        public virtual DbSet<TShoppingCart> TShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbDemo;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tCustomer");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(50)
                    .HasColumnName("fAddress");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPassword)
                    .HasMaxLength(50)
                    .HasColumnName("fPassword");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(50)
                    .HasColumnName("fPhone");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tProduct");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FCost)
                    .HasColumnType("money")
                    .HasColumnName("fCost");

                entity.Property(e => e.FImage)
                    .HasColumnType("image")
                    .HasColumnName("fImage");

                entity.Property(e => e.FImagePath)
                    .HasMaxLength(200)
                    .HasColumnName("fImagePath");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPrice)
                    .HasColumnType("money")
                    .HasColumnName("fPrice");

                entity.Property(e => e.FQty).HasColumnName("fQty");
            });

            modelBuilder.Entity<TShoppingCart>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tShoppingCart");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.FCount).HasColumnName("fCount");

                entity.Property(e => e.FCustomer).HasColumnName("fCustomer");

                entity.Property(e => e.FDate)
                    .HasMaxLength(50)
                    .HasColumnName("fDate");

                entity.Property(e => e.FPrice)
                    .HasColumnType("money")
                    .HasColumnName("fPrice");

                entity.Property(e => e.FProduct).HasColumnName("fProduct");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
