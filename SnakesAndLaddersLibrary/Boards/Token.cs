using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards;

public class Token : IToken
{
    public Token(int tokenId, IBoard gameBoard, IAnimationLogger animationLogger)
    {
        TokenId = tokenId;
        Position = gameBoard.StartPosition;
        GameBoard = gameBoard;
        AnimationLogger = animationLogger;
    }

    protected IAnimationLogger AnimationLogger { get; set; }
    public int TokenId { get; protected set; }
    public int Position { get; protected set; }
    public IBoard GameBoard { get; protected set; }

    public async Task Move(int spaces)
    {
        var oldPosition = Position;
        if (GameBoard.CanMoveTokenToNextPosition(Position, spaces))
            Position = GameBoard.GetNextTokenPosition(Position, spaces);

        await MoveAnimation(oldPosition);
    }

    #region Animation

    private async Task MoveAnimation(int oldPosition)
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(Token),
            Animation = nameof(Move),
            Values = new List<KeyValuePair<string, string>>
            {
                new("TokenId", TokenId.ToString()),
                new("OldPosition", oldPosition.ToString()),
                new("Position", Position.ToString())
            }
        });
    }

    #endregion
}