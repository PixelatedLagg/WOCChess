using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("2K5/8/4Q3/2P3k1/8/Pr3p2/2p1pp2/8 w - - 0 1");
        Bitboard.Debug(ValidMoves.KingMoves(game.BlackKing, game.GetWhiteChecks()));
        Bitboard.Debug(ValidMoves.QueenMovesWhite(game.WhiteQueens, game.AllWhitePieces, game.AllBlackPieces, game));
    }
}