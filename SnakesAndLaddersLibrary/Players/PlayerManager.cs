using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players
{
    public class PlayerManager : IPlayerManager
    {
        public IDice TheDice { get; }
        protected IAnimationLogger AnimationLogger { get; }
        public IPlayerFactory PlayerFactory { get; }

        protected int PlayerCount { get; }
        protected IPlayer[] PlayerList { get; set; }
        protected IPlayer? CurrentPlayer { get; set; }
        public IPlayer GetPlayer() => CurrentPlayer;

        public PlayerManager(int playerCount, IDice theDice, IAnimationLogger animationLogger, IPlayerFactory playerFactory)
        {

            this.TheDice = theDice;
            this.PlayerCount = playerCount;
            this.AnimationLogger = animationLogger;
            this.PlayerFactory = playerFactory;

            this.PlayerList = new IPlayer[playerCount];
            this.CurrentPlayer = null;

        }

        public async Task CreatePlayerList(IBoard gameBoard)
        {
            for (int playerId = 1; playerId <= PlayerCount; playerId++)
            {
                IToken? token = gameBoard.CreateToken(playerId);
                await CreatePlayer(token, gameBoard, playerId);
            }
        }

        private async Task CreatePlayer(IToken token, IBoard gameBoard, int playerId)
        {
            PlayerList[playerId - 1] = PlayerFactory.CreatePlayer(playerId, token, TheDice, AnimationLogger);
            await CrateNewPlayerAnimation(playerId);
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
                    new KeyValuePair<string, string>("PlayerId",CurrentPlayer?.PlayerId.ToString()??string.Empty)

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
