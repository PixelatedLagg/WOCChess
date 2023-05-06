using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("8/n6k/8/3p4/1R1Pr3/7B/3P3p/K7 w - - 0 1");
        Bitboard.Debug(ValidMoves.KingMoves(game.WhiteKing, game.GetBlackChecks()));
    }
}