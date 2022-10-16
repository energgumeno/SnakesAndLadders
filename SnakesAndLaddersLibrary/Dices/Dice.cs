namespace SnakesAndLaddersLibrary.Dices;

public class Dice : IDice
{
    protected Dice(int minValue, int maxValue)
    {
        RandomNumber = new Random(DateTime.Now.Millisecond);
        MinValue = minValue;
        MaxValue = maxValue;
    }

    protected int MinValue { get; set; }
    protected int MaxValue { get; set; }
    private Random RandomNumber { get; }

    public int Roll()
    {
        return RandomNumber.Next(MinValue, MaxValue);
    }
}