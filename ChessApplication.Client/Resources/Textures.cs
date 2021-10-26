using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Client.Resources
{
    public static class Textures
    {
        public static readonly Point boardOffset = new Point(0, 0);
        public static Rectangle boardRectangle;
            
        public static int SquareSize { get; private set; }
        public static Dictionary<string, Texture2D> Container;
        public static Dictionary<string, Rectangle> SourceRectangles; 
        public static void LoadTextures(ContentManager Content)
        {
            Container = new Dictionary<string, Texture2D>();

            Container.Add("button", Content.Load<Texture2D>("button"));
            // Load the chessboard sprite, and set square size
            Container.Add("chessboard", Content.Load<Texture2D>("chessboard"));
            SquareSize = Container["chessboard"].Width / 8;
            boardRectangle = new Rectangle(boardOffset,
            new Point(SquareSize * 8, SquareSize * 8)
                );

            Container.Add("spritesheet", Content.Load<Texture2D>("spritesheet"));
            LoadSourceRectangles();
        }
        static void LoadSourceRectangles()
        {
            SourceRectangles = new Dictionary<string, Rectangle>();

            SourceRectangles.Add("white_king", new Rectangle(SquareSize * 0, 0, SquareSize, SquareSize));
            SourceRectangles.Add("white_queen", new Rectangle(SquareSize * 1, 0, SquareSize, SquareSize));
            SourceRectangles.Add("white_bishop", new Rectangle(SquareSize * 2, 0, SquareSize, SquareSize));
            SourceRectangles.Add("white_knight", new Rectangle(SquareSize * 3, 0, SquareSize, SquareSize));
            SourceRectangles.Add("white_rook", new Rectangle(SquareSize * 4, 0, SquareSize, SquareSize));
            SourceRectangles.Add("white_pawn", new Rectangle(SquareSize * 5, 0, SquareSize, SquareSize));

            SourceRectangles.Add("black_king", new Rectangle(SquareSize * 0, SquareSize, SquareSize, SquareSize));
            SourceRectangles.Add("black_queen", new Rectangle(SquareSize * 1, SquareSize, SquareSize, SquareSize));
            SourceRectangles.Add("black_bishop", new Rectangle(SquareSize * 2, SquareSize, SquareSize, SquareSize));
            SourceRectangles.Add("black_knight", new Rectangle(SquareSize * 3, SquareSize, SquareSize, SquareSize));
            SourceRectangles.Add("black_rook", new Rectangle(SquareSize * 4, SquareSize, SquareSize, SquareSize));
            SourceRectangles.Add("black_pawn", new Rectangle(SquareSize * 5, SquareSize, SquareSize, SquareSize));
        }
    }
}
