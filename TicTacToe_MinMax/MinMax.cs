using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TicTacToe_MinMax
{
    public static class MinMax
    {
        public static SByte minimax(Board board, int depth, bool isMaximizing)
        {
            if (depth < 6)
            {
                Dictionary<byte, SByte> scores = new Dictionary<byte, SByte>(3);

                scores.Add(3, 0);
                scores.Add(1, -1);
                scores.Add(2, 1);

                byte result = (byte)board.checkWin();

                if (result != 0)
                {
                    SByte score = scores[result];
                    return score;

                }

                if (isMaximizing)
                {
                    int tempPlayer = board.ai == 2 ? board.ai : board.player;

                    double bestScore = double.NegativeInfinity;
                    for (byte i = 0; i < board.board.GetLength(0); i++)
                    {
                        for (byte j = 0; j < board.board.GetLength(1); j++)
                        {
                            if (board.board[i, j] == 0)
                            {
                                board.board[i, j] = tempPlayer;

                                SByte score = minimax(board, depth + 1, false);
                                board.board[i, j] = 0;
                                bestScore = (int)Math.Max(score, bestScore);
                            }
                        }
                    }
                    return (SByte)bestScore;
                }
                else
                {
                    int tempPlayer = board.player == 1 ? board.player : board.ai;

                    double bestScore = double.PositiveInfinity;
                    for (byte i = 0; i < board.board.GetLength(0); i++)
                    {
                        for (byte j = 0; j < board.board.GetLength(1); j++)
                        {

                            if (board.board[i, j] == 0)
                            {
                                board.board[i, j] = tempPlayer;

                                SByte score = minimax(board, depth + 1, true);
                                board.board[i, j] = 0;
                                bestScore = (int)Math.Min(score, bestScore);
                            }
                        }
                    }
                    return (SByte)bestScore;
                }
            } 
            else
            {
                return 0;
            }
        }
    }
}
