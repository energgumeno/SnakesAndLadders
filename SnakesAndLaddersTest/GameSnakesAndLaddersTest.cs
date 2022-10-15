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



    public class GameSnakesAndLaddersTest
    {


        IToken? PlayerToken { get; set; }
        IDice? TheDice { get; set; }
        IAnimationLogger? AnimationLogger { get; set; }
        IPlayerManager? PlayerManager { get; set; }
        IBoard? Board { get; set; }
        int position = 97;

        [SetUp]
        public void Setup()
        {
            var playerTokenMock = new Mock<IToken>();
            playerTokenMock.Setup(d => d.Move(It.IsAny<int>())).Returns<int>(
                (d) =>
                {
                    position += d;
                    return Task.FromResult(true);
                });
            playerTokenMock.Setup(d => d.Position).Returns(() =>
            {
                return position;
            }
                );
            PlayerToken = playerTokenMock.Object;

            var theDiceMock = new Mock<IDice>();
            theDiceMock.Setup(d => d.Roll()).Returns(3);
            TheDice = theDiceMock.Object;

            var AnimationLoggerMock = new Mock<IAnimationLogger>();
            AnimationLogger = AnimationLoggerMock.Object;

            var PlayerManagerMock = new Mock<IPlayerManager>();
            PlayerManager = PlayerManagerMock.Object;

            var BoardMock = new Mock<IBoard>();
            Board = BoardMock.Object;
   
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
            IGame test = new GameSnakesAndLadders(1,AnimationLogger,PlayerManager,Board);

            await test.StartGame();
            await test.();



        }




        /* 
            UAT2
            Given the token is on square 97
            When the token is moved 4 spaces
            Then the token is on square 97
            And the player has not won the game
        */
        [Test]
        public async Task GameSnakesAndLadders_square97moved4_ReturnsNoMove()
        {
            IGame test = new GameSnakesAndLadders(1, AnimationLogger, PlayerManager, Board);

            await test.StartGame();
            await test.Play();


        }


    }
}