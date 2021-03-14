using System.Threading.Tasks;
using Chess.Enums;
using Chess.Extensions;
using Chess.Interfaces;
using Chess.Models;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(IBoard board, ISquare square, Color color) : base(board, square, color, Type.Rook)
        {
            
        }

        public override Task<string> Move(ISquare square)
        {
            if (CurrentSquare == square)
            {
                //todo: Отдавать человеческое описание ошибки
                Task.FromResult(string.Empty);
            }
            
            var task = CurrentSquare.Position.IsHorizontalMove(square.Position)
                ? HorizontalMove(square)
                : VerticalMove(square);

            return task;
        }
    }
}