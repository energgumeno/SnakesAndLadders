namespace SnakesAndLaddersLibrary.Boards;

public class SnakesAndLaddersTilesFactory : ITileFactory
{
    private static readonly Tuple<int, int>[] Ladders =
    {
        new(2, 38),
        new(7, 14),
        new(8, 31),
        new(15, 26),
        new(21, 42),
        new(28, 84),
        new(36, 44),
        new(51, 67),
        new(71, 91),
        new(78, 98),
        new(87, 94)
    };

    private static readonly Tuple<int, int>[] Snakes =
    {
        new(16, 6),
        new(46, 25),
        new(49, 11),
        new(62, 19),
        new(64, 60),
        new(74, 53),
        new(89, 68),
        new(95, 75),
        new(99, 80)
    };

    public ITile CreateTile(int tilePosition)
    {
        return new SnakesAndLaddersTile(tilePosition, Ladders, Snakes);
    }
}