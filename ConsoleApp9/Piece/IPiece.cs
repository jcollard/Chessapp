namespace Chess;
public interface IPiece 
{
    /// <summary>
    /// Given a position on the board, returns a list of possible moves
    /// that can be made by this IPiece.
    /// </summary>
    public List<(int, int)> GetMoves((int row, int col) pos, GameState gameState);

    /// <summary>
    /// Given a starting position and a target position, returns true if the
    /// piece selected can perform such a move and false otherwise.
    /// </summary>
    public bool Logic((int row, int col) startPos, (int row, int col) targetPos, GameState gameState);

    /// <summary>
    /// The 2 character string representing this IPiece on the board
    /// </summary>
    public string Symbol { get; }

    /// <summary>
    /// The color of this IPiece
    /// </summary>
    public PieceColor Color { get; }

    /// <summary>
    /// The position of this IPiece on the board.
    /// </summary>
    public (int, int) Position { get; }
}