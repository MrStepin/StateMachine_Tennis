using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Stateless;
using Stateless.Graph;

namespace StateMachine_Tennis
{
    public class Game
    {
        public enum State
        {
            Start,
            S15_0,
            S0_15,
            S30_0,
            S15_15,
            S0_30,
            S40_0,
            S30_15,
            S15_30,
            S0_40,
            P1Win,
            S40_15,
            S30_30,
            S15_40,
            P2Win,
            S40_30,
            S30_40,
            P1More,
            Equals,
            P2More
        }

        public enum Serve
        {
            P1,
            P2
        }

        private State _state = State.Start;

        public StateMachine<State, Serve> Machine;

        //private int P1WonGame = 0;
        //private int P2WonGame = 0;

        Random randomNumber = new Random();

        public Game()
        {
            Machine = new StateMachine<State, Serve>(() => _state, s => _state = s);

            Machine.Configure(State.Start).Permit(Serve.P1, State.S15_0)
                .Permit(Serve.P2, State.S0_15);

            Machine.Configure(State.S15_0).Permit(Serve.P1, State.S30_0)
                .Permit(Serve.P2, State.S15_15);

            Machine.Configure(State.S0_15).Permit(Serve.P1, State.S15_15)
                .Permit(Serve.P2, State.S0_30);

            Machine.Configure(State.S30_0).Permit(Serve.P1, State.S40_0)
                .Permit(Serve.P2, State.S30_15);

            Machine.Configure(State.S15_15).Permit(Serve.P1, State.S30_15)
                .Permit(Serve.P2, State.S15_30);

            Machine.Configure(State.S0_30).Permit(Serve.P1, State.S15_30)
                .Permit(Serve.P2, State.S0_40);

            Machine.Configure(State.S40_0).Permit(Serve.P1, State.P1Win)
                .Permit(Serve.P2, State.S40_15);

            Machine.Configure(State.S30_15).Permit(Serve.P1, State.S40_15)
                .Permit(Serve.P2, State.S30_30);

            Machine.Configure(State.S15_30).Permit(Serve.P1, State.S30_30)
                .Permit(Serve.P2, State.S15_40);

            Machine.Configure(State.S0_40).Permit(Serve.P1, State.S15_40)
                .Permit(Serve.P2, State.P2Win);

            Machine.Configure(State.S40_15).Permit(Serve.P1, State.P1Win)
                .Permit(Serve.P2, State.S40_30);

            Machine.Configure(State.S30_30).Permit(Serve.P1, State.S40_30)
                .Permit(Serve.P2, State.S30_40);

            Machine.Configure(State.S15_40).Permit(Serve.P1, State.S30_40)
                .Permit(Serve.P2, State.P2Win);

            Machine.Configure(State.S40_30).Permit(Serve.P1, State.P1Win)
                .Permit(Serve.P2, State.Equals);

            Machine.Configure(State.S30_40).Permit(Serve.P1, State.Equals)
                .Permit(Serve.P2, State.P2Win);

            Machine.Configure(State.P1More).Permit(Serve.P1, State.P1Win)
                .Permit(Serve.P2, State.Equals);

            Machine.Configure(State.P2More).Permit(Serve.P1, State.Equals)
                .Permit(Serve.P2, State.P2Win);

            Machine.Configure(State.P1Win).Ignore(Serve.P1).Ignore(Serve.P2);

            Machine.Configure(State.P2Win).Ignore(Serve.P1).Ignore(Serve.P2);

            Machine.Configure(State.Equals).Permit(Serve.P1, State.P1More)
                .Permit(Serve.P2, State.P2More);

        }

        public void Start()
        {
            while (_state != State.P1Win && _state != State.P2Win)
            {
                Machine.Fire((Serve) randomNumber.Next(Enum.GetNames(typeof(Serve)).Length));
            }
        }

        public void Winner()
        {
            if (_state == State.P1Win)
            {
                Console.WriteLine("P1 Win");
            }
            else
            {
                Console.WriteLine("P2 Win");
            }
        }
    }
}
