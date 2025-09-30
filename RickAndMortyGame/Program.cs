using RickAndMortyCore;
using RickAndMortyGame;
using System.Reflection;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length != 3)
        {
            PrintUsage("Incorrect number of arguments.");
            return 1;
        }

        if (!int.TryParse(args[0], out int boxCount) || boxCount < 3)
        {
            Console.WriteLine("Error: <boxCount> must be an integer >= 3.");
            Console.WriteLine("Example: RickAndMortyGame.exe 3 MortyImpls.dll MortyImpls.ClassicMorty");
            return 1;
        }

        string mortyDllPath = args[1];
        string mortyClassName = args[2];

        if (!File.Exists(mortyDllPath))
        {
            Console.WriteLine($"Error: Morty DLL not found: {mortyDllPath}");
            return 1;
        }

        try
        {
            var asm = Assembly.LoadFrom(mortyDllPath);
            var type = asm.GetType(mortyClassName);
            if (type == null || !typeof(IMorty).IsAssignableFrom(type))
            {
                Console.WriteLine($"Error: '{mortyClassName}' not found or doesn’t implement IMorty.");
                return 1;
            }

            var morty = (IMorty)Activator.CreateInstance(type)!;
            var stats = new Statistics();
            var game = new Game(morty, boxCount, stats);

            game.Play();
            stats.Print(morty, boxCount);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to load Morty: {ex.Message}");
            return 1;
        }
    }

}
