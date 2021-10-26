using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Logic 
{
    /// <summary>
    /// 0 empty, 1-6 white, 7-12 black
    /// </summary>
    public enum Pieces
    {
        NoPiece,
        WhitePawn,
        WhiteRook,
        WhiteBishop,
        WhiteKnight,
        WhiteQueen,
        WhiteKing,
        BlackPawn,
        BlackRook,
        BlackBishop,
        BlackKnight,
        BlackQueen,
        BlackKing
    }
    public class Board : ICloneable
    {
        public bool WhiteToMove { get; set; }
        public Pieces[,] Pieces { get; private set; }
        /// <summary>
        /// Creates a board with starting position
        /// </summary>
        public Board()
        {
            Parse("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        }
        public Board(string pieces)
        {
            Parse(pieces);
        }
        void Parse(string pieces)
        {
            Pieces = new Pieces[8,8];
            // Points to the square where the next piece will be
            int pointerX = 0;
            int pointerY = 0;

            // If empty squares, increment the pointer
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i] == '1' ||
                    pieces[i] == '2' ||
                    pieces[i] == '3' ||
                    pieces[i] == '4' ||
                    pieces[i] == '5' ||
                    pieces[i] == '6' ||
                    pieces[i] == '7' ||
                    pieces[i] == '8')
                {
                    pointerX += (int)char.GetNumericValue(pieces[i]);
                }
                else
                {
                    switch (pieces[i])
                    {
                        case '/':
                            pointerX = 0;
                            pointerY++;
                            break;

                        case 'P':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhitePawn;
                            pointerX++;

                            break;
                        case 'R':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhiteRook;
                            pointerX++;

                            break;
                        case 'N':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhiteKnight;
                            pointerX++;

                            break;
                        case 'B':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhiteBishop;
                            pointerX++;

                            break;
                        case 'Q':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhiteQueen;
                            pointerX++;

                            break;
                        case 'K':
                            Pieces[pointerX, pointerY] = Logic.Pieces.WhiteKing;
                            pointerX++;

                            break;
                        case 'p':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackPawn;
                            pointerX++;

                            break;
                        case 'r':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackRook;
                            pointerX++;

                            break;
                        case 'n':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackKnight;
                            pointerX++;

                            break;
                        case 'b':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackBishop;
                            pointerX++;

                            break;
                        case 'q':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackQueen;
                            pointerX++;

                            break;
                        case 'k':
                            Pieces[pointerX, pointerY] = Logic.Pieces.BlackKing;
                            pointerX++;

                            break;
                    }
                }
            }
        }

        public object Clone()
        {
            Board Clone = new Board();

            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    Clone.Pieces[i, j] = Pieces[i, j];
                }
            }

            Clone.WhiteToMove = WhiteToMove;

            return Clone;
        }
    }
}
