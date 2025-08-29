using Data.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Db;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}