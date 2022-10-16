

using SnakesAndLaddersLibrary;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Games;
using SnakesAndLaddersLibrary.Players;

int playerCount = 2;

IAnimationLogger logger = new AnimationLogger();
ITileFactory tileFactory = new TileFactory();
ITokenFactory tokenFactory = new TokenFactory();


GameSnakesAndLadders game = new(playerCount, 
    logger,
    new PlayerManager(playerCount, DiceSixSided.Singleton, logger), 
    new Board(logger,tokenFactory,tileFactory)
    );
await game.StartGame();
await game.Play();
Console.ReadLine();