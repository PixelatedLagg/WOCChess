using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
        game.UnsafeMove(new Move(11, 27, Piece.Pawn));
        Bitboard.Debug(game.AllPieces);
    }
}