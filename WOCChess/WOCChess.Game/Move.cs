namespace WOCChess.Game
{
    public class Move
    {
        public int From, To;
        public Piece PieceMoved;

        public Move(int from, int to, Piece pieceMoved)
        {
            From = from;
            To = to;
            PieceMoved = pieceMoved;
        }
    }
}