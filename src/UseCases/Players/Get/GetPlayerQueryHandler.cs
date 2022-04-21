using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Saritasa.Tools.EFCore;

namespace UseCases.Players;

/// <summary>
/// Handler for <inheritdoc cref="GetPlayerQuery"/>.
/// </summary>
internal class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, Player>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public GetPlayerQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<Player> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.Players.GetAsync(player => player.Id == request.Id, cancellationToken);
    }
}
