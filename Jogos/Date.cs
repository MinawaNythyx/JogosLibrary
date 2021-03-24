namespace Jogos
{
    public class Date
    {
        private int Day { get; set; }
        private int Month { get; set; }
        private int Year { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"{Year}-{Month}-{Day}";
        }
    }
}