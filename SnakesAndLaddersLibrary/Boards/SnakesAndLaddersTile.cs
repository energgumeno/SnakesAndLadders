namespace SnakesAndLaddersLibrary.Boards;

public class SnakesAndLaddersTile : ITile
{
    public SnakesAndLaddersTile(int position, Tuple<int, int>[] ladders, Tuple<int, int>[] snakes)
    {
        Position = position;
        SnakeOrLadder =
            ladders.FirstOrDefault(d => d.Item1 == position) ??
            snakes.FirstOrDefault(d => d.Item1 == position);
    }

    private int Position { get; }
    private Tuple<int, int>? SnakeOrLadder { get; }

    public int GetCurrentPosition()
    {
        return Position;
    }

    public int GetNextPosition()
    {
        return SnakeOrLadder?.Item2 ?? Position;
    }
}