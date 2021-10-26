using ChessApplication.AI;
using ChessApplication.Client.Resources;
using ChessApplication.Client.UI;
using ChessApplication.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Client.ClientStates
{
    static class GameState
    {
        static bool whiteAI, blackAI;
        static Agent agent;
        public static bool Active { get; private set; }

        static LogicUpdater logic = new LogicUpdater();

        public static void Begin(bool whiteAI, bool blackAI)
        {
            Active = true;
            GameState.whiteAI = whiteAI;
            GameState.blackAI = blackAI;
            // Create AI
            agent = new Agent();
        }
        public static void End()
        {
            Active = false;
        }

        public static void Update()
        {
            if (logic.BoardState.Board.WhiteToMove && !whiteAI ||
                !logic.BoardState.Board.WhiteToMove && !blackAI)
            {
                CheckForPlayerInput();
            }
            else if (logic.BoardState.Board.WhiteToMove && whiteAI ||
                    !logic.BoardState.Board.WhiteToMove && blackAI)
            {
                logic.Input(agent.GetNextMove(logic));
                Sounds.Container["pieceMovement"].Play();
            }
            //CheckForPlayerInput();
            logic.Input();
        }

        private static void CheckForPlayerInput()
        {
            // Check for move inputs from the player
            if (MouseManager.LeftClick)
            {
                // Check if click happened in the boundries of the board
                if (Textures.boardRectangle.Contains(MouseManager.ClickStartingPosition) &&
                    Textures.boardRectangle.Contains(MouseManager.Position)
                    )
                {
                    int fromX = (MouseManager.ClickStartingPosition.X - Textures.boardOffset.X) / Textures.SquareSize;
                    int fromY = (MouseManager.ClickStartingPosition.Y - Textures.boardOffset.Y) / Textures.SquareSize;
                    int toX = (MouseManager.Position.X - Textures.boardOffset.X) / Textures.SquareSize;
                    int toY = (MouseManager.Position.Y - Textures.boardOffset.X) / Textures.SquareSize;

                    if(logic.Input(new Move(fromX, fromY, toX, toY)))
                    {
                        Sounds.Container["pieceMovement"].Play();
                    }
                }
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(Textures.Container["chessboard"], Textures.boardOffset.ToVector2(), Color.White);
            DrawPieces(sb, logic.BoardState.Board);
        }

        static void DrawPieces(SpriteBatch sb, Board board)
        {
            for (int i = 0; i < board.Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < board.Pieces.GetLength(1); j++)
                {
                    switch (board.Pieces[i,j])
                    {
                        case Pieces.NoPiece:
                            break;
                        case Pieces.WhitePawn:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_pawn"],
                                    Color.White
                                    );

                            break;
                        case Pieces.WhiteRook:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_rook"],
                                    Color.White
                                    );

                            break;
                        case Pieces.WhiteBishop:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_bishop"],
                                    Color.White
                                    );

                            break;
                        case Pieces.WhiteKnight:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_knight"],
                                    Color.White
                                    );

                            break;
                        case Pieces.WhiteQueen:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_queen"],
                                    Color.White
                                    );

                            break;
                        case Pieces.WhiteKing:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["white_king"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackPawn:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_pawn"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackRook:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_rook"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackBishop:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_bishop"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackKnight:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_knight"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackQueen:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_queen"],
                                    Color.White
                                    );

                            break;
                        case Pieces.BlackKing:
                            sb.Draw(Textures.Container["spritesheet"],
                                    new Vector2(
                                        i * Textures.SquareSize + Textures.boardOffset.X, 
                                        j * Textures.SquareSize + Textures.boardOffset.Y),
                                    Textures.SourceRectangles["black_king"],
                                    Color.White
                                    );

                            break;
                        default:
                            break;
                    }
                }
            }
            
            
        }
    }
}
