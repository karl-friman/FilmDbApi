using System.Threading.Tasks;
using TMDbLib.Client;

namespace FilmDbApiConsole
{
    public interface ITMDbConnection
    {
        Task FetchConfig(TMDbClient client);
        Task FetchMovieExample(TMDbClient client);
        void Spacer();
    }
}