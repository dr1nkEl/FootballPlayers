using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Teams;

namespace WEB.Controllers.API;

/// <summary>
/// API team controller.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public TeamController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// GET teams.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Teams.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> List(CancellationToken cancellationToken)
    {
        return Ok((await mediator.Send(new GetTeamsQuery(), cancellationToken)).OrderBy(team=>team.Name));
    }

    /// <summary>
    /// POST create team.
    /// </summary>
    /// <param name="team">Team.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromQuery]Team team, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(new CreateTeamCommand(team), cancellationToken);
        return Ok();
    }
}
