
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersLibrary.Games
{
    public interface IGame
    {
        Task<Player> Play();
        Task StartGame();
        Player? GetWinner();
    }
}