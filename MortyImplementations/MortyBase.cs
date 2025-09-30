using RickAndMortyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortyImplementations
{
    public abstract class MortyBase : IMorty
    {
        protected int HiddenBox { get; private set; }
        public abstract string Name { get; }

        public int GetHiddenBox() => HiddenBox; // so Game can read it

        public virtual void HidePortalGun(IFairRng rng, int numberOfBoxes)
        {
            // Correct protocol in one call
            HiddenBox = rng.GenerateFair(
                numberOfBoxes,
                "HMAC1",
                $"enter your number [0,{numberOfBoxes}) so you don’t whine later that I cheated: ");
        }

        public abstract int[] ChooseBoxesToKeep(IFairRng rng, int numberOfBoxes, int rickChoice, int gunBox);
        public abstract double GetSwitchWinProbability(int numberOfBoxes);
        public abstract double GetStayWinProbability(int numberOfBoxes);
    }


}
