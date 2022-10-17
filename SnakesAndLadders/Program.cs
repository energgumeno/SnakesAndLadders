using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Games;
using SnakesAndLaddersLibrary.Players;

var playerCount = 2;

//use this to go move by move
//IAnimationLogger? logger = new DefaultAnimationLoggerWithStop();

//recomended dependency injection

IAnimationLogger? logger = new DefaultAnimationLogger();
ITileFactory tileFactory = new SnakesAndLaddersTilesFactory();
ITokenFactory tokenFactory = new TokenFactory();
IPlayerFactory playerFactory = new PlayerFactory();
IBoard board = new Board(logger, tokenFactory, tileFactory);
IPlayerManager playerManager = new PlayerManager(playerCount, DiceSixSided.Singleton, logger, playerFactory);

IGame game = new GameSnakesAndLadders(
    playerManager,
    board
);

await game.StartGame();
await game.Play();
var player = game.GetWinner();
Console.WriteLine($"Player {player?.PlayerId} Won!");
Console.ReadLine();