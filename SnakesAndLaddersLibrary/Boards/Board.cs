using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards;

public class Board : IBoard
{
    private const int MaxTiles = 100;


    public Board(IAnimationLogger? animationLogger, ITokenFactory tokenFactory, ITileFactory tileFactory)
    {
        StartPosition = 1;
        Tiles = new ITile[MaxTiles];
        AnimationLogger = animationLogger;
        TokenFactory = tokenFactory;
        TokenFactory = tokenFactory;
        TileFactory = tileFactory;
    }

    private IAnimationLogger? AnimationLogger { get; set; }
    private ITokenFactory TokenFactory { get; set; }
    private ITileFactory TileFactory { get; set; }
    private ITile[] Tiles { get; set; }

    public int StartPosition { get; }

    public async Task FillTiles()
    {
        for (var tilePosition = 1; tilePosition <= MaxTiles; tilePosition++)
        {
            Tiles[tilePosition - 1] = TileFactory.CreateTile(tilePosition);
            await FillTilesAnimation(tilePosition);
        }
    }

    public int GetNextTokenPosition(int oldPosition, int spaces)
    {
        if (CanMoveTokenToNextPosition(oldPosition, spaces))
            return Tiles.ElementAt(oldPosition + spaces - 1).GetNextPosition();
        return oldPosition;
    }

    public bool CanMoveTokenToNextPosition(int oldPosition, int spaces)
    {
        return oldPosition + spaces <= MaxTiles;
    }

    public bool IsTokenInLastPosition(int position)
    {
        return position == MaxTiles;
    }

    public IToken? CreateToken(int playerId)
    {
        return TokenFactory.CreateToken(playerId, this, AnimationLogger);
    }

    #region Animations

    private async Task FillTilesAnimation(int tilePosition)
    {
        await AnimationLogger!.AnimationMessage(new Message
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