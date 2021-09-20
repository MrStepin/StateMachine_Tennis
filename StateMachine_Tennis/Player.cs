using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine_Tennis
{
    class Player
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }


        Game.State value { get; set; }

        public int WonSets(int value)
        {
            return value += 1;
        }

        public int WonGames(int value)
        {
            return value += 1;
        }
    }
}
