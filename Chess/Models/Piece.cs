using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Interfaces;
using Type = Chess.Enums.Type;

namespace Chess.Models
{
    public abstract class Piece
    {
        #region События
        public event Action<Piece> CaptureEvent;
        #endregion
        
        #region Свойства
        public Color Color  { get; }
        protected Type Type { get; }
        protected IBoard Board { get; }
        protected ISquare CurrentSquare { get; set; }
        protected ISquare StartSquare { get; }
        #endregion

        #region Конструктор
        protected Piece(IBoard board, ISquare square, Color color, Type type)
        {
            Board = board;
            
            CurrentSquare = square;
            StartSquare = square;

            Type = type;
            Color = color;
        }
        #endregion

        #region Перемещение
        public abstract Task<string> Move(ISquare square);
        #endregion

        #region Получение текущего положения
        public InternalPosition GetPosition()
        {
            return CurrentSquare.Position;
        }
        #endregion

        #region Проверка возможности перемещения по вертикали
        protected virtual bool CanVerticalMove(ISquare square, out string error)
        {
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;

            if (square.Piece != null && square.Piece.Color == Color)
            {
                return false;
            }
            
            var vertical = Board.GetVertical(this);
            return HasBlockingSquare(vertical, square);
        }
        #endregion

        #region Перемещение по вертикали
        protected virtual Task<string> VerticalMove(ISquare square)
        {
            if (!CanVerticalMove(square, out var error))
            {
                return Task.FromResult(error);
            }
            
            Shift(square);
            return Task.FromResult(error);
        }
        #endregion
        
        #region Проверка возможности перемещения по горизонтали
        protected virtual bool CanHorizontalMove(ISquare square, out string error)
        {
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;

            if (square.Piece != null && square.Piece.Color == Color)
            {
                return false;
            }
            
            var vertical = Board.GetHorizontal(this);
            return HasBlockingSquare(vertical, square);
        }
        #endregion
        
        #region Перемещение по горизонтали
        protected virtual Task<string> HorizontalMove(ISquare square)
        {
            if (!CanHorizontalMove(square, out var error))
            {
                return Task.FromResult(error);
            }
            
            Shift(square);
            return Task.FromResult(error);
        }
        #endregion
        
        #region Проверка возможности перемещения по диагонали
        protected virtual bool CanDiagonalMove(ISquare square, out string error)
        {
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;
            
            if (square.Piece != null && square.Piece.Color == Color)
            {
                return false;
            }

            var vertical = Board.GetDiagonal(this);
            return HasBlockingSquare(vertical, square);
        }
        #endregion
        
        #region Перемещение по диагонали
        protected virtual Task<string> DiagonalMove(ISquare square)
        {
            if (!CanDiagonalMove(square, out var error))
            {
                return Task.FromResult(error);
            }
            
            Shift(square);
            return Task.FromResult(error);
        }
        #endregion
        
        #region Проверка возможности перемещения по побочной диагонали
        protected virtual bool CanAntiDiagonalMove(ISquare square, out string error)
        {
            //todo: Отдавать человеческое описание ошибки
            error = string.Empty;

            if (square.Piece != null && square.Piece.Color == Color)
            {
                return false;
            }
            
            var vertical = Board.GetAntiDiagonal(this);
            return HasBlockingSquare(vertical, square);
        }
        #endregion
        
        #region Перемещение по побочной диагонали
        protected virtual Task<string> AntiDiagonalMove(ISquare square)
        {
            if (!CanAntiDiagonalMove(square, out var error))
            {
                return Task.FromResult(error);
            }
            
            Shift(square);
            return Task.FromResult(error);
        }
        #endregion

        #region Проверка на блокировку перемещения другими фигурами
        private bool HasBlockingSquare(List<ISquare> squares, ISquare futureSquare)
        {
            var currentPositionOnBoard = squares.IndexOf(CurrentSquare);
            var futurePositionOnBoard = squares.IndexOf(futureSquare);

            if (futurePositionOnBoard == -1)
            {
                return false;
            }
            
            var startIndex = Math.Min(currentPositionOnBoard, futurePositionOnBoard);
            var length = Math.Abs(futurePositionOnBoard - currentPositionOnBoard) + 1;
            
            var availableSquares = squares.GetRange(startIndex, length);
            availableSquares.Remove(CurrentSquare);
            
            if (currentPositionOnBoard > futurePositionOnBoard)
            {
                availableSquares.Reverse();
            }

            var blockingSquare = availableSquares.FirstOrDefault(x => x.Piece != null);
            return blockingSquare == null || blockingSquare == futureSquare;
        }
        #endregion
        
        #region Смещение
        protected void Shift(ISquare square)
        {
            var piece = square.Piece;
            
            if (piece != null)
            {
                CaptureEvent?.Invoke(piece);
            }
            
            square.Piece = this;
            CurrentSquare.Piece = null;
            CurrentSquare = square;
        }
        #endregion
    }
}