using System;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Extensions;
using Chess.Interfaces;
using Chess.Models;
using Type = Chess.Enums.Type;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(IBoard board, ISquare square, Color color) : base(board, square, color, Type.King)
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

        protected override bool CanVerticalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Vertical - square.Position.Vertical);

            if (step > 1)
            {
                //todo: Отдавать человеческое описание ошибки
                error = string.Empty;
                return false;
            }
            
            return base.CanVerticalMove(square, out error);
        }

        protected override bool CanHorizontalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Horizontal - square.Position.Horizontal);

            if (step > 1)
            {
                //todo: Отдавать человеческое описание ошибки
                error = string.Empty;
                return false;
            }
            
            return base.CanHorizontalMove(square, out error);
        }

        protected override bool CanDiagonalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Horizontal - square.Position.Horizontal);

            if (step > 1)
            {
                //todo: Отдавать человеческое описание ошибки
                error = string.Empty;
                return false;
            }
            
            return base.CanDiagonalMove(square, out error);
        }

        protected override bool CanAntiDiagonalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Horizontal - square.Position.Horizontal);

            if (step > 1)
            {
                //todo: Отдавать человеческое описание ошибки
                error = string.Empty;
                return false;
            }
            
            return base.CanAntiDiagonalMove(square, out error);
        }
    }
}