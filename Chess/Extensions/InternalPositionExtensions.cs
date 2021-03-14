using System;
using Chess.Models;

namespace Chess.Extensions
{
    public static class InternalPositionExtensions
    {
        public static bool IsVerticalMove(this InternalPosition currentPosition, InternalPosition futurePosition)
        {
            if (futurePosition == null)
            {
                throw new ArgumentNullException(nameof(futurePosition));
            }
            
            return currentPosition.Horizontal == futurePosition.Horizontal;
        }
        
        public static bool IsHorizontalMove(this InternalPosition currentPosition, InternalPosition futurePosition)
        {
            if (futurePosition == null)
            {
                throw new ArgumentNullException(nameof(futurePosition));
            }
            
            return currentPosition.Vertical == futurePosition.Vertical;
        }
        
        //todo: Возможно есть адекватная реализация данной проверки, переделать
        public static bool IsDiagonalMove(this InternalPosition currentPosition, InternalPosition futurePosition)
        {
            if (futurePosition == null)
            {
                throw new ArgumentNullException(nameof(futurePosition));
            }
            
            return currentPosition.Horizontal > futurePosition.Horizontal &&
                   currentPosition.Vertical > futurePosition.Vertical ||
                   currentPosition.Horizontal < futurePosition.Horizontal &&
                   currentPosition.Vertical < futurePosition.Vertical;
        }
        
        //todo: Возможно есть адекватная реализация данной проверки, переделать
        public static bool IsAntiDiagonalMove(this InternalPosition currentPosition, InternalPosition futurePosition)
        {
            if (futurePosition == null)
            {
                throw new ArgumentNullException(nameof(futurePosition));
            }
            
            return currentPosition.Horizontal > futurePosition.Horizontal &&
                   currentPosition.Vertical < futurePosition.Vertical ||
                   currentPosition.Horizontal < futurePosition.Horizontal &&
                   currentPosition.Vertical > futurePosition.Vertical;
        }

        public static bool Contains(this InternalPosition currentPosition, InternalPosition startPosition, InternalPosition endPosition)
        {
            if (startPosition == null)
            {
                throw new ArgumentNullException(nameof(startPosition));
            }
            
            if (endPosition == null)
            {
                throw new ArgumentNullException(nameof(endPosition));
            }
            
            return currentPosition.Horizontal >= startPosition.Horizontal &&
                   currentPosition.Vertical >= startPosition.Vertical &&
                   currentPosition.Horizontal <= endPosition.Horizontal &&
                   currentPosition.Vertical <= endPosition.Vertical;
        }
    }
}