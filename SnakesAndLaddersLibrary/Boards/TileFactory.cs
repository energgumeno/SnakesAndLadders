namespace SnakesAndLaddersLibrary.Boards;

public class TileFactory : ITileFactory
{
    public ITile CreateTile(int tilePosition)
    {
        return new Tile(tilePosition);
    }
}