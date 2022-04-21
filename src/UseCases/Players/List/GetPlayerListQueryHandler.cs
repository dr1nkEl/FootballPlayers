using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Players;

/// <summary>
/// Handler for <inheritdoc cref="GetPlayerListQuery"/>.
/// </summary>
internal class GetPlayerListQueryHandler : IRequestHandler<GetPlayerListQuery, IEnumerable<Player>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public GetPlayerListQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Player>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.Players.ToListAsync(cancellationToken);
    }
}
