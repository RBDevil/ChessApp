using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApplication.Logic
{
    // This class, represents a BoardState in a single string format
    public class FEN
    {
        public string BoardState { get; private set; }
        public FEN(bool startingPosition = false)
        {
            // Starting position
            if (startingPosition)
            {
                BoardState = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            }
        }
        public FEN(string Fen)
        {
            BoardState = Fen;
        }
        public static FEN Parse(BoardState boardState)
        {
            // Defining the new FEN that will be returned
            FEN Fen = new FEN();

            // First part, the place of the pieces on the board
            int emptySquareCounter = 0;
            for (int i = 0; i < boardState.Board.Pieces.GetLength(0); i++)
            {
                if (i > 0)
                {
                    if(emptySquareCounter != 0)
                    {
                        Fen.BoardState += emptySquareCounter.ToString();
                        emptySquareCounter = 0;
                    }
                    Fen.BoardState += '/';
                }
                for (int j = 0; j < boardState.Board.Pieces.GetLength(1); j++)
                {
                    if (emptySquareCounter > 0 && boardState.Board.Pieces[j, i] != Pieces.NoPiece)
                    {
                        Fen.BoardState += emptySquareCounter.ToString();
                        emptySquareCounter = 0;
                    }
                    switch (boardState.Board.Pieces[j,i])
                    {
                        case Pieces.NoPiece:
                            emptySquareCounter++;
                            break;
                        case Pieces.WhitePawn:
                            Fen.BoardState += 'P';
                            break;
                        case Pieces.WhiteRook:
                            Fen.BoardState += 'R';
                            break;
                        case Pieces.WhiteBishop:
                            Fen.BoardState += 'B';
                            break;
                        case Pieces.WhiteKnight:
                            Fen.BoardState += 'N';
                            break;
                        case Pieces.WhiteQueen:
                            Fen.BoardState += 'Q';
                            break;
                        case Pieces.WhiteKing:
                            Fen.BoardState += 'K';
                            break;
                        case Pieces.BlackPawn:
                            Fen.BoardState += 'p';
                            break;
                        case Pieces.BlackRook:
                            Fen.BoardState += 'r';
                            break;
                        case Pieces.BlackBishop:
                            Fen.BoardState += 'b';
                            break;
                        case Pieces.BlackKnight:
                            Fen.BoardState += 'n';
                            break;
                        case Pieces.BlackQueen:
                            Fen.BoardState += 'q';
                            break;
                        case Pieces.BlackKing:
                            Fen.BoardState += 'k';
                            break;
                    }
                }
            }


            // Second part, active color, "w" means white moves next, "b" means black moves next.          
            // Add space
            Fen.BoardState += " ";
            // Add the correct letter
            if (boardState.Board.WhiteToMove)
            {
                Fen.BoardState += "w";
            }
            else Fen.BoardState += "b";

            // Third part, castling availability
            // Add space
            Fen.BoardState += " ";
            // Check if neither player can castle, then write '-' 
            if (!boardState.CanBlackCastleKingSide && 
                !boardState.CanBlackCastleQueenSide &&
                !boardState.CanWhiteCastleKingSide &&
                !boardState.CanWhiteCastleQueenSide)
            {
                Fen.BoardState += "-";
            }
            // Else add the correct letters
            else
            {
                if (boardState.CanBlackCastleKingSide)
                {
                    Fen.BoardState += "k";
                }
                if (boardState.CanBlackCastleQueenSide)
                {
                    Fen.BoardState += "q";
                }
                if (boardState.CanWhiteCastleKingSide)
                {
                    Fen.BoardState += "K";
                }
                if (boardState.CanWhiteCastleQueenSide)
                {
                    Fen.BoardState += "Q";
                }
            }

            // Fourth part, en passant availability
            // Add space
            Fen.BoardState += " ";
            // If no en passant available
            if (boardState.EnPassantX == 0 &&
                boardState.EnPassantX == 0)
            {
                Fen.BoardState += "-";
            }
            else
            {
                // 'a' = 96
                // '0' = 48
                int ASCIIOffset = 48;
                Fen.BoardState += boardState.EnPassantX.ToString();
                Fen.BoardState += boardState.EnPassantY.ToString();
            }

            // Fifth part, half move clock and full move counter
            // Add space
            Fen.BoardState += " ";
            Fen.BoardState += boardState.HalfMoveClock.ToString();

            // Add space
            Fen.BoardState += " ";
            Fen.BoardState += boardState.FullMoveCounter.ToString();

            return Fen;
        }
    }
}
