using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Lambda.Common.Interfaces;
using Newtonsoft.Json;

namespace Lambda.Common.Extensions
{
    public static class HttpClientExtensions
    {
        public const string ApiKeyName = "apikey";
        public const string ApiVersionName = "api_version";
        public const string ContentType = "application/json";

        public static HttpRequestMessage BuildPostRequest(this IHttpClient client, Uri endPointUri, object jsonContent, string apiKey, string apiVersion = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = endPointUri,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType),
            };
            
            AddDefaultHeaders(client, request, apiKey, apiVersion, ApiKeyName);
            return request;
        }

        public static HttpRequestMessage BuildPostRequest(this IHttpClient client, Uri endPointUri, object jsonContent, AuthenticationHeaderValue authHeader,
            string apiVersion = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = endPointUri,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType),
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildPostRequest(this IHttpClient client, string endPointUrl, object jsonContent, string apiKey, string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType)
            };

            AddDefaultHeaders(client, request, apiKey, apiVersion, ApiKeyName);
            return request;
        }

        public static HttpRequestMessage BuildPostRequest(this IHttpClient client, string endPointUrl, object jsonContent, AuthenticationHeaderValue authHeader,
            string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);
            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType),
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildPostRequestFromStringContent(this IHttpClient client, string endPointUrl, string jsonContent, string apiKey, string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(jsonContent, Encoding.UTF8, ContentType),
            };

            AddDefaultHeaders(client, request, apiKey, apiVersion, ApiKeyName);
            return request;
        }

        public static HttpRequestMessage BuildPostRequestFromStringContent(this IHttpClient client, string endPointUrl, string jsonContent, AuthenticationHeaderValue authHeader,
            string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(jsonContent, Encoding.UTF8, ContentType),
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildGetRequest(this IHttpClient client, string endPointUrl, AuthenticationHeaderValue authHeader, string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Get,
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildGetRequest(this IHttpClient client, Uri endPointUri, AuthenticationHeaderValue authHeader, string apiVersion = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = endPointUri,
                Method = HttpMethod.Get,
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildGetRequest(this IHttpClient client, string endPointUrl, Guid traceContextId, string apiKey, string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Get,
            };

            AddDefaultHeaders(client, request, apiKey, apiVersion, ApiKeyName);

            if (!string.IsNullOrEmpty(traceContextId.ToString()))
            {
                request.Headers.Add("CpasContextId", traceContextId.ToString());
            }

            return request;
        }

        public static HttpRequestMessage BuildGetRequest(this IHttpClient client, Uri endPointUri, string apiKey, string apiVersion = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = endPointUri,
                Method = HttpMethod.Get,
            };

            AddDefaultHeaders(client, request, apiKey, apiVersion, ApiKeyName);
            return request;
        }

        public static HttpRequestMessage BuildPostRequestFromStringContent(this IHttpClient client, string endPointUrl, object jsonContent, string apiKey, string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType),
            };

            var authHeader = CreateAuthHeader(apiKey);
            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        public static HttpRequestMessage BuildPostRequestFromStringContent(this IHttpClient client, string endPointUrl, object jsonContent, AuthenticationHeaderValue authHeader,
            string apiVersion = null)
        {
            if (!Uri.TryCreate(endPointUrl, UriKind.Absolute, out var endpoint)) throw new UriFormatException(endPointUrl);

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, ContentType),
            };

            AddDefaultHeaders(client, request, authHeader, apiVersion);
            return request;
        }

        private static void AddDefaultHeaders(IHttpClient client, HttpRequestMessage request, AuthenticationHeaderValue authHeader, string apiVersion)
        {
            if (authHeader != null) request.Headers.Authorization = authHeader;
            var mediaType = BuildApiVersionHeader(apiVersion);
            if (mediaType != null) request.Headers.Accept.Add(mediaType);
        }

        private static void AddDefaultHeaders(IHttpClient client, HttpRequestMessage request, string apiKey, string apiVersion, string scheme = null)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", apiKey);
            }
            else
            {
                var authHeader = CreateAuthHeader(apiKey, scheme);
                if (authHeader != null) request.Headers.Authorization = authHeader;
            }

            var mediaType = BuildApiVersionHeader(apiVersion);
            if (mediaType != null) request.Headers.Accept.Add(mediaType);
        }

        public static AuthenticationHeaderValue CreateAuthHeader(string apiKey, string scheme = null)
        {
            try
            {
                if (string.IsNullOrEmpty(apiKey)) return null;

                var encodedApiKey = apiKey;

                try
                {
                    var output = Convert.FromBase64String(apiKey);
                }
                catch (FormatException)
                {
                    encodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey));
                }

                return string.IsNullOrWhiteSpace(scheme)
                    ? new AuthenticationHeaderValue(encodedApiKey)
                    : new AuthenticationHeaderValue(scheme, encodedApiKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public static MediaTypeWithQualityHeaderValue BuildApiVersionHeader(string apiVersion, string contentType = null, string apiVersionName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(apiVersion)) return null;
                var mediaType = new MediaTypeWithQualityHeaderValue(contentType ?? ContentType);
                mediaType.Parameters.Add(new NameValueHeaderValue(apiVersionName ?? ApiVersionName, apiVersion));
                return mediaType;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

    }
}
