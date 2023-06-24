using System.Numerics;

namespace WOCChess.Game
{
    /// <summary>Utilities for working with bitboards.</summary>
    public static class Bitboard
    {
        /// <summary>Debug a bitboard by printing out all values in the shape of a chess board.</summary>
        /// <param name="board">The board to debug.</param>
        public static void Debug(ulong board)
        {
            IEnumerable<string> ranks = Enumerable.Range(0, 8).Select(i => $"{String.Concat(Enumerable.Repeat('0', BitOperations.LeadingZeroCount(board)))}{Convert.ToString((long)board, 2)}".Substring(i * 8, 8));
            foreach (string rank in ranks)
            {
                Console.WriteLine($"{rank[7]}  {rank[6]}  {rank[5]}  {rank[4]}  {rank[3]}  {rank[2]}  {rank[1]}  {rank[0]}");
            }
            Console.WriteLine($"{String.Concat(Enumerable.Repeat('0', BitOperations.LeadingZeroCount(board)))}{Convert.ToString((long)board, 2)}");
        }
        
        /// <summary>Set every value in a rank (x axis) to zeros.</summary>
        /// <param name="rank">The rank to clear.</param>
        public static ulong ClearRank(Rank rank)
        {
            return Convert.ToUInt64($"{String.Concat(Enumerable.Repeat("11111111", 7 - (int)rank))}00000000{String.Concat(Enumerable.Repeat("11111111", (int)rank))}", 2);
        }

        /// <summary>Set every value in a rank (x axis) to ones.</summary>
        /// <param name="rank">The rank to mask.</param>
        public static ulong MaskRank(Rank rank)
        {
            return Convert.ToUInt64($"{String.Concat(Enumerable.Repeat("00000000", 7 - (int)rank))}11111111{String.Concat(Enumerable.Repeat("00000000", (int)rank))}", 2);
        }

        /// <summary>Set every value in a file (y axis) to zeros.</summary>
        /// <param name="file">The file to clear.</param>
        public static ulong ClearFile(File file)
        {
            string rank = $"{String.Concat(Enumerable.Repeat("1", 7 - (int)file))}0{String.Concat(Enumerable.Repeat("1", (int)file))}";
            return Convert.ToUInt64($"{rank}{rank}{rank}{rank}{rank}{rank}{rank}{rank}", 2);
        }

        /// <summary>Set every value in a file (y axis) to ones.</summary>
        /// <param name="file">The file to mask.</param>
        public static ulong MaskFile(File file)
        {
            string rank = $"{String.Concat(Enumerable.Repeat("0", 7 - (int)file))}1{String.Concat(Enumerable.Repeat("0", (int)file))}";
            return Convert.ToUInt64($"{rank}{rank}{rank}{rank}{rank}{rank}{rank}{rank}", 2);
        }

        /// <summary>Generate a bitboard with the value represented by chess coordinates set to one.</summary>
        /// <param name="position">The chess coordinates: a letter (A-H) followed by a number (1-8), case insensitive.</param>
        public static ulong GetBoard(string position)
        {
            ulong empty = 0B_0000000000000000000000000000000000000000000000000000000000000000UL;
            return empty |= 1UL << Char.ToLower(position[0]) - 'a' + (position[1] - '1') * 8;
        }

        /// <summary>Iterate over each value set to one on a bitboard.</summary>
        /// <param name="board">The bitboard to iterate over.</param>
        public static IEnumerable<ulong> DivideBoard(ulong board)
        {
            for (int i = 0; i < 64; i++)
            {
                if ((board | 1UL << i) == board)
                {
                    yield return 1UL << i;
                }
            }
        }

        /// <summary>Check if a bitboard contains a one in common with another bitboard.</summary>
        /// <param name="board">The first bitboard.</param>
        /// <param name="piece">The second bitboard, generally a piece.</param>
        public static bool Contains(this ulong board, ulong piece) => (board & piece) != 0;
    }
}