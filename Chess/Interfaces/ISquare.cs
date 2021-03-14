using Chess.Enums;
using Chess.Models;

namespace Chess.Interfaces
{
    public interface ISquare
    {
        Color Color { get; }
        InternalPosition Position { get; }
        
        Piece Piece { get; set; }
    }
}