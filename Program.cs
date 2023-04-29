using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
    }
}