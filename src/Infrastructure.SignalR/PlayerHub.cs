using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

/// <summary>
/// Players hub.
/// </summary>
public class PlayerHub : Hub
{
    /// <summary>
    /// Update.
    /// </summary>
    /// <param name="playerId">Player ID.</param>
    public async Task Update(int playerId)
    {
        await Clients.Others.SendAsync("Update", playerId, CancellationToken.None);
    }

    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="playerId">Player ID.</param>
    public async Task Add(int playerId)
    {
        await Clients.Others.SendAsync("Add", playerId, CancellationToken.None);
    }
}
