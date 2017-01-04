using Discipline.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;

namespace Discipline.Web.Infrastructure {
    public static class Extensions {

        public static ApplicationUser GetUser() =>
            HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(HttpContext.Current.User.Identity.GetUserId());

        public static TimeZoneInfo GetUserTimeZone() => TimeZoneInfo.FindSystemTimeZoneById(GetUser().TimeZone);

        public static DateTime ConvertToUserTimeZone(this DateTime dateTime) =>
            TimeZoneInfo.ConvertTimeFromUtc(dateTime, GetUserTimeZone());

        public static DateTime FromUnixTimeToUtc(this long ticks) =>
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ticks);
    }
}