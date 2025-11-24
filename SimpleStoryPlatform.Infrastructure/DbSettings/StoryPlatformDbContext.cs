using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using SimpleStoryPlatform.Infrastructure.Configurations.SeedingDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.DbSettings
{
    public class StoryPlatformDbContext : DbContext
    {
        private readonly ICurrentUserToken _currentUser;
        public StoryPlatformDbContext(DbContextOptions<StoryPlatformDbContext> options,
            ICurrentUserToken currentUserToken)
            : base(options)
        {
            _currentUser = currentUserToken;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //applying configurations
            modelBuilder.ApplyConfiguration(new UsersSeeding());


            //applying relations
            modelBuilder.Entity<User>()
                .HasMany(u => u.Inbox)
                .WithOne(n => n.ReciveUser)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(u => u.ReciveUser)
                .WithMany(n => n.Inbox)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.WritedStories)
                .WithOne(s => s.Writer)
                .HasForeignKey(s => s.WriterId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Warnings)
                .WithOne(r => r.ReciverUser);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Reviewer)
                .HasForeignKey(r => r.ReviewerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.StoryReleaseRequests)
                .WithOne(c => c.TargetUser)
                .HasForeignKey(r => r.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.Data)
                .WithOne(d => d.BelongTo)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.TargetStory)
                .HasForeignKey(r => r.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoryPlayList>()
                .HasMany(p => p.Stories)
                .WithOne(s => s.PlayList)
                .HasForeignKey(s => s.PlayListId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.Reports)
                .WithOne(r => r.Object)
                .HasForeignKey(r => r.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.ReleaseRequests)
                .WithOne()
                .HasForeignKey(r => r.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoryReleaseRequest>()
                .HasOne(s => s.Object)
                .WithMany()
                .HasForeignKey(s => s.StoryReportId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoryReview>()
                .HasOne(s => s.Reviewer)
                .WithMany()
                .HasForeignKey(s => s.ReviewerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoryReview>()
                .HasOne(s => s.TargetStory)
                .WithMany(s => s.Reviews)
                .HasForeignKey(s => s.StoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoryReview>()
                .HasMany(s => s.Reports)
                .WithOne(r => r.Object)
                .HasForeignKey(r => r.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoryReviewReport>()
                .HasOne(s=> s.TargetUser)
                .WithMany()
                .HasForeignKey(s=> s.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoryReport>()
                .HasOne(s => s.TargetUser)
                .WithMany()
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUser.UserGuid ?? Guid.Empty;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        //DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Warning> Warnings { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StorySection> StorySections { get; set; }
        public DbSet<StoryReview> StoryReviews { get; set; }
        public DbSet<StoryPlayList> StoryPlayLists { get; set; }

        public DbSet<StoryReport> StoryReports { get; set; }
        public DbSet<StoryReviewReport> ReviewReports { get; set; }
        public DbSet<StoryReleaseRequest> StoryReleaseRequests { get; set; }



    }
}
