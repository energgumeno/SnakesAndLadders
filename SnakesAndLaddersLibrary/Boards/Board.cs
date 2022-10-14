using SnakesAndLaddersLibrary.AnimationMessage;

namespace SnakesAndLaddersLibrary.Boards
{
    public class Board : IBoard
    {

        public int StartPosition { get; }

        protected const int MaxTiles = 100;
       
        protected IAnimationLogger AnimationLogger { get; set; }

        protected ITile[] Tiles { get; set; }


        public Board(IAnimationLogger animationLogger)
        {
            this.StartPosition = 1;
            this.Tiles = new ITile[MaxTiles];
            this.AnimationLogger = animationLogger;
        }

        public async Task FillTiles()
        {
            for (int tilePosition = 1; tilePosition <= MaxTiles; tilePosition++)
            {
                Tiles[tilePosition-1]=new Tile(tilePosition);
                await FillTilesAnimation(tilePosition);
            }

        }

        public int GetNextTokenPosition(int OldPosition, int spaces)
        {
            if (CanMoveTokenToNextPosition(OldPosition, spaces))
            {
                return Tiles.ElementAt(OldPosition + spaces - 1).GetNextPosition();
            }
            return OldPosition;
        }

     

        public bool CanMoveTokenToNextPosition(int OldPosition, int spaces)
        {
            return (OldPosition + spaces) <= MaxTiles;
        }

        public bool IsTokenInLastPosition(int Position)
        {
            return Position == MaxTiles;
        }

        #region Animations
        private async Task FillTilesAnimation(int tilePosition)
        {
            await AnimationLogger.AnimationMessage(new Message
            {
                Sender = nameof(IBoard).ToString(),
                Animation = nameof(FillTiles).ToString(),
                Values = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("TilePosition",tilePosition.ToString())
                }
            });
        }
        #endregion

    }
}
