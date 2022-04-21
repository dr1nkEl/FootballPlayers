using Domain;
using MediatR;

namespace UseCases.Players;

/// <summary>
/// Create player command.
/// </summary>
/// <param name="Player">Player.</param>
public record CreatePlayerCommand(Player Player) : IRequest<int>;
