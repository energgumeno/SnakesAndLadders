using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards;

public interface ITokenFactory
{
    IToken CreateToken(int playerId, IBoard board, IAnimationLogger animationLogger);
}