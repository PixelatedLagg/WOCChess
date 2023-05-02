namespace WOCChess.Game
{
    public static class Parser
    {
        public static Game LoadFEN(string fen)
        {
            Game game = new Game();
            int pos = 56;
            int i = 0;
            for (; i < fen.Length; i++)
            {
                switch (fen[i])
                {
                    case 'P':
                        game.WhitePawns |= 1U << pos;
                        break;
                    case 'R':
                        game.WhiteRooks |= 1U << pos;
                        break;
                    case 'N':
                        game.WhiteKnights |= 1U << pos;
                        break;
                    case 'B':
                        game.WhiteBishops |= 1U << pos;
                        break;
                    case 'Q':
                        game.WhiteQueens |= 1U << pos;
                        break;
                    case 'K':
                        game.WhiteKing |= 1U << pos;
                        break;
                    case 'p':
                        game.BlackPawns |= 1U << pos;
                        break;
                    case 'r':
                        game.BlackRooks |= 1U << pos;
                        break;
                    case 'n':
                        game.BlackKnights |= 1U << pos;
                        break;
                    case 'b':
                        game.BlackBishops |= 1U << pos;
                        break;
                    case 'q':
                        game.BlackQueens |= 1U << pos;
                        break;
                    case 'k':
                        game.BlackKing |= 1U << pos;
                        break;
                    case '/':
                        pos -= 15;
                        break;
                    case ' ':
                        goto turn;
                    default:
                        pos += fen[i] - '0';
                        break;
                }
                pos++;
            }
            turn:
            i += 2;
            game.Turn = fen[i] == 'w';
            i += 2;
            while (i != ' ')
            {
                switch (fen[i])
                {
                    case 'K':
                        break;
                    case 'Q':
                        break;
                    case 'k':
                        break;
                    case 'q':
                        break;
                }
                i++;
            }
            return game;
        }
    }
}