using System;
using System.Collections.Generic;

namespace Zaaby.DDD.Abstractions.Domain
{
    public interface IEntity
    {
    }

    public interface IEntity<out TId> : IEntity
    {
        TId Id { get; }
    }

    public class Entity : IEntity
    {
        public int Version { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void PublishEvent(Func<IDomainEvent> domainEventFunc)
        {
            Version++;
            _domainEvents.Add(domainEventFunc());
        }
    }

    public class Entity<TId> : Entity, IEntity<TId>
    {
        public TId Id { get; protected set; }
    }
}