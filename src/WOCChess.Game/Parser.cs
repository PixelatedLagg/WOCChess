namespace WOCChess.Game
{
    /// <summary>Provides parsing for loading into Games in a custom position.</summary>
    public static class Parser
    {
        /// <summary>Load a Game at a position defined by a FEN string.</summary>
        /// <param name="fen">The FEN string to grab the position from.</param>
        public static Game LoadFEN(string fen)
        {
            Game game = new Game();
            int pos = 56;
            int i = 0;
            while (fen[i] != ' ')
            {
                switch (fen[i])
                {
                    case 'P':
                        game.WhitePawns |= 1UL << pos;
                        break;
                    case 'R':
                        game.WhiteRooks |= 1UL << pos;
                        break;
                    case 'N':
                        game.WhiteKnights |= 1UL << pos;
                        break;
                    case 'B':
                        game.WhiteBishops |= 1UL << pos;
                        break;
                    case 'Q':
                        game.WhiteQueens |= 1UL << pos;
                        break;
                    case 'K':
                        game.WhiteKing |= 1UL << pos;
                        break;
                    case 'p':
                        game.BlackPawns |= 1UL << pos;
                        break;
                    case 'r':
                        game.BlackRooks |= 1UL << pos;
                        break;
                    case 'n':
                        game.BlackKnights |= 1UL << pos;
                        break;
                    case 'b':
                        game.BlackBishops |= 1UL << pos;
                        break;
                    case 'q':
                        game.BlackQueens |= 1UL << pos;
                        break;
                    case 'k':
                        game.BlackKing |= 1UL << pos;
                        break;
                    case '/':
                        pos -= 17;
                        break;
                    default:
                        pos += fen[i] - '1';
                        break;
                }
                pos++;
                i++;
            }
            game.Turn = fen[i + 1] == 'w';
            i += 3;
            while (fen[i] != ' ')
            {
                switch (fen[i])
                {
                    case 'K':
                        game.WhiteShortCastle = true;
                        break;
                    case 'Q':
                        game.WhiteLongCastle = true;
                        break;
                    case 'k':
                        game.BlackShortCastle = true;
                        break;
                    case 'q':
                        game.BlackLongCastle = true;
                        break;
                }
                i++;
            }
            i++;
            if (fen[i] != '-')
            {
                if (fen[i + 1] == '3') //white can be en passanted
                {
                    game.WhiteEPPawn = Bitboard.GetBoard($"{fen[i]}{fen[i + 1]}") >> 8;
                }
                else //black can be en passanted
                {
                    game.BlackEPPawn = Bitboard.GetBoard($"{fen[i]}{fen[i + 1]}") << 8;
                }
            }
            i += 2;
            string halfMoves = "";
            while (fen[i] != ' ')
            {
                halfMoves += $"{fen[i]}";
                i++;
            }
            game.HalfMoves = Convert.ToInt32(halfMoves);
            i++;
            string fullMoves = "";
            while (fen.Length > i)
            {
                fullMoves += $"{fen[i]}";
                i++;
            }
            return game;
        }
    }
}