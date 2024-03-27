namespace StackOverflow_Tags_Api.Models
{
    public record CollectiveExternalLink
    {
        public string Link { get; init; } = string.Empty;
        public LinkType Type { get; init; }
    }
}