using AutoMapper;
using HashidsNet;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services.HashService
{
    public class HashIdService : IHashIdService
    {
        private readonly Hashids _hashids;
        public HashIdService(IConfiguration configuration)
        {
            var salt = configuration["HashIds:Salt"] ?? "lkemgsekmbet";     //randomized :D
            _hashids = new Hashids(salt, minHashLength: 8);
        }
        public string Encode(int id) => _hashids.Encode(id);

        public int Decode(string hash) => _hashids.Decode(hash)[0];

        public bool TryDecode(string hash, out int id)
        {
            var decoded = _hashids.Decode(hash);
            if (decoded.Length > 0)
            {
                id = decoded[0];
                return true;
            }
            id = 0;
            return false;
        }
    }




    public class HashIdResolver : IMemberValueResolver<object, object, int, string>
    {
        private readonly IHashIdService _hashIdService;

        public HashIdResolver(IHashIdService hashIdService)
        {
            _hashIdService = hashIdService;
        }

        public string Resolve(object source, object destination, int sourceMember, string destMember, ResolutionContext context)
        {
            return _hashIdService.Encode(sourceMember);
        }
    }

    // Resolver برای تبدیل HashId به int (فقط در صورت وجود)
    public  class HashIdReverseResolver : IMemberValueResolver<object, object, string, int>
    {
        private readonly IHashIdService _hashIdService;

        public HashIdReverseResolver(IHashIdService hashIdService)
        {
            _hashIdService = hashIdService;
        }

        public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(sourceMember))
                return 0; // New entity

            if (_hashIdService.TryDecode(sourceMember, out int id))
                return id;

            //throw new InvalidOperationException("Invalid hash id");
            return 0;
        }
    }
}
