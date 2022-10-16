using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Dices
{
    public class Dice : IDice
    {
        protected int MinValue { get; set; }
        protected int MaxValue { get; set; }
        private Random RandomNumber { get; set; }

        protected Dice(int minValue, int maxValue)
        {
            RandomNumber = new Random(DateTime.Now.Millisecond);
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public  int Roll()
        {
            return RandomNumber.Next(MinValue, MaxValue);
        }
    }
}