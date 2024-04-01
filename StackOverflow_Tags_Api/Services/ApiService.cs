using StackOverflow_Tags_Api.Models;
using System.IO.Compression;
using System.Text.Json;

namespace StackOverflow_Tags_Api.Services
{
    public static class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Tag>> GetTags()
        {
            try
            {
                var response = await client.GetStreamAsync("https://api.stackexchange.com/2.3/tags?order=desc&sort=popular&site=stackoverflow");

                using GZipStream decompressionStream = new GZipStream(response, CompressionMode.Decompress);
                using StreamReader reader = new StreamReader(decompressionStream);
                var tagsArray = JsonSerializer.Deserialize<dynamic>(reader.ReadToEnd());
                return JsonSerializer.Deserialize<List<Tag>>(tagsArray?.GetProperty("items").ToString());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Tag>();
            }
        }
    }
}