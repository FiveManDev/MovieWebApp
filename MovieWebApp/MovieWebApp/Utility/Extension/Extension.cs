using System.Text.Json;

namespace MovieWebApp.Utility.Extension
{
    public static class Extension
    {
        private static readonly JsonSerializerOptions serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static T GetObject<T>(this JsonElement json)
        {
            return json.Deserialize<T>(serializerOptions)!;
        }

        public static async Task<List<T?>> TaskToValue<T>(this List<Task<T?>> list)
        {
            await Task.WhenAll(list);
            var result = new List<T?>(list.Count);
            foreach (var item in list) result.Add(await item);
            return result;
        }
    }
}
