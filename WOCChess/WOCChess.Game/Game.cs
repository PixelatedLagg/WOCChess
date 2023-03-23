namespace WOCChess.Game
{
    public class Game
    {
        public Action? WhiteToMove;
        public Action? BlackToMove;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw

        ulong WhitePawns = 0B_000000000000000000000000000000000000000000000000_11111111_00000000UL;
        ulong WhiteRooks = 0B_000000000000000000000000000000000000000000000000_00000000_10000001UL;
        ulong WhiteKnights = 0B_000000000000000000000000000000000000000000000000_00000000_01000010UL;
        ulong WhiteBishops = 0B_000000000000000000000000000000000000000000000000_00000000_00100100UL;
        ulong WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
        ulong WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
        ulong BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
        ulong BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
        ulong BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
        ulong BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
        ulong BlackQueens = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;
        ulong BlackKing = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;

        ulong AllWhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        ulong AllBlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        ulong AllPieces => AllWhitePieces | AllBlackPieces;

        public bool Turn = true; //true is white, false is black

        public void Start()
        {
            WhiteToMove?.Invoke();
        }
    }
}
