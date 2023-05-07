using WOCChess.Game;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = Parser.LoadFEN("2r5/4K2B/np6/8/6p1/2P5/6NN/7k w - - 0 1");
        Bitboard.Debug(game.GetBlackChecks());
        //Bitboard.Debug(ValidMoves.KingMoves(game.WhiteKing, game.GetBlackChecks()));
        //Bitboard.Debug(ValidMoves.KingMoves(0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00001000UL, 0));
    }
}