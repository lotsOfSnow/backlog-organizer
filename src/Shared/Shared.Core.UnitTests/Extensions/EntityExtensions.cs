using System.Collections;
using System.Reflection;
using BacklogOrganizer.Shared.Core.Domain.DomainEvents;
using BacklogOrganizer.Shared.Core.Domain.Entities;
using FluentAssertions;

namespace BacklogOrganizer.Shared.Core.UnitTests.Extensions;

public static class EntityExtensions
{
    public static T AssertPublishedDomainEvent<T>(this EntityBase aggregate)
    {
        var domainEvent = aggregate.GetAllDomainEvents().OfType<T>().SingleOrDefault();

        return domainEvent ?? throw new Exception($"{typeof(T).Name} event not published");
    }

    public static List<T> AssertPublishedDomainEvents<T>(this EntityBase aggregate)
        where T : IDomainEvent
    {
        var domainEvents = aggregate.GetAllDomainEvents().OfType<T>().ToList();

        return domainEvents.Count == 0 ? throw new Exception($"{typeof(T).Name} event not published") : domainEvents;
    }

    public static void AssertDomainEventNotPublished<T>(this EntityBase aggregate)
        where T : IDomainEvent
    {
        var domainEvent = aggregate.GetAllDomainEvents().OfType<T>().SingleOrDefault();
        domainEvent.Should().BeNull();
    }

    private static List<IDomainEvent> GetAllDomainEvents(this EntityBase aggregate)
    {
        var domainEvents = new List<IDomainEvent>();

        if (aggregate.DomainEvents != null)
        {
            domainEvents.AddRange(aggregate.DomainEvents);
        }

        var fields = aggregate.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
            .Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

        foreach (var field in fields)
        {
            var isEntity = typeof(EntityBase).IsAssignableFrom(field.FieldType);

            if (isEntity)
            {
                var entity = field.GetValue(aggregate) as EntityBase;
                domainEvents.AddRange(entity.GetAllDomainEvents().ToList());
            }

            if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType)
                && field.GetValue(aggregate) is IEnumerable enumerable)
            {
                foreach (var entityItem in enumerable.OfType<EntityBase>())
                {
                    domainEvents.AddRange(entityItem.GetAllDomainEvents());
                }
            }
        }

        return domainEvents;
    }
}
