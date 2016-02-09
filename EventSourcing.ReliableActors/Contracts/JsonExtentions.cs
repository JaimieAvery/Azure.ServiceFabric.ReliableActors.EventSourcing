using Newtonsoft.Json;

namespace Contracts
{
    public static class JsonExtentions
    {
        public static string ToJson(this IMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}
