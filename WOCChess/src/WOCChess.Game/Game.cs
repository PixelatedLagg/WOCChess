namespace WOCChess.Game
{
    public class Game
    {

        /// <summary>Triggered when it is whites turn to move. Passes a boolean which indicates whether or not white is in check.</summary>
        public Action<bool>? WhiteToMove;

        /// <summary>Triggered when it is blacks turn to move. Passes a boolean which indicates whether or not black is in check.</summary>
        public Action<bool>? BlackToMove;

        /// <summary>Triggered when the chess game ends. Passes an integer as an argument with: 0 meaning white wins by checkmate, 1 meaning black wins by checkmate, 2 meaning draw by half-move, and 3 meaning draw by stalemate.</summary>
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw by halfmove, 3 is stalemate

        /// <summary>Whose move it is. True means whites move, while false means blacks move.</summary>
        public bool Turn = true;

        /// <summary>All moves in between a pawn advance or a piece capture. Game will tie when count reaches 100.</summary>
        public int HalfMoves = 0;

        /// <summary>All full-moves. Increments after both white and black move.</summary>
        public int FullMoves = 0;

        /// <summary>All white pawns.</summary>
        public ulong WhitePawns = 0UL;

        /// <summary>All white rooks.</summary>
        public ulong WhiteRooks = 0UL;

        /// <summary>All white knights.</summary>
        public ulong WhiteKnights = 0UL;

        /// <summary>All white bishops.</summary>
        public ulong WhiteBishops = 0UL;

        /// <summary>All white queens.</summary>
        public ulong WhiteQueens = 0UL;

        /// <summary>The white king.</summary>
        public ulong WhiteKing = 0UL;

        /// <summary>Any white pawn that can be taken by en passant.</summary>
        public ulong WhiteEPPawn = 0UL;

        /// <summary>All black pawns.</summary>
        public ulong BlackPawns = 0UL;

        /// <summary>All black rooks.</summary>
        public ulong BlackRooks = 0UL;

        /// <summary>All black knights.</summary>
        public ulong BlackKnights = 0UL;

        /// <summary>All black bishops.</summary>
        public ulong BlackBishops = 0UL;

        /// <summary>All black queens.</summary>
        public ulong BlackQueens = 0UL;

        /// <summary>The black king.</summary>
        public ulong BlackKing = 0UL;

        /// <summary>Any black pawn that can be taken by en passant.</summary>
        public ulong BlackEPPawn = 0UL;

        /// <summary>Whether or not white can long-castle.</summary>
        public bool WhiteLongCastle = true;

        /// <summary>Whether or not white can short-castle.</summary>
        public bool WhiteShortCastle = true;

        /// <summary>Whether or not black can long-castle.</summary>
        public bool BlackLongCastle = true;

        /// <summary>Whether or not black can short-castle.</summary>
        public bool BlackShortCastle = true;

        /// <summary>All white pieces.</summary>
        public ulong AllWhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;

        /// <summary>All black pieces.</summary>
        public ulong AllBlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;

        /// <summary>All white and black pieces.</summary>
        public ulong AllPieces => AllWhitePieces | AllBlackPieces;
        
        /// <summary>Check if a white piece has taken a black piece.</summary>
        /// <param name="piece">The white piece to check.</param>
        private bool WhiteVerifyAttack(ulong piece)
        {
            if (piece.Contains(AllBlackPieces))
            {
                if (piece.Contains(BlackPawns))
                {
                    BlackPawns ^= piece;
                }
                if (piece.Contains(BlackKnights))
                {
                    BlackKnights ^= piece;
                }
                if (piece.Contains(BlackBishops))
                {
                    BlackBishops ^= piece;
                }
                if (piece.Contains(BlackRooks))
                {
                    BlackRooks ^= piece;
                }
                if (piece.Contains(BlackQueens))
                {
                    BlackQueens ^= piece;
                }
                return true;
            }
            return false;
        }

        /// <summary>Check if a black piece has taken a white piece.</summary>
        /// <param name="piece">The black piece to check.</param>
        private bool BlackVerifyAttack(ulong piece)
        {
            if (piece.Contains(AllWhitePieces))
            {
                if (piece.Contains(WhitePawns))
                {
                    WhitePawns ^= piece;
                }
                if (piece.Contains(WhiteKnights))
                {
                    WhiteKnights ^= piece;
                }
                if (piece.Contains(WhiteBishops))
                {
                    WhiteBishops ^= piece;
                }
                if (piece.Contains(WhiteRooks))
                {
                    WhiteRooks ^= piece;
                }
                if (piece.Contains(WhiteQueens))
                {
                    WhiteQueens ^= piece;
                }
                return true;
            }
            return false;
        }

        /// <summary>Set all pieces to their default positions.</summary>
        public Game Default()
        {
            WhitePawns = 0B_000000000000000000000000000000000000000000000000_11111111_00000000UL;
            WhiteRooks = 0B_000000000000000000000000000000000000000000000000_00000000_10000001UL;
            WhiteKnights = 0B_000000000000000000000000000000000000000000000000_00000000_01000010UL;
            WhiteBishops = 0B_000000000000000000000000000000000000000000000000_00000000_00100100UL;
            WhiteQueens = 0B_000000000000000000000000000000000000000000000000_00000000_00001000UL;
            WhiteKing = 0B_000000000000000000000000000000000000000000000000_00000000_00010000UL;
            WhiteEPPawn = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackPawns = 0b_00000000_11111111_000000000000000000000000000000000000000000000000UL;
            BlackRooks = 0b_10000001_00000000_000000000000000000000000000000000000000000000000UL;
            BlackKnights = 0b_01000010_00000000_000000000000000000000000000000000000000000000000UL;
            BlackBishops = 0b_00100100_00000000_000000000000000000000000000000000000000000000000UL;
            BlackQueens = 0b_00001000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackKing = 0b_00010000_00000000_000000000000000000000000000000000000000000000000UL;
            BlackEPPawn = 0b_00000000_00000000_000000000000000000000000000000000000000000000000UL;
            return this;
        }

        /// <summary>Start the chess game.</summary>
        public void Start()
        {
            WhiteToMove?.Invoke(false);
        }

        /// <summary>Check all events directly following black's move. Do not call this method if you are using the move methods provided in the Game class.</summary>
        public void WhiteEvents()
        {
            FullMoves++;
            WhiteEPPawn = 0;
            if (HalfMoves == 100)
            {
                GameEnd?.Invoke(2);
            }
            if (GetBlackChecks().Contains(WhiteKing))
            {
                if (GetWhiteMoves() == 0)
                {
                    GameEnd?.Invoke(1);
                }
                else
                {
                    WhiteToMove?.Invoke(true);
                }
            }
            else
            {
                if (GetWhiteMoves() == 0)
                {
                    GameEnd?.Invoke(3);
                }
                else
                {
                    WhiteToMove?.Invoke(false);
                }
            }
        }

        /// <summary>Check all events directly following white's move. Do not call this method if you are using the move methods provided in the Game class.</summary>
        public void BlackEvents()
        {
            BlackEPPawn = 0;
            if (HalfMoves == 100)
            {
                GameEnd?.Invoke(2);
            }
            if (GetWhiteChecks().Contains(WhiteKing))
            {
                if (GetBlackMoves() == 0)
                {
                    GameEnd?.Invoke(0);
                }
                else
                {
                    BlackToMove?.Invoke(true);
                }
            }
            else
            {
                if (GetBlackMoves() == 0)
                {
                    GameEnd?.Invoke(3);
                }
                else
                {
                    BlackToMove?.Invoke(false);
                }
            }
        }

        /// <summary>Get all legal white moves, with or without the white king.</summary>
        /// <param name="king">Whether to include the white king. True means include, false means exclude.</param>
        public ulong GetWhiteMoves(bool king = true)
        {
            ulong whiteMoves = GetWhitePawnMoves(WhitePawns) | ValidMoves.KnightChecks(WhiteKnights);
            if (king)
            {
                whiteMoves |= ValidMoves.KingMoves(WhiteKing, GetBlackChecks());
            }
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

        /// <summary>Get all white piece threats.</summary>
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

        /// <summary>Get all sliding white piece threats.</summary>
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

        /// <summary>Get all legal black moves, with or without the black king.</summary>
        /// <param name="king">Whether to include the black king. True means include, false means exclude.</param>
        public ulong GetBlackMoves(bool king = true)
        {
            ulong blackMoves = GetBlackPawnMoves(BlackPawns) | ValidMoves.KnightChecks(BlackKnights);
            if (king)
            {
                blackMoves |= ValidMoves.KingMoves(BlackKing, GetWhiteChecks());
            }
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

        /// <summary>Get all black piece threats.</summary>
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

        /// <summary>Get all sliding black piece threats.</summary>
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

        /// <summary>Get all legal white pawn moves.</summary>
        /// <param name="pawn">The position of the white pawn to check.</param>
        public ulong GetWhitePawnMoves(ulong pawn)
        {
            ulong moves = ((pawn << 8 & ~AllPieces) | (((pawn << 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R3)) << 8) & ~AllPieces) | 
            ((((pawn & Bitboard.ClearFile(File.A)) << 7) | ((pawn & Bitboard.ClearFile(File.H)) << 9)) & AllBlackPieces) | ((pawn >> 1 & Bitboard.ClearFile(File.A)) & BlackEPPawn) << 8 | ((pawn << 1 & Bitboard.ClearFile(File.H)) & BlackEPPawn) << 8;
            ulong actualMoves = 0;
            foreach (ulong board in Bitboard.DivideBoard(moves))
            {
                WhitePawns ^= pawn;
                WhitePawns |= board;
                if ((WhiteKing & GetBlackPins()) != 0)
                {
                    actualMoves |= board;
                }
                WhitePawns ^= board;
                WhitePawns |= pawn;
            }
            return actualMoves;
        }

        /// <summary>Get all legal black pawn moves.</summary>
        /// <param name="pawn">The position of the black pawn to check.</param>
        public ulong GetBlackPawnMoves(ulong pawn)
        {
            ulong moves = ((pawn >> 8 & ~AllPieces) | (((pawn >> 8 & ~AllPieces) & Bitboard.MaskRank(Rank.R6)) >> 8) & ~AllPieces) | 
            ((((pawn & Bitboard.ClearFile(File.A)) >> 9) | ((pawn & Bitboard.ClearFile(File.H)) >> 7)) & AllWhitePieces) | ((pawn >> 1 & Bitboard.ClearFile(File.A)) & WhiteEPPawn) >> 8 | ((pawn << 1 & Bitboard.ClearFile(File.H)) & WhiteEPPawn) >> 8;
            ulong actualMoves = 0;
            foreach (ulong board in Bitboard.DivideBoard(moves))
            {
                BlackPawns ^= pawn;
                BlackPawns |= board;
                if ((BlackKing & GetWhitePins()) != 0)
                {
                    actualMoves |= board;
                }
                BlackPawns ^= board;
                BlackPawns |= pawn;
            }
            return actualMoves;

        }

        /// <summary>Promote a white pawn.</summary>
        /// <param name="pawn">The position of the white pawn to promote.</param>
        /// <param name="promotionPiece">The piece to promote to.</param>
        public void PromoteWhite(ulong previous, ulong current, PromotionPiece promotionPiece)
        {
            #if verification
                if (!Turn || !WhitePawns.Contains(previous) || !Bitboard.MaskRank(Rank.R7).Contains(previous) || !GetWhitePawnMoves(previous).Contains(current))
                {
                    return;
                }
            #endif
            WhitePawns ^= previous;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    WhiteKnights |= current;
                    break;
                case PromotionPiece.Bishop:
                    WhiteBishops |= current;
                    break;
                case PromotionPiece.Rook:
                    WhiteRooks |= current;
                    break;
                case PromotionPiece.Queen:
                    WhiteQueens |= current;
                    break;
            }
            HalfMoves = 0;
            BlackEvents();
        }

        /// <summary>Promote a black pawn.</summary>
        /// <param name="pawn">The position of the black pawn to promote.</param>
        /// <param name="promotionPiece">The piece to promote to.</param>
        public void PromoteBlack(ulong previous, ulong current, PromotionPiece promotionPiece)
        {
            #if verification
                if (turn || !BlackPawns.Contains(previous) || !Bitboard.MaskRank(Rank.R2).Contains(previous) || !GetBlackPawnMoves(previous).Contains(current))
                {
                    return;
                }
            #endif
            BlackPawns ^= previous;
            switch (promotionPiece)
            {
                case PromotionPiece.Knight:
                    BlackKnights |= current;
                    break;
                case PromotionPiece.Bishop:
                    BlackBishops |= current;
                    break;
                case PromotionPiece.Rook:
                    BlackRooks |= current;
                    break;
                case PromotionPiece.Queen:
                    BlackQueens |= current;
                    break;
            }
            HalfMoves = 0;
            WhiteEvents();
        }

        /// <summary>Castle the white king.</summary>
        /// <param name="longCastle">Whether or not you are long castling. True means long castling, false means short castling.</param>
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
            HalfMoves++;
            WhiteEPPawn = 0;
            BlackEvents();
        }

        /// <summary>Castle the black king.</summary>
        /// <param name="longCastle">Whether or not you are long castling. True means long castling, false means short castling.</param>
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
            HalfMoves++;
            BlackEPPawn = 0;
            WhiteEvents();
        }

        /// <summary>Move the white king.</summary>
        /// <param name="king">The position you are moving the white king to.</param>
        public void MoveWhiteKing(ulong king)
        {
            #if verification
                if ((ValidMoves.KingMoves(WhiteKing, AllWhitePieces) & king) == 0)
                {
                    return;
                }
            #endif
            WhiteKing = king;
            if (!WhiteVerifyAttack(king))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move the black king.</summary>
        /// <param name="king">The position you are moving the black king to.</param>
        public void MoveBlackKing(ulong king)
        {
            #if verification
                if ((ValidMoves.KingMoves(BlackKing, AllBlackPieces) & king) == 0)
                {
                    return;
                }
            #endif
            BlackKing = king;
            if (!BlackVerifyAttack(king))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Move a white pawn.</summary>
        /// <param name="previous">The position of the white pawn you are moving.</param>
        /// <param name="current">The position you are moving the white pawn to.</param>
        public void MoveWhitePawn(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !WhitePawns.Contains(previous) || !GetWhitePawnMoves(previous, AllWhitePieces, AllBlackPieces, this).Contains(current) || Bitboard.MaskRank(Rank.R7).Contains(current))
                {
                    return;
                }
            #endif
            WhitePawns ^= previous;
            WhitePawns |= current;
            if (Bitboard.MaskRank(Rank.R2).Contains(previous) && Bitboard.MaskRank(Rank.R4).Contains(current))
            {
                WhiteEPPawn = current;
            }
            if (!WhiteVerifyAttack(current))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move a black pawn.</summary>
        /// <param name="previous">The position of the black pawn you are moving.</param>
        /// <param name="current">The position you are moving the black pawn to.</param>
        public void MoveBlackPawn(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackPawns.Contains(previous) || !GetBlackPawnMoves(previous, AllBlackPieces, AllWhitePieces, this).Contains(current) || Bitboard.MaskRank(Rank.R2).Contains(current))
                {
                    return;
                }
            #endif
            BlackPawns ^= previous;
            BlackPawns |= current;
            if (Bitboard.MaskRank(Rank.R7).Contains(previous) && Bitboard.MaskRank(Rank.R5).Contains(current))
            {
                BlackEPPawn = current;
            }
            if (!BlackVerifyAttack(current))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Move a white knight.</summary>
        /// <param name="previous">The position of the white knight you are moving.</param>
        /// <param name="current">The position you are moving the white knight to.</param>
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
            BlackEPPawn = 0;
            if (!WhiteVerifyAttack(current))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move a black knight.</summary>
        /// <param name="previous">The position of the black knight you are moving.</param>
        /// <param name="current">The position you are moving the black knight to.</param>
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
            WhiteEPPawn = 0;
            if (!BlackVerifyAttack(current))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Move a white bishop.</summary>
        /// <param name="previous">The position of the white bishop you are moving.</param>
        /// <param name="current">The position you are moving the white bishop to.</param>
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
            BlackEPPawn = 0;
            if (!WhiteVerifyAttack(current))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move a black bishop.</summary>
        /// <param name="previous">The position of the black bishop you are moving.</param>
        /// <param name="current">The position you are moving the black bishop to.</param>
        public void MoveBlackBishop(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackBishops.Contains(previous) || !ValidMoves.BishopMovesBlack(previous, AllBlackPieces, AllWhitePieces, this).Contains(current))
                {
                    return;
                }
            #endif
            BlackBishops ^= previous;
            BlackBishops |= current;
            WhiteEPPawn = 0;
            if (!BlackVerifyAttack(current))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Move a white rook.</summary>
        /// <param name="previous">The position of the white rook you are moving.</param>
        /// <param name="current">The position you are moving the white rook to.</param>
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
            BlackEPPawn = 0;
            if (!WhiteVerifyAttack(current))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move a black rook.</summary>
        /// <param name="previous">The position of the black rook you are moving.</param>
        /// <param name="current">The position you are moving the black rook to.</param>
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
            WhiteEPPawn = 0;
            if (!BlackVerifyAttack(current))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Move a white queen.</summary>
        /// <param name="previous">The position of the white queen you are moving.</param>
        /// <param name="current">The position you are moving the white queen to.</param>
        public void MoveWhiteQueen(ulong previous, ulong current)
        {
            #if verification
                if (!Turn || !WhiteQueens.Contains(previous) || !ValidMoves.QueenMovesWhite(previous, AllWhitePieces, AllBlackPieces, this).Contains(current))
                {
                    return;
                }
            #endif
            WhiteQueens ^= previous;
            WhiteQueens |= current;
            BlackEPPawn = 0;
            if (!WhiteVerifyAttack(current))
            {
                HalfMoves++;
            }
            BlackEvents();
        }

        /// <summary>Move a black queen.</summary>
        /// <param name="previous">The position of the black queen you are moving.</param>
        /// <param name="current">The position you are moving the black queen to.</param>
        public void MoveBlackQueen(ulong previous, ulong current)
        {
            #if verification
                if (Turn || !BlackQueens.Contains(previous) || !ValidMoves.QueenMovesBlack(previous, AllBlackPieces, AllWhitePieces, this).Contains(current))
                {
                    return;
                }
            #endif
            BlackQueens ^= previous;
            BlackQueens |= current;
            WhiteEPPawn = 0;
            if (!BlackVerifyAttack(current))
            {
                HalfMoves++;
            }
            WhiteEvents();
        }

        /// <summary>Print out the board from a certain side's perspective. White pieces are uppercase, while black pieces are lowercase.</summary>
        /// <param name="perspective">The perspective to flip the board to. True means white, while false means black.</param>
        public void Print(bool perspective = true)
        {
            char[] board = "................................................................".ToCharArray();
            int i = 0;
            for (; i < 64; i++)
            {
                if ((WhitePawns | 1UL << i) == WhitePawns)
                {
                    board[63 - i] = 'P';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((WhiteKnights | 1UL << i) == WhiteKnights)
                {
                    board[63 - i] = 'N';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((WhiteBishops | 1UL << i) == WhiteBishops)
                {
                    board[63 - i] = 'B';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((WhiteRooks | 1UL << i) == WhiteRooks)
                {
                    board[63 - i] = 'R';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((WhiteQueens | 1UL << i) == WhiteQueens)
                {
                    board[63 - i] = 'Q';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((WhiteKing | 1UL << i) == WhiteKing)
                {
                    board[63 - i] = 'K';
                    break;
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackPawns | 1UL << i) == BlackPawns)
                {
                    board[63 - i] = 'p';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackKnights | 1UL << i) == BlackKnights)
                {
                    board[63 - i] = 'n';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackBishops | 1UL << i) == BlackBishops)
                {
                    board[63 - i] = 'b';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackRooks | 1UL << i) == BlackRooks)
                {
                    board[63 - i] = 'r';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackQueens | 1UL << i) == BlackQueens)
                {
                    board[63 - i] = 'q';
                }
            }
            for (i = 0; i < 64; i++)
            {
                if ((BlackKing | 1UL << i) == BlackKing)
                {
                    board[63 - i] = 'k';
                    break;
                }
            }
            IEnumerable<string> ranks = Enumerable.Range(0, 8).Select(i => (new string(board) ?? "").Substring(i * 8, 8));
            if (perspective)
            {
                for (i = 0; i < 8; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(8 - i);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"    {ranks.ElementAt(i)[7]}    {ranks.ElementAt(i)[6]}    {ranks.ElementAt(i)[5]}    {ranks.ElementAt(i)[4]}    {ranks.ElementAt(i)[3]}    {ranks.ElementAt(i)[2]}    {ranks.ElementAt(i)[1]}    {ranks.ElementAt(i)[0]}\n");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("     a    b    c    d    e    f    g    h");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                for (i = 7; i > -1; i--)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(8 - i);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"    {ranks.ElementAt(i)[7]}    {ranks.ElementAt(i)[6]}    {ranks.ElementAt(i)[5]}    {ranks.ElementAt(i)[4]}    {ranks.ElementAt(i)[3]}    {ranks.ElementAt(i)[2]}    {ranks.ElementAt(i)[1]}    {ranks.ElementAt(i)[0]}\n");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("     a    b    c    d    e    f    g    h");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}