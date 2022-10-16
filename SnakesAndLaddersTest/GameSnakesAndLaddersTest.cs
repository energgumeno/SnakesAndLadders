using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Games;
using SnakesAndLaddersLibrary.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersTest
{



    /*      
    US 2 - Player Can Win the Game
    As a player
    I want to be able to win the game
    So that I can gloat to everyone around
    */


    [TestFixture]
    public class GameSnakesAndLaddersTest
    {



        IDice? TheDice { get; set; }
        IAnimationLogger? AnimationLogger { get; set; }
        IBoard? Board { get; set; }



        [SetUp]
        public void Setup()
        {

            var AnimationLoggerMock = new Mock<IAnimationLogger>();
            AnimationLogger = AnimationLoggerMock.Object;

            var theDiceMock = new Mock<IDice>();
            theDiceMock.Setup(d => d.Roll()).Returns(3);
            TheDice = theDiceMock.Object;

            var BoardMock = new Mock<IBoard>();
            BoardMock.Setup(d => d.IsTokenInLastPosition(It.IsAny<int>())).Returns<int>(position => position == 100);
            Board = BoardMock.Object;
        }

        private (IPlayerManager, IPlayer) CreatePlayerTest(int rollResult)
        {
            int position = 97;
            IToken PlayerToken;
            IPlayer Player;
            IPlayerManager PlayerManager;


            var playerTokenMock = new Mock<IToken>();
            playerTokenMock.Setup(d => d.Move(It.IsAny<int>())).Returns<int>(
                (rollResult) =>
                {
                    if ((position + rollResult) <= 100)
                    {
                        position += rollResult;
                    }
                    return Task.FromResult(true);
                });
            playerTokenMock.Setup(d => d.Position).Returns(() =>
            {
                return position;
            }
                );

            PlayerToken = playerTokenMock.Object;
            var PlayerMock = new Mock<IPlayer>();
            PlayerMock.Setup(d => d.PlayerToken).Returns(PlayerToken);
            PlayerMock.Setup(d => d.RollDice()).Returns(Task.FromResult(rollResult));
            PlayerMock.Setup(d => d.Move(
                It.IsAny<int>())).Returns<int>(roll =>
                {
                    position += roll;
                    return Task.FromResult(true);
                });
            Player = PlayerMock.Object;
            var PlayerManagerMock = new Mock<IPlayerManager>();
            PlayerManagerMock.Setup(d => d.GetPlayer()).Returns(Player);
            PlayerManager = PlayerManagerMock.Object;
            return (PlayerManager, Player);
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
            (IPlayerManager playerManager, IPlayer player) = CreatePlayerTest(3);


#pragma warning disable CS8604 // Possible null reference argument.
            IGame test = new GameSnakesAndLadders(1, AnimationLogger, playerManager, Board);
#pragma warning restore CS8604 // Possible null reference argument.

            await test.StartGame();
            await test.PlayOnemove();

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

            (IPlayerManager playerManager, IPlayer player) = CreatePlayerTest(4);


#pragma warning disable CS8604 // Possible null reference argument.
            IGame test = new GameSnakesAndLadders(1, AnimationLogger, playerManager, Board);
#pragma warning restore CS8604 // Possible null reference argument.

            await test.StartGame();
            await test.PlayOnemove();

            Assert.IsFalse(test.CheckForWinner());
            Assert.AreEqual(null, test.GetWinner());

        }








    }
}