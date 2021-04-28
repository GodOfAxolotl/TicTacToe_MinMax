using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TicTacToe_MinMax
{
    public class Board
    {
        public int[,] board = new int[3,3];
        public int player, ai;
        public bool minPlayer;

        public Board(int p)
        {
            if(p != 1 && p != 2)
            {
                throw new Exception("BadPlayerException");
            }
            player = p;
            ai = p == 1 ? 2 : 1;
            minPlayer = p == 2;
            

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        public void printOut()
        {

            Console.WriteLine("\n\n\t---------------");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("\t");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (board[i, j])
                    {
                        case 0:
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.Write("X");
                            break;
                        case 2:
                            Console.Write("O");
                            break;
                    }

                    if (j == board.GetLength(1) - 1)
                    {
                        Console.Write("\n\t---------------\n");
                    }
                    else
                    {
                        Console.Write(" | ");
                    }

                }
            }

            Console.WriteLine("");
        }

        public bool change(int x, int y, int val)
        {
            if ((val == 1 || val == 2) && board[x, y] == 0)
            {
                board[x, y] = val;
                return true;
            }
            return false;

        }

        public void nextMove()
        {
            double bestScore = double.NegativeInfinity;
            int[] bestMove = { 0, 0 };

            for(int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++) {

                    if (board[i, j] == 0)
                    {
                        board[i, j] = ai;

                        double score = 0;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                             score = MinMax.minimax(this, 0, false);
                        }));
                            
                        t.Start();

                        // int score = MinMax.minimax(this, 0, false);
                        t.Join();
                        board[i, j] = 0;
                        if(score > bestScore)
                        {
                            bestScore = score;
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                    }
                }
            }
            board[bestMove[0], bestMove[1]] = ai;
            GC.Collect();
        }


        public void nextMove(int user, bool b)
        {
            double bestScore;
            if (!b)
            {
                bestScore = double.NegativeInfinity;
            } else
            {
                bestScore = double.PositiveInfinity;
            }

            int[] bestMove = { 0, 0 };

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = user;

                        int score = 0;

                        //Thread t = new Thread(new ThreadStart(() =>
                       // {
                       //    score = MinMax.minimax(this, 0, false);
                       // }));

                       // t.Start();

                        score = MinMax.minimax(this, 0, b);
                        //t.Join();

                        board[i, j] = 0;
                        if (score > bestScore && !b)
                        {
                            bestScore = score;
                            bestMove[0] = i;
                            bestMove[1] = j;
                        } 
                        else if ( score < bestScore && b)
                        {
                            bestScore = score;
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                    }
                }
            }
            board[bestMove[0], bestMove[1]] = user;
        }

        public int checkWin()
        {
            int counter1 = 0;
            int counter2 = 0;

            for (int i = 0; i < board.GetLength(0); i++)                //Check for vertical lines
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 1)
                    {
                        counter1++;
                    }
                    else if (board[i, j] == 2)
                    {
                        counter2++;
                    }
                }

                if (counter1 == board.GetLength(0))
                {
                    return 1;
                }
                else if (counter2 == board.GetLength(0))
                {
                    return 2;
                }

                counter1 = 0;
                counter2 = 0;
            }

            for (int i = 0; i < board.GetLength(0); i++)            //Horizontal
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == 1)
                    {
                        counter1++;
                    }
                    else if (board[j, i] == 2)
                    {
                        counter2++;
                    }
                }

                if (counter1 == board.GetLength(1))
                {
                    return 1;
                }
                else if (counter2 == board.GetLength(1))
                {
                    return 2;
                }

                counter1 = 0;
                counter2 = 0;
            }

            if (board.GetLength(0) == 3)
            {
                for (int i = 0; i <= 2; i++)
                {
                    if (board[0, 2] == i && board[1, 1] == i && board[2, 0] == i)
                    {
                        return i;
                    }

                    if (board[0, 0] == i && board[1, 1] == i && board[2, 2] == i)
                    {
                        return i;
                    }
                }
            }
            else if(board.GetLength(0) == 4)
            {
                for (int i = 0; i <= 2; i++)
                {
                    if (board[0, 3] == i && board[1, 2] == i && board[2, 1] == i && board[3,0] == i)
                    {
                        return i;
                    }

                    if (board[0, 0] == i && board[1, 1] == i && board[2, 2] == i && board[3,3] == i)
                    {
                        return i;
                    }
                }
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j] == 0)
                    {
                        return 0;
                    }
                }
            }
            return 3;
        }
    }
}
