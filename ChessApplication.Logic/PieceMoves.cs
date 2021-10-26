using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Logic
{
    static class PieceMoves
    {
        public static List<Move> GetWhitePawnMoves(int positionX, int positionY, BoardState boardState)
        {
            List<Move> moves = new List<Move>();

            // Check if can move forward 1 square
            if (boardState.Board.Pieces[positionX, positionY - 1] == Pieces.NoPiece)
            {
                moves.Add(new Move(positionX, positionY, positionX, positionY - 1));
                // Check if pawn is in the starting position, and can move 2 squares forward
                if(positionY == 6 && boardState.Board.Pieces[positionX, positionY - 2] == Pieces.NoPiece)
                {
                    moves.Add(new Move(positionX, positionY, positionX, positionY - 2));
                }
            }
            // Check if can capture anything, only black pieces
            // Have to check if its not out of index.
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + 1, positionY - 1) &&
                LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX + 1, positionY - 1]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + 1, positionY - 1));
            }
            if (LogicUpdater.CheckIfSquareIndexExists(positionX - 1, positionY - 1) &&
                LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX - 1, positionY - 1]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX - 1, positionY - 1));
            }

            // Check en passant
            if ((boardState.EnPassantX != 0 || boardState.EnPassantY != 0) &&
                (positionX + 1 == boardState.EnPassantX || positionX - 1 == boardState.EnPassantX) &&
                positionY -1 == boardState.EnPassantY
                )
            {
                moves.Add(new Move(positionX, positionY, boardState.EnPassantX, boardState.EnPassantY));
            }

            return moves;
        }
        public static List<Move> GetWhiteRookMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check rows, and ranks, until something is in the way (white piece, black piece, edge of the board)
            // Moves to the right
            int i = 1;
            int j = 0;
            
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves backwards
            i = 0;
            j = 1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                j++;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves forward
            i = -1;
            j = 0;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves to the left
            i = 0;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                j--;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetWhiteBishopMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check the four diagonals until something is in the way (white piece, black piece, edge of the board)
            // moves to right-down
            int i = 1;
            int j = 1;

            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
                j++;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // moves to left-down
            i = 1;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
                j--;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }
            
            // moves to right-up
            i = -1;
            j = 1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
                j++;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // moves to right-down
            i = -1;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
                j--;
            }
            // If blocked by a black piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetWhiteKnightMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check the 8 squares

            int i = 1;
            int j = 2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 1;
            j = -2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -1;
            j = 2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -1;
            j = -2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 2;
            j = 1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -2;
            j = 1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 2;
            j = -1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -2;
            j = -1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetWhiteQueenMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(GetWhiteRookMoves(positionX, positionY, board));
            moves.AddRange(GetWhiteBishopMoves(positionX, positionY, board));

            return moves;
        }
        public static List<Move> GetWhiteKingMoves(int positionX, int positionY, BoardState boardState)
        {
            List<Move> moves = new List<Move>();

            // Check all the squares adjacent if empty or black piece
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                        LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX + i, positionY + j]) != 0)
                    {
                        moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                    }
                }
            }

            // Castling
            // Check if squares are empty and castling is still available
            // Kingside
            if (boardState.CanWhiteCastleKingSide &&
                boardState.Board.Pieces[7, 7] == Pieces.WhiteRook &&
                boardState.Board.Pieces[6, 7] == Pieces.NoPiece &&
                boardState.Board.Pieces[5, 7] == Pieces.NoPiece
                )
            {
                moves.Add(new Move(positionX, positionY, 6, 7));
            }
            // Queenside
            if (boardState.CanWhiteCastleQueenSide &&
                boardState.Board.Pieces[0, 7] == Pieces.WhiteRook &&
                boardState.Board.Pieces[1, 7] == Pieces.NoPiece &&
                boardState.Board.Pieces[2, 7] == Pieces.NoPiece &&
                boardState.Board.Pieces[3, 7] == Pieces.NoPiece 
                )
            {
                moves.Add(new Move(positionX, positionY, 2, 7));
            }

            return moves;
        }

        public static List<Move> GetBlackPawnMoves(int positionX, int positionY, BoardState boardState)
        {
            List<Move> moves = new List<Move>();

            // Check if can move forward 1 square
            if (boardState.Board.Pieces[positionX, positionY + 1] == Pieces.NoPiece)
            {
                moves.Add(new Move(positionX, positionY, positionX, positionY + 1));
                // Check if pawn is in the starting position, and can move 2 squares forward
                if (positionY == 1 && boardState.Board.Pieces[positionX, positionY + 2] == Pieces.NoPiece)
                {
                    moves.Add(new Move(positionX, positionY, positionX, positionY + 2));
                }
            }
            // Check if can capture anything, only white pieces
            // Have to check if its not out of index.
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + 1, positionY + 1) &&
                LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX + 1, positionY + 1]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + 1, positionY + 1));
            }
            if (LogicUpdater.CheckIfSquareIndexExists(positionX - 1, positionY + 1) &&
                LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX - 1, positionY + 1]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX - 1, positionY + 1));
            }

            // Check en passant
            if ((boardState.EnPassantX != 0 || boardState.EnPassantY != 0) &&
                (positionX + 1 == boardState.EnPassantX || positionX - 1 == boardState.EnPassantX) &&
                positionY + 1 == boardState.EnPassantY
                )
            {
                moves.Add(new Move(positionX, positionY, boardState.EnPassantX, boardState.EnPassantY));
            }

            return moves;
        }
        public static List<Move> GetBlackRookMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check rows, and ranks, until something is in the way (white piece, black piece, edge of the board)
            // Moves to the right
            int i = 1;
            int j = 0;

            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves backwards
            i = 0;
            j = 1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                j++;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves forward
            i = -1;
            j = 0;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // Moves to the left
            i = 0;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                j--;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetBlackBishopMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check the four diagonals until something is in the way (white piece, black piece, edge of the board)
            // moves to right-down
            int i = 1;
            int j = 1;

            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
                j++;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // moves to left-down
            i = 1;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i++;
                j--;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // moves to right-up
            i = -1;
            j = 1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
                j++;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            // moves to right-down
            i = -1;
            j = -1;
            while (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                   LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == -1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                i--;
                j--;
            }
            // If blocked by a white piece add capture move
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) == 0)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetBlackKnightMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            // Check the 8 squares

            int i = 1;
            int j = 2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 1;
            j = -2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -1;
            j = 2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -1;
            j = -2;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 2;
            j = 1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -2;
            j = 1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = 2;
            j = -1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            i = -2;
            j = -1;
            if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                LogicUpdater.CheckSquare(board.Pieces[positionX + i, positionY + j]) != 1)
            {
                moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
            }

            return moves;
        }
        public static List<Move> GetBlackQueenMoves(int positionX, int positionY, Board board)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(GetBlackRookMoves(positionX, positionY, board));
            moves.AddRange(GetBlackBishopMoves(positionX, positionY, board));

            return moves;
        }
        public static List<Move> GetBlackKingMoves(int positionX, int positionY, BoardState boardState)
        {
            List<Move> moves = new List<Move>();

            // Check all the squares adjacent if empty or white piece
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (LogicUpdater.CheckIfSquareIndexExists(positionX + i, positionY + j) &&
                        LogicUpdater.CheckSquare(boardState.Board.Pieces[positionX + i, positionY + j]) != 1)
                    {
                        moves.Add(new Move(positionX, positionY, positionX + i, positionY + j));
                    }
                }
            }

            // Castling
            // Check if squares are empty and castling is still available
            // Kingside
            if (boardState.CanBlackCastleKingSide &&
                boardState.Board.Pieces[7, 0] == Pieces.BlackRook &&
                boardState.Board.Pieces[6, 0] == Pieces.NoPiece &&
                boardState.Board.Pieces[5, 0] == Pieces.NoPiece
                )
            {
                moves.Add(new Move(positionX, positionY, 6, 0));
            }
            // Queenside
            if (boardState.CanBlackCastleQueenSide &&
                boardState.Board.Pieces[0, 0] == Pieces.BlackRook &&
                boardState.Board.Pieces[1, 0] == Pieces.NoPiece &&
                boardState.Board.Pieces[2, 0] == Pieces.NoPiece &&
                boardState.Board.Pieces[3, 0] == Pieces.NoPiece
                )
            {
                moves.Add(new Move(positionX, positionY, 2, 0));
            }

            return moves;
        }
    }
}
