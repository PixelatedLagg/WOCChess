using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.WhiteToMove += WhiteMove;
        game.BlackToMove += BlackMove;
        game.Error += Error;
    }
    static void WhiteMove() //called when white's move
    {

    }
    static void BlackMove() //called when black's move
    {

    }
    static void Error(string message) //all move errors go here
    {

    }
}