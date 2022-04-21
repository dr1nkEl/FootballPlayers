using Domain;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

/// <inheritdoc/>
public class AppDbContext : DbContext, IAppDbContext
{
    /// <inheritdoc/>
    public DbSet<Player> Players { get; protected set; }

    /// <inheritdoc/>
    public DbSet<Team> Teams { get; protected set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
