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

        public int ExpireAt { get; internal set; }

        public int NotBefore { get; internal set; }

        public int IssuedAt { get; internal set; }

        public string[] Roles { get; internal set; }

        public string Name { get; internal set; }

        public string GivenName { get; internal set; }

        public string FamilyName { get; internal set; }

        public string PreferredUserName { get; internal set; }

        public string Email { get; internal set; }

        public bool EmailVerified { get; internal set; }

        public string ZoneInfo { get; internal set; }

        [JsonIgnore]
        public string PrincipalId
        {
            get
            {
                var indexOfSlash = this.Subject.IndexOf('/');
                if (indexOfSlash >= 0)
                {
                    return this.Subject.Substring(indexOfSlash + 1);
                }

                return null;
            }
        }

        [JsonIgnore]
        public string PrincipalType
        {
            get
            {
                var indexOfSlash = this.Subject.IndexOf('/');
                if (indexOfSlash >= 0)
                {
                    return this.Subject.Substring(0, indexOfSlash);
                }

                return null;
            }
        }

        public static AuthToken Parse(string jwt)
        {
            if (jwt.Contains(".") == false) throw new ArgumentException("Invalid JWT");
            var jwtParts = jwt.Split(new[] { '.' });
            if (jwtParts.Length != 3) throw new ArgumentException("Invalid JWT");

            var headerBytes = Convert.FromBase64String(jwtParts[0]);
            var payloadBytes = Convert.FromBase64String(jwtParts[1]);
            var signature = Convert.FromBase64String(jwtParts[2]);
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
            sat.IssuedAt = (int)payload["iat"];
            sat.Subject = (string)payload["sub"];
            sat.Audience = (string)payload["aud"];
            sat.ExpireAt = (int)payload["exp"];
            sat.NotBefore = (int)payload["nbf"];

            if (payload["name"] != null) sat.Name = (string)payload["name"];
            if (payload["given_name"] != null) sat.GivenName = (string)payload["given_name"];
            if (payload["family_name"] != null) sat.FamilyName = (string)payload["family_name"];
            if (payload["preferred_username"] != null) sat.PreferredUserName = (string)payload["preferred_username"];
            if (payload["email"] != null) sat.Email = (string)payload["email"];
            if (payload["email_verified"] != null) sat.EmailVerified = (bool)payload["email_verified"];
            if (payload["roles"] != null) sat.Roles = ((JArray)payload["roles"]).Select(i => (string)i).ToArray();
            if (payload["zoneinfo"] != null) sat.ZoneInfo = (string)payload["zoneinfo"];

            return sat;
        }
    }
}
