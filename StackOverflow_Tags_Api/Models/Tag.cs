using Microsoft.EntityFrameworkCore;

namespace StackOverflow_Tags_Api.Models
{
    [PrimaryKey(nameof(Id))]
    public record Tag
    {
        public int Id { get; init; }
        public List<Collective> Collectives { get; init; } = [];
        public int Count { get; init; }
        public bool HasSynonyms { get; init; }
        public bool IsModeratorOnly { get; init; }
        public bool IsRequired { get; init; }
        public string LastActivityDate { get; init; } = String.Empty;
        public string Name { get; init; } = String.Empty;
        public List<String> Synonyms { get; init; } = [];
        public int UserId { get; init; }
    }
}