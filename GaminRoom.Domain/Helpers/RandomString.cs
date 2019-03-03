using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GaminRoom.Domain.Helpers
{
    public static class RandomString
    {
        private static Random random = new Random();
        public static string GenerateString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~`!@#$%^&*()_+=-\\|[{]}'\";:/?.>,<";
            var result = new string(
              Enumerable.Repeat(pool, length)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());

            return result;
        }
    }
}
