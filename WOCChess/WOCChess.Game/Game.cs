using System.Numerics;

namespace WOCChess.Game
{
    public class Game
    {
        public Action? WhiteToMove;
        public Action? BlackToMove;
        public Action<string>? Error;
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
        public ulong WhiteAttacks => ValidKnightMoves(WhiteKnights, AllWhitePieces) | WhitePawnAttacks() | ValidKingMoves(WhiteKing, AllWhitePieces) 
        | ValidQueenMoves(WhiteQueens, AllWhitePieces, AllBlackPieces) | ValidRookMoves(WhiteRooks, AllWhitePieces, AllBlackPieces) | ValidBishopMoves(WhiteBishops, AllWhitePieces, AllBlackPieces);

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

        public ulong ValidRookMoves(ulong rook, ulong friendly, ulong enemy)
        {
            ulong validMoves = 0;
            ulong temp = rook; //for down
            while (temp != 0)
            {
                temp &= Bitboard.ClearRank(Rank.R1);
                if ((temp >> 8 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp >>= 8;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = rook; //for up
            while (temp != 0)
            {
                temp &= Bitboard.ClearRank(Rank.R8);
                if ((temp << 8 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp <<= 8;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = rook; //for left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A);
                if ((temp >> 1 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp >>= 1;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = rook; //for right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.H);
                if ((temp << 1 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp <<= 1;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }

            //store ulong of current rook move test case
            //start from up-down
            //on down, clear rank 1
            //on up, clear rank 8
            //do same for sides
            //in all directions, make checks for friendly + enemy pieces
            //to end directional movement, check if all bits have been cleared (setting ulong equal to zero) from test ulong (meaning all possibilities have been tried)
            return validMoves;
        }

        public ulong ValidBishopMoves(ulong bishop, ulong friendly, ulong enemy)
        {
            ulong validMoves = 0;
            ulong temp = bishop; //for top left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R8);
                if ((temp << 7 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp <<= 7;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = bishop; //for bottom left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R1);
                if ((temp >> 9 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp >>= 9;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = bishop; //for top right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R1);
                if ((temp << 9 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp <<= 9;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            temp = bishop; //for bottom right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R1);
                if ((temp >> 7 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                temp >>= 7;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            return validMoves;
        }
        public ulong ValidQueenMoves(ulong queen, ulong friendly, ulong enemy) => ValidBishopMoves(queen, friendly, enemy) | ValidRookMoves(queen, friendly, enemy);

        /// <summary>Move the white king without any verification.</summary>
        /// <param name="pawn">The white king to move.</param>
        public void MoveWhiteKingUnsafe(ulong king)
        {
            WhiteKing = king;
            BlackToMove?.Invoke();
        }

        /// <summary>Move the black king without any verification.</summary>
        /// <param name="pawn">The black king to move.</param>
        public void MoveBlackKingUnsafe(ulong king)
        {
            BlackKing = king;
            WhiteToMove?.Invoke();
        }

        public ulong GetWhitePawnAttacks()
        {
            return ((WhitePawns & Bitboard.ClearFile(File.A)) << 7) | ((WhitePawns & Bitboard.ClearFile(File.H)) << 9);
        }

        /// <summary>Get all valid moves for a knight.</summary>
        /// <param name="knight">The knight to get all valid moves for.</param>
        /// <param name="friendly">All pieces friendly to the knight.</param>
        public ulong GetKnightMoves(ulong knight, ulong friendly)
        {
            return ((knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) << 6 | (knight & Bitboard.ClearFile(File.A)) << 15 | (knight & Bitboard.ClearFile(File.H)) << 17 
            | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) << 10 | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) >> 6 
            | (knight & Bitboard.ClearFile(File.H)) >> 15 | (knight & Bitboard.ClearFile(File.A)) >> 17 | (knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) >> 10) & ~friendly;
        }

        /// <summary>Get all valid moves for a white pawn.</summary>
        /// <param name="knight">The white pawn to get all valid moves for.</param>
        /// <param name="friendly">All pieces friendly to the knight.</param>
        public ulong GetWhitePawnMoves(ulong pawn)
        {
            return (((pawn << 8) & ~AllPieces) | ((((pawn << 8) & ~AllPieces) & Bitboard.MaskRank(Rank.R3)) << 8) & ~AllPieces) | ((((pawn & Bitboard.ClearFile(File.A)) << 7) | ((pawn & Bitboard.ClearFile(File.H)) << 9)) & AllBlackPieces);
        }

        /// <summary>Get all valid moves for a black pawn.</summary>
        /// <param name="pawn">The black pawn.</param>
        public ulong GetBlackPawnMoves(ulong pawn)
        {
            return (((pawn >> 8) & ~AllPieces) | (((((pawn >> 8) & ~AllPieces) & Bitboard.MaskRank(Rank.R6)) >> 8) & ~AllPieces)) | 
            ((((pawn & Bitboard.ClearFile(File.A)) >> 9) | ((pawn & Bitboard.ClearFile(File.H)) >> 7)) & AllWhitePieces);
        }

        /// <summary>Promote a white pawn without any verification.</summary>
        /// <param name="pawn">The white pawn to promote.</param>
        /// <param name="promotionPiece">The piece to get from promotion.</param>
        public void PromoteWhiteUnsafe(ulong pawn, PromotionPiece promotionPiece)
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

        /// <summary>Promote a black pawn without any verification.</summary>
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