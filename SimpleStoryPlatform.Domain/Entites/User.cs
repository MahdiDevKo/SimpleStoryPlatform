using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class User : BaseDomainEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<Guid>? Library { get; set; }

        public bool IsBan { get; set; }
        public DateTime? UnBanDate { get; set; }
        public string? BanReason { get; set; }

        //relations
        public ICollection<Notification>? Inbox { get; set; }
        public ICollection<Warning>? Warnings { get; set; }
        public ICollection<Story>? WritedStories { get; set; }
        public ICollection<StoryReview> Comments { get; set; }
        public ICollection<StoryReleaseRequest> StoryReleaseRequests { get; set; } = new List<StoryReleaseRequest>();


    }
}
