﻿using avwx_metar_service;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Metar> Metars { get; set; } // Replace `Metar` with your actual model

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure owned types or complex relationships here if needed
        modelBuilder.Entity<Metar>(entity =>
        {
            entity.OwnsOne(m => m.Time);
            entity.OwnsOne(m => m.WindDirection);
            entity.OwnsOne(m => m.WindSpeed);
            entity.OwnsOne(m => m.Visibility, v =>
            {
                v.Property(p => p.Value).HasDefaultValue(-1);
            });
            entity.OwnsOne(m => m.Altimeter);
            entity.OwnsOne(m => m.Temperature);
            entity.OwnsOne(m => m.Dewpoint);
        });

        base.OnModelCreating(modelBuilder);
    }

}