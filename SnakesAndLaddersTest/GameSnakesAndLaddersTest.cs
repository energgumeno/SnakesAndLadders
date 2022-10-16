using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Games;
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersTest;

/*      
US 2 - Player Can Win the Game
As a player
I want to be able to win the game
So that I can gloat to everyone around
*/

[TestFixture]
public class GameSnakesAndLaddersTest
{
    [SetUp]
    public void Setup()
    {
        var animationLoggerMock = new Mock<IAnimationLogger>();
        AnimationLogger = animationLoggerMock.Object;

        var theDiceMock = new Mock<IDice>();
        theDiceMock.Setup(d => d.Roll()).Returns(3);
        TheDice = theDiceMock.Object;

        var boardMock = new Mock<IBoard>();
        boardMock.Setup(d => d.IsTokenInLastPosition(It.IsAny<int>())).Returns<int>(position => position == 100);
        Board = boardMock.Object;
    }


    private IDice? TheDice { get; set; }
    private IAnimationLogger? AnimationLogger { get; set; }
    private IBoard? Board { get; set; }

    private (IPlayerManager, IPlayer) CreatePlayerTest(int rollResult)
    {
        var position = 97;

        var playerTokenMock = new Mock<IToken>();
        playerTokenMock.Setup(d => d.Move(It.IsAny<int>())).Returns<int>(
            result =>
            {
                if (position + result <= 100) position += result;
                return Task.FromResult(true);
            });
        playerTokenMock.Setup(d => d.Position).Returns(() => { return position; }
        );

        var playerToken = playerTokenMock.Object;
        var playerMock = new Mock<IPlayer>();
        playerMock.Setup(d => d.PlayerToken).Returns(playerToken);
        playerMock.Setup(d => d.RollDice()).Returns(Task.FromResult(rollResult));
        playerMock.Setup(d => d.Move(
            It.IsAny<int>())).Returns<int>(roll =>
        {
            position += roll;
            return Task.FromResult(true);
        });
        var player = playerMock.Object;
        var playerManagerMock = new Mock<IPlayerManager>();
        playerManagerMock.Setup(d => d.GetPlayer()).Returns(player);
        var playerManager = playerManagerMock.Object;
        return (playerManager, player);
    }


    /* 
        UAT1

        Given the token is on square 97
        When the token is moved 3 spaces
        Then the token is on square 100
        And the player has won the game
     */

    [Test]
    public async Task GameSnakesAndLadders_square97moved3_ReturnsWin()
    {
        var (playerManager, player) = CreatePlayerTest(3);



        IGame test = new GameSnakesAndLadders(  playerManager, Board!);


        await test.StartGame();
        await test.PlayOneMove();

        Assert.IsTrue(test.CheckForWinner());
        Assert.IsTrue(player == test.GetWinner());
    }

    /* 
         UAT2
         Given the token is on square 97
         When the token is moved 4 spaces
         Then the token is on square 97
         And the player has not won the game
     */
    [Test]
    public async Task GameSnakesAndLadders_square97moved3_ReturnsNoWin()
    {
        var (playerManager, player) = CreatePlayerTest(4);



        IGame test = new GameSnakesAndLadders(playerManager, Board!);


        await test.StartGame();
        await test.PlayOneMove();

        Assert.IsFalse(test.CheckForWinner());
        Assert.AreEqual(null, test.GetWinner());
    }
}