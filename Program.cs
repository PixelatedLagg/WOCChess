using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("8/5Q2/1kbK4/5R2/P7/p4B2/3P2P1/8 w - - 0 1");
        Bitboard.Debug(game.AllWhitePieces);
    }
}