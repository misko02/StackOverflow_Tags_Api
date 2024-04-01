using Microsoft.EntityFrameworkCore;

namespace StackOverflow_Tags_Api.Models
{
    [PrimaryKey(nameof(Id))]
    public record CollectiveExternalLink
    {
        public int Id { get; init; }
        public virtual Collective Collective { get; init; }
        public virtual int CollectiveId { get; init; }
        public string Link { get; init; } = string.Empty;
        public LinkType Type { get; init; }
    }
}