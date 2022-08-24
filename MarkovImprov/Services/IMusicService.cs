using MarkovImprov.Models;

namespace MarkovImprov.Services;

public interface IMusicService
{
    Note GetNextNote();
}