using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game.Move(7, 4, 3);
        Bitboard.Debug(Game.BitBoards[3]);
    }
}