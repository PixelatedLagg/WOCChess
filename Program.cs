using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        Bitboard.Debug(game.WhiteRooks);
        Bitboard.Debug(game.GetWhiteChecks());
    }
}