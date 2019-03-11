using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace GaminRoom.Domain.Helpers
{
    public static class AuthTokenExtension
    {
        public static string GetToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["authtoken"];
            return token;
        }
        public static User GetUser(this HttpContext httpContext)
        {
            GamingRoomContext db = (GamingRoomContext)httpContext.RequestServices.GetService(typeof(GamingRoomContext));

            string token = httpContext.GetToken();
            User user = db.Users.FirstOrDefault(x => x.Token == token);
            return user;
        }
    }
}
