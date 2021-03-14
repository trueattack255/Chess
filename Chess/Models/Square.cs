using System;
using Chess.Enums;
using Chess.Interfaces;

namespace Chess.Models
{
    public class Square : ISquare
    {
        public Color Color { get; }
        public Piece Piece { get; set; }
        public InternalPosition Position { get; }
        
        public Square(InternalPosition position) : this(position,null)
        {

        }
        
        public Square(InternalPosition position, Piece piece)
        {
            Piece = piece;
            Position = position;

            Color = GetColor(position);
        }

        private static Color GetColor(InternalPosition position)
        {
            return (Color) (Math.Abs(position.Horizontal - position.Vertical) % 2);
        }
    }
}