namespace SnakesAndLaddersLibrary.Boards
{
    public class Tile : ITile
    {
        public int Position { get; set; }

        public Tile(int position)
        {
            Position = position;
        }

        public int GetCurrentPosition()
        {
            return Position;
        }

        public int GetNextPosition()
        {
            return Position ;
        }
    }
}
