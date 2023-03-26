namespace WOCChess.Game
{
    public class Game
    {
        public Action? WhiteToMove;
        public Action? BlackToMove;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw
        public List<Move> Moves = new List<Move>();
        public bool Turn => Moves.Count % 2 == 0; //true is white, false is black

        public ulong WhitePawns = 0B_000000000000000000000000000000000000000000000000_11111111_00000000UL;
        public ulong WhiteRooks = 0B_000000000000000000000000000000000000000000000000_00000000_10000001UL;
        public ulong WhiteKnights = 0B_000000000000000000000000000000000000000000000000_00000000_01000010UL;
        public ulong WhiteBishops = 0B_000000000000000000000000000000000000000000000000_00000000_00100100UL;
        public ulong WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
        public ulong WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
        public ulong BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
        public ulong BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackQueens = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKing = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;

        public ulong AllWhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        public ulong AllBlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        public ulong AllPieces => AllWhitePieces | AllBlackPieces;

        public void Start()
        {
            WhiteToMove?.Invoke();
        }

        public void UnsafeMove(Move move) //does not check if move is legal
        {
            if (Turn) //white move
            {
                Console.WriteLine("a");
                switch (move.PieceMoved)
                {
                    case Piece.Pawn:
                        Console.WriteLine("b");
                        WhitePawns &= ~(1U << move.From);
                        WhitePawns |= 1U << move.To;
                        break;
                    case Piece.Knight:
                        WhiteKnights &= ~(1U << move.From);
                        WhiteKnights |= 1U << move.To;
                        break;
                    case Piece.Bishop:
                        WhiteBishops &= ~(1U << move.From);
                        WhiteBishops |= 1U << move.To;
                        break;
                    case Piece.Rook:
                        WhiteRooks &= ~(1U << move.From);
                        WhiteRooks |= 1U << move.To;
                        break;
                    case Piece.Queen:
                        WhiteQueens &= ~(1U << move.From);
                        WhiteQueens |= 1U << move.To;
                        break;
                    case Piece.King:
                        WhiteKing &= ~(1U << move.From);
                        WhiteKing |= 1U << move.To;
                        break;
                }
                BlackToMove?.Invoke();
            }
            else //black move
            {
                WhiteToMove?.Invoke();
            }
            Moves.Add(move);
        }
    }
}

/*

BitBoards[piece] &= ~(1U << from); //reset bit at previous position
BitBoards[piece] |= 1U << to; //set bit at current position

*/