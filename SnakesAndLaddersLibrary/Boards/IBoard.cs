namespace SnakesAndLaddersLibrary.Boards;

public interface IBoard
{
    int StartPosition { get; }

    bool CanMoveTokenToNextPosition(int oldPosition, int spaces);
    Task FillTiles();
    int GetNextTokenPosition(int oldPosition, int spaces);
    bool IsTokenInLastPosition(int position);
    IToken? CreateToken(int playerId);
}