﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data.Configurations;
using PrivateLibrary.Data.Models;

namespace PrivateLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<TakenBook> TakenBooks { get; set; } = default!;

        public DbSet<Employee> Employees { get; set; } = default!;

        public DbSet<Reader> Readers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Reader>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
        }
    }
}