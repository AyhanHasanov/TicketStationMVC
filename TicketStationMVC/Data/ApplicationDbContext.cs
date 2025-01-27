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
            //makes logs more detailed
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<CartVM>();
            modelBuilder.Ignore<CartItemVM>();

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
                .HasOne(cart => cart.Owner)
                .WithMany()
                .HasForeignKey(cart => cart.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasMany(cart => cart.CartItems)
                .WithOne(cartItem => cartItem.Cart)
                .HasForeignKey(cartItem => cartItem.CartId)
                .OnDelete(DeleteBehavior.Cascade);


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
                },
                new Category()
                {
                    Id = 11,
                    Name = "Gaming tournament",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 12,
                    Name = "Fashion show",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 13,
                    Name = "Bussines networking",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 14,
                    Name = "Health and wellness",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                },
                new Category()
                {
                    Id = 15,
                    Name = "Comedy",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
                );

            modelBuilder.Entity<Event>()
                .HasData(
                new Event
                {
                    Id = 1,
                    ImageURL = @"/image/rock-music.jpg",
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
                new Event
                {
                    Id = 2,
                    ImageURL = @"/image/charity.jpg",
                    Name = "Charity Gala: \"Hope for Tomorrow\"",
                    Description = "An elegant evening of dinner, live entertainment, and fundraising for a noble cause. Join us to make a difference in the lives of those in need.",
                    DateOfEvent = DateTime.Now.AddDays(50),
                    Price = 100,
                    Quantity = 300,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 3,
                    ImageURL = @"/image/outdoor-adventure.jpg",
                    Name = "Outdoor Adventure: \"Mountain Hike Experience\"",
                    Description = "Explore the great outdoors with an exhilarating mountain hike. Perfect for nature enthusiasts and adventure seekers.",
                    DateOfEvent = DateTime.Now.AddDays(15),
                    Price = 20,
                    Quantity = 150,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 4,
                    ImageURL = @"/image/tech-conference.jpg",
                    Name = "Tech Conference: \"Innovators Unite 2025\"",
                    Description = "Join industry leaders and tech enthusiasts to discuss the latest trends in technology and innovation.",
                    DateOfEvent = DateTime.Now.AddDays(60),
                    Price = 75,
                    Quantity = 200,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 5,
                    ImageURL = @"/image/food-festival.jpg",
                    Name = "Food Festival: \"Taste the World\"",
                    Description = "A culinary journey featuring dishes from around the world. Come hungry and explore a variety of international cuisines.",
                    DateOfEvent = DateTime.Now.AddDays(25),
                    Price = 40,
                    Quantity = 500,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 6,
                    ImageURL = @"/image/comedy-night.jpg",
                    Name = "Comedy Night: \"Laugh Out Loud\"",
                    Description = "An evening of side-splitting comedy featuring top stand-up comedians. Prepare to laugh until it hurts!",
                    DateOfEvent = DateTime.Now.AddDays(10),
                    Price = 50,
                    Quantity = 100,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 7,
                    ImageURL = @"/image/art-exhibition.jpg",
                    Name = "Art Exhibition: \"Modern Visions\"",
                    Description = "Discover stunning works of art by contemporary artists. A must-visit for art lovers and collectors.",
                    DateOfEvent = DateTime.Now.AddDays(40),
                    Price = 25,
                    Quantity = 200,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 8,
                    ImageURL = @"/image/gaming-tournament.jpg",
                    Name = "Gaming Tournament: \"Battle Royale Championship\"",
                    Description = "Compete with the best gamers in a thrilling tournament. Cash prizes and bragging rights await the victors!",
                    DateOfEvent = DateTime.Now.AddDays(70),
                    Price = 30,
                    Quantity = 300,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 9,
                    ImageURL = @"/image/dancing.jpg",
                    Name = "Dance Workshop: \"Salsa Night\"",
                    Description = "Learn to dance like a pro in this fun and engaging salsa workshop. No experience needed!",
                    DateOfEvent = DateTime.Now.AddDays(20),
                    Price = 35,
                    Quantity = 50,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 10,
                    ImageURL = @"/image/family.jpg",
                    Name = "Family Fun Day: \"Adventure Park\"",
                    Description = "A fun-filled day for the whole family at the adventure park. Activities for kids and adults alike.",
                    DateOfEvent = DateTime.Now.AddDays(100),
                    Price = 60,
                    Quantity = 600,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 11,
                    ImageURL = @"/image/book-reading.jpg",
                    Name = "Book Reading: \"Authors' Evening\"",
                    Description = "Join bestselling authors for an intimate evening of book readings and discussions.",
                    DateOfEvent = DateTime.Now.AddDays(85),
                    Price = 15,
                    Quantity = 80,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 12,
                    ImageURL = @"/image/bussines.jpg",
                    Name = "Business Networking: \"Entrepreneur Meet 2025\"",
                    Description = "Connect with industry leaders and like-minded entrepreneurs at this exclusive networking event.",
                    DateOfEvent = DateTime.Now.AddDays(30),
                    Price = 50,
                    Quantity = 150,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 13,
                    ImageURL = @"/image/yoga.jpg",
                    Name = "Health & Wellness: \"Yoga Retreat\"",
                    Description = "Recharge your mind and body with a relaxing yoga retreat in a serene location.",
                    DateOfEvent = DateTime.Now.AddDays(45),
                    Price = 100,
                    Quantity = 40,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 14,
                    ImageURL = @"/image/cultural-event.jpg",
                    Name = "Cultural Festival: \"Heritage Celebrations\"",
                    Description = "Experience the richness of culture and traditions at the annual heritage festival.",
                    DateOfEvent = DateTime.Now.AddDays(95),
                    Price = 20,
                    Quantity = 400,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 15,
                    ImageURL = @"/image/film-screening.jpg",
                    Name = "Film Screening: \"Indie Movie Premiere\"",
                    Description = "Watch the premiere of a highly anticipated indie movie, followed by a Q&A with the director.",
                    DateOfEvent = DateTime.Now.AddDays(110),
                    Price = 25,
                    Quantity = 200,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                },
                new Event
                {
                    Id = 16,
                    ImageURL = @"/image/fashion.jpg",
                    Name = "Fashion Show: \"Style Icons 2025\"",
                    Description = "A glamorous evening featuring the latest collections from top fashion designers.",
                    DateOfEvent = DateTime.Now.AddDays(130),
                    Price = 75,
                    Quantity = 150,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = true
                }
                );

            modelBuilder.Entity<EventCategories>()
               .HasData(
                new EventCategories() { EventId = 1, CategoryId = 1 },
                new EventCategories() { EventId = 1, CategoryId = 5 },

                new EventCategories() { EventId = 2, CategoryId = 2 },
                new EventCategories() { EventId = 2, CategoryId = 10 },

                new EventCategories() { EventId = 3, CategoryId = 3 },
                new EventCategories() { EventId = 3, CategoryId = 4 },

                new EventCategories() { EventId = 4, CategoryId = 6 },
                new EventCategories() { EventId = 4, CategoryId = 13 },
                new EventCategories() { EventId = 4, CategoryId = 9 },

                new EventCategories() { EventId = 5, CategoryId = 10 },
                new EventCategories() { EventId = 5, CategoryId = 8 },

                new EventCategories() { EventId = 6, CategoryId = 11 },
                new EventCategories() { EventId = 6, CategoryId = 5 },

                new EventCategories() { EventId = 7, CategoryId = 14 },
                new EventCategories() { EventId = 7, CategoryId = 9 },

                new EventCategories() { EventId = 8, CategoryId = 12 },
                new EventCategories() { EventId = 8, CategoryId = 4 },

                new EventCategories() { EventId = 9, CategoryId = 15 },
                new EventCategories() { EventId = 9, CategoryId = 8 },

                new EventCategories() { EventId = 10, CategoryId = 4 },
                new EventCategories() { EventId = 10, CategoryId = 6 },

                new EventCategories() { EventId = 11, CategoryId = 1 },
                new EventCategories() { EventId = 11, CategoryId = 5 },

                new EventCategories() { EventId = 12, CategoryId = 7 },
                new EventCategories() { EventId = 12, CategoryId = 8 },

                new EventCategories() { EventId = 13, CategoryId = 13 },
                new EventCategories() { EventId = 13, CategoryId = 6 },

                new EventCategories() { EventId = 14, CategoryId = 10 },
                new EventCategories() { EventId = 14, CategoryId = 5 },

                new EventCategories() { EventId = 15, CategoryId = 8 },
                new EventCategories() { EventId = 15, CategoryId = 5 },

                new EventCategories() { EventId = 16, CategoryId = 2 },
                new EventCategories() { EventId = 16, CategoryId = 10 }
               );


           base.OnModelCreating(modelBuilder);
        }
        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
        }
    }
}
