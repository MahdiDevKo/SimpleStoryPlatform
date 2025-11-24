using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class StoryPlayList : BaseDomainEntity
    {
        public string Name { get; set; }

        //relations
        public ICollection<Story> Stories { get; set; }
    }
}
