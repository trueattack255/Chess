namespace Chess.Models
{
    public class ExternalPosition
    {
        public int Vertical { get; }
        public char Horizontal { get; }
        
        public ExternalPosition(char posX, int posY)
        {
            Vertical = posY;
            Horizontal = posX;
        }

        #region Переопределения
        public override string ToString()
        {
            return $"{Horizontal}{Vertical}";
        }
        #endregion

        #region Перегрузки
        public static explicit operator ExternalPosition(InternalPosition position)
        {
            return new ExternalPosition((char)(position.Horizontal + 65), position.Vertical + 1);
        }
        
        public static explicit operator ExternalPosition(string position)
        {
            return new ExternalPosition(position[0], position[1] - 48);
        }
        #endregion
    }
}