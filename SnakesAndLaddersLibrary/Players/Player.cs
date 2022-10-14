using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players
{
    public class Player
    {
        public int PlayerId { get; protected set; }
        public Token PlayerToken { get; protected set; }
        protected IDice TheDice { get; set; }
        protected IAnimationLogger AnimationLogger { get; set; }

        public Player(int playerId, Token playerToken, IDice theDice, IAnimationLogger animationLogger)
        {
            this.PlayerToken = playerToken;
            this.PlayerId = playerId;
            this.AnimationLogger = animationLogger;
            this.TheDice = theDice;
        }

        internal async Task<int> RollDice()
        {
            var diceRolled = TheDice.Roll();
            await ThrowsDiceAnimation(diceRolled);

            return diceRolled;
        }

        #region Animations
        public virtual async Task Gloat()
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(Player).ToString(),
                Animation = nameof(Gloat).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("PlayerId",PlayerId.ToString())

                }
            });
        }

        internal async Task Move(int spaces)
        {
           await PlayerToken.Move(spaces);
        }

        private async Task ThrowsDiceAnimation(int diceRolled)
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(Player).ToString(),
                Animation = nameof(RollDice).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("PlayerId",PlayerId.ToString()),
                     new KeyValuePair<string, string>("diceRolled",diceRolled.ToString())

                }
            });
        }
        #endregion
    }
}
