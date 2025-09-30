using RickAndMortyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortyImplementations
{
    public class LazyMorty : MortyBase
    {
        public override string Name => "LazyMorty";

        public override int[] ChooseBoxesToKeep(IFairRng rng, int n, int rickChoice, int gunBox)
        {
            // Always keep Rick’s box.
            // Also keep the gun box (if different); otherwise keep the lowest other index.
            if (rickChoice != gunBox) return new[] { rickChoice, gunBox };

            for (int i = 0; i < n; i++) if (i != rickChoice) return new[] { rickChoice, i };
            return new[] { rickChoice, (rickChoice + 1) % n }; // fallback
        }

        public override double GetSwitchWinProbability(int n) => (n - 1.0) / n;
        public override double GetStayWinProbability(int n) => 1.0 / n;
    }

}
