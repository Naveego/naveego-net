﻿/* Copyright 2015 Naveego Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Naveego.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Naveego.Security;
using System.Web;

namespace Naveego
{
    public class ApiClientBase
    {

        private JsonSerializer _serializer;

        public string ApiUrl { get; set; }

        public AuthToken AuthToken { get; set; }

        public string BasePath { get; set; }

        public string ProxyUrl { get; set; }

        public int ProxyPort { get; set; }

        protected string ToResourceUri(string uri)
        {
            if(String.IsNullOrEmpty(BasePath) == false)
            {
                uri = string.Format("{0}/{1}", BasePath, uri);
            }

            return string.Format("{0}/{1}", ApiUrl, uri);
        }

        protected void AuthenticateRequest(WebClient wc)
        {
            wc.Headers["Authorization"] = string.Format("Bearer {0}", this.AuthToken.JWT);
        }

        public bool Authenticate(string repository, string username, string password)
        {
            var url = string.Format("{0}/authenticate", ApiUrl);

            var loginRequest = new JObject(
                new JProperty("repository", repository),
                new JProperty("username", username),
                new JProperty("password", password));

            var response = ExecuteRequest<JObject>(url, new ApiRequestOptions
            {
                Method = "POST",
                Data = loginRequest,
                IsAnonymous = true
            });

            if ((bool)response["success"] == false)
            {
                return false;
            }

            var tokenStr = (string)response["token"];
            this.AuthToken = AuthToken.Parse(tokenStr);
            return true;
        }

        public bool RefreshToken()
        {
            try
            {
                var url = string.Format("{0}/token/refresh", ApiUrl);
                var response = ExecuteRequest<JObject>(url);
                var tokenStr = (string)response["token"];
                this.AuthToken = AuthToken.Parse(tokenStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AuthToken RefreshToken(AuthToken token)
        {
            var url = string.Format("{0}/token/refresh?_t={1}", ApiUrl, HttpUtility.UrlEncode(token.JWT));
            var response = ExecuteRequest<JObject>(url, new ApiRequestOptions
            {
                IsAnonymous = true 
            });
            var tokenStr = (string)response["token"];
            return AuthToken.Parse(tokenStr);
        }

        public User WhoAmI()
        {
            var url = ToResourceUri("whoami");
            return ExecuteRequest<User>(url);
        }

        public TResponse ExecuteRequest<TRequest, TResponse>(string uri, TRequest request, ApiRequestOptions options = null)
        {
            options.Data = request;
            return ExecuteRequest<TResponse>(uri, options);
        }

        protected TResponse ExecuteRequest<TResponse>(string resourceUri, ApiRequestOptions options = null)
        {
            var url = resourceUri;
            var serializer = GetSerializer();
            var requestOptions = options ?? new ApiRequestOptions();
            var queryString = new Dictionary<string, string>();
            byte[] responseBytes;
            TResponse response = default(TResponse);
            

            using (var wc = new WebClient())
            {
                if (string.IsNullOrWhiteSpace(ProxyUrl) == false)
                {
                    wc.Proxy = new WebProxy(ProxyUrl, ProxyPort);
                }
                else
                {
                    wc.Proxy = null;
                }

                // Setup some of the headers
                wc.Headers["Content-Type"] = "application/json";

                // If this isn't anonymous then lets authenticate it
                if (requestOptions.IsAnonymous == false)
                {
                    AuthenticateRequest(wc);
                }

                // If there are collection options defined
                if (requestOptions.GetCollectionOptions != null)
                {
                    if(string.IsNullOrEmpty(requestOptions.GetCollectionOptions.Query)== false)
                    {
                        queryString["q"] = System.Web.HttpUtility.UrlEncode(requestOptions.GetCollectionOptions.Query);
                    }

                    if(requestOptions.GetCollectionOptions.Page != null)
                    {
                        queryString["page"] = requestOptions.GetCollectionOptions.Page.Value.ToString();
                    }

                    if(requestOptions.GetCollectionOptions.PageSize != null)
                    {
                        queryString["pagesize"] = requestOptions.GetCollectionOptions.PageSize.Value.ToString();
                    }
                }

                if(queryString.Count > 0)
                {
                    url += "?";

                    foreach(var p in queryString)
                    {
                        url += string.Format("{0}={1}&", p.Key, System.Web.HttpUtility.UrlEncode(p.Value));
                    }

                    url = url.Substring(0, url.Length - 1);
                }

                // If we are sending data then we need to upload it
                if(requestOptions.Data != null)
                {
                    using (var ms = new MemoryStream())
                    using (var jw = new JsonTextWriter(new StreamWriter(ms)))
                    {
                        byte[] sendBytes;
                        serializer.Serialize(jw, requestOptions.Data);
                        jw.Flush();
                        sendBytes = ms.ToArray();
                        responseBytes = wc.UploadData(url, requestOptions.Method, sendBytes);
                    }
                }
                else
                {
                    responseBytes = wc.DownloadData(url);
                }

                if(responseBytes != null)
                {
                    using (var ms = new MemoryStream(responseBytes))
                    using (var jr = new JsonTextReader(new StreamReader(ms)))
                    {
                        response = serializer.Deserialize<TResponse>(jr);
                    }
                }
            }

            return response;
        }


        private JsonSerializer GetSerializer()
        {
            if(_serializer == null)
            {
                _serializer = JsonSerializerFactory.CreateSerializer();
            }

            return _serializer;
        }


    }
}
