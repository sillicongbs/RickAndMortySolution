using MortyImplementations;
using RickAndMortyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyGame
{
    public class Game
    {
        private readonly IMorty _morty;
        private readonly int _boxes;
        private readonly Statistics _stats;

        public Game(IMorty morty, int boxes, Statistics stats)
        { _morty = morty; _boxes = boxes; _stats = stats; }

        public void Play()
        {
            bool again = true;
            while (again)
            {
                using var rng = new FairRng();

                // Hide
                if (_morty is MortyBase mb) mb.HidePortalGun(rng, _boxes);
                else _morty.HidePortalGun(rng, _boxes); // in case you keep IMorty direct calls

                int gunBox = (_morty is MortyBase mb2) ? mb2.GetHiddenBox() : throw new InvalidOperationException("Expose HiddenBox");

                Console.Write($"Rick: Choose a box [0..{_boxes - 1}]: ");
                int rickChoice = int.Parse(Console.ReadLine()!);

                var kept = _morty.ChooseBoxesToKeep(rng, _boxes, rickChoice, gunBox);
                Console.WriteLine($"Morty keeps boxes: {string.Join(", ", kept)}");

                Console.Write("Rick: Do you want to switch? (y/n): ");
                bool stayed = (Console.ReadLine() ?? "").Trim().ToLower() != "y";
                int finalChoice = stayed ? rickChoice : kept.First(b => b != rickChoice);

                bool win = finalChoice == gunBox;
                Console.WriteLine(win ? "Rick: Yay, I found the gun!" : "Morty: Haha, I win!");

                _stats.Record(win, stayed);

                // Reveal both fair generations used this round
                rng.RevealAll();

                Console.Write("Morty: Play another round (y/n)? ");
                again = ((Console.ReadLine() ?? "").Trim().ToLower()) == "y";
            }
        }
    }

}
