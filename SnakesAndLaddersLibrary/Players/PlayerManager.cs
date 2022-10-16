using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players;

public class PlayerManager : IPlayerManager
{
    public PlayerManager(int playerCount, IDice theDice, IAnimationLogger animationLogger, IPlayerFactory playerFactory)
    {
        TheDice = theDice;
        PlayerCount = playerCount;
        AnimationLogger = animationLogger;
        PlayerFactory = playerFactory;

        PlayerList = new IPlayer[playerCount];
        CurrentPlayer = null;
    }

    public IDice TheDice { get; }
    protected IAnimationLogger AnimationLogger { get; }
    public IPlayerFactory PlayerFactory { get; }

    protected int PlayerCount { get; }
    protected IPlayer[] PlayerList { get; set; }
    protected IPlayer? CurrentPlayer { get; set; }

    public IPlayer GetPlayer()
    {
        return CurrentPlayer;
    }

    public async Task CreatePlayerList(IBoard gameBoard)
    {
        for (var playerId = 1; playerId <= PlayerCount; playerId++)
        {
            var token = gameBoard.CreateToken(playerId);
            await CreatePlayer(token, gameBoard, playerId);
        }
    }


    public async Task SetNextPlayer()
    {
        CurrentPlayer = CurrentPlayer == null ? PlayerList[0] : PlayerList[CurrentPlayer.PlayerId % PlayerCount];

        await NextPlayerAnimation();
    }

    private async Task CreatePlayer(IToken token, IBoard gameBoard, int playerId)
    {
        PlayerList[playerId - 1] = PlayerFactory.CreatePlayer(playerId, token, TheDice, AnimationLogger);
        await CrateNewPlayerAnimation(playerId);
    }

    #region Animation

    private async Task CrateNewPlayerAnimation(int playerId)
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(IPlayerManager),
            Animation = nameof(CreatePlayer),
            Values = new List<KeyValuePair<string, string>>
            {
                new("PlayerId", playerId.ToString())
            }
        });
    }

    private async Task NextPlayerAnimation()
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(IPlayerManager),
            Animation = nameof(SetNextPlayer),
            Values = new List<KeyValuePair<string, string>>
            {
                new("PlayerId", CurrentPlayer?.PlayerId.ToString() ?? string.Empty)
            }
        });
    }

    public void CheckPlayersCount()
    {
        if (PlayerCount <= 0) throw new ArgumentException("Players must be 1 or more");
    }

    #endregion
}