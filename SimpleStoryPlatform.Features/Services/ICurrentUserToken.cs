using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface ICurrentUserToken
    {
        Guid? UserGuid { get; }
        string? UserRole { get; }
        string? UserName { get; }
    }
}
