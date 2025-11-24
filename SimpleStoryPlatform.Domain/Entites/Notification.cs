using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class Notification : BaseDomainEntity
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsReaded { get; set; }

        //relations
        public User ReciveUser { get; set; }
        public int UserId { get; set; }

    }
}
