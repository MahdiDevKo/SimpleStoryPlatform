using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class Story : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Preview{ get; set; }
        public bool IsPublished { get; set; }
        public bool IsStriked { get; set; }
        public bool IsVisible => IsPublished && !IsStriked;

        //relations
        public User Writer { get; set; }
        public int WriterId { get; set; }
        public ICollection<StorySection> Data { get; set; }
        public ICollection<StoryReview> Reviews { get; set; } = new List<StoryReview>();
        public StoryPlayList? PlayList { get; set; }
        public int? PlayListId { get; set; }

        public ICollection<User> InLibraryOf { get; set; } = new List<User>();
        public ICollection<StoryReport> Reports { get; set; } = new List<StoryReport>();
        public ICollection<StoryReleaseRequest> ReleaseRequests { get; set; } = new List<StoryReleaseRequest>();
    }
}
