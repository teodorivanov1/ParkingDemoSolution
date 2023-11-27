namespace Demo.WebApi.Application.Extensions
{
    public static class TypeExtensions
    {
        public static string TypeName(this Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`', StringComparison.OrdinalIgnoreCase))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string TypeName(this object objectInstance)
        {
            return objectInstance?.GetType().TypeName()!;
        }
    }
}
