using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Players;

namespace SnakesAndLaddersTest;

[TestFixture]
public class PlayerTest
{
    [SetUp]
    public void Setup()
    {
        _position = 1;

        var playerTokenMock = new Mock<IToken?>();
        playerTokenMock.Setup(token => token!.Move(It.IsAny<int>())).Returns<int>(
            d =>
            {
                _position += d;
                return Task.FromResult(true);
            });
        playerTokenMock.Setup(token => token!.Position).Returns(() => { return _position; });
        PlayerToken = playerTokenMock.Object;

        var theDiceMock = new Mock<IDice?>();
        theDiceMock.Setup(dice => dice!.Roll()).Returns(4);
        TheDice = theDiceMock.Object;

        var animationLoggerMock = new Mock<IAnimationLogger?>();
        
        AnimationLogger = animationLoggerMock.Object;
    }

    private int _position = 1;
    /*
        UAT2
        Given the player rolls a 4
        When they move their token
        Then the token should move 4 spaces
    */

    [Test]
    public async Task Player_Rolls4_ReturnsMoves4()
    {
        IPlayer player = new Player(1, PlayerToken, TheDice, AnimationLogger);


            var oldPosition = player.PlayerToken!.Position;
            var spaces = await player.RollDice();
            await player.Move(spaces);
            Assert.AreEqual(spaces, player.PlayerToken.Position - oldPosition);
      
    }

    private IToken? PlayerToken { get; set; }
    private IDice? TheDice { get; set; }
    private IAnimationLogger? AnimationLogger { get; set; }

}