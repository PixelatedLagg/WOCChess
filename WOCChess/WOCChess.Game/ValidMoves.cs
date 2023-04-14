namespace WOCChess.Game
{
    public static class ValidMoves
    {
        public static ulong Queen(ulong queen, ulong friendly, ulong enemy) => Bishop(queen, friendly, enemy) | Rook(queen, friendly, enemy);

        public static ulong Rook(ulong rook, ulong friendly, ulong enemy)
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
            return validMoves;
        }

        public static ulong Bishop(ulong bishop, ulong friendly, ulong enemy)
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

        public static ulong Knight(ulong knight, ulong friendly)
        {
            return ((knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) << 6 | (knight & Bitboard.ClearFile(File.A)) << 15 | (knight & Bitboard.ClearFile(File.H)) << 17 
            | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) << 10 | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) >> 6 
            | (knight & Bitboard.ClearFile(File.H)) >> 15 | (knight & Bitboard.ClearFile(File.A)) >> 17 | (knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) >> 10) & ~friendly;
        }

        public static ulong King(ulong king, ulong side)
        {
            ulong kingClipFileH = king & Bitboard.ClearFile(File.H);
            ulong kingClipFileA = king & Bitboard.ClearFile(File.A);
            return (kingClipFileH << 7 | king << 8 | kingClipFileH << 9 | kingClipFileH << 1 | kingClipFileA >> 7 | king >> 8 | kingClipFileA >> 9 | kingClipFileA >> 1) & ~side;
        }
    }
}