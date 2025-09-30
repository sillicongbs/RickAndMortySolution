using RickAndMortyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortyImplementations
{
    public class ClassicMorty : MortyBase
    {
        public override string Name => "ClassicMorty";

        public override int[] ChooseBoxesToKeep(IFairRng rng, int n, int rickChoice, int gunBox)
        {
            if (rickChoice != gunBox)
            {
                // Rick guessed wrong ⇒ keep (rickChoice, gunBox); still "pretend" fair gen for show
                _ = rng.GenerateFair(n - 1, "HMAC2", $"enter your number [0,{n - 1}) for the second gen: ");
                return new[] { rickChoice, gunBox };
            }

            // Rick guessed right ⇒ use the generated value to pick the other box (≠ Rick)
            int v = rng.GenerateFair(n - 1, "HMAC2", $"enter your number [0,{n - 1}) for the second gen: ");

            int MapExcludingRick(int x) => x >= rickChoice ? x + 1 : x;
            int other = MapExcludingRick(v);
            return new[] { rickChoice, other };
        }

        public override double GetSwitchWinProbability(int n) => (n - 1.0) / n;
        public override double GetStayWinProbability(int n) => 1.0 / n;
    }

}
