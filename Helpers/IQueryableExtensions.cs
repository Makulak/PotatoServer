using System.Linq;

namespace PotatoServer.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> x, int? skip, int? take)
        {
            if (skip.HasValue && take.HasValue)
                return x.Skip(skip.Value).Take(take.Value);
            else if (skip.HasValue)
                return x.Skip(skip.Value);
            else if (take.HasValue)
                return x.Take(take.Value);
            else
                return x;
        }
    }
}
