using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.ViewModels.Cart;

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
                .HasKey(ec => new { ec.EventId, ec.CategoryId });

            modelBuilder.Entity<Cart>()
        .HasOne(cart => cart.Owner) // Cart has a navigation property to User
        .WithMany() // User does NOT have a navigation property to Cart
        .HasForeignKey(cart => cart.OwnerId) // Foreign key in Cart
        .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete


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
                    Name = "ordinaryuser",
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

            modelBuilder.Entity<Category>()
                .HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Music Concert",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 2,
                    Name = "Sport",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 3,
                    Name = "Theater",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 4,
                    Name = "Art",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 5,
                    Name = "Festival",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 6,
                    Name = "Workshop",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 7,
                    Name = "Charity",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 8,
                    Name = "Food & Drinks",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 9,
                    Name = "Trade show",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 10,
                    Name = "Family & kids",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });

            modelBuilder.Entity<Event>()
                .HasData(
                new Event()
                {
                    Id = 1,
                    ImageURL = @"/image/rock.jpg",
                    Name = "Music Concert: \"Summer Rock Fest\"",
                    Description = "A high-energy music festival featuring top rock bands from around the world. Enjoy live performances, food stalls, and exciting activities for all ages. Come for the music, stay for the unforgettable atmosphere!",
                    DateOfEvent = DateTime.Now.AddDays(35),
                    Price = 60,
                    Quantity = 250,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event()
                {
                    Id = 2,
                    ImageURL = @"/image/football.jpg",
                    Name = "Basketball Game: \"City Championship Finals\"",
                    Description = "Witness the most thrilling basketball match of the season as the two best teams in the city battle it out for the championship title. Come support your local team and experience the adrenaline rush of the final game!",
                    DateOfEvent = DateTime.Now.AddDays(105),
                    Price = 120,
                    Quantity = 400,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true

                },
                new Event()
                {
                    Id = 3,
                    ImageURL = @"/image/william.jpeg",
                    Name = "Theater Play: \"Shakespeare Under the Stars\"",
                    Description = "An outdoor performance of one of Shakespeare's greatest plays, \"A Midsummer Night's Dream.\" Enjoy a magical evening under the stars, with actors performing in a beautiful outdoor setting surrounded by nature.",
                    DateOfEvent = DateTime.Now.AddDays(125),
                    Price = 90,
                    Quantity = 50,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event()
                {
                    Id = 4,
                    ImageURL = @"/image/ai.jpg",
                    Name = "Tech Conference: \"Future of AI Summit\"",
                    Description = "Join industry leaders, innovators, and tech enthusiasts at the Future of AI Summit. This conference covers the latest advancements in artificial intelligence, machine learning, and their impact on industries like healthcare, finance, and entertainment.",
                    DateOfEvent = DateTime.Now.AddDays(25),
                    Price = 20,
                    Quantity = 70,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event()
                {
                    Id = 5,
                    ImageURL = @"/image/disney.jpg",
                    Name = "Family Fun Day: \"Magic Kingdom Adventure\"",
                    Description = "A day of family-friendly activities, games, and entertainment at the Magic Kingdom. Kids can enjoy carnival rides, face painting, storytelling sessions, and more. Perfect for families looking to spend quality time together.",
                    DateOfEvent = DateTime.Now.AddDays(295),
                    Price = 200,
                    Quantity = 700,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                }
                );

            modelBuilder.Entity<EventCategories>()
                .HasData(
                new EventCategories()
                {
                    EventId = 1,
                    CategoryId = 1
                },
                new EventCategories()
                {
                    EventId = 2,
                    CategoryId = 2
                },
                new EventCategories()
                {
                    EventId = 3,
                    CategoryId = 3
                },
                new EventCategories()
                {
                    EventId = 3,
                    CategoryId = 4
                },
                new EventCategories()
                {
                    EventId = 4,
                    CategoryId = 6
                },
                new EventCategories()
                {
                    EventId = 4,
                    CategoryId = 7
                },
                new EventCategories()
                {
                    EventId = 5,
                    CategoryId = 8
                },
                new EventCategories()
                {
                    EventId = 5,
                    CategoryId = 10
                });

            base.OnModelCreating(modelBuilder);
        }
        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
        }
    }
}
