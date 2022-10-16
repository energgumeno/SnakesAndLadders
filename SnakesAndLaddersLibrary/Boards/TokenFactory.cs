using SnakesAndLaddersLibrary.AnimationMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersLibrary.Boards
{
    public class TokenFactory : ITokenFactory
    {


        public IToken CreateToken(int playerId, IBoard board, IAnimationLogger animationLogger)
        {
            return new Token(playerId, board, animationLogger);
        }
    }
}
