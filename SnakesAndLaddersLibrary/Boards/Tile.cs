namespace SnakesAndLaddersLibrary.Boards;

public class Tile : ITile
{
    public Tile(int position)
    {
        Position = position;
    }

    public int Position { get; set; }

    public int GetCurrentPosition()
    {
        return Position;
    }

    public int GetNextPosition()
    {
        return Position;
    }
}