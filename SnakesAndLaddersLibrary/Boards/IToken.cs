using SnakesAndLaddersLibrary.Boards;

namespace SnakesAndLaddersLibrary.Boards
{
    public interface IToken
    {
        IBoard GameBoard { get; }
        int Position { get; }
        int TokenId { get; }

        Task Move(int spaces);
    }
}