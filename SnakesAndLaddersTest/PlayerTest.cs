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
        position = 1;

        var playerTokenMock = new Mock<IToken>();
        playerTokenMock.Setup(d => d.Move(It.IsAny<int>())).Returns<int>(
            d =>
            {
                position += d;
                return Task.FromResult(true);
            });
        playerTokenMock.Setup(d => d.Position).Returns(() => { return position; });
        PlayerToken = playerTokenMock.Object;

        var theDiceMock = new Mock<IDice>();
        theDiceMock.Setup(d => d.Roll()).Returns(4);
        TheDice = theDiceMock.Object;

        var AnimationLoggerMock = new Mock<IAnimationLogger>();
        AnimationLogger = AnimationLoggerMock.Object;
    }

    private int position = 1;
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

        var oldPosition = player.PlayerToken.Position;
        var spaces = await player.RollDice();
        await player.Move(spaces);
        Assert.AreEqual(spaces, player.PlayerToken.Position - oldPosition);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected IToken PlayerToken { get; set; }
    protected IDice TheDice { get; set; }
    protected IAnimationLogger AnimationLogger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}