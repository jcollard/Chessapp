namespace Chessapp.Piece;
public interface IPiece 
{
    /// <summary>
    /// The 2 character string representing this IPiece on the board
    /// </summary>
    public string Symbol { get; }

    /// <summary>
    /// The color of this IPiece
    /// </summary>
    public PieceColor Color { get; }

    /// <summary>
    /// true if this piece has been captured and false otherwise
    /// </summary>
    public bool IsCaptured { get; set; }

    /// <summary>
    /// Given a position on the board, returns a list of possible moves
    /// that can be made by this IPiece.
    /// </summary>
    public IList<(int, int)> GetMoves();

    /// <summary>
    /// Returns true if the piece can perform the move. Otherwise false.
    /// </summary>
    public bool CheckMove((int row, int col) targetPos);

    /// <summary>
    /// Given a targetPos position, attempts to move this piece
    /// on the board. If successful, returns true and false otherwise.
    /// </summary>
    public bool Move((int row, int col) targetPos);
}