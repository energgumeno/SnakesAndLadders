using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersLibrary.Games
{
    public class GameSnakesAndLadders : IGame
    {

        protected Board GameBoard { get; set; }
        protected IPlayerManager PlayerManager { get; set; }
   
        protected bool IsEndGame { get; set; }

        public GameSnakesAndLadders(int playerCount, IAnimationLogger animationLogger)
        {
            
            this.GameBoard = new Board( animationLogger);
            this.PlayerManager = new PlayerManager(playerCount, DiceSixSided.Singleton, animationLogger);
            this.IsEndGame = false;
        }

        public async Task StartGame()
        {
            PlayerManager.CheckPlayers();
            await GameBoard.FillTiles();
            await PlayerManager.CreatePlayerList(GameBoard);
        }

        public async Task<Player> Play()
        {
            await PlayerManager.SetNextPlayer();
            while (!IsEndGame)
            {
                var currentPlayer = PlayerManager.GetPlayer();
                var spaces = await currentPlayer.RollDice();
                await currentPlayer.Move(spaces);
                if ( CheckForWinner(currentPlayer))
                {
                    IsEndGame = true;
                    break;
                }
                await PlayerManager.SetNextPlayer();

            }
            await PlayerManager.GetPlayer().Gloat();
            return PlayerManager.GetPlayer();

        }
        public  Player? GetWinner()
        {
            return IsEndGame ? PlayerManager.GetPlayer() : null;
        }

        protected  bool CheckForWinner(Player currentPlayer)
        {
            return  this.GameBoard.IsTokenInLastPosition(currentPlayer.PlayerToken.Position);
        }


    }
}