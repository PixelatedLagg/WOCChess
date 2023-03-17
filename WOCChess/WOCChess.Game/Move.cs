namespace WOCChess.Game
{
    public class Move
    {
        public int FromX, FromY, ToX, ToY, Piece;

        public Move(int fromX, int fromY, int toX, int toY, int piece)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
            Piece = piece;
        }
    }
}