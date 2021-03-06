using System;

namespace Zaaby.DDD.Abstractions.Infrastructure.MessageBus
{
    public class UnpublishedMessage
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string Content { get; set; }
        public DateTime PersistenceUtcTime { get; set; }
    }
}