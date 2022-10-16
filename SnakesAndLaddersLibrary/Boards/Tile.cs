namespace SnakesAndLaddersLibrary.Boards;

public class Tile : ITile
{
    public Tile(int position)
    {
        Position = position;
    }

    private int Position { get;  }

    public int GetCurrentPosition()
    {
        return Position;
    }

    public int GetNextPosition()
    {
        return Position;
    }
}