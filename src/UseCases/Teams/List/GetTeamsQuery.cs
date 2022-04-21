using Domain;
using MediatR;

namespace UseCases.Teams;

/// <summary>
/// Get teams query.
/// </summary>
public record GetTeamsQuery : IRequest<IEnumerable<Team>>;
