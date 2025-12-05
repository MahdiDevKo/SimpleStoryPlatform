using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.Configurations.SeedingDatabase
{
    public class UsersSeeding : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = 1,
                    PublicId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Username = "MahdiDevKo",
                    Email = "mahdi8414m@gmail.com",
                    FirstName = "Mahdi",
                    LastName = "Heidari",
                    Role = "owner",
                    Password = "passworld",
                    CreatedAt = DateTime.Parse("2025/1/1"),
                    BanReason = null,
                    CreatedBy = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Inbox = null,
                    IsBan = false,
                    UnBanDate = null,
                    WritedStories = null,
                    Library = new List<Guid>(),
                    Warnings = null
                },
                new User()
                {
                    Id = 2,
                    PublicId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Username = "AdminAli",
                    Email = "myOskoolAdmin@gmail.com",
                    FirstName = "Ali",
                    LastName = "BaBaHaji",
                    Role = "admin",
                    Password = "12341234",
                    CreatedAt = DateTime.Parse("2025/1/1"),
                    BanReason = null,
                    CreatedBy = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Inbox = null,
                    IsBan = false,
                    UnBanDate = null,
                    WritedStories = null,
                    Library = new List<Guid>(),
                    Warnings = null
                }
            );
        }
    }
}
