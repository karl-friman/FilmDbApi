using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Utilities.Serializer;

namespace FilmDbApiConsole
{
    public class TMDbConnection : ITMDbConnection
    {

        public async Task FetchConfig(TMDbClient client)
        {
            System.IO.FileInfo configJson = new FileInfo("config.json");

            Console.WriteLine("Config file: " + configJson.FullName + ", Exists: " + configJson.Exists);

            if (configJson.Exists && configJson.LastWriteTimeUtc >= DateTime.UtcNow.AddHours(-1))
            {
                Console.WriteLine("Using stored config");
                string json = File.ReadAllText(configJson.FullName, Encoding.UTF8);

                client.SetConfig(TMDbJsonSerializer.Instance.DeserializeFromString<TMDbConfig>(json));
            }
            else
            {
                Console.WriteLine("Getting new config");
                TMDbConfig config = await client.GetConfigAsync();

                Console.WriteLine("Storing config");
                string json = TMDbJsonSerializer.Instance.SerializeToString(config);
                File.WriteAllText(configJson.FullName, json, Encoding.UTF8);
            }

            Spacer();
        }

        public async Task FetchMovieExample(TMDbClient client, string query)
        {
            //string query = "Thor";

            // This example shows the fetching of a movie.
            // Say the user searches for "Thor" in order to find "Thor: The Dark World" or "Thor"
            SearchContainer<SearchMovie> results = await client.SearchMovieAsync(query);

            // The results is a list, currently on page 1 because we didn't specify any page.
            Console.WriteLine("Searched for movies: '" + query + "', found " + results.TotalResults + " results in " +
                              results.TotalPages + " pages");

            // Let's iterate the first few hits
            foreach (SearchMovie result in results.Results.Take(3))
            {
                // Print out each hit
                Console.WriteLine(result.Id + ": " + result.Title);
                Console.WriteLine("\t Original Title: " + result.OriginalTitle);
                Console.WriteLine("\t Release date  : " + result.ReleaseDate);
                Console.WriteLine("\t Popularity    : " + result.Popularity);
                Console.WriteLine("\t Vote Average  : " + result.VoteAverage);
                Console.WriteLine("\t Vote Count    : " + result.VoteCount);
                Console.WriteLine();
                Console.WriteLine("\t Backdrop Path : " + result.BackdropPath);
                Console.WriteLine("\t Poster Path   : " + result.PosterPath);

                Console.WriteLine();
            }

            Spacer();
        }

        public void Spacer()
        {
            Console.WriteLine();
            Console.WriteLine(" ----- ");
            Console.WriteLine();
        }
    }
}
