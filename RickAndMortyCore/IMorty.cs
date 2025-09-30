using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyCore
{
    // Fair RNG used by the core and Morty implementations
    public interface IFairRng
    {
        // Return uniformly random integer in [0, maxExclusive)
        void CommitRickValue(string value);
        int Generate(int n, int mortyValue);
        void RevealAll();
    }

    // Morty (pluggable) interface
    public interface IMorty
    {
        //int HiddenBox { get; }
        // Called at the start of a round
        void HidePortalGun(IFairRng rng, int numberOfBoxes);

        // Called after Rick picks a box
        int[] ChooseBoxesToKeep(IFairRng rng, int numberOfBoxes, int rickChoice, int gunBox);

        // Probability of Rick winning if he switches
        double GetSwitchWinProbability(int numberOfBoxes);

        // Probability of Rick winning if he stays
        double GetStayWinProbability(int numberOfBoxes);
    }
    
}
