using System.Collections;
using System.Reflection;
using BacklogOrganizer.Shared.Core.Domain.DomainEvents;
using BacklogOrganizer.Shared.Core.Domain.Entities;
using FluentAssertions;

namespace BacklogOrganizer.Shared.Core.UnitTests.Extensions;

public static class EntityExtensions
{
    public static T AssertPublishedDomainEvent<T>(this Entity aggregate)
    {
        var domainEvent = aggregate.GetAllDomainEvents().OfType<T>().SingleOrDefault();

        return domainEvent ?? throw new Exception($"{typeof(T).Name} event not published");
    }

    public static List<T> AssertPublishedDomainEvents<T>(this Entity aggregate)
        where T : IDomainEvent
    {
        var domainEvents = aggregate.GetAllDomainEvents().OfType<T>().ToList();

        return domainEvents.Count == 0 ? throw new Exception($"{typeof(T).Name} event not published") : domainEvents;
    }

    public static void AssertDomainEventNotPublished<T>(this Entity aggregate)
        where T : IDomainEvent
    {
        var domainEvent = aggregate.GetAllDomainEvents().OfType<T>().SingleOrDefault();
        domainEvent.Should().BeNull();
    }

    public static void ClearAllDomainEvents(this Entity aggregate)
    {
        foreach (var entity in aggregate.GetAllEntities())
        {
            entity.ClearDomainEvents();
        }
    }

    private static List<IDomainEvent> GetAllDomainEvents(this Entity aggregate)
    {
        var domainEvents = new List<IDomainEvent>();

        foreach (var entity in aggregate.GetAllEntities())
        {
            domainEvents.AddRange(entity.DomainEvents);
        }

        return domainEvents;
    }

    private static IEnumerable<Entity> GetAllEntities(this Entity aggregate)
    {
        yield return aggregate;

        var fields = aggregate.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
            .Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

        foreach (var field in fields)
        {
            var isEntity = typeof(Entity).IsAssignableFrom(field.FieldType);

            if (isEntity)
            {
                var entity = field.GetValue(aggregate) as Entity;
                foreach (var subEntity in entity.GetAllEntities())
                {
                    yield return subEntity;
                }
            }

            if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType)
                && field.GetValue(aggregate) is IEnumerable enumerable)
            {
                foreach (var entityItem in enumerable.OfType<Entity>())
                {
                    foreach (var subEntity in entityItem.GetAllEntities())
                    {
                        yield return subEntity;
                    }
                }
            }
        }
    }
}
