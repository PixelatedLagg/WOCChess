namespace WOCChess.Game
{
    public class Game
    {
        public Action<Move[]>? WhiteToMove;
        public Action<Move[]>? BlackToMove;
        public Action<int>? GameEnd; //0 is white win, 1 is black win, 2 is draw

        int[,] Board = new int[8, 8] 
        {
            { 15, 12, 13, 15, 16, 13, 12, 15 },
            { 11, 11, 11, 11, 11, 11, 11, 11 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1 },
            { 5, 2, 3, 5, 6, 3, 2, 5 }
        };

        public bool Turn = true; //true is white, false is black

        public void Start()
        {
            WhiteToMove?.Invoke(AllLegalMovesWhite());
        }

        Move[] AllLegalMovesWhite()
        {
            List<Move> moves = new List<Move>();
            int piece;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    piece = Board[i, j];
                    if (piece == 0 || piece > 10)
                    {
                        continue;
                    }
                    switch (piece)
                    {
                        case 1: //pawn
                        {
                            if (i == 0) { break; } //will add promotion later
                            if (i == 6 && Empty(4, j) && LegalMoveWhite(6, j, 4, j, 1)) //checking if can move forward two
                            {
                                moves.Add(new Move(6, j, 4, j, 1));
                            }
                            if (Empty(i - 1, j))
                            {

                            }
                            break; 
                        }
                        case 2: //knight
                        {
                            break; 
                        }
                        case 3: //bishop
                        {
                            break; 
                        }
                        case 4: //rook
                        {
                            break; 
                        }
                        case 5: //queen
                        {
                            break; 
                        }
                        case 6: //king
                        {
                            break; 
                        }
                    }
                }
            }
            return moves.ToArray();
        }

        bool LegalMoveWhite(int fromX, int fromY, int toX, int toY, int possibleKing) //only included possibleKing param in case player moves king
        {
            int[,] board = Board;
            board[fromX, fromY] = 0;
            board[toX, toY] = possibleKing;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int piece = Board[i, j];
                    if (piece < 13 || piece == 16) //searching for black pieces that can pin white pieces
                    {
                        continue;
                    }
                    switch (piece)
                    {
                        case 13: //bishop
                        {
                            int oX = 1, oY = -1; //3, 4 -> 4, 3
                            while (i + oX != 8 && j + oY != -1)
                            {
                                if (Board[i + oX, j + oY] == 6)
                                {
                                    return false;
                                }
                                if (Board[i + oX, j + oY] != 0)
                                {
                                    goto Next0;
                                }
                                oX++;
                                oY--;
                            }
                            Next0:
                            oX = -1;
                            oY = 1; //4, 3 -> 3, 4
                            while (i + oX != -1 && j + oY != 8)
                            {
                                if (Board[i + oX, j + oY] == 6)
                                {
                                    return false;
                                }
                                if (Board[i + oX, j + oY] != 0)
                                {
                                    goto Next1;
                                }
                                oX--;
                                oY++;
                            }
                            Next1:
                            oX = -1;
                            oY = -1; //4, 3 -> 3, 2
                            while (i + oX != -1 && j + oY != -1)
                            {
                                if (Board[i + oX, j + oY] == 6)
                                {
                                    return false;
                                }
                                if (Board[i + oX, j + oY] != 0)
                                {
                                    goto Next2;
                                }
                                oX--;
                                oY--;
                            }
                            Next2:
                            oX = 1;
                            oY = 1; //4, 3 -> 5, 4
                            while (i + oX != 8 && j + oY != 8)
                            {
                                if (Board[i + oX, j + oY] == 6)
                                {
                                    return false;
                                }
                                if (Board[i + oX, j + oY] != 0)
                                {
                                    goto Next2;
                                }
                                oX++;
                                oY++;
                            }
                            break;
                        }
                        case 14: //rook
                        {
                            break;
                        }
                        case 15: //queen
                        {
                            break;
                        }
                    }
                }
            }
            return true;
        }

        bool Empty(int x, int y) => Board[x, y] == 0;

        public void Move(Move move)
        {
            Board[move.FromX, move.FromY] = 0;
            Board[move.ToX, move.ToY] = move.Piece;
            Turn = !Turn;
            if (Turn)
            {
                WhiteToMove?.Invoke(AllLegalMovesWhite());
            }
            else
            {
                BlackToMove?.Invoke(AllLegalMovesWhite()); //change to all legal black moves
            }
        }
    }
}

/*
pawn
1

knight
2

bishop
3

rook
4

queen
5

king
6

black +10
*/