﻿using Microservice.Domain.Entities;
using Microservice.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure
{
    public class DbContext(DbContextOptions<DbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<Entity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed the data as part of initial migration
            modelBuilder
                .Entity<Entity>()
                .HasData(EntityData.Seed());
        }
    }
}