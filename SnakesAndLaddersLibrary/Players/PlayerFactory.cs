using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players;

public class PlayerFactory : IPlayerFactory
{
    public IPlayer CreatePlayer(int playerId, IToken? playerToken, IDice? theDice, IAnimationLogger? animationLogger)
    {
        return new Player(playerId, playerToken, theDice, animationLogger);
    }
}