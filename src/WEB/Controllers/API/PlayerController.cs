using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.People.Web.Services;
using UseCases.Players;
using WEB.ViewModels;

namespace WEB.Controllers.API;

/// <summary>
/// API player controller.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class PlayerController : ControllerBase
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
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Players.</returns>
    [HttpGet]
    public async Task<IEnumerable<Player>> List(CancellationToken cancellationToken)
    {
        return (await mediator.Send(new GetPlayerListQuery(), cancellationToken)).OrderBy(player => player.FullName);
    }

    /// <summary>
    /// GET countries.
    /// </summary>
    /// <returns>Countries.</returns>
    [HttpGet]
    public IEnumerable<OptionViewModel> Countries()
    {
        return EnumService.ParseEnum<Country>();
    }

    /// <summary>
    /// PATCH player.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPatch]
    public async Task<ActionResult> Patch([FromForm] PlayerViewModel player, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (player.BirthDay > DateTime.UtcNow)
        {
            return BadRequest("Invalid birthday!");
        }
        await mediator.Send(new PatchPlayerCommand(mapper.Map<Player>(player)), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// POST create player.
    /// </summary>
    /// <param name="vm">View model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of created entity.</returns>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromForm] CreatePlayerViewModel vm, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (vm.Player.BirthDay > DateTime.UtcNow)
        {
            return BadRequest("Incorrect birthday!");
        }

        var player = mapper.Map<Player>(vm.Player);
        var res = await mediator.Send(new CreatePlayerCommand(player), cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// GET player.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Player.</returns>
    [HttpGet("{id}")]
    public async Task<Player> Get(int id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPlayerQuery(id), cancellationToken);
    }

    /// <summary>
    /// GET genders.
    /// </summary>
    /// <returns>Genders.</returns>
    [HttpGet]
    public IEnumerable<OptionViewModel> Genders()
    {
        return EnumService.ParseEnum<Gender>();
    }
}
