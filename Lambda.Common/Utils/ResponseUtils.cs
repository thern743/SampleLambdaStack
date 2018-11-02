namespace Lambda.Common.Utils
{
    public static class ResponseUtils
    {
        public static bool IsSuccessfulStatusCode(int statusCode) => statusCode >= 200 && statusCode < 300;
    }
}