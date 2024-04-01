using Microsoft.EntityFrameworkCore;

namespace StackOverflow_Tags_Api.Models
{
    [PrimaryKey(nameof(Id))]
    public record Collective
    {
        public int Id { get; init; }
        public virtual Tag Tag { get; init; }
        public virtual int TagId { get; init; }
        public string Description { get; init; } = String.Empty;
        public List<CollectiveExternalLink> Collective_External_Link { get; init; } = [];
        public string Link { get; init; } = String.Empty;
        public string Name { get; init; } = String.Empty;
        public string Slug { get; init; } = String.Empty;
        public List<String> Tags { get; init; } = [];
    }
}