using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards;

public class TokenFactory : ITokenFactory
{
    public IToken? CreateToken(int playerId, IBoard board, IAnimationLogger? animationLogger)
    {
        return new Token(playerId, board, animationLogger);
    }
}