﻿using ChessApplication.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessApplication.AI
{
    public class Agent
    {
        class MoveAndValue
        {
            
            public Move Move;
            public int Value;

            public MoveAndValue(Move move, int value)
            {
                this.Move = move;
                this.Value = value;
            }
        }
        class Position
        {
            public Move FirstMove { get; }
            public LogicUpdater LogicUpdater { get; }
            public Position(LogicUpdater position, Move firstMove)
            {
                LogicUpdater = position;
                FirstMove = firstMove;
            }
        }
        public Agent()
        {

        }
        public Move GetNextMove(LogicUpdater startingPosition)
        {
            bool maximizingPlayer = startingPosition.BoardState.Board.WhiteToMove;
            
            return Minimax(new Position(startingPosition, null), 3, -1000, 1000, maximizingPlayer).Move;
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
        public static int EvaluatePosition(LogicUpdater logic, List<Move> allValidMoves)
        {
            // Checking if the position is checkmate
            if (allValidMoves.Count == 0)
            {
                if (logic.BoardState.Board.WhiteToMove)
                {
                    return -999;
                }
                else return 999;
            }
            // Counting pieces and 
            int value = 0;

            for (int i = 0; i < logic.BoardState.Board.Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < logic.BoardState.Board.Pieces.GetLength(1); j++)
                {
                    switch (logic.BoardState.Board.Pieces[i,j])
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

            // Evaluating center control
            for (int i = 3; i < 5; i++)
            {
                for (int j = 3; j < 5; j++)
                {
                    int num = LogicUpdater.CheckSquare(logic.BoardState.Board.Pieces[i, j]);
                    if (num == 0)
                    {
                        value += 1;
                    }
                    else if (num == 1)
                    {
                        value -= 1;         
                    }
                }
            }

            return value;
        }
        MoveAndValue Minimax(Position position, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            List<Move> allValidMoves = position.LogicUpdater.GetAllValidMoves();

            if (depth == 0 || allValidMoves.Count == 0)
            {
                return new MoveAndValue(null, 
                    EvaluatePosition(position.LogicUpdater, allValidMoves));
            }

            if (maximizingPlayer)
            {
                int maxEval = -10000;
                int maxIndex = 0;

                for (int i = 0; i < allValidMoves.Count; i++)
                {
                    LogicUpdater newPosition = (LogicUpdater)position.LogicUpdater.Clone();
                    newPosition.Input(allValidMoves[i]);
                    int eval = Minimax(new Position(newPosition, allValidMoves[i]), depth - 1, alpha, beta, false).Value;
                    if(maxEval < eval)
                    {
                        maxEval = eval;
                        maxIndex = i;
                    }
                    if (alpha < eval)
                    {
                        alpha = eval;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return new MoveAndValue(allValidMoves[maxIndex], maxEval);
            }
            else
            {
                int minEval = 10000;
                int minIndex = 0;

                for (int i = 0; i < allValidMoves.Count; i++)
                {
                    LogicUpdater newPosition = (LogicUpdater)position.LogicUpdater.Clone();
                    newPosition.Input(allValidMoves[i]);
                    int eval = Minimax(new Position(newPosition, allValidMoves[i]), depth - 1, alpha, beta, true).Value;
                    if (minEval > eval)
                    {
                        minEval = eval;
                        minIndex = i;
                    }
                    if (beta > eval)
                    {
                        beta = eval;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return new MoveAndValue(allValidMoves[minIndex], minEval);
            }
        }
    }
}
