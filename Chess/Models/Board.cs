using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Interfaces;
using Chess.Pieces;
using Type = Chess.Enums.Type;

namespace Chess.Models
{
    public class Board : IBoard
    {
        #region Индексатор
        public ISquare this[int x, int y]
        {
            get => _squares[y, x];
            private set => _squares[y, x] = value;
        }
        #endregion

        #region Свойства
        public List<Piece> White => _whiteList;
        public List<Piece> Black => _blackList;
        public List<Piece> WhiteCaptured => _whiteCapturedList;
        public List<Piece> BlackCaptured => _blackCapturedList;
        #endregion

        #region Поля
        private Dictionary<int, List<ISquare>> _diagonalCache;
        private Dictionary<int, List<ISquare>> _antiDiagonalCache;
        private Dictionary<int, List<ISquare>> _verticalCache;
        private Dictionary<int, List<ISquare>> _horizontalCache;

        private List<Piece> _whiteList;
        private List<Piece> _blackList;
        private List<Piece> _whiteCapturedList;
        private List<Piece> _blackCapturedList;
        
        private ISquare[,] _squares;
        private const int Size = 8;
        #endregion

        #region Конструктор
        public Board()
        {
            _squares = new ISquare[Size, Size];
            
            _diagonalCache = new Dictionary<int, List<ISquare>>();
            _antiDiagonalCache = new Dictionary<int, List<ISquare>>();
            _verticalCache = new Dictionary<int, List<ISquare>>();
            _horizontalCache = new Dictionary<int, List<ISquare>>();
            
            _whiteList = new List<Piece>();
            _blackList = new List<Piece>();
            _whiteCapturedList = new List<Piece>();
            _blackCapturedList = new List<Piece>();
            
            FillBoard();
            InitCache();
        }
        #endregion

        #region Инициализация кэша
        private void InitCache()
        {
            var board = this;
            
            for (var i = 0; i < Size; i++)
            {
                var verticals = new List<ISquare>(Size);
                var horizontals = new List<ISquare>(Size);
                
                for (var j = 0; j < Size; j++)
                {
                    verticals.Add(board[i, j]);
                    horizontals.Add(board[j, i]);
                    
                    var sum = i + j;

                    if (!_antiDiagonalCache.TryGetValue(sum, out var antiDiagonal))
                    {
                        antiDiagonal = new List<ISquare>();
                        _antiDiagonalCache.Add(sum, antiDiagonal);
                    }
                    
                    antiDiagonal.Add(board[i, j]);
                    
                    if (!_diagonalCache.TryGetValue(sum, out var diagonal))
                    {
                        diagonal = new List<ISquare>();
                        _diagonalCache.Add(sum, diagonal);
                    }
                    
                    diagonal.Add(board[j, Size-1-i]);
                }
                
                _verticalCache.Add(i, verticals);
                _horizontalCache.Add(i, horizontals);
            }
        }
        #endregion

        #region Заполнение борда
        private void FillBoard()
        {
            var board = this;
            
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    board[j,i] = new Square(new InternalPosition(j, i));
                }
            }
            
            #region Белые
            CreatePiece(0, 0, Type.Rook, Color.White);
            CreatePiece(1, 0, Type.Knight, Color.White);
            CreatePiece(2, 0, Type.Bishop, Color.White);
            CreatePiece(3, 0, Type.Queen, Color.White);
            CreatePiece(4, 0, Type.King, Color.White);
            CreatePiece(5, 0, Type.Bishop, Color.White);
            CreatePiece(6, 0, Type.Knight, Color.White);
            CreatePiece(7, 0, Type.Rook, Color.White);

            CreatePiece(0, 1, Type.Pawn, Color.White);
            CreatePiece(1, 1, Type.Pawn, Color.White);
            CreatePiece(2, 1, Type.Pawn, Color.White);
            CreatePiece(3, 1, Type.Pawn, Color.White);
            CreatePiece(4, 1, Type.Pawn, Color.White);
            CreatePiece(5, 1, Type.Pawn, Color.White);
            CreatePiece(6, 1, Type.Pawn, Color.White);
            CreatePiece(7, 1, Type.Pawn, Color.White);
            #endregion
            
            #region Черные
            CreatePiece(0, 7, Type.Rook, Color.Black);
            CreatePiece(1, 7, Type.Knight, Color.Black);
            CreatePiece(2, 7, Type.Bishop, Color.Black);
            CreatePiece(3, 7, Type.Queen, Color.Black);
            CreatePiece(4, 7, Type.King, Color.Black);
            CreatePiece(5, 7, Type.Bishop, Color.Black);
            CreatePiece(6, 7, Type.Knight, Color.Black);
            CreatePiece(7, 7, Type.Rook, Color.Black);

            CreatePiece(0, 6, Type.Pawn, Color.Black);
            CreatePiece(1, 6, Type.Pawn, Color.Black);
            CreatePiece(2, 6, Type.Pawn, Color.Black);
            CreatePiece(3, 6, Type.Pawn, Color.Black);
            CreatePiece(4, 6, Type.Pawn, Color.Black);
            CreatePiece(5, 6, Type.Pawn, Color.Black);
            CreatePiece(6, 6, Type.Pawn, Color.Black);
            CreatePiece(7, 6, Type.Pawn, Color.Black);
            #endregion
        }
        #endregion

        #region Создание фигуры
        private void CreatePiece(int x, int y, Type type, Color color)
        {
            var board = this;
            
            Piece piece = type switch
            {
                Type.Pawn => new Pawn(board, board[x, y], color),
                Type.Rook => new Rook(board, board[x, y], color),
                Type.Knight => new Knight(board, board[x, y], color),
                Type.Bishop => new Bishop(board, board[x, y], color),
                Type.Queen => new Queen(board, board[x, y], color),
                Type.King => new King(board, board[x, y], color),
                _ => throw new NotSupportedException()
            };
            
            switch (color)
            {
                case Color.Black:
                    _blackList.Add(piece);
                    break;
                case Color.White:
                    _whiteList.Add(piece);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            board[x, y].Piece = piece;
            piece.CaptureEvent += CapturePiece;
        }
        #endregion

        #region Захват фигуры
        private void CapturePiece(Piece piece)
        {
            switch (piece.Color)
            {
                case Color.Black:
                    _blackCapturedList.Add(piece);
                    break;
                case Color.White:
                    _whiteCapturedList.Add(piece);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Перемещение
        public void Move(ExternalPosition startPosition, ExternalPosition endPosition)
        {
            var board = this;
            var squarePosition = (InternalPosition) startPosition;
            var futurePosition = (InternalPosition) endPosition;
            
            var squareStart = board[squarePosition.Horizontal, squarePosition.Vertical];
            var squareEnd = board[futurePosition.Horizontal, futurePosition.Vertical];

            squareStart?.Piece?.Move(squareEnd);
        }
        #endregion

        #region Получение списка клеток вертикали, содержащей указаную фигуру
        public List<ISquare> GetVertical(Piece piece)
        {
            var position = piece.GetPosition();
            
            _verticalCache.TryGetValue(position.Horizontal, out var result);
            return result;
        }
        #endregion

        #region Получение списка клеток горизонтали, содержащей указаную фигуру
        public List<ISquare> GetHorizontal(Piece piece)
        {
            var position = piece.GetPosition();
            
            _horizontalCache.TryGetValue(position.Vertical, out var result);
            return result;
        }
        #endregion
        
        #region Получение списка клеток диагонали, содержащей указаную фигуру
        public List<ISquare> GetDiagonal(Piece piece)
        {
            var position = piece.GetPosition();
            var rotateSum = position.Horizontal + Size - 1 - position.Vertical;
            
            _diagonalCache.TryGetValue(rotateSum, out var result);
            return result;
        }
        #endregion

        #region Получение списка клеток побочной диагонали, содержащей указаную фигуру
        public List<ISquare> GetAntiDiagonal(Piece piece)
        {
            var position = piece.GetPosition();
            var sum = position.Vertical + position.Horizontal;
            
            _antiDiagonalCache.TryGetValue(sum, out var result);
            return result;
        }
        #endregion
    }
}