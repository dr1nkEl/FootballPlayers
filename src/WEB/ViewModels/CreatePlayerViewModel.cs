using Domain;

namespace WEB.ViewModels;

/// <summary>
/// Create player view model.
/// </summary>
public record CreatePlayerViewModel
{
    /// <summary>
    /// Countries.
    /// </summary>
    public IEnumerable<OptionViewModel> Countries { get; init; } = new List<OptionViewModel>();

    /// <summary>
    /// Genders.
    /// </summary>
    public IEnumerable<OptionViewModel> Genders { get; init; } = new List<OptionViewModel>();

    /// <summary>
    /// Teams.
    /// </summary>
    public IEnumerable<Team> Teams { get; init; } = new List<Team>();

    /// <summary>
    /// Player.
    /// </summary>
    public PlayerViewModel Player { get; init; }
}
