using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.ViewModels.Category;
using TicketStationMVC.ViewModels.Account;
using TicketStationMVC.ViewModels.User;
using TicketStationMVC.ViewModels.Events;

namespace TicketStationMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Role>? Roles { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<Event>? Events { get; set; }
        public virtual DbSet<EventCategories>? EventCategories { get; set; }
        public virtual DbSet<CartItem>? CartItems { get; set; }
        public virtual DbSet<Cart>? Carts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<EventCategories>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId);

            modelBuilder.Entity<EventCategories>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.EventCategories)
                .HasForeignKey(ec => ec.CategoryId);

            modelBuilder.Entity<EventCategories>()
                .HasKey(ec => new {ec.EventId, ec.CategoryId});

            modelBuilder.Entity<Role>()
                .HasData(new Role()
                {
                    Id = 1,
                    Name = "adminuser",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Role()
                {
                    Id = 2,
                    Name="ordinaryuser",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });

            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    Name = "Administrator",
                    Username = "admin",
                    Email = "admin@admin.com",
                    RegisteredOn = DateTime.Now,
                    RoleId = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword("adminpass123"),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
                );

            base.OnModelCreating(modelBuilder);
        }
        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
        }
        public DbSet<TicketStationMVC.ViewModels.Events.EventViewVM> EventViewVM { get; set; } = default!;
        public DbSet<TicketStationMVC.ViewModels.Events.EventCreateVM> EventCreateVM { get; set; } = default!;
        public DbSet<TicketStationMVC.ViewModels.Events.EventDetailedVM> EventDetailedVM { get; set; } = default!;
        public DbSet<TicketStationMVC.ViewModels.Events.EventEditVM> EventEditVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.Category.CategoryVM> CategoryCreateVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.Account.AccountDetailsVM> AccountDetailsVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.Account.AccountChangePassVM> AccountChangePassVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.User.UserViewVM> UserViewVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.User.UserDetailsVM> UserDetailsVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.User.UserCreateVM> UserCreateVM { get; set; } = default!;
        //public DbSet<TicketStationMVC.ViewModels.User.UserEditVM> UserEditVM { get; set; } = default!;
    }
}
