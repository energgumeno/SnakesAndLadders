﻿using NUnit.Framework;
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
        [SetUp]
        public void Setup()
        {

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
            Assert.AreEqual(spaces, player.PlayerToken.Position- oldPosition);
            }


        }
}
