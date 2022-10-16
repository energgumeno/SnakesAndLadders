namespace SnakesAndLaddersLibrary.Dices;

public class DiceSixSided : Dice
{
    private static DiceSixSided? _singleton;

    private DiceSixSided() : base(1, 6)
    {
    }

    public static DiceSixSided Singleton => _singleton ??= new DiceSixSided();
}