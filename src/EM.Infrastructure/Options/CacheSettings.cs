namespace EM.Infrastructure.Options
{
    public record CacheSettings
    {
        public int SlidingExpiration { get; set; }
    }
}
