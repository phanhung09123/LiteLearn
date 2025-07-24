using FlashCardApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FlashCardApp.Data;

public partial class LiteLearnContext : DbContext
{
    public LiteLearnContext()
    {
    }

    public LiteLearnContext(DbContextOptions<LiteLearnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DBContext"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.FlashcardId).HasName("PK__Flashcar__D36F8572333457FD");

            entity.Property(e => e.Tags).HasMaxLength(100);

            entity.HasOne(d => d.Topic).WithMany(p => p.Flashcards)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Flashcards_Topics");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Notes__EACE355F43BD1BD3");

            entity.HasOne(d => d.Topic).WithMany(p => p.Notes)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Notes_Topics");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__022E0F5D210B3AA1");

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Topics)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Topics_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C4618E68B");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E461D30E04").IsUnique();

            entity.Property(e => e.Role).HasMaxLength(10);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
