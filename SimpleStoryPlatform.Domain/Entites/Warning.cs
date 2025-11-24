using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class Warning : BaseDomainEntity
    {
        public string Subject { get; set; }
        public string Reason { get; set; }
        public string? Details { get; set; }

        //relations
        public User ReciverUser { get; set; }
        public int UserId { get; set; }
    }
}
