namespace WOCChess.Game
{
    public static class ValidMoves
    {
        public static ulong QueenChecks(ulong queen, ulong allPieces) => BishopChecks(queen, allPieces) | RookChecks(queen, allPieces);

        public static ulong RookChecks(ulong rook, ulong allPieces)
        {
            ulong validMoves = 0;
            ulong temp = rook; //down
            while (temp != 0)
            {
                temp &= Bitboard.ClearRank(Rank.R1);
                if ((temp >> 8 & allPieces) != 0)
                {
                    break;
                }
                temp >>= 8;
                validMoves |= temp;
            }
            temp = rook; //up
            while (temp != 0)
            {
                temp &= Bitboard.ClearRank(Rank.R8);
                if ((temp << 8 & allPieces) != 0)
                {
                    break;
                }
                temp <<= 8;
                validMoves |= temp;
            }
            temp = rook; //left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A);
                if ((temp >> 1 & allPieces) != 0)
                {
                    break;
                }
                temp >>= 1;
                validMoves |= temp;
            }
            temp = rook; //right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.H);
                if ((temp << 1 & allPieces) != 0)
                {
                    break;
                }
                temp <<= 1;
                validMoves |= temp;
            }
            return validMoves;
        }

        public static ulong BishopChecks(ulong bishop, ulong allPieces)
        {
            ulong validMoves = 0;
            ulong temp = bishop; //for top left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R8);
                if ((temp << 7 & allPieces) != 0)
                {
                    break;
                }
                temp <<= 7;
                validMoves |= temp;
            }
            temp = bishop; //for bottom left
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.A) & Bitboard.ClearRank(Rank.R1);
                if ((temp >> 9 & allPieces) != 0)
                {
                    break;
                }
                temp >>= 9;
                validMoves |= temp;
            }
            temp = bishop; //for top right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R1);
                if ((temp << 9 & allPieces) != 0)
                {
                    break;
                }
                temp <<= 9;
                validMoves |= temp;
            }
            temp = bishop; //for bottom right
            while (temp != 0)
            {
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R8);
                if ((temp >> 7 & allPieces) != 0)
                {
                    break;
                }
                temp >>= 7;
                validMoves |= temp;
            }
            return validMoves;
        }

        public static ulong KnightChecks(ulong knight)
        {
            return ((knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) << 6 | (knight & Bitboard.ClearFile(File.A)) << 15 | (knight & Bitboard.ClearFile(File.H)) << 17 
            | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) << 10 | (knight & Bitboard.ClearFile(File.H) & Bitboard.ClearFile(File.G)) >> 6 
            | (knight & Bitboard.ClearFile(File.H)) >> 15 | (knight & Bitboard.ClearFile(File.A)) >> 17 | (knight & Bitboard.ClearFile(File.A) & Bitboard.ClearFile(File.B)) >> 10);
        }

        public static ulong KingChecks(ulong king)
        {
            ulong kingClipFileH = Bitboard.ClearFile(File.H) & king;
            ulong kingClipFileA = Bitboard.ClearFile(File.A) & king;
            return (kingClipFileA << 7 | king << 8 | kingClipFileH << 9 | kingClipFileH << 1 | kingClipFileH >> 7 | king >> 8 | kingClipFileA >> 9 | kingClipFileA >> 1);
        }

        public static ulong KingMoves(ulong king, ulong otherChecks)
        {
            ulong kingClipFileH = Bitboard.ClearFile(File.H) & king;
            ulong kingClipFileA = Bitboard.ClearFile(File.A) & king;
            return (kingClipFileA << 7 | king << 8 | kingClipFileH << 9 | kingClipFileH << 1 | kingClipFileH >> 7 | king >> 8 | kingClipFileA >> 9 | kingClipFileA >> 1) & ~otherChecks;
        }

        public static ulong WhitePawnChecks(ulong pawn)
        {
            return ((Bitboard.ClearFile(File.A) & pawn) << 7) | ((Bitboard.ClearFile(File.H) & pawn) << 9);
        }

        public static ulong BlackPawnChecks(ulong pawn)
        {
            return ((Bitboard.ClearFile(File.A) & pawn) >> 7) | ((Bitboard.ClearFile(File.H) & pawn) >> 9);
        }

        public static ulong RookMovesWhite(ulong rook, ulong friendly, ulong enemy, Game game) //need to add discovered check verification
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
                game.WhiteRooks |= temp >> 8; //add temp rook
                game.WhiteRooks ^= rook; //remove previous rook
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteRooks ^= temp >> 8;
                    game.WhiteRooks |= rook;
                    break;
                }
                game.WhiteRooks ^= temp >> 8;
                game.WhiteRooks |= rook;
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
                game.WhiteRooks |= temp << 8; //add temp rook
                game.WhiteRooks ^= rook; //remove previous rook
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteRooks ^= temp << 8;
                    game.WhiteRooks |= rook;
                    break;
                }
                game.WhiteRooks ^= temp << 8;
                game.WhiteRooks |= rook;
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
                game.WhiteRooks |= temp >> 1; //add temp rook
                game.WhiteRooks ^= rook; //remove previous rook
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteRooks ^= temp >> 1;
                    game.WhiteRooks |= rook;
                    break;
                }
                game.WhiteRooks ^= temp >> 1;
                game.WhiteRooks |= rook;
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
                game.WhiteRooks |= temp << 1; //add temp rook
                game.WhiteRooks ^= rook; //remove previous rook
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteRooks ^= temp << 1;
                    game.WhiteRooks |= rook;
                    break;
                }
                game.WhiteRooks ^= temp << 1;
                game.WhiteRooks |= rook;
                temp <<= 1;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            return validMoves;
        }
        public static ulong RookMovesBlack(ulong rook, ulong friendly, ulong enemy, Game game) //need to add discovered check verification
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
                game.BlackRooks |= temp >> 8; //add temp rook
                game.BlackRooks ^= rook; //remove previous rook
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackRooks ^= temp >> 8;
                    game.BlackRooks |= rook;
                    break;
                }
                game.BlackRooks ^= temp >> 8;
                game.BlackRooks |= rook;
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
                game.BlackRooks |= temp << 8; //add temp rook
                game.BlackRooks ^= rook; //remove previous rook
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackRooks ^= temp << 8;
                    game.BlackRooks |= rook;
                    break;
                }
                game.BlackRooks ^= temp << 8;
                game.BlackRooks |= rook;
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
                game.BlackRooks |= temp >> 1; //add temp rook
                game.BlackRooks ^= rook; //remove previous rook
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackRooks ^= temp >> 1;
                    game.BlackRooks |= rook;
                    break;
                }
                game.BlackRooks ^= temp >> 1;
                game.BlackRooks |= rook;
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
                game.BlackRooks |= temp << 1; //add temp rook
                game.BlackRooks ^= rook; //remove previous rook
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackRooks ^= temp << 1;
                    game.BlackRooks |= rook;
                    break;
                }
                game.BlackRooks ^= temp << 1;
                game.BlackRooks |= rook;
                temp <<= 1;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            return validMoves;
        }

        public static ulong BishopMovesWhite(ulong bishop, ulong friendly, ulong enemy, Game game) //need to add discovered check verification
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
                game.WhiteBishops |= temp << 7; //add temp bishop
                game.WhiteBishops ^= bishop; //remove previous bishop
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteBishops ^= temp << 7;
                    game.WhiteBishops |= bishop;
                    break;
                }
                game.WhiteBishops ^= temp << 7;
                game.WhiteBishops |= bishop;
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
                game.WhiteBishops |= temp >> 9; //add temp bishop
                game.WhiteBishops ^= bishop; //remove previous bishop
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteBishops ^= temp >> 9;
                    game.WhiteBishops |= bishop;
                    break;
                }
                game.WhiteBishops ^= temp >> 9;
                game.WhiteBishops |= bishop;
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
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R1);
                if ((temp << 9 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                game.WhiteBishops |= temp << 9; //add temp bishop
                game.WhiteBishops ^= bishop; //remove previous bishop
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteBishops ^= temp << 9;
                    game.WhiteBishops |= bishop;
                    break;
                }
                game.WhiteBishops ^= temp << 9;
                game.WhiteBishops |= bishop;
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
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R8);
                if ((temp >> 7 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                game.WhiteBishops |= temp >> 7; //add temp bishop
                game.WhiteBishops ^= bishop; //remove previous bishop
                if ((game.WhiteKing & game.GetBlackPins()) != 0) //piece is pinned
                {
                    game.WhiteBishops ^= temp >> 7;
                    game.WhiteBishops |= bishop;
                    break;
                }
                game.WhiteBishops ^= temp >> 7;
                game.WhiteBishops |= bishop;
                temp >>= 7;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            return validMoves;
        }

        public static ulong BishopMovesBlack(ulong bishop, ulong friendly, ulong enemy, Game game) //need to add discovered check verification
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
                game.BlackBishops |= temp << 7; //add temp bishop
                game.BlackBishops ^= bishop; //remove previous bishop
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackBishops ^= temp << 7;
                    game.BlackBishops |= bishop;
                    break;
                }
                game.BlackBishops ^= temp << 7;
                game.BlackBishops |= bishop;
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
                game.BlackBishops |= temp >> 9; //add temp bishop
                game.BlackBishops ^= bishop; //remove previous bishop
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackBishops ^= temp >> 9;
                    game.BlackBishops |= bishop;
                    break;
                }
                game.BlackBishops ^= temp >> 9;
                game.BlackBishops |= bishop;
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
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R1);
                if ((temp << 9 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                game.BlackBishops |= temp << 9; //add temp bishop
                game.BlackBishops ^= bishop; //remove previous bishop
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackBishops ^= temp << 9;
                    game.BlackBishops |= bishop;
                    break;
                }
                game.BlackBishops ^= temp << 9;
                game.BlackBishops |= bishop;
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
                temp &= Bitboard.ClearFile(File.H) & Bitboard.ClearRank(Rank.R8);
                if ((temp >> 7 & friendly) != 0) //if overlap with friendly pieces, stop and dont save
                {
                    break;
                }
                game.BlackBishops |= temp >> 7; //add temp bishop
                game.BlackBishops ^= bishop; //remove previous bishop
                if ((game.BlackKing & game.GetWhitePins()) != 0) //piece is pinned
                {
                    game.BlackBishops ^= temp >> 7;
                    game.BlackBishops |= bishop;
                    break;
                }
                game.BlackBishops ^= temp >> 7;
                game.BlackBishops |= bishop;
                temp >>= 7;
                validMoves |= temp;
                if ((temp & enemy) != 0) //if overlap with enemy pieces, allow to save move as attack, but stop after
                {
                    break;
                }
            }
            return validMoves;
        }

        public static ulong QueenMovesWhite(ulong queen, ulong friendly, ulong enemy, Game game) => RookMovesWhite(queen, friendly, enemy, game) | BishopMovesWhite(queen, friendly, enemy, game);
        public static ulong QueenMovesBlack(ulong queen, ulong friendly, ulong enemy, Game game) => RookMovesBlack(queen, friendly, enemy, game) | BishopMovesBlack(queen, friendly, enemy, game);
    }
}