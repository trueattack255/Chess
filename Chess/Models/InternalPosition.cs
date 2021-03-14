using System;

namespace Chess.Models
{
    public class InternalPosition : IEquatable<InternalPosition>
    {
        public int Vertical { get; }
        public int Horizontal { get; }

        public InternalPosition(int posX, int posY)
        {
            Vertical = posY;
            Horizontal = posX;
        }

        #region Переопределения
        public override string ToString()
        {
            return $"{Horizontal}:{Vertical}";
        }

        public override bool Equals(object obj)
        {
            return obj is InternalPosition position && Equals(position);
        }

        public bool Equals(InternalPosition position)
        {
            return position is not null && (Vertical, Horizontal).Equals((position.Vertical, position.Horizontal));
        }
        
        public override int GetHashCode()
        {
            return (Vertical, Horizontal).GetHashCode();
        }
        
        public static bool operator == (InternalPosition lhs, InternalPosition rhs)
        {
            return lhs is not null && lhs.Equals(rhs);
        }

        public static bool operator != (InternalPosition lhs, InternalPosition rhs)
        {
            return lhs is not null && !lhs.Equals(rhs);
        }
        #endregion

        #region Перегрузки
        public static explicit operator InternalPosition(ExternalPosition position)
        {
            return new InternalPosition(position.Horizontal - 65, position.Vertical - 1);
        }
        #endregion
    }
}