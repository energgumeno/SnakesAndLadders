using NUnit.Framework;
using SnakesAndLaddersLibrary.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersTest
{

    /*
        US 3 - Moves Are Determined By Dice Rolls
        As a player
        I want to move my token based on the roll of a die
        So that there is an element of chance in the game
         
    */

    /*
        UAT2
        Given the player rolls a 4
        When they move their token
        Then the token should move 4 spaces
    */
    [TestFixture]
    public class DiceTest
    {
        [SetUp]
        public void Setup()
        {

        }
        /*
            UAT1
            Given the game is started
            When the player rolls a die
            Then the result should be between 1-6 inclusive 
        */
        [Test]
        public void DiceToken_GameStarted_ReturnsSquare1() 
        {
            IDice dice = DiceSixSided.Singleton;

            Assert.GreaterOrEqual(1, dice.Roll());
            Assert.LessOrEqual(6, dice.Roll());

        }



    }
