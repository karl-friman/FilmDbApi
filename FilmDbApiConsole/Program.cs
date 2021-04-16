using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace FilmDbApiConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ------------- Basic TagLibSharp Code -------------
            string sample_file = @"E:\sample.m4v";
            File file = File.Create(sample_file);
            Console.WriteLine(file.Tag.FirstPerformer);

            // ------------- TMDbLib Code -------------

            // Instantiate a new client, all that's needed is an API key, but it's possible to 
            // also specify if SSL should be used, and if another server address should be used.
            using TMDbClient client = new TMDbClient("86699bea70cf1bf47755f1e6bb2f60b0");

            TMDbConnection connection = new TMDbConnection();

            // We need the config from TMDb in case we want to get stuff like images
            // The config needs to be fetched for each new client we create, but we can cache it to a file (as in this example).
            await connection.FetchConfig(client);

            // Try fetching a movie
            await connection.FetchMovieExample(client);
        }
        
        
    }
}
