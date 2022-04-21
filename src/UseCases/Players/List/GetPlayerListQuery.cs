using Domain;
using MediatR;

namespace UseCases.Players;

/// <summary>
/// Get players list query.
/// </summary>
public record GetPlayerListQuery() : IRequest<IEnumerable<Player>>;
