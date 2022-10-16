using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards;

public class Board : IBoard
{
    protected const int MaxTiles = 100;


    public Board(IAnimationLogger animationLogger, ITokenFactory tokenFactory, ITileFactory tileFactory)
    {
        StartPosition = 1;
        Tiles = new ITile[MaxTiles];
        AnimationLogger = animationLogger;
        TokenFactory = tokenFactory;
        TokenFactory = tokenFactory;
        TileFactory = tileFactory;
    }

    protected IAnimationLogger AnimationLogger { get; set; }
    protected ITokenFactory TokenFactory { get; set; }
    protected ITileFactory TileFactory { get; set; }
    protected ITile[] Tiles { get; set; }

    public int StartPosition { get; }

    public async Task FillTiles()
    {
        for (var tilePosition = 1; tilePosition <= MaxTiles; tilePosition++)
        {
            Tiles[tilePosition - 1] = TileFactory.CreateTile(tilePosition);
            await FillTilesAnimation(tilePosition);
        }
    }

    public int GetNextTokenPosition(int OldPosition, int spaces)
    {
        if (CanMoveTokenToNextPosition(OldPosition, spaces))
            return Tiles.ElementAt(OldPosition + spaces - 1).GetNextPosition();
        return OldPosition;
    }

    public bool CanMoveTokenToNextPosition(int OldPosition, int spaces)
    {
        return OldPosition + spaces <= MaxTiles;
    }

    public bool IsTokenInLastPosition(int Position)
    {
        return Position == MaxTiles;
    }

    public IToken CreateToken(int playerId)
    {
        return TokenFactory.CreateToken(playerId, this, AnimationLogger);
    }

    #region Animations

    private async Task FillTilesAnimation(int tilePosition)
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(IBoard),
            Animation = nameof(FillTiles),
            Values = new List<KeyValuePair<string, string>>
            {
                new("TilePosition", tilePosition.ToString())
            }
        });
    }

    #endregion
}