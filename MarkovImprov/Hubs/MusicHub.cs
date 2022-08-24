using MarkovImprov.Models;
using Microsoft.AspNetCore.SignalR;

namespace MarkovImprov.Hubs;

public class MusicHub : Hub<IMusic>
{
    public async Task SendNoteToClients(Note note)
    {
        await Clients.All.SendNote(note);
    }
}
