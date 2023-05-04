namespace WOCChess.Game
{
    public class Game
    {
        public Action? WhiteToMove;
        public Action? BlackToMove;
        public Action<string>? Error;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw
        public bool Turn = true; //true is white, false is black
        public bool Check = false;
        public int HalfMoves = 0;

        public ulong WhitePawns = 0B_000000000000000000000000000000000000000000000000_11111111_00000000UL;
        //public ulong WhiteRooks = 0B_000000000000000000000000000000000000000000000000_00000000_10000001UL;
        public ulong WhiteRooks = 0B_000000000000000000000000000000000010000000001000_00000000_00000000UL;
        public ulong WhiteKnights = 0B_000000000000000000000000000000000000000000000000_00000000_01000010UL;
        public ulong WhiteBishops = 0B_000000000000000000000000000000000000000000000000_00000000_00100100UL;
        public ulong WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
        public ulong WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
        public ulong WhiteEPPawns = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
        public ulong BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackQueens = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackKing = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;
        public ulong BlackEPPawns = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;

        public bool WhiteLongCastle = true;
        public bool WhiteShortCastle = true;
        public bool BlackLongCastle = true;
        public bool BlackShortCastle = true;

        public ulong AllWhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        public ulong AllBlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        public ulong AllPieces => AllWhitePieces | AllBlackPieces;
        /*public ulong WhiteAttacks => ValidMoves.Knight(WhiteKnights, AllWhitePieces) | GetWhitePawnAttacks() | ValidMoves.King(WhiteKing, AllWhitePieces) 
        | ValidMoves.Queen(WhiteQueens, AllWhitePieces, AllBlackPieces) | ValidMoves.Rook(WhiteRooks, AllWhitePieces, AllBlackPieces) | ValidMoves.Bishop(WhiteBishops, AllWhitePieces, AllBlackPieces);
        public ulong BlackAttacks => ValidMoves.Knight(BlackKnights, AllBlackPieces) | GetBlackPawnAttacks() | ValidMoves.King(BlackKing, AllBlackPieces) 
        | ValidMoves.Queen(BlackQueens, AllBlackPieces, AllWhitePieces) | ValidMoves.Rook(BlackRooks, AllBlackPieces, AllWhitePieces) | ValidMoves.Bishop(BlackBishops, AllBlackPieces, AllWhitePieces);*/
        
        public void Start()
        {
            WhiteToMove?.Invoke();
        }

        /// <summary>Get all valid moves for a white pawn.</summary>
        /// <param name="knight">The white pawn to get all valid moves for.</param>
        /// <param name="friendly">All pieces friendly to the knight.</param>
        public ulong GetWhitePawnMoves()
        {
            return ((WhitePawns << 8 & ~AllPieces) | (((WhitePawns << 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R3)) << 8) & ~AllPieces) | 
            ((((WhitePawns & Bitboard.ClearFile(File.A)) << 7) | ((WhitePawns & Bitboard.ClearFile(File.H)) << 9)) & AllBlackPieces);
        }

        public ulong GetWhiteMoves()
        {
            ulong whiteMoves = GetWhitePawnMoves() | ValidMoves.KnightChecks(WhiteKnights) | ValidMoves.KingChecks(WhiteKing);
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((WhiteRooks | 1UL << i) == WhiteRooks) //found a rook
                {
                    whiteMoves |= ValidMoves.RookMovesWhite(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteQueens | 1UL << i) == WhiteQueens) //found a queen
                {
                    whiteMoves |= ValidMoves.QueenMovesWhite(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteBishops | 1UL << i) == WhiteBishops) //found a queen
                {
                    whiteMoves |= ValidMoves.BishopMovesWhite(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            return whiteMoves;
        }

        public ulong GetWhiteChecks()
        {
            ulong whiteChecks = ValidMoves.WhitePawnChecks(WhitePawns) | ValidMoves.KnightChecks(WhiteKnights) | ValidMoves.KingChecks(WhiteKing);
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((WhiteRooks | 1UL << i) == WhiteRooks) //found a rook
                {
                    whiteChecks |= ValidMoves.RookChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteQueens | 1UL << i) == WhiteQueens) //found a queen
                {
                    whiteChecks |= ValidMoves.QueenChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteBishops | 1UL << i) == WhiteBishops) //found a queen
                {
                    whiteChecks |= ValidMoves.BishopChecks(1UL << i, AllPieces);
                }
            }
            return whiteChecks;
        }

        public ulong GetWhitePins()
        {
            ulong whiteChecks = 0;
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((WhiteRooks | 1UL << i) == WhiteRooks) //found a rook
                {
                    whiteChecks |= ValidMoves.RookChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteQueens | 1UL << i) == WhiteQueens) //found a queen
                {
                    whiteChecks |= ValidMoves.QueenChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((WhiteBishops | 1UL << i) == WhiteBishops) //found a queen
                {
                    whiteChecks |= ValidMoves.BishopChecks(1UL << i, AllPieces);
                }
            }
            return whiteChecks;
        }

        public ulong GetBlackMoves()
        {
            ulong blackMoves = GetBlackPawnMoves() | ValidMoves.KnightChecks(BlackKnights) | ValidMoves.KingChecks(BlackKing);
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((BlackRooks | 1UL << i) == BlackRooks) //found a rook
                {
                    blackMoves |= ValidMoves.RookMovesBlack(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackQueens | 1UL << i) == BlackQueens) //found a queen
                {
                    blackMoves |= ValidMoves.QueenMovesBlack(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackBishops | 1UL << i) == BlackBishops) //found a queen
                {
                    blackMoves |= ValidMoves.BishopMovesBlack(1UL << i, AllPieces, AllBlackPieces, this);
                }
            }
            return blackMoves;
        }

        public ulong GetBlackChecks()
        {
            ulong blackChecks = ValidMoves.BlackPawnChecks(BlackPawns) | ValidMoves.KnightChecks(BlackKnights) | ValidMoves.KingChecks(BlackKing);
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((BlackRooks | 1UL << i) == BlackRooks) //found a rook
                {
                    blackChecks |= ValidMoves.RookChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackQueens | 1UL << i) == BlackQueens) //found a queen
                {
                    blackChecks |= ValidMoves.QueenChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackBishops | 1UL << i) == BlackBishops) //found a queen
                {
                    blackChecks |= ValidMoves.BishopChecks(1UL << i, AllPieces);
                }
            }
            return blackChecks;
        }

        public ulong GetBlackPins()
        {
            ulong blackChecks = 0;
            for (int i = 0; i < 64; i++) //iterating over rooks
            {
                if ((BlackRooks | 1UL << i) == BlackRooks) //found a rook
                {
                    blackChecks |= ValidMoves.RookChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackQueens | 1UL << i) == BlackQueens) //found a queen
                {
                    blackChecks |= ValidMoves.QueenChecks(1UL << i, AllPieces);
                }
            }
            for (int i = 0; i < 64; i++) //iterating over queens
            {
                if ((BlackBishops | 1UL << i) == BlackBishops) //found a queen
                {
                    blackChecks |= ValidMoves.BishopChecks(1UL << i, AllPieces);
                }
            }
            return blackChecks;
        }

        /// <summary>Get all valid moves for a black pawn.</summary>
        public ulong GetBlackPawnMoves()
        {
            return ((BlackPawns >> 8 & ~AllPieces) | ((((BlackPawns >> 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R6)) >> 8) & ~AllPieces)) | 
            ((((BlackPawns & Bitboard.ClearFile(File.A)) >> 9) | ((BlackPawns & Bitboard.ClearFile(File.H)) >> 7)) & AllWhitePieces);
        }

        /*public bool MoveWhiteKing(ulong king)
        {
            if ((ValidMoves.King(WhiteKing, AllWhitePieces) & king) == 0 || (AllWhitePieces & king) != 0 || (king & BlackAttacks) != 0) //check for valid moves, no check, and 
            {
                return false;
            }

            return true;
        }*/

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

        public void CastleWhite(bool longCastle)
        {
            if (!Turn)
            {
                Error?.Invoke("CastleWhite: Incorrect turn.");
                return;
            }
            if (Check)
            {
                Error?.Invoke("CastleWhite: Cannot castle while in check.");
            }
            //check if king moved
        }
    }
}