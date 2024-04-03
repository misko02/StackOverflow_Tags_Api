using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackOverflow_Tags_Api.Models;
using System.IO.Compression;

namespace StackOverflow_Tags_Api.Services
{
    public static class ApiService
    {
        private static readonly HttpClient client = new();

        private static async Task LoadTags(List<Tag> tags, int page = 1)
        {
            try
            {
                var response = await client.GetStreamAsync($"https://api.stackexchange.com//2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                using var decompressionStream = new GZipStream(response, CompressionMode.Decompress);
                using var reader = new StreamReader(decompressionStream);
                var tagsArray = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                tags.AddRange(JsonConvert.DeserializeObject<List<Tag>>(tagsArray?.items.ToString()));
                if ((bool)tagsArray?.has_more && page <= 10)
                {
                    await LoadTags(tags, page + 1);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine(e.Message);
                return;
            }
        }

        public static async Task<List<Tag>> GetTags()
        {
            var tags = new List<Tag>();
            await LoadTags(tags);
            return tags;
        }
    }
}