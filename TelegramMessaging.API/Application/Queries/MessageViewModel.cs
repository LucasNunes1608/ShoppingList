namespace TelegramMessaging.API.Application.Queries
{
    public record Message
    {
        public int Id { get; init; }
        public string messagetext { get; init; }
        public DateTime timestamp { get; init; }
    }
}
