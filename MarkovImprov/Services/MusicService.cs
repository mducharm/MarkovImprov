using MarkovImprov.Models;
using Melanchall.DryWetMidi.MusicTheory;

namespace MarkovImprov.Services;

public class MusicService : IMusicService
{

    private readonly List<List<double>> _adjacencyMatrix = new()
    {
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
        new() {0, 0, 0, 0, 0, 0, 0},
    };

    private int _currentNote = 0;

    private static Random rand = new();

    public MusicService()
    { }

    public NoteName GetNextNote()
    {
        var nextNoteIndex = GetNextNoteIndex();

        NoteName note = nextNoteIndex switch
        {
            0 => NoteName.C,

        };

        return note;
    }

    private Scale CurrentScale => DateTime.UtcNow.DayOfWeek switch
    {
        DayOfWeek.Sunday => throw new NotImplementedException(),
        DayOfWeek.Monday => throw new NotImplementedException(),
        DayOfWeek.Tuesday => throw new NotImplementedException(),
        DayOfWeek.Wednesday => throw new NotImplementedException(),
        DayOfWeek.Thursday => throw new NotImplementedException(),
        DayOfWeek.Friday => throw new NotImplementedException(),
        DayOfWeek.Saturday => throw new NotImplementedException(),
        _ => throw new NotImplementedException(),
    };

    private NoteName CurrentNote
    {
        get
        {
            var seed = DateTime.UtcNow.Date.Year * 1000 + DateTime.UtcNow.Date.Day;
            var r = new Random(seed);

            var values = Enum.GetValues(typeof(NoteName));

            NoteName note = (NoteName)values.GetValue(r.Next(values.Length));

            return note;
        }
    }

    private int GetNextNoteIndex()
    {
        var neighbors = _adjacencyMatrix[_currentNote];
        double total = neighbors.Sum();
        double randomNumber = rand.NextDouble() * total;

        double sum = 0;

        for (int i = 0; i < neighbors.Count; i++)
        {
            sum += neighbors[i];

            if (randomNumber < sum)
            {
                return i;
            }
        }

        throw new Exception($"Failed to select next note following ${_currentNote}");
    }
}
