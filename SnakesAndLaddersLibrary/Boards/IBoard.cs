namespace SnakesAndLaddersLibrary.Boards;

public interface IBoard
{
    int StartPosition { get; }

    bool CanMoveTokenToNextPosition(int OldPosition, int spaces);
    Task FillTiles();
    int GetNextTokenPosition(int OldPosition, int spaces);
    bool IsTokenInLastPosition(int Position);
    IToken CreateToken(int playerId);
}