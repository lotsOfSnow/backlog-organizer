using System.Reflection;

namespace BacklogOrganizer.Shared.Core.Reflection;

public static class Assemblies
{
    public static Assembly[] GetAllNonDynamic()
        => GetAll(true);

    public static Assembly[] GetAll(bool ignoreDynamic = false)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        if (ignoreDynamic)
        {
            assemblies = assemblies.Where(x => !x.IsDynamic).ToArray();
        }

        return assemblies;
    }
}
