namespace WOCChess.Game
{
    public class Game
    {
        public Action<Move[]>? WhiteToMove;
        public Action<Move[]>? BlackToMove;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw

        ulong[] BitBoards = new ulong[12];
        /*
        0 W Pawns
        1 W Knights
        2 W Bishops
        3 W Rooks
        4 W Queen
        5 W King
        6 B Pawns
        7 B Knights
        8 B Bishops
        9 B Rooks
        10 B Queen
        11 B King
        */
        int[,] Board = new int[8, 8] 
        {
            { 14, 12, 13, 15, 16, 13, 12, 14 },
            { 11, 11, 11, 11, 11, 11, 11, 11 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1 },
            { 4, 2, 3, 5, 6, 3, 2, 4 }
        };

        List<(int, int)> PinnedPieces = new List<(int, int)>();

        public bool Turn = true; //true is white, false is black

        public void Start()
        {
            //WhiteToMove?.Invoke(AllLegalMovesWhite());
        }

        private string Format(int i)
        {
            if (i > 9)
            {
                return $"{i}";
            }
            return $"{i} ";
        }
    }
}
