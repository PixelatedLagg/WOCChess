using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        //Game game = new Game();
        //game.Start();
        //game.UnsafeMove(new Move(11, 27, Piece.Pawn));
        Game game = new Game();
        Bitboard.Debug(game.ValidWhitePawnMoves(Bitboard.GetBoard("A7")));
    }
}