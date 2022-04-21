using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Teams;

/// <summary>
/// Handler for <inheritdoc cref="GetTeamsQuery"/>.
/// </summary>
internal class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, IEnumerable<Team>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public GetTeamsQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.Teams.ToListAsync(cancellationToken);
    }
}
