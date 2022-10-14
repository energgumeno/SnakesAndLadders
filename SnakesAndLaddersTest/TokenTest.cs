using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using System.Threading.Tasks;

namespace SnakesAndLaddersTest
{


    /*      
            US 2 - Player Can Win the Game
            As a player
            I want to be able to win the game
            So that I can gloat to everyone around
            UAT1

            Given the token is on square 97
            When the token is moved 3 spaces
            Then the token is on square 100
            And the player has won the game
            UAT2

            Given the token is on square 97
            When the token is moved 4 spaces
            Then the token is on square 97
            And the player has not won the game
            US 3 - Moves Are Determined By Dice Rolls
            As a player
            I want to move my token based on the roll of a die
            So that there is an element of chance in the game
            UAT1

            Given the game is started
            When the player rolls a die
            UAT2

            Given the player rolls a 4
            When they move their token
            Then the token should move 4 spaces
     */
    public class Tests
    {
        public IAnimationLogger? AnimationLogger {get;set;}
        public IBoard? GameBoard { get; set; }
        [SetUp]
        public void Setup()
        {
            var animationMock = new Mock<IAnimationLogger>();
            animationMock.Setup(s => s.AnimationMessage(It.IsAny<Message>()));
            AnimationLogger= animationMock.Object;

            var boardMock = new Mock<IBoard>();
              boardMock.Setup(s=> s.StartPosition).Returns(1);
            GameBoard = boardMock.Object;

        }

        /*US 1 - Token Can Move Across the Board
        As a player
        I want to be able to move my token
        So that I can get closer to the goal
        UAT1

        Given the game is started
        When the token is placed on the board
        Then the token is on square 1
        UAT2

        Given the token is on square 1
        When the token is moved 3 spaces
        Then the token is on square 4
        UAT3

        Given the token is on square 1
        When the token is moved 3 spaces
        And then it is moved 4 spaces
        Then the token is on square 8*/
        [Test]
        public void Token_GameStarted_ReturnsSquare1()
        {
            IToken token = new Token(0, GameBoard.StartPosition, GameBoard, AnimationLogger);
            Assert.AreEqual(1, token.Position);
        }
    }
}