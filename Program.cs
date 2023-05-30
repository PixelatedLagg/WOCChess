using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("8/1p4P1/1P5P/2Pp1Pp1/P1P4p/1Pppp3/p7/K1k5 w - - 0 1");
        game.Debug();
    }
} 