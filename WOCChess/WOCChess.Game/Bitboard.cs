using System.Numerics;

namespace WOCChess.Game
{
    public static class Bitboard
    {
        public static void Debug(ulong board)
        {
            IEnumerable<string> ranks = Enumerable.Range(0, 8).Select(i => $"{String.Concat(Enumerable.Repeat('0', BitOperations.LeadingZeroCount(board)))}{Convert.ToString((long)board, 2)}".Substring(i * 8, 8));
            foreach (string rank in ranks)
            {
                Console.WriteLine($"{rank[7]}  {rank[6]}  {rank[5]}  {rank[4]}  {rank[3]}  {rank[2]}  {rank[1]}  {rank[0]}");
            }
        }
        
        public static ulong ClearRank(Rank rank)
        {
            return Convert.ToUInt64($"{String.Concat(Enumerable.Repeat("11111111", 7 - (int)rank))}00000000{String.Concat(Enumerable.Repeat("11111111", (int)rank))}", 2);
        }

        public static ulong MaskRank(Rank rank)
        {
            return Convert.ToUInt64($"{String.Concat(Enumerable.Repeat("00000000", 7 - (int)rank))}11111111{String.Concat(Enumerable.Repeat("00000000", (int)rank))}", 2);
        }

        public static ulong ClearFile(File file)
        {
            string rank = $"{String.Concat(Enumerable.Repeat("1", (int)file))}0{String.Concat(Enumerable.Repeat("1", 7 - (int)file))}";
            return Convert.ToUInt64($"{rank}{rank}{rank}{rank}{rank}{rank}{rank}{rank}", 2);
        }

        public static ulong MaskFile(File file)
        {
            string rank = $"{String.Concat(Enumerable.Repeat("0", (int)file))}1{String.Concat(Enumerable.Repeat("0", 7 - (int)file))}";
            return Convert.ToUInt64($"{rank}{rank}{rank}{rank}{rank}{rank}{rank}{rank}", 2);
        }

        public static ulong GetPosition(string position)
        {
            ulong empty = 0B_0000000000000000000000000000000000000000000000000000000000000000UL;
            return empty |= 1UL << position[0] - 'A' + (position[1] - '1') * 8;
        }
    }
}