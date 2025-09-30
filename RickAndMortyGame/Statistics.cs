using ConsoleTables;
using RickAndMortyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyGame
{
    public class Statistics
    {
        private int _rounds = 0;
        private int _stayPlays = 0;
        private int _stayWins = 0;
        private int _switchPlays = 0;
        private int _switchWins = 0;

        public void Record(bool win, bool stayed)
        {
            _rounds++;
            if (stayed)
            {
                _stayPlays++;
                if (win) _stayWins++;
            }
            else
            {
                _switchPlays++;
                if (win) _switchWins++;
            }
        }
        public void Print(IMorty morty, int numberOfBoxes)
        {
            var table = new ConsoleTable("Game results", "Rick switched", "Rick stayed");

            table.AddRow("Rounds", _switchPlays, _stayPlays);
            table.AddRow("Wins", _switchWins, _stayWins);

            double estimateSwitch = _switchPlays > 0 ? (double)_switchWins / _switchPlays : 0;
            double estimateStay = _stayPlays > 0 ? (double)_stayWins / _stayPlays : 0;

            table.AddRow("P (estimate)", estimateSwitch.ToString("0.000"), estimateStay.ToString("0.000"));
            table.AddRow("P (exact)",
                morty.GetSwitchWinProbability(numberOfBoxes).ToString("0.000"),
                morty.GetStayWinProbability(numberOfBoxes).ToString("0.000"));

            Console.WriteLine();
            table.Write(Format.Alternative);
        }
    }
}
