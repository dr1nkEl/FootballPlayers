using Saritasa.Tools.Common.Utils;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// Player.
/// </summary>
public class Player
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    /// <summary>
    /// Surname.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Surname { get; set; }

    /// <summary>
    /// Fullname.
    /// </summary>
    public string FullName => StringUtils.JoinIgnoreEmpty(" ", Surname, Name);

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Country.
    /// </summary>
    public Country Country { get; set; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime BirthDay { get; set; }

    /// <summary>
    /// ID of <inheritdoc cref="Team"/>.
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Team.
    /// </summary>
    public Team Team { get; set; }
}
