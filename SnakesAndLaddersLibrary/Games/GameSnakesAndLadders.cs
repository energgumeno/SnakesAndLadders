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

        public GameSnakesAndLadders(int playerCount, IAnimationLogger animationLogger)
        {
            
            this.GameBoard = new Board( animationLogger);
            this.PlayerManager = new PlayerManager(playerCount, DiceSixSided.Singleton, animationLogger);
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
            IPlayer? currentPlayer;
            await PlayerManager.SetNextPlayer();
            while (true)
            {
                currentPlayer = PlayerManager.GetPlayer();
                var spaces = await currentPlayer.RollDice();
                await currentPlayer.Move(spaces);
                if ( CheckForWinner(currentPlayer))
                {
                    break;
                }
                await PlayerManager.SetNextPlayer();

            }
            IsEndGame = false;
            await currentPlayer.Gloat();
           

        }
        public  IPlayer? GetWinner()
        {
            return IsEndGame ? PlayerManager.GetPlayer() : null;
        }

        protected  bool CheckForWinner(IPlayer currentPlayer)
        {
            return  this.GameBoard.IsTokenInLastPosition(currentPlayer.PlayerToken.Position);
        }


    }
}