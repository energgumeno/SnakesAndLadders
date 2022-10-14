using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersLibrary.Players
{
    public class PlayerManager : IPlayerManager
    {

        protected Player[] PlayerList { get; set; }
        protected Player? CurrentPlayer { get; set; }
        protected int PlayerCount { get; }
        protected IAnimationLogger AnimationLogger { get; }
        public IDice TheDice { get; }

        public Player GetPlayer() => CurrentPlayer;
        public PlayerManager(int playerCount, IDice theDice, IAnimationLogger animationLogger)
        {
            this.PlayerList = new Player[playerCount];
            this.PlayerCount = playerCount;
            this.AnimationLogger = animationLogger;
            this.TheDice = theDice;
            this.CurrentPlayer = null;

        }

        public async Task CreatePlayerList(Board gameBoard)
        {
            List<Player> playerList = new List<Player>();
            for (int playerId = 1; playerId <= PlayerCount; playerId++)
            {
                await CreatePlayer(gameBoard, playerList, playerId);
            }
            this.PlayerList = playerList.ToArray();
        }

        private async Task CreatePlayer(IBoard gameBoard, List<Player> playerList, int id)
        {
            var token = new Token(id, gameBoard.StartPosition, gameBoard, this.AnimationLogger);
            playerList.Add(new Player(id, token, TheDice, this.AnimationLogger));
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
                Sender = nameof(PlayerManager).ToString(),
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
                Sender = nameof(PlayerManager).ToString(),
                Animation = nameof(SetNextPlayer).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("PlayerId",CurrentPlayer.PlayerId.ToString())

                }
            });
        }

        public void CheckPlayers()
        {
            if (PlayerCount <= 0)
            {
                throw new ArgumentException("Players must be 1 or more");
            }
        }
        #endregion
    }
}
