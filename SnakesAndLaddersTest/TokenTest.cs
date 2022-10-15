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


     */


    /*
        US 1 - Token Can Move Across the Board
        As a player
        I want to be able to move my token
        So that I can get closer to the goal
    */
    [TestFixture]
    public class TokenTest
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
              boardMock.Setup(s => s.CanMoveTokenToNextPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
              boardMock.Setup(s => s.GetNextTokenPosition(It.IsAny<int>(), It.IsAny<int>())).Returns<int,int>((startpos,spaces)=> (startpos+ spaces));
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
            IToken token = new Token(0, GameBoard, AnimationLogger);
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
            IToken token = new Token(0, GameBoard, AnimationLogger);
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
            IToken token = new Token(0, GameBoard, AnimationLogger);
            token.Move(3);
            token.Move(4);
            Assert.AreEqual(8, token.Position);
        }








    }
}