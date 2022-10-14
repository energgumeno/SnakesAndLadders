

using SnakesAndLaddersLibrary;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Games;

GameSnakesAndLadders game = new(2, new AnimationLogger());
await game.StartGame();
await game.Play();
Console.ReadLine();