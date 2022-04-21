using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Players;

/// <summary>
/// Handler for <inheritdoc cref="CreatePlayerCommand"/>.
/// </summary>
internal class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public CreatePlayerCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var team = await appDbContext.Teams.FirstOrDefaultAsync(team => team.Name == request.Player.Team.Name, cancellationToken);
        if (team != null)
        {
            request.Player.Team = team;
        }
        request.Player.BirthDay = request.Player.BirthDay.ToUniversalTime();
        await appDbContext.Players.AddAsync(request.Player, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return request.Player.Id;
    }
}
