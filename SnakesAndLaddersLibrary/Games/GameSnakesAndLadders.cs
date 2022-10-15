using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersLibrary.Games
{
    public class GameSnakesAndLadders : IGame
    {

        protected IBoard GameBoard { get; set; }
        protected IPlayerManager PlayerManager { get; set; }
   
        protected bool IsEndGame { get; set; }

        public GameSnakesAndLadders(int playerCount, IAnimationLogger animationLogger,IPlayerManager playerManager, IBoard gameBoard)
        {

            this.PlayerManager = playerManager;
            this.GameBoard = gameBoard;
            this.IsEndGame = false;
        }
        

        public async Task StartGame()
        {
            PlayerManager.CheckPlayersCount();
            await GameBoard.FillTiles();
            await PlayerManager.CreatePlayerList(GameBoard);
        }

        public async Task Play()
        {
            while (!IsEndGame)
            {
                 await PlayOnemove();
            }

            await PlayerManager.GetPlayer().Gloat();
           

        }

        public async Task PlayOnemove()
        {

            await PlayerManager.SetNextPlayer();
            IPlayer  currentPlayer = PlayerManager.GetPlayer();
            var spaces = await currentPlayer.RollDice();
            await currentPlayer.Move(spaces);
            CheckForWinner();
     
        }

        public  IPlayer? GetWinner()
        {
            return IsEndGame ? PlayerManager.GetPlayer() : null;
        }

        public bool CheckForWinner()
        {
            return IsEndGame= this.GameBoard.IsTokenInLastPosition(PlayerManager.GetPlayer().PlayerToken.Position);
        }


    }
}