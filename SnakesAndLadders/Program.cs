

using SnakesAndLaddersLibrary;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Games;
using SnakesAndLaddersLibrary.Players;

IAnimationLogger logger = new AnimationLogger();
int playerCount = 2;
GameSnakesAndLadders game = new(playerCount, 
    logger,
    new PlayerManager(playerCount, DiceSixSided.Singleton, logger), 
    new Board(logger)
    );
await game.StartGame();
await game.Play();
Console.ReadLine();