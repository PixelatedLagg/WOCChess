using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Bitboard.Debug(Bitboard.MaskFile(WOCChess.Game.File.A));
    }
}