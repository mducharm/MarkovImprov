using MarkovImprov.Models;

namespace MarkovImprov.Hubs;

public interface IMusic
{
    Task SendNote(Note note);
}
