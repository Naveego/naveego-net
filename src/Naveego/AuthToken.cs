using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace Naveego
{
    public class AuthToken
    {
        private readonly string _type;
        private readonly string _alg;
        private readonly string _jwt;
        private readonly string _headerRaw;
        private readonly string _payloadRaw;
        private readonly byte[] _signatureBytes;

        internal AuthToken(string jwt, string type, string alg, string headerRaw, string payloadRaw, byte[] signatureBytes)
        {
            if (string.IsNullOrEmpty(jwt)) throw new ArgumentNullException("jwt");
            if (signatureBytes == null) throw new ArgumentNullException("signature");
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(alg)) throw new ArgumentNullException("alg");
            if (string.IsNullOrEmpty(headerRaw)) throw new ArgumentNullException("headerRaw");
            if (string.IsNullOrEmpty(payloadRaw)) throw new ArgumentNullException("payloadRaw");
            _jwt = jwt;
            _signatureBytes = signatureBytes;
            _type = type;
            _alg = alg;
            _headerRaw = headerRaw;
            _payloadRaw = payloadRaw;
        }

        public string JWT
        {
            get { return _jwt; }
        }

        public string Algorithm
        {
            get { return _alg; }
        }

        public string Type
        {
            get { return _type; }
        }

        public string RawHeader
        {
            get { return _headerRaw; }
        }

        public string RawPayload
        {
            get { return _payloadRaw; }
        }

        public byte[] Signature
        {
            get { return _signatureBytes; }
        }

        public string Issuer { get; internal set; }

        public string Subject { get; internal set; }

        public string Audience { get; internal set; }

        public string ExpireAt { get; internal set; }

        public string NotBefore { get; internal set; }

        public string IssuedAt { get; internal set; }

        public string Role { get; set; }

        public string[] Roles
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Role))
                {
                    return new string[0];
                }

                return Role.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public string Name { get; internal set; }

        public string GivenName { get; internal set; }

        public string FamilyName { get; internal set; }

        public string PreferredUserName { get; internal set; }

        public string Email { get; internal set; }

        public string EmailVerified { get; internal set; }

        public string ZoneInfo { get; internal set; }


        [JsonIgnore]
        public string PrincipalType { get; set; }

        public static AuthToken Parse(string jwt)
        {
            if (jwt.Contains(".") == false) throw new ArgumentException("Invalid JWT");
            var jwtParts = jwt.Split(new[] { '.' });
            if (jwtParts.Length != 3) throw new ArgumentException("Invalid JWT");

            var headerBytes = Convert.FromBase64String(URLDecode(jwtParts[0]));
            var payloadBytes = Convert.FromBase64String(URLDecode(jwtParts[1]));
            var signature = Convert.FromBase64String(URLDecode(jwtParts[2]));
            JObject header;
            JObject payload;

            using (var ms = new MemoryStream(headerBytes))
            using (var jr = new JsonTextReader(new StreamReader(ms)))
            {
                header = JObject.Load(jr);
            }

            using (var ms = new MemoryStream(payloadBytes))
            using (var jr = new JsonTextReader(new StreamReader(ms)))
            {
                payload = JObject.Load(jr);
            }

            var type = (string)header["typ"];
            var alg = (string)header["alg"];
            var sat = new AuthToken(jwt, type, alg, jwtParts[0], jwtParts[1], signature);

            sat.Issuer = (string)payload["iss"];
            sat.IssuedAt = (string)payload["iat"];
            sat.Subject = (string)payload["sub"];
          
            sat.Audience = (string)payload["aud"];
            sat.ExpireAt = (string)payload["exp"];
            sat.NotBefore = (string)payload["nbf"];

            if (payload["sub_type"] != null) sat.PrincipalType = (string)payload["sub_type"];
            if (payload["name"] != null) sat.Name = (string)payload["name"];
            if (payload["given_name"] != null) sat.GivenName = (string)payload["given_name"];
            if (payload["family_name"] != null) sat.FamilyName = (string)payload["family_name"];
            if (payload["preferred_username"] != null) sat.PreferredUserName = (string)payload["preferred_username"];
            if (payload["email"] != null) sat.Email = (string)payload["email"];
            if (payload["email_verified"] != null) sat.EmailVerified = (string)payload["email_verified"];
            if (payload["role"] != null) sat.Role = (string)payload["role"];
            if (payload["zoneinfo"] != null) sat.ZoneInfo = (string)payload["zoneinfo"];

            return sat;
        }

        private static string URLDecode(string str)
        {
            // handle URL safe base64
            str = str.Replace('-', '+');
            str = str.Replace('_', '/');
            switch (str.Length % 4)
            {
                case 2: str += "=="; break;
                case 3: str += "="; break;
            }
            return str;
        }
    }
}
