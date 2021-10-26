using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Logic
{
    public class LogicUpdater : ICloneable
    {
        public BoardState BoardState { get; private set; }

        public LogicUpdater(BoardState boardState = null)
        {
            if (boardState == null)
            {
                BoardState = new BoardState();
            }
            else
            {
                BoardState = boardState;
            }
        }

        public void Input(Move move = null)
        {
            // if got input, convert it to a move and check if valid
            if(move != null)
            {
                if (ValidateMove(move))
                {
                    BoardState.ApplyMove(move);
                }
            }
        }

        public List<Move> GetAllValidMoves()
        {
            List<Move> allMoves = GetAllMoves();
            List<Move> allValidMoves = new List<Move>();
            foreach (Move move in allMoves)
            {
                if (ValidateMove(move))
                {
                    allValidMoves.Add(move);
                }
            }

            return allValidMoves;
        }
        bool ValidateMove(Move move)
        {
            // Check if the move is possible
            if (GetAllMovesFromPosition(move.fromX, move.fromY).Contains(move))
            {
                // Check if castling through check
                if (!CheckIfCastlingThroughCheck(move))
                {
                    // Check if the boardState after the move is valid
                    // Save boardState so the move later can be inverted
                    FEN save = FEN.Parse(BoardState);
                    BoardState.ApplyMove(move);
                    if (BoardState.IsValid(GetAllMoves()))
                    {
                        BoardState = BoardState.Parse(save);
                        return true;
                    }
                    BoardState = BoardState.Parse(save);
                }
            }

            return false;
        }
        /// <summary>
        /// Returns all the possible moves from one side, does not check for rules
        /// </summary>
        /// <returns></returns>
        List<Move> GetAllMoves()
        {
            List<Move> allMoves = new List<Move>();

            for (int i = 0; i < BoardState.Board.Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < BoardState.Board.Pieces.GetLength(1); j++)
                {
                    // Checks if empty, and if not, checks if the piece is on the correct side
                    if (CheckIfItsTurn(BoardState.Board.Pieces[i,j], BoardState.Board.WhiteToMove))
                    {
                        switch (BoardState.Board.Pieces[i, j])
                        {
                            case Pieces.WhitePawn:
                                allMoves.AddRange(PieceMoves.GetWhitePawnMoves(i, j, BoardState));
                                break;
                            case Pieces.WhiteRook:
                                allMoves.AddRange(PieceMoves.GetWhiteRookMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.WhiteBishop:
                                allMoves.AddRange(PieceMoves.GetWhiteBishopMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.WhiteKnight:
                                allMoves.AddRange(PieceMoves.GetWhiteKnightMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.WhiteQueen:
                                allMoves.AddRange(PieceMoves.GetWhiteQueenMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.WhiteKing:
                                allMoves.AddRange(PieceMoves.GetWhiteKingMoves(i, j, BoardState));
                                break;
                            case Pieces.BlackPawn:
                                allMoves.AddRange(PieceMoves.GetBlackPawnMoves(i, j, BoardState));
                                break;
                            case Pieces.BlackRook:
                                allMoves.AddRange(PieceMoves.GetBlackRookMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.BlackBishop:
                                allMoves.AddRange(PieceMoves.GetBlackBishopMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.BlackKnight:
                                allMoves.AddRange(PieceMoves.GetBlackKnightMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.BlackQueen:
                                allMoves.AddRange(PieceMoves.GetBlackQueenMoves(i, j, BoardState.Board));
                                break;
                            case Pieces.BlackKing:
                                allMoves.AddRange(PieceMoves.GetBlackKingMoves(i, j, BoardState));
                                break;
                        }
                    }
                }
            }


            return allMoves;
        }
        /// <summary>
        /// Returns all the possible moves from the given position, does not check for rules
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name=""></param>
        /// <returns></returns>
        List<Move> GetAllMovesFromPosition(int positionX, int positionY)
        {
            List<Move> moves = new List<Move>();

            // Check if empty, and if not checks if the piece is on the correct side
            if (CheckIfItsTurn(BoardState.Board.Pieces[positionX, positionY], BoardState.Board.WhiteToMove))
            {
                switch (BoardState.Board.Pieces[positionX, positionY])
                {
                    case Pieces.WhitePawn:
                        moves.AddRange(PieceMoves.GetWhitePawnMoves(positionX, positionY, BoardState));
                        break;
                    case Pieces.WhiteRook:
                        moves.AddRange(PieceMoves.GetWhiteRookMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.WhiteBishop:
                        moves.AddRange(PieceMoves.GetWhiteBishopMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.WhiteKnight:
                        moves.AddRange(PieceMoves.GetWhiteKnightMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.WhiteQueen:
                        moves.AddRange(PieceMoves.GetWhiteQueenMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.WhiteKing:
                        moves.AddRange(PieceMoves.GetWhiteKingMoves(positionX, positionY, BoardState));
                        break;
                    case Pieces.BlackPawn:
                        moves.AddRange(PieceMoves.GetBlackPawnMoves(positionX, positionY, BoardState));
                        break;
                    case Pieces.BlackRook:
                        moves.AddRange(PieceMoves.GetBlackRookMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.BlackBishop:
                        moves.AddRange(PieceMoves.GetBlackBishopMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.BlackKnight:
                        moves.AddRange(PieceMoves.GetBlackKnightMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.BlackQueen:
                        moves.AddRange(PieceMoves.GetBlackQueenMoves(positionX, positionY, BoardState.Board));
                        break;
                    case Pieces.BlackKing:
                        moves.AddRange(PieceMoves.GetBlackKingMoves(positionX, positionY, BoardState));
                        break;
                }
            }

            return moves;
        }
        /// <summary>
        /// Returns if the given piece is on turn.
        /// Returns false, if the square is empty.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="whiteToMove"></param>
        /// <returns></returns>
        bool CheckIfItsTurn(Pieces piece, bool whiteToMove)
        {
            if (whiteToMove && CheckSquare(piece) == 0)
            {
                return true;
            }
            else if (!whiteToMove && CheckSquare(piece) == 1)
            {
                return true;
            }

            return false;
        }
        bool CheckIfCastlingThroughCheck(Move move)
        {
            if (BoardState.Board.WhiteToMove)
            {
                if (BoardState.Board.Pieces[move.fromX, move.fromY] == Pieces.WhiteKing &&
                    Math.Abs(move.fromX - move.toX) > 1
                    )
                {
                    // Castling queenside
                    if (move.toX == 2)
                    {
                        return !ValidateMove(new Move(move.fromX, move.fromY, move.toX + 1, move.toY));
                    }
                    // Castling kingside
                    else
                    {
                        return !ValidateMove(new Move(move.fromX, move.fromY, move.toX - 1, move.toY));
                    }
                }
            }
            else
            {
                if (BoardState.Board.Pieces[move.fromX, move.fromY] == Pieces.BlackKing &&
                    Math.Abs(move.fromX - move.toX) > 1
                    )
                {
                    // Castling queenside
                    if (move.toX == 2)
                    {
                        return !ValidateMove(new Move(move.fromX, move.fromY, move.toX + 1, move.toY));
                    }
                    // Castling kingside
                    else
                    {
                        return !ValidateMove(new Move(move.fromX, move.fromY, move.toX - 1, move.toY));
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Returns 0 if the piece is white. 
        /// Returns 1 if the piece is black.
        /// Returns -1 if there is no piece on the square.
        /// </summary>
        /// <returns></returns>
        public static int CheckSquare(Pieces piece)
        {
            if ((int)piece < 7 &&
                (int)piece > 0)
            {
                return 0;
            }
            if ((int)piece > 6 &&
                (int)piece < 13)
            {
                return 1;
            }

            return -1;
        }
        public static bool CheckIfSquareIndexExists(int x, int y)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8) return true;
            return false;
        }

        public object Clone()
        {
            return new LogicUpdater((BoardState)BoardState.Clone());
        }
    }
}
