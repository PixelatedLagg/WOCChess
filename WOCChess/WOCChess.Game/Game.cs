namespace WOCChess.Game
{
    public class Game
    {
        public Action? WhiteToMove;
        public Action? BlackToMove;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw

        public static ulong[] BitBoards = new ulong[12]
        {
            0b_00000000_11111111_000000000000000000000000000000000000000000000000UL, //0 w pawns
            0b_01000010_00000000_000000000000000000000000000000000000000000000000UL, //1 w knights
            0b_00100100_00000000_000000000000000000000000000000000000000000000000UL, //2 w bishops
            0b_10000001_00000000_000000000000000000000000000000000000000000000000UL, //3 w rooks
            0b_00010000_00000000_000000000000000000000000000000000000000000000000UL, //4 w queen
            0b_00001000_00000000_000000000000000000000000000000000000000000000000UL, //5 w king
            0b_000000000000000000000000000000000000000000000000_11111111_00000000UL, //6 b pawns
            0b_000000000000000000000000000000000000000000000000_00000000_01000010UL, //7 b knights
            0b_000000000000000000000000000000000000000000000000_00000000_00100100UL, //8 b bishops
            0b_000000000000000000000000000000000000000000000000_00000000_10000001UL, //9 b rooks
            0b_000000000000000000000000000000000000000000000000_00000000_00010000UL, //10 b queen
            0b_000000000000000000000000000000000000000000000000_00000000_00001000UL, //11 b king
        };

        public bool Turn = true; //true is white, false is black

        public void Start()
        {
            WhiteToMove?.Invoke();
        }

        public void Move(int from, int to, int piece)
        {
            BitBoards[piece] &= ~(1U << from); //reset bit at previous position
            BitBoards[piece] |= 1U << to; //set bit at current position
        }
    }
}
