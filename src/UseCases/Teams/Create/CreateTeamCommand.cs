using Domain;
using MediatR;

namespace UseCases.Teams;

/// <summary>
/// Create team command.
/// </summary>
/// <param name="Team">Team.</param>
public record CreateTeamCommand(Team Team) : IRequest;
