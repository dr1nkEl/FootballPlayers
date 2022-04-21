using Domain;
using System.ComponentModel.DataAnnotations;

namespace WEB.ViewModels;

/// <summary>
/// Player view model.
/// </summary>
public record PlayerViewModel
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; init; }

    /// <summary>
    /// Surname.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Surname { get; init; }

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Country.
    /// </summary>
    public Country Country { get; init; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime BirthDay { get; init; }

    /// <summary>
    /// Team name.
    /// </summary>
    public string TeamName { get; init; }
}
