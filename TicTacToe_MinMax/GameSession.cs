using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TicTacToe_MinMax
{
    public class GameSession
    {
        Board board;

        private int counter;
       
        public GameSession()
        {
            counter = 0;
        }

        public void setup()
        {
            Console.WriteLine("Welcome...");

            while (true) {
                try
                {
                    Console.WriteLine("Which player do you Want to be? (1 == X || 2 == O) // X starts");
                    int p = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Initializing...");
                    board = new Board(p);
                    break;
                } catch (Exception e)
                {
                    Console.WriteLine("\nPlaese choose player 1 or player 2" + e);
                }
             }
            Console.WriteLine("Done!");
            if(board.player == 2)
            {
                board.nextMove();
            }
            board.printOut();
            Console.WriteLine("Input an x and a y coordinate like this : x,y   Use numbers from 1-3");
        }

        public void loop()
        {
                while (true)
                {
                    validateUserInput();

                    board.printOut();

                    if (counter < (board.board.GetLength(0) * board.board.GetLength(1)) / 2)
                    {
                        board.nextMove(board.ai, board.minPlayer);
                        board.printOut();
                    }

                    int winner = board.checkWin();

                    if (winner != 0)
                    {
                        if (winner == 3)
                        {
                            Console.WriteLine("Tie...");
                        }
                        else
                        {
                            Console.WriteLine("Winner is {0}", winner);
                        }
                        break;
                    }

                    counter++;
                    Console.WriteLine("{0}", counter);
                }

            Console.WriteLine("Done | Goodbye...");
        }

        public void automatedGame(int times, bool debug)
        {

            int winn1 = 0;
            int winn2 = 0;
            int tie = 0;

            for(int i = 0; i < times; i++)
            {
                board = new Board(1+(i % 2));
                for (int j = 0; j < (board.board.GetLength(0) * board.board.GetLength(1)) / 2; j++)
                {
                    board.nextMove(1, false);
                    if(j < 4)
                        board.nextMove(2, true);

                    if(debug)
                        board.printOut();

                    int winner = board.checkWin();

                    if (winner != 0)
                    {
                        if (winner == 3)
                        {
                            Console.WriteLine("Tie...");
                            tie++;                        }
                        else
                        {
                            Console.WriteLine("Winner is {0}", winner);
                            if (winner == 1)
                            {
                                winn1++;
                            }
                            else
                            {
                                winn2++;
                            }
                        }
                        board.printOut();
                        break;
                    }
                }


            }

            Console.WriteLine($"Wins by 1: {winn1}; Wins by 2: {winn2}; Ties: {tie}");

        }

        private void validateUserInput()
        {
            while (true)
            {
                try
                {
                    getUserInputAndParse();
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        private void getUserInputAndParse()
        {
            char[] input_char = Console.ReadLine().ToCharArray();

            int y = Int32.Parse(input_char[0].ToString());
            int x = Int32.Parse(input_char[2].ToString());

            if (!board.change(x - 1, y - 1, board.player))
            {
                throw new Exception("BadCoordinateException");
            }
        }
    }
}
