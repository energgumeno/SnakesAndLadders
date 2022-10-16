using NUnit.Framework;
using SnakesAndLaddersLibrary.Dices;

namespace SnakesAndLaddersTest;

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
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)]
    public void DiceToken_GameStarted_ResultBetween1and6(int numberTry)
    {
        IDice dice = DiceSixSided.Singleton;
        var diceRoll = dice.Roll();
        Assert.GreaterOrEqual(diceRoll, 1);
        Assert.LessOrEqual(diceRoll, 6);
    }
}