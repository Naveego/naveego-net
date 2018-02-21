using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Naveego.Tests
{
    [TestFixture]
    public class AuthTokenTests
    {

        [Test]
        public void ShouldParseURLEncodedToken()
        {
            var tokenStr = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlZvMFdlYTM5TnYiLCJ0eXAiOiJKV1QifQ.eyJFbWFpbCI6bnVsbCwiTmFtZSI6IkRlcGxveWluZyIsImF1ZCI6Im5hdmVlZ28iLCJlbWFpbF92ZXJpZmllZCI6bnVsbCwiZXhwIjoiMTUxNDQ3NjE5MCIsImZhbWlseV9uYW1lIjpudWxsLCJnaXZlbl9uYW1lIjpudWxsLCJpYXQiOiIxNTE0MzAzMzkwIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5uYXZlZWdvLmNvbSIsIm5iZiI6IjE1MTQzMDMzOTAiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOm51bGwsInJvbGUiOiJzeW5jLmNsaWVudCIsInN1YiI6IkI0Qzc0OTJELTNGMjMtNDJFQy1BQzlDLUI5NzlFQ0ZCMzUzOCIsInN1Yl90eXBlIjoic3luY2NsaWVudCIsInRpZCI6ImRlbW8iLCJ6b25laW5mbyI6bnVsbH0.KzEIwf05JdqO_8t6f2DP1OZsRWK8bEb2G6uE4P5L4iq9Ueq2OjqIKaVg4fuKGkbluEjY1kI_xu517a0nT6-c5PP2wPKBDB3G5QcaEV1jxfrURedz-1zxx4A8xQO6XK5MsuM3mLs0rj0E9OJh5YnqvyYd_O6vZE83A030iwMmxZ_VzY4VKLLpJx7AyoWI44ayXnoGHLg9C4LuRWc7SE86aNA_mF48EPYfBbnW9juJOTrNjqsBQQyN_kn2iEoflEmX62cM_cbyBVn6HqTMiOzhDpgQkj5dcoLHnOuNDwXKPxjMQ3BMZarMe4ZZMn44bY4aUM0tqiX3L6PraOG_KeYrkA";
            var token = AuthToken.Parse(tokenStr);
        }

        [Test]
        public void ShouldParseNonURLEncodedTokane()
        {
            var tokenStr = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2xvZ2luLm5hdmVlZ28uY29tIiwic3ViIjoiNDFiNzJmOGUtMGVjMS00ODZjLWFjZWUtZjJiNzc5MGRjYjY3IiwiYXVkIjoibmF2ZWVnbyIsInRpZCI6InRlbG9naWNhbCIsImV4cCI6IjE1MTg3MDgyMjciLCJuYmYiOiIxNTE4NTM1NDI3IiwiaWF0IjoiMTUxODUzNTQyNyIsInJvbGUiOiJzeW5jLmNsaWVudCIsIk5hbWUiOiJEZXBsb3lpbmciLCJzdWJfdHlwZSI6InN5bmNjbGllbnQifQ==.L1VDzhN3WnXn58zR14tGJ+hVz9Nf7J4dACeAEhhV3oQ=";
            var token = AuthToken.Parse(tokenStr);
            Assert.That(token.Audience, Is.EqualTo("naveego"));
            Assert.That(token.TenantId, Is.EqualTo("telogical"));
        }
        

    }
}
