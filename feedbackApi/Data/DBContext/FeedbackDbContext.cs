using feedbackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Data.Entity;

using System.Data.Entity.ModelConfiguration;

namespace feedbackApi.Data.DBContext
{
    public class FeedbackDbContext : DbContext
    {

        public FeedbackDbContext() : base("Name=FDBConstring")
        {


        }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Media> MediaItems => Set<Media>();
        public DbSet<Reply> Replies => Set<Reply>();
        public DbSet<ReviewRequest> ReviewRequests => Set<ReviewRequest>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Plan> Plans => Set<Plan>();
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>()
                .Property(p => p.MonthlyCost)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            modelBuilder.Entity<Plan>()
               .Property(p => p.DiscountonYearly)
               .HasColumnType("decimal")
               .HasPrecision(5, 2);

            base.OnModelCreating(modelBuilder);
        }



    }
}