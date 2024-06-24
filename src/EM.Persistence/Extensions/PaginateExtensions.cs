using EM.Domain.Common.Paging;
using Microsoft.EntityFrameworkCore;

namespace EM.Persistence.Extensions;

public static class PaginateExtensions
{
    public static IPaginate<T> ToPaginate<T>(this IEnumerable<T> source,
                                             PageRequest? pageRequest)
    {
        ArgumentNullException.ThrowIfNull(pageRequest, nameof(pageRequest));

        int count = source.Count();
        List<T> items = source.Skip(pageRequest.Page * pageRequest.Size)
                              .Take(pageRequest.Size)
                              .ToList();

        int pages = (int)Math.Ceiling(count / (double)pageRequest.Size);

        Paginate<T> paginatedResult = new(pageRequest.Page, pageRequest.Size, count, pages, items);

        return paginatedResult;
    }

    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source,
                                                              PageRequest? pageRequest,
                                                              CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageRequest, nameof(pageRequest));

        int count = await source.CountAsync(cancellationToken);
        List<T> items = await source.Skip(pageRequest.Page * pageRequest.Size)
                                    .Take(pageRequest.Size)
                                    .ToListAsync(cancellationToken);

        int pages = (int)Math.Ceiling(count / (double)pageRequest.Size);

        Paginate<T> paginatedResult = new(pageRequest.Page, pageRequest.Size, count, pages, items);

        return paginatedResult;
    }
}