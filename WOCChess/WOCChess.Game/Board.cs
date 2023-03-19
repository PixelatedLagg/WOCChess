namespace WOCChess.Game
{
    public static class Bitboard
    {
        public static void Debug(ulong board)
        {
            IEnumerable<string> ranks = Enumerable.Range(0, 8).Select(i => Convert.ToString((long)board, 2).Substring(i * 8, 8));
            foreach (string rank in ranks)
            {
                Console.WriteLine($"{rank[0]}  {rank[1]}  {rank[2]}  {rank[3]}  {rank[4]}  {rank[5]}  {rank[6]}  {rank[7]}");
            }
        }
    }
}