using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fucking awesome game of doom is starting right NOW ");
            Game game = new Game();
            game.Run();
        }
    }
}
