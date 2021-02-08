using Microsoft.EntityFrameworkCore;
using PotatoServer.Exceptions;
using PotatoServer.ViewModels.Core;
using System.Linq;
using System.Threading.Tasks;

namespace PotatoServer.Helpers.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedVmResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int? skip, int? take)
        {
            if (skip < 0)
                throw new BadRequestException("Skip < 0"); // TODO: Message
            if (take < 0)
                throw new BadRequestException("Take < 0"); // TODO: Message

            var itemsCount = await query.CountAsync();

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            var items = await query.ToListAsync();

            return new PagedVmResult<T>(items, itemsCount);
        }
    }
}
