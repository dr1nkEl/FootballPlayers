using System.Collections.ObjectModel;

namespace Domain;

/// <summary>
/// Team.
/// </summary>
public class Team
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Players.
    /// </summary>
    public ICollection<Player> Players { get; set; } = new Collection<Player>();
}
