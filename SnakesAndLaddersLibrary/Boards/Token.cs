using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards
{
    public class Token : IToken
    {
        public int TokenId { get; protected set; }
        public int Position { get; protected set; }
        public IBoard GameBoard { get; protected set; }
        protected IAnimationLogger AnimationLogger { get; set; }

        public Token(int tokenId,  IBoard gameBoard, IAnimationLogger animationLogger)
        {
            this.TokenId = tokenId;
            this.Position = gameBoard.StartPosition;
            this.GameBoard = gameBoard;
            this.AnimationLogger = animationLogger;
        }

        public async Task Move(int spaces)
        {
            var oldPosition = Position;
            if (GameBoard.CanMoveTokenToNextPosition(Position, spaces))
            {
                Position = GameBoard.GetNextTokenPosition(Position, spaces);
            }

            await MoveAnimation(oldPosition);
        }

        #region Animation
        private async Task MoveAnimation(int oldPosition)
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(Token).ToString(),
                Animation = nameof(Move).ToString(),
                Values = new List<KeyValuePair<string, string>>() {

                    new KeyValuePair<string, string>("TokenId",TokenId.ToString()),
                    new KeyValuePair<string, string>("OldPosition",oldPosition.ToString()),
                     new KeyValuePair<string, string>("Position",Position.ToString())

                }
            });
        }
        #endregion
    }
}
