using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApplication.Logic
{
    public class BoardState : ICloneable
    {
        public Board Board { get; private set; }
        public bool CanWhiteCastleQueenSide { get; private set; }
        public bool CanWhiteCastleKingSide { get; private set; }
        public bool CanBlackCastleQueenSide { get; private set; }
        public bool CanBlackCastleKingSide { get; private set; }
        public int EnPassantX { get; private set; }
        public int EnPassantY { get; private set; }
        public int HalfMoveClock { get; private set; }
        public int FullMoveCounter { get; private set; }

        /// <summary>
        /// Creates a board with default starting position
        /// </summary>
        public BoardState()
        {
            // Starting position
            Board = new Board();

            Board.WhiteToMove = true;

            CanBlackCastleKingSide = true;
            CanBlackCastleQueenSide = true;
            CanWhiteCastleKingSide = true;
            CanWhiteCastleQueenSide = true;

            HalfMoveClock = 0;

            FullMoveCounter = 1;
        }
        public static BoardState Parse(FEN Fen)
        {
            // Defining the new boardstate that will be returned
            BoardState boardState = new BoardState();
            
            // Spliting the FEN string
            string[] data = Fen.BoardState.Split(' ');

            // Place of the pieces on the board
            boardState.Board = new Board(data[0]);

            // Next move
            if (data[1] == "w")
            {
                boardState.Board.WhiteToMove = true;
            }
            else
            {
                boardState.Board.WhiteToMove = false;
            }

            // Castle availability
            if (data[2].Contains("K"))
            {
                boardState.CanWhiteCastleKingSide = true;
            }
            else 
            {
                boardState.CanWhiteCastleKingSide = false;
            }
            if (data[2].Contains("Q"))
            {
                boardState.CanWhiteCastleQueenSide = true;
            }
            else
            {
                boardState.CanWhiteCastleQueenSide = false;
            }
            if (data[2].Contains("q"))
            {
                boardState.CanBlackCastleQueenSide = true;
            }
            else
            {
                boardState.CanBlackCastleQueenSide = false;
            }
            if (data[2].Contains("k"))
            {
                boardState.CanBlackCastleKingSide = true;
            }
            else
            {
                boardState.CanBlackCastleKingSide = false;
            }

            if (data[3] != "-")
            {
                boardState.EnPassantX = (int)Char.GetNumericValue(data[3][0]);
                boardState.EnPassantY = (int)Char.GetNumericValue(data[3][1]);
            }

            boardState.HalfMoveClock = (int)Char.GetNumericValue(data[4][0]);
            boardState.FullMoveCounter = (int)Char.GetNumericValue(data[5][0]);

            return boardState;
        }
        public bool IsValid(List<Move> possibleMoves)
        {
            // A state is not valid if the king of the side who was on turn is still in check
            if (!Board.WhiteToMove)
            {
                // Search for the white king,
                int[] kingPosition = GetKingPosition(true);
                // if any of the moves to field points to the white king, it is in check
                foreach (Move move in possibleMoves)
                {
                    if (move.toX == kingPosition[0] && move.toY == kingPosition[1])
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Search for the black king,
                int[] kingPosition = GetKingPosition(false);
                // if any of the moves to field points to the black king, it is in check
                foreach (Move move in possibleMoves)
                {
                    if (move.toX == kingPosition[0] && move.toY == kingPosition[1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void ApplyMove(Move move)
        {
            // Set enPassant to zero
            EnPassantX = 0;
            EnPassantY = 0;

            CheckEnPassantAvailability(move);
            CheckEnPassant(move);
            CheckRookAndKingFirstMoves(move);
            CheckCastling(move);
            CheckPromotion(move);
            
            // Check if black moved, than increment FullMoveCounter
            if (!Board.WhiteToMove)
            {
                FullMoveCounter++;
            }
            // Check halfmove clock

            // Change turn 
            Board.WhiteToMove = !Board.WhiteToMove;

            Board.Pieces[move.toX, move.toY] = Board.Pieces[move.fromX, move.fromY];
            Board.Pieces[move.fromX, move.fromY] = Pieces.NoPiece;           
        }

        private void CheckPromotion(Move move)
        {
            if (Board.WhiteToMove)
            {
                if (move.toY == 0 && Board.Pieces[move.fromX, move.fromY] == Pieces.WhitePawn)
                {
                    Board.Pieces[move.fromX, move.fromY] = Pieces.WhiteQueen;
                }
            }
            else if (move.toY == 7 && Board.Pieces[move.fromX, move.fromY] == Pieces.BlackPawn)
            {
                Board.Pieces[move.fromX, move.fromY] = Pieces.BlackQueen;
            }
        }

        void CheckRookAndKingFirstMoves(Move move)
        {
            // Check if a rook moved for the first time, and adjust castling availabbility flags
            if (Board.Pieces[move.fromX, move.fromY] == Pieces.WhiteRook)
            {
                if (CanWhiteCastleQueenSide && move.fromX == 0)
                {
                    CanWhiteCastleQueenSide = false;
                }
                else if (CanWhiteCastleQueenSide && move.fromX == 7)
                {
                    CanWhiteCastleKingSide = false;
                }
            }
            else if (Board.Pieces[move.fromX, move.fromY] == Pieces.BlackRook)
            {
                if (CanBlackCastleQueenSide && move.fromX == 0)
                {
                    CanBlackCastleQueenSide = false;
                }
                else if (CanBlackCastleKingSide && move.fromX == 7)
                {
                    CanBlackCastleKingSide = false;
                }
            }
            // Check if one of the kings move
            else if (Board.Pieces[move.fromX, move.fromY] == Pieces.WhiteKing)
            {
                CanWhiteCastleKingSide = false;
                CanWhiteCastleQueenSide = false;
            }
            else if (Board.Pieces[move.fromX, move.fromY] == Pieces.BlackKing)
            {
                CanBlackCastleKingSide = false;
                CanBlackCastleQueenSide = false;
            }
            // Check if one of the rooks have been captured when castling was still available
            // TO IMPLEMENT!
            // ...
        }
        void CheckCastling(Move move)
        {
            // Check if the move was castling than place the rook to the right square, and adjust flags
            if (Board.WhiteToMove)
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.WhiteKing &&
                    Math.Abs(move.fromX - move.toX) > 1
                    )
                {
                    CanWhiteCastleKingSide = false;
                    CanWhiteCastleQueenSide = false;
                    // Castling queenside, move the rook
                    if (move.toX == 2)
                    {
                        Board.Pieces[3, 7] = Pieces.WhiteRook;
                        Board.Pieces[0, 7] = Pieces.NoPiece;
                    }
                    // Castling kingside
                    else
                    {
                        Board.Pieces[5, 7] = Pieces.WhiteRook;
                        Board.Pieces[7, 7] = Pieces.NoPiece;
                    }
                }
            }
            else
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.BlackKing &&
                    Math.Abs(move.fromX - move.toX) > 1
                    )
                {
                    CanBlackCastleKingSide = false;
                    CanBlackCastleQueenSide = false;
                    // Castling queenside, move the rook
                    if (move.toX == 2)
                    {
                        Board.Pieces[3, 0] = Pieces.BlackRook;
                        Board.Pieces[0, 0] = Pieces.NoPiece;
                    }
                    // Castling kingside
                    else
                    {
                        Board.Pieces[5, 0] = Pieces.BlackRook;
                        Board.Pieces[7, 0] = Pieces.NoPiece;
                    }
                }
            }
        }
        void CheckEnPassant(Move move)
        {
            // If a pawn moves diagonaly and "captures nothing" it is an en passant
            // Than delete the captured pawn
            if (Board.WhiteToMove)
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.WhitePawn &&
                    move.fromX != move.toX &&
                    Board.Pieces[move.toX, move.toY] == Pieces.NoPiece)
                {
                    Board.Pieces[move.toX, move.toY + 1] = Pieces.NoPiece;
                }
            }
            else
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.BlackPawn &&
                    move.fromX != move.toX &&
                    Board.Pieces[move.toX, move.toY] == Pieces.NoPiece)
                {
                    Board.Pieces[move.toX, move.toY - 1] = Pieces.NoPiece;
                }
            }
        }
        void CheckEnPassantAvailability(Move move)
        {
            // Check if En passant is available after this move
            if (Board.WhiteToMove)
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.WhitePawn &&
                    move.fromY == 6 && move.toY == 4)
                {
                    EnPassantX = move.toX;
                    EnPassantY = move.toY + 1;
                }
            }
            else
            {
                if (Board.Pieces[move.fromX, move.fromY] == Pieces.BlackPawn &&
                    move.fromY == 1 && move.toY == 3)
                {
                    EnPassantX = move.toX;
                    EnPassantY = move.toY - 1;
                }
            }
        }
        /// <summary>
        /// Returns the position of the king.
        /// The first element of the array is the X coordinate,
        /// the second element is the Y coordinate.
        /// </summary>
        /// <param name="white"></param>
        /// <returns></returns>
        public int[] GetKingPosition(bool white)
        {
            int[] position = new int[2];

            if (white)
            {
                for (int i = 0; i < Board.Pieces.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.Pieces.GetLength(1); j++)
                    {
                        if (Board.Pieces[i,j] == Pieces.WhiteKing)
                        {
                            position[0] = i;
                            position[1] = j;
                            return position;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Board.Pieces.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.Pieces.GetLength(1); j++)
                    {
                        if (Board.Pieces[i, j] == Pieces.BlackKing)
                        {
                            position[0] = i;
                            position[1] = j;
                            return position;
                        }
                    }
                }
            }

            return position;
        }

        public object Clone()
        {
            BoardState Clone = new BoardState();

            Clone.Board = (Board)Board.Clone();

            Clone.CanBlackCastleKingSide = CanBlackCastleKingSide;
            Clone.CanBlackCastleQueenSide = CanBlackCastleQueenSide;
            Clone.CanWhiteCastleKingSide = CanWhiteCastleKingSide;
            Clone.CanBlackCastleQueenSide = CanWhiteCastleQueenSide;

            Clone.EnPassantX = EnPassantX;
            Clone.EnPassantY = EnPassantY;

            Clone.HalfMoveClock = HalfMoveClock;
            Clone.FullMoveCounter = FullMoveCounter;

            return Clone;
        }
    }
}
