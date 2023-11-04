using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eris.Shared;
using Eris.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Eris.Server.Controllers;

[ApiController]
[Route("api/netflix")]
public class NetflixController : ControllerBase
{
    private readonly IHubContext<NetflixHub, INetflixClient> _hub;
    private readonly ILogger<NetflixController> _logger;

    public NetflixController(IHubContext<NetflixHub, INetflixClient> hub, ILogger<NetflixController> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    [HttpGet]
    public PlaybackStatus Get()
    {
        return new PlaybackStatus(IsPlaying: true);
    }

    [HttpPut("pause")]
    public async Task<PlaybackStatus> Pause()
    {
        await _hub.Clients.All.Pause();
        return new PlaybackStatus(IsPlaying: false);
    }

    [HttpPut("play")]
    public async Task<PlaybackStatus> Play()
    {
        await _hub.Clients.All.Play();
        return new PlaybackStatus(IsPlaying: true);
    }
}
