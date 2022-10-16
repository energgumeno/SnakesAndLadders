namespace SnakesAndLaddersLibrary.Boards;

public interface ITileFactory
{
    ITile CreateTile(int tilePosition);
}