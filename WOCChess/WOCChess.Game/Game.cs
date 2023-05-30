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
        public int FullMoves = 0;

        public ulong WhitePawns = 0UL;
        public ulong WhiteRooks = 0UL;
        public ulong WhiteKnights = 0UL;
        public ulong WhiteBishops = 0UL;
        public ulong WhiteQueens = 0UL;
        public ulong WhiteKing = 0UL;
        public ulong WhiteEPPawns = 0UL;
        public ulong BlackPawns = 0UL;
        public ulong BlackRooks = 0UL;
        public ulong BlackKnights = 0UL;
        public ulong BlackBishops = 0UL;
        public ulong BlackQueens = 0UL;
        public ulong BlackKing = 0UL;
        public ulong BlackEPPawns = 0UL;

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
        
        public void Default()
        {
            WhitePawns = 0B_000000000000000000000000000000000000000000000000_11111111_00000000UL;
            WhiteRooks = 0B_000000000000000000000000000000000010000000001000_00000000_00000000UL;
            WhiteKnights = 0B_000000000000000000000000000000000000000000000000_00000000_01000010UL;
            WhiteBishops = 0B_000000000000000000000000000000000000000000000000_00000000_00100100UL;
            WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
            WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
            WhiteEPPawns = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
            BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
            BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
            BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
            BlackQueens = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackKing = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackEPPawns = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;
        }

        public void Start()
        {
            WhiteToMove?.Invoke();
        }

        public ulong GetWhitePawnMoves(ulong pawn)
        {
            return ((pawn << 8 & ~AllPieces) | (((pawn << 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R3)) << 8) & ~AllPieces) | 
            ((((pawn & Bitboard.ClearFile(File.A)) << 7) | ((pawn & Bitboard.ClearFile(File.H)) << 9)) & AllBlackPieces);
        }

        public ulong GetWhiteMoves()
        {
            ulong whiteMoves = GetWhitePawnMoves(WhitePawns) | ValidMoves.KnightChecks(WhiteKnights) | ValidMoves.KingMoves(WhiteKing, GetBlackChecks());
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
            ulong blackMoves = GetBlackPawnMoves(BlackPawns) | ValidMoves.KnightChecks(BlackKnights) | ValidMoves.KingMoves(BlackKing, GetWhiteChecks());
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
        public ulong GetBlackPawnMoves(ulong pawn)
        {
            return ((pawn >> 8 & ~AllPieces) | ((((pawn >> 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R6)) >> 8) & ~AllPieces)) | 
            ((((pawn & Bitboard.ClearFile(File.A)) >> 9) | ((pawn & Bitboard.ClearFile(File.H)) >> 7)) & AllWhitePieces);
        }

        public bool MoveWhiteKing(ulong king)
        {
            #if verification
                if ((ValidMoves.KingMoves(WhiteKing, AllWhitePieces) & king) == 0) //check if move is one of the valid moves
                {
                    return false;
                }
            #endif
            HalfMoves++;
            WhiteKing = king;
            return true;
        }

        /// <summary>Promote a white pawn.</summary>
        /// <param name="pawn">The white pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void PromoteWhite(ulong pawn, PromotionPiece promotionPiece)
        {
            #if verification
                if (!Turn || !WhitePawns.Contains(pawn) || !Bitboard.MaskRank(Rank.R7).Contains(pawn))
                {
                    return;
                }
            #endif
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
            #if verification
                if (turn || !BlackPawns.Contains(pawn) || !Bitboard.MaskRank(Rank.R2).Contains(pawn))
                {
                    return;
                }
            #endif
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
            FullMoves++;
        }

        public void CastleWhite(bool longCastle)
        {
            if (longCastle)
            {
                #if verification
                    if (!Turn || !WhiteLongCastle || GetBlackChecks().Contains(0B_000000000000000000000000000000000000000000000000_00000000_00011110UL))
                    {
                        return;
                    }
                #endif
                WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_01000000UL;
                WhiteRooks |= 0B_000000000000000000000000000000000000000000000000_00000000_00100000UL;
            }
            else
            {
                #if verification
                    if (!Turn || !WhiteShortCastle || GetBlackChecks().Contains(0B_000000000000000000000000000000000000000000000000_00000000_01110000UL))
                    {
                        return;
                    }
                #endif
                WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00000100UL;
                WhiteRooks |= 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
            }
            WhiteEPPawns = 0;
        }

        public void CastleBlack(bool longCastle)
        {
            if (longCastle)
            {
                #if verification
                    if (Turn || !BlackLongCastle || GetWhiteChecks().Contains(0B_00011110_0000000000000000000000000000000000000000_00000000_00000000UL))
                    {
                        return;
                    }
                #endif
                BlackKing = 0B_00000100_0000000000000000000000000000000000000000_00000000_00000000UL;
                BlackRooks |= 0B_00001000_0000000000000000000000000000000000000000_00000000_00000000UL;
            }
            else
            {
                #if verification
                    if (Turn || !BlackShortCastle || GetWhiteChecks().Contains(0B_01110000_0000000000000000000000000000000000000000_00000000_00000000UL))
                    {
                        return;
                    }
                #endif
                BlackKing = 0B_01000000_0000000000000000000000000000000000000000_00000000_00000000UL;
                BlackRooks |= 0B_00100000_0000000000000000000000000000000000000000_00000000_00000000UL;
            }
            FullMoves++;
            BlackEPPawns = 0;
        }

        public void MoveWhitePawn(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !WhitePawns.Contains(previous) || !GetWhitePawnMoves(previous, AllWhitePieces, AllBlackPieces, this).Contains(current))
                {
                    return;
                }
            #endif
            WhitePawns ^= previous;
            WhitePawns |= current;
            if (Bitboard.MaskRank(Rank.R2).Contains(previous) && Bitboard.MaskRank(Rank.R4).Contains(current))
            {
                WhiteEPPawns = current;
            }
            BlackToMove?.Invoke();
        }

        public void MoveBlackPawn(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackPawns.Contains(previous) || !GetBlackPawnMoves(previous, AllBlackPieces, AllWhitePieces, this).Contains(current))
                {
                    return;
                }
            #endif
            BlackPawns ^= previous;
            BlackPawns |= current;
            if (Bitboard.MaskRank(Rank.R7).Contains(previous) && Bitboard.MaskRank(Rank.R5).Contains(current))
            {
                BlackEPPawns = current;
            }
            FullMoves++;
            WhiteToMove?.Invoke();
        }

        public void MoveWhiteKnight(ulong previous, ulong current)
        {
            #if verification
                if (!Turn || !WhiteKnights.Contains(previous) || !GetKnightMoves(previous, AllWhitePieces, AllBlackPieces, this).Contains(current))
                {
                    return;
                }
            #endif
            WhiteKnights ^= previous;
            WhiteKnights |= current;
            BlackEPPawns = 0;
            BlackToMove?.Invoke();
        }

        public void MoveBlackKnight(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackKnights.Contains(previous) || !GetKnightMoves(previous, AllBlackPieces, AllWhitePieces, this).Contains(current))
                {
                    return;
                }
            #endif
            BlackKnights ^= previous;
            BlackKnights |= current;
            WhiteEPPawns = 0;
            WhiteToMove?.Invoke();
        }

        public void MoveWhiteRook(ulong previous, ulong current)
        {
            #if verification
                if (!Turn || !WhiteRooks.Contains(previous) || !ValidMoves.RookMovesWhite(previous, AllWhitePieces, AllBlackPieces, this).Contains(current))
                {
                    return;
                }
            #endif
            WhiteRooks ^= previous;
            WhiteRooks |= current;
            BlackEPPawns = 0;
            BlackToMove?.Invoke();
        }

        public void MoveBlackRook(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackRooks.Contains(previous) || !ValidMoves.RookMovesBlack(previous, AllBlackPieces, AllWhitePieces, this).Contains(current))
                {
                    return;
                }
            #endif
            BlackRooks ^= previous;
            BlackRooks |= current;
            WhiteEPPawns = 0;
            WhiteToMove?.Invoke();
        }

        public void MoveWhiteBishop(ulong previous, ulong current)
        {
            #if verification
                if (!Turn || !WhiteBishops.Contains(previous) || !ValidMoves.BishopMovesWhite(previous, AllWhitePieces, AllBlackPieces, this).Contains(current))
                {
                    return;
                }
            #endif
            WhiteBishops ^= previous;
            WhiteBishops |= current;
            BlackEPPawns = 0;
            BlackToMove?.Invoke();
        }

        public void Debug()
        {
            char[] board = "................................................................".ToCharArray();
            int i = 0;
            for (; i < 64; i++) //iterating over positions
            {
                if ((WhitePawns | 1UL << i) == WhitePawns) //found a position
                {
                    board[63 - i] = 'P';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((WhiteKnights | 1UL << i) == WhiteKnights) //found a position
                {
                    board[63 - i] = 'N';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((WhiteBishops | 1UL << i) == WhiteBishops) //found a position
                {
                    board[63 - i] = 'B';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((WhiteQueens | 1UL << i) == WhiteQueens) //found a position
                {
                    board[63 - i] = 'Q';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((WhiteKing | 1UL << i) == WhiteKing) //found a position
                {
                    board[63 - i] = 'K';
                    break;
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((BlackPawns | 1UL << i) == BlackPawns) //found a position
                {
                    board[63 - i] = 'p';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((BlackKnights | 1UL << i) == BlackKnights) //found a position
                {
                    board[63 - i] = 'n';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((BlackBishops | 1UL << i) == BlackBishops) //found a position
                {
                    board[63 - i] = 'b';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((BlackQueens | 1UL << i) == BlackQueens) //found a position
                {
                    board[63 - i] = 'q';
                }
            }
            for (i = 0; i < 64; i++) //iterating over positions
            {
                if ((BlackKing | 1UL << i) == BlackKing) //found a position
                {
                    board[63 - i] = 'k';
                    break;
                }
            }
            IEnumerable<string> ranks = Enumerable.Range(0, 8).Select(i => (new string(board) ?? "").Substring(i * 8, 8));
            foreach (string rank in ranks)
            {
                Console.WriteLine($"{rank[7]}  {rank[6]}  {rank[5]}  {rank[4]}  {rank[3]}  {rank[2]}  {rank[1]}  {rank[0]}");
            }
        }
    }
}