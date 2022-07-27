namespace BacklogOrganizer.Shared.Core.Types;

public static class TypesExtensions
{
    /// <param name="matchTType">
    ///     Whether the type <see cref="TType" /> itself should be matched too.
    /// </param>
    public static bool IsConcreteImplementationOf<TType>(this Type type, bool matchTType = false)
        => typeof(TType).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract && (matchTType || type != typeof(TType));
}
