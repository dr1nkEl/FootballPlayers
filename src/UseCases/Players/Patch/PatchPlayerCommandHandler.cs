using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;

namespace UseCases.Players;

/// <summary>
/// Handler for <inheritdoc cref="PatchPlayerCommand"/>.
/// </summary>
internal class PatchPlayerCommandHandler : AsyncRequestHandler<PatchPlayerCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public PatchPlayerCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(PatchPlayerCommand request, CancellationToken cancellationToken)
    {
        var team = await appDbContext.Teams.FirstOrDefaultAsync(team => team.Name == request.Player.Team.Name, cancellationToken);
        if (team != null)
        {
            request.Player.Team = team;
        }

        request.Player.BirthDay = request.Player.BirthDay.ToUniversalTime();

        appDbContext.Players.Update(request.Player);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
