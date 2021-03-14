using System.Reflection.Metadata;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Extensions;
using Chess.Interfaces;
using Chess.Models;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(IBoard board, ISquare square, Color color) : base(board, square, color, Type.Queen)
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
                : CurrentSquare.Position.IsVerticalMove(square.Position)
                    ? VerticalMove(square)
                    : CurrentSquare.Position.IsDiagonalMove(square.Position)
                        ? DiagonalMove(square)
                        : AntiDiagonalMove(square);

            return task;
        }
    }
}