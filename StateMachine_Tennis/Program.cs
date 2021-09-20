using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Stateless;
using Stateless.Graph;

namespace StateMachine_Tennis
{
    class Program
    {
        static void Main(string[] args)
        {
            Game newGame = new Game();

            newGame.Start();
            newGame.Winner();
            Console.ReadKey();
        }
    }
}
