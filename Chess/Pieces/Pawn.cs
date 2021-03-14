using System;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Extensions;
using Chess.Interfaces;
using Chess.Models;
using Type = Chess.Enums.Type;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(IBoard board, ISquare square, Color color) : base(board, square, color, Type.Pawn)
        {

        }

        public override Task<string> Move(ISquare square)
        {
            if (CurrentSquare == square)
            {
                //todo: Отдавать человеческое описание ошибки
                Task.FromResult(string.Empty);
            }

            var task = CurrentSquare.Position.IsVerticalMove(square.Position)
                ? VerticalMove(square)
                : CurrentSquare.Position.IsDiagonalMove(square.Position)
                    ? DiagonalMove(square)
                    : AntiDiagonalMove(square);

            return task;
        }

        protected override bool CanVerticalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Vertical - square.Position.Vertical);

            if (step == 1 || step == 2 && StartSquare == CurrentSquare)
            {
                return base.CanVerticalMove(square, out error);
            }
            
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;
            return false;
        }

        protected override bool CanDiagonalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Vertical - square.Position.Vertical);

            if (step == 1 && square.Piece != null)
            {
                return base.CanDiagonalMove(square, out error);
            }
            
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;
            return false;
        }

        protected override bool CanAntiDiagonalMove(ISquare square, out string error)
        {
            var step = Math.Abs(CurrentSquare.Position.Vertical - square.Position.Vertical);

            if (step == 1 && square.Piece != null)
            {
                return base.CanAntiDiagonalMove(square, out error);
            }
            
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;
            return false;
        }
    }
}