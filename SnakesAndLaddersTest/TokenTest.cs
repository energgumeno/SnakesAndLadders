using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;

namespace SnakesAndLaddersTest;

/*
    US 1 - Token Can Move Across the Board
    As a player
    I want to be able to move my token
    So that I can get closer to the goal
*/
[TestFixture]
public class TokenTest
{
    [SetUp]
    public void Setup()
    {
        var animationMock = new Mock<IAnimationLogger?>();
        animationMock.Setup(animationLogger => animationLogger!.AnimationMessage(It.IsAny<Message>()));
        AnimationLogger = animationMock.Object;

        var boardMock = new Mock<IBoard>();
        boardMock.Setup(s => s.StartPosition).Returns(1);
        boardMock.Setup(s => s.CanMoveTokenToNextPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        boardMock.Setup(s => s.GetNextTokenPosition(It.IsAny<int>(), It.IsAny<int>()))
            .Returns<int, int>((startPosition, spaces) => startPosition + spaces);
        GameBoard = boardMock.Object;
    }


    /*  
        UAT1
        Given the game is started
        When the token is placed on the board
        Then the token is on square 1
    */
    [Test]
    public void Token_GameStarted_ReturnsSquare1()
    {
        IToken token = new Token(0, GameBoard!, AnimationLogger);
        Assert.AreEqual(1, token.Position);
    }

    /*
        UAT2
        Given the token is on square 1
        When the token is moved 3 spaces
        Then the token is on square 4
    */

    [Test]
    public void Token_Move_ReturnsSquare4()
    {
        IToken token = new Token(0, GameBoard!, AnimationLogger);
        token.Move(3);
        Assert.AreEqual(4, token.Position);
    }

    /*
        UAT3
        Given the token is on square 1
        When the token is moved 3 spaces
        And then it is moved 4 spaces
        Then the token is on square 8
    */
    [Test]
    public void Token_Moves_ReturnsSquare8()
    {
        IToken token = new Token(0, GameBoard!, AnimationLogger);
        token.Move(3);
        token.Move(4);
        Assert.AreEqual(8, token.Position);
    }

    private IAnimationLogger? AnimationLogger { get; set; }
    private IBoard? GameBoard { get; set; }

}