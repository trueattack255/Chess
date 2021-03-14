using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Extensions;
using Chess.Interfaces;
using Chess.Models;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(IBoard board, ISquare square, Color color) : base(board, square, color, Type.Knight)
        {

        }

        public override Task<string> Move(ISquare square)
        {
            if (!CanMove(square, out var error))
            {
                return Task.FromResult(error);
            }
            
            Shift(square);
            return Task.FromResult(error);
        }

        protected virtual bool CanMove(ISquare square, out string error)
        {
            var currentPosition = CurrentSquare.Position;
            var startBoardPosition = new InternalPosition(0, 0);
            var endBoardPosition = new InternalPosition(7, 7);

            //todo: Возможно есть адекватная реализация данной проверки, переделать
            var availablePosition = new List<InternalPosition>
                {
                    new InternalPosition(currentPosition.Horizontal - 1, currentPosition.Vertical + 2),
                    new InternalPosition(currentPosition.Horizontal + 1, currentPosition.Vertical + 2),
                    new InternalPosition(currentPosition.Horizontal + 2, currentPosition.Vertical + 1),
                    new InternalPosition(currentPosition.Horizontal + 2, currentPosition.Vertical - 1),
                    new InternalPosition(currentPosition.Horizontal + 1, currentPosition.Vertical - 2),
                    new InternalPosition(currentPosition.Horizontal - 1, currentPosition.Vertical - 2),
                    new InternalPosition(currentPosition.Horizontal - 2, currentPosition.Vertical - 1),
                    new InternalPosition(currentPosition.Horizontal - 2, currentPosition.Vertical + 1)
                }
                .Where(x => x.Contains(startBoardPosition, endBoardPosition))
                .FirstOrDefault(x => square.Position == x);
            
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;
            
            return availablePosition != null && (square.Piece == null || square.Piece.Color != Color);
        }
    }
}