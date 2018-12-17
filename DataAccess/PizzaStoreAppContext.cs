using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess
{
    public class PizzaStoreAppContext : DbContext
    {
       public PizzaStoreAppContext(DbContextOptions<PizzaStoreAppContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Locat__628FA481");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__UserI__6383C8BA");



            });


                // using dataannotations for now
            }

    }
}
