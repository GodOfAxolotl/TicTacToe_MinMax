using System;

namespace TicTacToe_MinMax
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSession game = new GameSession();
           
          // game.automatedGame(8, true);
            
           game.setup();
           game.loop();
            
        }
    }
}
