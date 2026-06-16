using BoscoAFH.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

namespace BoscoAFH.Entities.Data;

public partial class BoscoAFHDbContext : DbContext
{

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted); 

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Loop through all properties of each entity
            foreach (var property in entityType.GetProperties())
            {
                // Apply value converter for DateTime properties
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(), // Convert to UTC when saving to DB
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Specify UTC when reading from DB
                    ));
                }

                // Optional: Handle nullable DateTime properties
                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new ValueConverter<DateTime?, DateTime?>(
                        v => v.HasValue ? v.Value.ToUniversalTime() : v, // Convert to UTC if value exists
                        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v // Specify UTC if value exists
                    ));
                }
            }
        }

    }
}
