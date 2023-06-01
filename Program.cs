using WOCChess.Game;

public class Program
{
    public static Game game = new Game();
    public static char piece;
    public static string previous = "", current = "";

    public static void Main(string[] args)
    {
        game.WhiteToMove += WhiteToMove;
        game.BlackToMove += BlackToMove;
        game.Default();
        game.Start();
    }

    public static void Input()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\nPiece to move: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        piece = Char.ToLower((Console.ReadLine() ?? "a")[0]);
        while (piece != 'p' && piece != 'n' && piece != 'b' && piece != 'r' && piece != 'q' && piece != 'k')
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Piece to move: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            piece = Char.ToLower((Console.ReadLine() ?? "a")[0]);
        }
        if (piece != 'k')
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nPiece position: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            previous = Console.ReadLine() ?? "";
            while (previous.Length != 2 || previous[0] > 'h' || previous[0] < 'a' || previous[1] > '8' || previous[1] < '1')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Piece position: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                previous = Console.ReadLine() ?? "";
            }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\nPosition to move: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        current = Console.ReadLine() ?? "";
        while (current.Length != 2 || current[0] > 'h' || current[0] < 'a' || current[1] > '8' || current[1] < '1')
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Position to move: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            current = Console.ReadLine() ?? "";
        }
    }

    public static void WhiteToMove()
    {
        game.Print();
        Input();
        switch (piece)
        {
            case 'p':
            {
                game.MoveWhitePawn(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'n':
            {
                game.MoveWhiteKnight(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'b':
            {
                game.MoveWhiteBishop(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'r':
            {
                game.MoveWhiteRook(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'q':
            {
                game.MoveWhiteQueen(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'k':
            {
                game.MoveWhiteKing(Bitboard.GetBoard(current));
                break;
            }
        }
    }

    public static void BlackToMove()
    {
        game.Print(false);
        Input();
        switch (piece)
        {
            case 'p':
            {
                game.MoveBlackPawn(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'n':
            {
                game.MoveBlackKnight(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'b':
            {
                game.MoveBlackBishop(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'r':
            {
                game.MoveBlackRook(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'q':
            {
                game.MoveBlackQueen(Bitboard.GetBoard(previous), Bitboard.GetBoard(current));
                break;
            }
            case 'k':
            {
                game.MoveBlackKing(Bitboard.GetBoard(current));
                break;
            }
        }
    }
} 