using EM.CrossCuttingConcerns.DateTimes;
using EM.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EM.Persistence.Interceptors
{
    public class AuditingSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                              InterceptionResult<int> result,
                                                                              CancellationToken cancellationToken = default)
        {
            DbContext dbContext = eventData.Context!;

            foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedAt = IDateTimeProvider.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.LastModifiedAt = IDateTimeProvider.Now;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
