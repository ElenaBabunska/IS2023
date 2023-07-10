using _193089.Domain.DomainModels;
using _193089.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _193089.Repository
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketsInCart> TicketsInCarts { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketsInOrder> TicketInOrders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TicketsInCart>().HasKey(c => new { c.CartId, c.TicketId });
            builder.Entity<TicketsInOrder>().HasKey(c => new { c.OrderId, c.TicketId });

            builder.Entity<TicketsInCart>()
                 .HasOne(z => z.Ticket)
                 .WithMany(z => z.TicketsInCarts)
                 .HasForeignKey(z => z.TicketId);

            builder.Entity<TicketsInCart>()
               .HasOne(z => z.ShoppingCart)
               .WithMany(z => z.TicketsInCarts)
               .HasForeignKey(z => z.CartId);

            builder.Entity<ShoppingCart>()
                  .HasOne<ApplicationUser>(z => z.CartOwner)
                  .WithOne(z => z.UserCart)
                  .HasForeignKey<ShoppingCart>(z => z.OwnerId);


            builder.Entity<TicketsInOrder>()
                .HasOne(z => z.OrderedTicket)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<TicketsInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.Tickets)
                .HasForeignKey(z => z.OrderId);

        } 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}