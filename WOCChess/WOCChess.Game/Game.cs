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
        public ulong WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
        public ulong WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
        public ulong BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
        public ulong BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackQueens = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKing = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;

        public bool WhiteLongCastle = false;
        public bool WhiteShortCastle = false;
        public bool BlackLongCastle = false;
        public bool BlackShortCastle = false;

        public ulong AllWhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        public ulong AllBlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        public ulong AllPieces => AllWhitePieces | AllBlackPieces;

        public void Start()
        {
            WhiteToMove?.Invoke();
        }

        /// <summary>Get all valid moves for a king.</summary>
        /// <param name="king">The king.</param>
        /// <param name="side">All of the pieces on the king's side.</param>
        public ulong ValidKingMoves(ulong king, ulong side)
        {
            ulong kingClipFileH = king & Bitboard.ClearFile(File.H);
            ulong kingClipFileA = king & Bitboard.ClearFile(File.A);
            return (kingClipFileH << 7 | king << 8 | kingClipFileH << 9 | kingClipFileH << 1 | kingClipFileA >> 7 | king >> 8 | kingClipFileA >> 9 | kingClipFileA >> 1) & ~side;
        }

        public void UnsafeWhiteKingMove(ulong king)
        {
            WhiteKing = king;
            BlackToMove?.Invoke();
        }

        /// <summary>Get all valid moves for a knight.</summary>
        /// <param name="knight">The knight.</param>
        /// <param name="side">All of the pieces on the knight's side.</param>
        public ulong ValidKnightMoves(ulong knight, ulong side)
        {
            ulong spot1Clip = Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B);
	        ulong spot2Clip = Bitboard.ClearFile(File.A);
	        ulong spot3Clip = Bitboard.ClearFile(File.H);
	        ulong spot4Clip = Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G);
	        ulong spot5Clip = Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G);
	        ulong spot6Clip = Bitboard.ClearFile(File.H);
	        ulong spot7Clip = Bitboard.ClearFile(File.A);
	        ulong spot8Clip = Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B);
            return ((knight & spot1Clip) << 6 | (knight & spot2Clip) << 15 | (knight & spot3Clip) << 17 | (knight & spot4Clip) << 10 | 
            (knight & spot5Clip) >> 6 | (knight & spot6Clip) >> 15 | (knight & spot7Clip) >> 17 | (knight & spot8Clip) >> 10) & ~side;
        }

        /// <summary>Get all valid moves for a white pawn.</summary>
        /// <param name="pawn">The white pawn.</param>
        public ulong ValidWhitePawnMoves(ulong pawn)
        {
            return (((pawn << 8) & ~AllPieces) | ((((pawn << 8) & ~AllPieces) & Bitboard.MaskRank(Rank.R3)) << 8) & ~AllPieces) | ((((pawn & Bitboard.ClearFile(File.A)) << 7) | ((pawn & Bitboard.ClearFile(File.H)) << 9)) & AllBlackPieces);
        }

        /// <summary>Get all valid moves for a black pawn.</summary>
        /// <param name="pawn">The black pawn.</param>
        public ulong ValidBlackPawnMoves(ulong pawn)
        {
            return (((pawn >> 8) & ~AllPieces) | (((((pawn >> 8) & ~AllPieces) & Bitboard.MaskRank(Rank.R6)) >> 8) & ~AllPieces)) | 
            ((((pawn & Bitboard.ClearFile(File.A)) >> 9) | ((pawn & Bitboard.ClearFile(File.H)) >> 7)) & AllWhitePieces);
        }

        /// <summary>Promote a white pawn without any checks.</summary>
        /// <param name="pawn">The white pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void UnsafePromoteWhite(ulong pawn, PromotionPiece promotionPiece)
        {
            WhitePawns ^= pawn;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    WhiteKnights |= pawn;
                    break;
                case PromotionPiece.Bishop:
                    WhiteBishops |= pawn;
                    break;
                case PromotionPiece.Rook:
                    WhiteRooks |= pawn;
                    break;
                case PromotionPiece.Queen:
                    WhiteQueens |= pawn;
                    break;
            }
            BlackToMove?.Invoke();
        }

        /// <summary>Promote a black pawn without any checks.</summary>
        /// <param name="pawn">The black pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void UnsafePromoteBlack(ulong pawn, PromotionPiece promotionPiece)
        {
            BlackPawns ^= pawn;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    BlackKnights |= pawn;
                    break;
                case PromotionPiece.Bishop:
                    BlackBishops |= pawn;
                    break;
                case PromotionPiece.Rook:
                    BlackRooks |= pawn;
                    break;
                case PromotionPiece.Queen:
                    BlackQueens |= pawn;
                    break;
            }
            WhiteToMove?.Invoke();
        }

        /// <summary>Promote a white pawn.</summary>
        /// <param name="pawn">The white pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void PromoteWhite(ulong pawn, PromotionPiece promotionPiece)
        {
            if (WhitePawns != (WhitePawns & pawn) || pawn == (pawn & Bitboard.MaskRank(Rank.R7)))
            {
                return;
            }
            WhitePawns ^= pawn;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    WhiteKnights |= pawn;
                    break;
                case PromotionPiece.Bishop:
                    WhiteBishops |= pawn;
                    break;
                case PromotionPiece.Rook:
                    WhiteRooks |= pawn;
                    break;
                case PromotionPiece.Queen:
                    WhiteQueens |= pawn;
                    break;
            }
            BlackToMove?.Invoke();
        }

        /// <summary>Promote a black pawn.</summary>
        /// <param name="pawn">The black pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void PromoteBlack(ulong pawn, PromotionPiece promotionPiece)
        {
            if (BlackPawns != (BlackPawns & pawn) || pawn == (pawn & Bitboard.MaskRank(Rank.R1)))
            {
                return;
            }
            BlackPawns ^= pawn;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    BlackKnights |= pawn;
                    break;
                case PromotionPiece.Bishop:
                    BlackBishops |= pawn;
                    break;
                case PromotionPiece.Rook:
                    BlackRooks |= pawn;
                    break;
                case PromotionPiece.Queen:
                    BlackQueens |= pawn;
                    break;
            }
            WhiteToMove?.Invoke();
        }
    }
}

/*

BitBoards[piece] &= ~(1U << from); //reset bit at previous position
BitBoards[piece] |= 1U << to; //set bit at current position

*/