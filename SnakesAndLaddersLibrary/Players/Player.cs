using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players;

public class Player : IPlayer
{
    public Player(int playerId, IToken playerToken, IDice theDice, IAnimationLogger animationLogger)
    {
        PlayerToken = playerToken;
        PlayerId = playerId;
        AnimationLogger = animationLogger;
        TheDice = theDice;
    }

    protected IDice TheDice { get; set; }
    protected IAnimationLogger AnimationLogger { get; set; }
    public int PlayerId { get; protected set; }
    public IToken PlayerToken { get; protected set; }

    public async Task<int> RollDice()
    {
        var diceRolled = TheDice.Roll();
        await ThrowsDiceAnimation(diceRolled);

        return diceRolled;
    }

    public async Task Move(int spaces)
    {
        await PlayerToken.Move(spaces);
    }


    #region Animations

    public virtual async Task Gloat()
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(IPlayer),
            Animation = nameof(Gloat),
            Values = new List<KeyValuePair<string, string>>
            {
                new("PlayerId", PlayerId.ToString())
            }
        });
    }


    private async Task ThrowsDiceAnimation(int diceRolled)
    {
        await AnimationLogger.AnimationMessage(new Message
        {
            Sender = nameof(IPlayer),
            Animation = nameof(RollDice),
            Values = new List<KeyValuePair<string, string>>
            {
                new("PlayerId", PlayerId.ToString()),
                new("diceRolled", diceRolled.ToString())
            }
        });
    }

    #endregion
}