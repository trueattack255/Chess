using System;
using Chess.Enums;
using Chess.Models;
using Chess.Pieces;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("[X]: Start Game!");
            PrintBoard(board);
            
            var startPosition = (ExternalPosition) "D2";
            var endPosition = (ExternalPosition) "D4";
            board.Move(startPosition, endPosition);
            var whiteScore = board.BlackCaptured.Count;
            var blackScore = board.WhiteCaptured.Count;
            
            Console.WriteLine($"[1]: {startPosition}->{endPosition}; [Score]: {whiteScore}-{blackScore}");
            PrintBoard(board);

            Console.ReadKey(true);
            Console.ResetColor();
        }

        static void PrintBoard(Board board)
        {
            for (var i = 7; i > -1; i--)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"{i + 1} ");
                
                for (var j = 0; j < 8; j++)
                {
                    Console.BackgroundColor = board[j, i].Color == Color.Black
                        ? ConsoleColor.DarkGray
                        : ConsoleColor.Gray;

                    var hasPiece = board[j, i].Piece != null;
                    
                    if (hasPiece)
                    {
                        Console.ForegroundColor = board[j, i].Piece.Color == Color.Black
                            ? ConsoleColor.DarkBlue
                            : ConsoleColor.Blue;
                    }
                    
                    var symbol = hasPiece
                        ? GetPieceSymbol(board[j, i].Piece)
                        : " ";

                    Console.Write($" {symbol} ");
                }
                
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("  ");

            foreach (var item in new[] {"A", "B", "C", "D", "E", "F", "G", "H"})
            {
                Console.Write($" {item} ");
            }

            Console.WriteLine();
        }
        
        static string GetPieceSymbol(Piece piece)
        {
            return piece switch
            {
                Rook => "♜",
                Bishop => "♝",
                Knight => "♞",
                Queen => "♛",
                King => "♚",
                Pawn => "♟",
                _ => throw new NotSupportedException()
            };
        }
    }
}