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

        public Move(bool longCastle)
        {
            From = -1;
            To = -1;
            if (longCastle)
            {
                PieceMoved = Piece.LongCastle;
            }
            else
            {
                PieceMoved = Piece.ShortCastle;
            }
        }

        public Move(Piece promotionPiece, int from, int to)
        {
            From = from;
            To = to;
            switch (promotionPiece)
            {
                //promotion check
            }
        }
    }
}