using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;

namespace DataManager.Models
{
    public partial class HwProj_DBContext : DbContext
    {
        public virtual DbSet<Userdata> Userdata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=HwProj_DB;Username=username;Password=pass");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Userdata>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("userdata");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("fullname");

                entity.Property(e => e.IsTeacher)
                    .HasColumnName("isTeacher")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });
        }
    }
}
