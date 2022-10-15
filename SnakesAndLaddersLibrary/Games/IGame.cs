
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersLibrary.Games
{
    public interface IGame
    {
        Task Play();
        Task StartGame();
        IPlayer? GetWinner();
        bool CheckForWinner();
    }
}