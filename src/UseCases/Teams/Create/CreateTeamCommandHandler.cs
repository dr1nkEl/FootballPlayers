using Infrastructure.Abstractions;
using MediatR;

namespace UseCases.Teams;

/// <summary>
/// Handler for <inheritdoc cref="CreateTeamCommand"/>.
/// </summary>
internal class CreateTeamCommandHandler : AsyncRequestHandler<CreateTeamCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public CreateTeamCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        await appDbContext.Teams.AddAsync(request.Team, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
