using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace StackOverflow_Tags_Api.Models
{
    [PrimaryKey(nameof(Id))]
    public record Tag
    {
        public int Id { get; init; }
        public List<Collective> Collectives { get; init; } = [];
        public int Count { get; init; }
        [JsonProperty("has_synonyms")]
        public bool HasSynonyms { get; init; }
        [JsonProperty("is_moderator_only")]
        public bool IsModeratorOnly { get; init; }
        [JsonProperty("is_required")]
        public bool IsRequired { get; init; }
        [JsonProperty("last_activity_date")]
        public string LastActivityDate { get; init; } = String.Empty;
        public string Name { get; init; } = String.Empty;
        public List<string> Synonyms { get; init; } = [];
        public int UserId { get; init; }
    }
}