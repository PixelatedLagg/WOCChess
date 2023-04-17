namespace WOCChess.Game
{
    public static class Parser
    {
        public static Game LoadFEN(string fen)
        {
            Game game = new Game();
            int pos = 56;
            while (pos > -1)
            {
                switch (fen[pos])
                {
                    case 'p':

                        break;
                    case 'r':
                        break;
                    case 'n':
                        break;
                    case 'b':
                        break;
                    case 'q':
                        break;
                    case 'k':
                        break;
                    case 'P':
                        break;
                    case 'R':
                        break;
                    case 'N':
                        break;
                    case 'B':
                        break;
                    case 'Q':
                        break;
                    case 'K':
                        break;
                    case '/':
                        break;
                    default:
                        break;
                }
            }
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
            return game;
        }
    }
}