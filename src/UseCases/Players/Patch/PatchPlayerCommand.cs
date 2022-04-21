using Domain;
using MediatR;

namespace UseCases.Players;

/// <summary>
/// Patch player command.
/// </summary>
/// <param name="Player">Player.</param>
public record PatchPlayerCommand(Player Player) : IRequest;
