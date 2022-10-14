using SnakesAndLaddersLibrary.Boards;

namespace SnakesAndLaddersLibrary.Players
{
    public interface IPlayer
    {
        int PlayerId { get; }
        IToken PlayerToken { get; }

        Task Gloat();
        Task Move(int spaces);
        Task<int> RollDice();
    }
}