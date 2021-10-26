using ChessApplication.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessApplication.AI
{
    public class Agent
    {
        class Position : IComparable
        {
            public Move FirstMove { get; }
            public LogicUpdater LogicUpdater { get; }
            public Position(LogicUpdater position, Move firstMove)
            {
                LogicUpdater = position;
                FirstMove = firstMove;
            }

            public int CompareTo(object obj)
            {
                int value = EvaluatePosition(LogicUpdater.BoardState.Board);

                Position position = (Position)obj;
                int value2 = EvaluatePosition(position.LogicUpdater.BoardState.Board);

                if (value > value2)
                {
                    return -1;
                }
                else if (value2 > value)
                {
                    return 1;
                }
                else return 0;
            }
        }
        public Agent()
        {

        }
        public Move GetNextMove(LogicUpdater startingPosition)
        {
            List<Position> positions = GetAllPositions(new Position(startingPosition, null), 3);

            positions.Sort();

            if (!startingPosition.BoardState.Board.WhiteToMove)
            {
                return positions[positions.Count - 1].FirstMove;
            }
            else
            {
                return positions[0].FirstMove;
            }
        }
        List<Position> GetAllPositions(Position startingPosition, int depth)
        {
            List<Position> allPositions = new List<Position>();
            allPositions.Add(startingPosition);

            for (int i = 0; i < depth; i++)
            {
                allPositions = NextDepth(allPositions);
                Debug.WriteLine("Count of the " + (i+1).ToString() + ". layer of positions: " + allPositions.Count);
            }
            
            return allPositions;
        }
        List<Position> NextDepth(List<Position> startingDepth)
        {
            List<Position> positions = new List<Position>();
            
            foreach (Position position in startingDepth)
            {
                positions.AddRange(NextDepthFromPosition(position));
            }

            return positions;
        }
        List<Position> NextDepthFromPosition(Position startingPosition)
        {
            List<Position> positions = new List<Position>();
            List<Move> moves = startingPosition.LogicUpdater.GetAllValidMoves();

            foreach (Move move in moves)
            {
                // Create the new position
                LogicUpdater newPosition = (LogicUpdater)startingPosition.LogicUpdater.Clone();
                newPosition.Input(move);

                // If this is the first move, than set it
                if (startingPosition.FirstMove == null)
                {
                    positions.Add(new Position(newPosition, move));
                }
                else
                {
                    positions.Add(new Position(newPosition, startingPosition.FirstMove));
                }
            } 

            return positions;
        }
        public static int EvaluatePosition(Board board)
        {
            int value = 0;

            // Counting pieces
            for (int i = 0; i < board.Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < board.Pieces.GetLength(1); j++)
                {
                    switch (board.Pieces[i,j])
                    {
                        case Pieces.NoPiece:
                            break;
                        case Pieces.WhitePawn:
                            value += (int)PieceValues.Pawn;
                            break;
                        case Pieces.WhiteRook:
                            value += (int)PieceValues.Rook;
                            break;
                        case Pieces.WhiteBishop:
                            value += (int)PieceValues.Bishop;
                            break;
                        case Pieces.WhiteKnight:
                            value += (int)PieceValues.Knight;
                            break;
                        case Pieces.WhiteQueen:
                            value += (int)PieceValues.Queen;
                            break;
                        case Pieces.WhiteKing:
                            break;
                        case Pieces.BlackPawn:
                            value -= (int)PieceValues.Pawn;
                            break;
                        case Pieces.BlackRook:
                            value -= (int)PieceValues.Rook;
                            break;
                        case Pieces.BlackBishop:
                            value -= (int)PieceValues.Bishop;
                            break;
                        case Pieces.BlackKnight:
                            value -= (int)PieceValues.Knight;
                            break;
                        case Pieces.BlackQueen:
                            value -= (int)PieceValues.Queen;
                            break;
                        case Pieces.BlackKing:
                            break;
                        default:
                            break;
                    }
                }
            }

            // Counting who's turn it is
            //if (board.WhiteToMove)
            //{
            //    value += 2;
            //}
            //else value -= 2;

            return value;
        }
    }
}
