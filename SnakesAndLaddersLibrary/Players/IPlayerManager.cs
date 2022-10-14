
using SnakesAndLaddersLibrary.Boards;

namespace SnakesAndLaddersLibrary.Players
{
    public interface IPlayerManager
    {
        Task CreatePlayerList(IBoard gameBoard);
        IPlayer GetPlayer();
        Task SetNextPlayer();
        void CheckPlayersCount();
    }
}