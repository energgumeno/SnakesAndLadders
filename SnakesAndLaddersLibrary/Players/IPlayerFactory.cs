using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players;

public interface IPlayerFactory
{
    IPlayer CreatePlayer(int playerId, IToken? playerToken, IDice? theDice, IAnimationLogger? animationLogger);
}