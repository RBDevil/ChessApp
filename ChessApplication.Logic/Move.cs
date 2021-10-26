using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Logic
{
    public class Move
    {
        public int fromX { get; }
        public int fromY { get; }
        public int toX { get; }
        public int toY { get; }
        
        public Move(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
        }

        public override bool Equals(object obj)
        {
            return obj is Move move &&
                   fromX == move.fromX &&
                   fromY == move.fromY &&
                   toX == move.toX &&
                   toY == move.toY;
        }
    }
}
