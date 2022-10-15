using Moq;
using NUnit.Framework;
using SnakesAndLaddersLibrary.AnimationMessage;
using SnakesAndLaddersLibrary.Boards;
using SnakesAndLaddersLibrary.Dices;
using SnakesAndLaddersLibrary.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersTest
{
    [TestFixture]
    public class PlayerTest
    {

        IToken PlayerToken { get; set; }
        IDice TheDice { get; set; }
        IAnimationLogger AnimationLogger { get; set; }
        int position = 1;
        [SetUp]
        public void Setup()
        {
            position = 1;

            var playerTokenMock = new Mock<IToken>();
            playerTokenMock.Setup(d => d.Move(It.IsAny<int>())).Returns<int>(
                (d) =>
                {
                    position += d;
                    return Task.FromResult(true);
                });
            playerTokenMock.Setup(d => d.Position).Returns(()=>
            {
                return position;
            }
                );

            var theDiceMock = new Mock<IDice>();
            theDiceMock.Setup(d => d.Roll()).Returns(4);

            var AnimationLoggerMock = new Mock<IAnimationLogger>();

            PlayerToken = playerTokenMock.Object;
            TheDice = theDiceMock.Object;
            AnimationLogger = AnimationLoggerMock.Object;

        }
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


    }
}
