using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

/// <summary>
/// Abstraction for application DB context.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Players.
    /// </summary>
    DbSet<Player> Players { get; }

    /// <summary>
    /// Teams.
    /// </summary>
    DbSet<Team> Teams { get; }

    /// <summary>
    /// Save pending changes.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
