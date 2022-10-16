using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersLibrary.Games;

public class GameSnakesAndLadders : IGame
{
    public GameSnakesAndLadders(int playerCount, IAnimationLogger animationLogger, IPlayerManager playerManager,
        IBoard gameBoard)
    {
        PlayerManager = playerManager;
        GameBoard = gameBoard;
        IsEndGame = false;
    }

    protected IBoard GameBoard { get; set; }
    protected IPlayerManager PlayerManager { get; set; }

    protected bool IsEndGame { get; set; }


    public async Task StartGame()
    {
        PlayerManager.CheckPlayersCount();
        await GameBoard.FillTiles();
        await PlayerManager.CreatePlayerList(GameBoard);
    }

    public async Task Play()
    {
        while (!IsEndGame) await PlayOnemove();

        await PlayerManager.GetPlayer().Gloat();
    }

    public async Task PlayOnemove()
    {
        await PlayerManager.SetNextPlayer();
        var currentPlayer = PlayerManager.GetPlayer();
        var spaces = await currentPlayer.RollDice();
        await currentPlayer.Move(spaces);
        CheckForWinner();
    }

    public IPlayer? GetWinner()
    {
        return IsEndGame ? PlayerManager.GetPlayer() : null;
    }

    public bool CheckForWinner()
    {
        return IsEndGame = GameBoard.IsTokenInLastPosition(PlayerManager.GetPlayer().PlayerToken.Position);
    }
}