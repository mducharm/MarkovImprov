using MarkovImprov.Hubs;
using MarkovImprov.Services;
using Microsoft.AspNetCore.SignalR;

namespace MarkovImprov.BackgroundServices;

public class Worker : BackgroundService
{
    private readonly IHubContext<MusicHub, IMusic> _hubContext;
    private readonly IMusicService _musicService;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(1000));

    public Worker(IHubContext<MusicHub, IMusic> hubContext, IMusicService musicService)
    {
        _hubContext = hubContext;
        _musicService = musicService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            var nextNote = _musicService.GetNextNote();
            await _hubContext.Clients.All.SendNote(nextNote);
        }
    }

}
