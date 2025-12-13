using AutoMapper;
using HashidsNet;
using System;

namespace SimpleStoryPlatform.Application.Services
{
    public static class HashIdHelper
    {
        private static readonly Hashids _hashids;
        private static readonly string _salt = "mgeekmsbelkt";

        static HashIdHelper()
        {
            // اگر می‌خوای از appsettings بخونی:
             //var salt = configuration["HashIds:Salt"] ?? "lkemgsekmbet";  //need to be refactored later
            _hashids = new Hashids(_salt, minHashLength: 8);
        }

        // Encode int to string
        public static string Encode(int id)
        {
            if (id <= 0)
                return string.Empty; // یا null

            return _hashids.Encode(id);
        }

        // Decode string to int
        public static int Decode(string? hash)
        {
            if (string.IsNullOrEmpty(hash))
                return 0;

            var decoded = _hashids.Decode(hash);
            return decoded.Length > 0 ? decoded[0] : 0;
        }

        // Try decode with bool return
        public static bool TryDecode(string? hash, out int id)
        {
            id = 0;

            if (string.IsNullOrEmpty(hash))
                return false;

            var decoded = _hashids.Decode(hash);
            if (decoded.Length > 0)
            {
                id = decoded[0];
                return true;
            }

            return false;
        }

        // برای استفاده مستقیم در AutoMapper (Encode)
        public static string EncodeFromInt(int sourceId, string? destinationHash, ResolutionContext context)
        {
            return Encode(sourceId);
        }

        // برای استفاده مستقیم در AutoMapper (Decode)
        public static int DecodeFromString(string? sourceHash, int destinationId, ResolutionContext context)
        {
            return Decode(sourceHash);
        }
    }
}