using Newtonsoft.Json;

namespace Lambda.Common.Extensions
{
    public static class Extensions
    {
        public static string ToJson(this object data)
        {
            if (data is string str) return str;

            try
            {
                var serializedObj = JsonConvert.SerializeObject(data, new JsonSerializerSettings(){ NullValueHandling = NullValueHandling.Ignore});
                return serializedObj;
            }
            catch (JsonSerializationException ex)
            {
                return ex.ToString();
            }
        }
    }
}
