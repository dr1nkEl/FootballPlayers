using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.People.Web.Services;
using UseCases.Players;
using UseCases.Teams;
using WEB.ViewModels;

namespace WEB.Controllers;

/// <summary>
/// Player controller.
/// </summary>
public class PlayerController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public PlayerController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET player list.
    /// </summary>
    /// <returns>View.</returns>
    [HttpGet]
    public IActionResult List()
    {
        return View();
    }

    /// <summary>
    /// GET create player.
    /// </summary>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var teams = await mediator.Send(new GetTeamsQuery(), cancellationToken);
        var model = new CreatePlayerViewModel
        {
            Teams = teams,
            Countries = EnumService.ParseEnum<Country>(),
            Genders = EnumService.ParseEnum<Gender>(),
        };
        return View(model);
    }
}
