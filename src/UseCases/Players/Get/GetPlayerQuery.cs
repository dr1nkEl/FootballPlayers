using Domain;
using MediatR;

namespace UseCases.Players;

/// <summary>
/// Get player query.
/// </summary>
/// <param name="Id">ID.</param>
public record GetPlayerQuery(int Id) : IRequest<Player>;
