using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players
{
    public class PlayerManager : IPlayerManager
    {

        protected IPlayer[] PlayerList { get; set; }
        protected IPlayer? CurrentPlayer { get; set; }
        protected int PlayerCount { get; }
        protected IAnimationLogger AnimationLogger { get; }
        public IDice TheDice { get; }

        public IPlayer GetPlayer() => CurrentPlayer;
        public PlayerManager(int playerCount, IDice theDice, IAnimationLogger animationLogger)
        {
            this.PlayerList = new Player[playerCount];
            this.PlayerCount = playerCount;
            this.AnimationLogger = animationLogger;
            this.TheDice = theDice;
            this.CurrentPlayer = null;

        }

        public async Task CreatePlayerList(IBoard gameBoard)
        {

            for (int playerId = 1; playerId <= PlayerCount; playerId++)
            {
                var token = new Token(playerId, gameBoard, this.AnimationLogger);
                await CreatePlayer(token, gameBoard, playerId);
            }

        }

        private async Task CreatePlayer(IToken token, IBoard gameBoard, int id)
        {

            PlayerList[id - 1] = new Player(id, token, TheDice, this.AnimationLogger);
            await CrateNewPlayerAnimation(id);
        }

        public async Task SetNextPlayer()
        {
            CurrentPlayer = CurrentPlayer == null ?
                PlayerList[0] :
                PlayerList[(CurrentPlayer.PlayerId) % PlayerCount];

            await NextPlayerAnimation();
        }

        #region Animation
        private async Task CrateNewPlayerAnimation(int playerId)
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(IPlayerManager).ToString(),
                Animation = nameof(CreatePlayer).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("PlayerId",playerId.ToString())

                }
            });
        }

        private async Task NextPlayerAnimation()
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(IPlayerManager).ToString(),
                Animation = nameof(SetNextPlayer).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("PlayerId",CurrentPlayer.PlayerId.ToString())

                }
            });
        }

        public void CheckPlayersCount()
        {
            if (PlayerCount <= 0)
            {
                throw new ArgumentException("Players must be 1 or more");
            }
        }
        #endregion
    }
}
