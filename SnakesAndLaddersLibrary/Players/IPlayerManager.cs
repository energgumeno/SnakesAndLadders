
using SnakesAndLaddersLibrary.Boards;

namespace SnakesAndLaddersLibrary.Players
{
    public interface IPlayerManager
    {
        Task CreatePlayerList(Board gameBoard);
        Player GetPlayer();
        Task SetNextPlayer();
        void CheckPlayers();
    }
}