using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services.HashService
{
    public interface IHashIdService
    {
        string Encode(int id);
        int Decode(string hash);
        bool TryDecode(string hash, out int id);
    }
}
