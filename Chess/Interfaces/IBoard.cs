using Chess.Models;
using System.Collections.Generic;

namespace Chess.Interfaces
{
    public interface IBoard
    {
        ISquare this[int x, int y] { get; }

        List<Piece> White { get; }
        List<Piece> Black { get; }
        
        List<Piece> WhiteCaptured { get; }
        List<Piece> BlackCaptured { get; }

        void Move(ExternalPosition startPosition, ExternalPosition endPosition);
        
        List<ISquare> GetVertical(Piece piece);
        List<ISquare> GetHorizontal(Piece piece);
        List<ISquare> GetDiagonal(Piece piece);
        List<ISquare> GetAntiDiagonal(Piece piece);
    }
}