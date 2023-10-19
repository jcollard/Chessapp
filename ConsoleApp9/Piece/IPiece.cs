using Chessapp;

namespace Chess;
public interface IPiece
{
    /// <summary>
    /// Given a position on the board, returns a list of possible moves
    /// that can be made by this IPiece.
    /// </summary>
    /// <param name="chessBoardController"></param>
    public List<(int, int)>? GetMoves(ChessBoardController chessBoardController);

    /// <summary>
    /// Given a starting position and a target position, returns true if the
    /// piece selected can perform such a move and false otherwise.
    /// </summary>
    public bool AllowableMove((int row, int col) targetPos, ChessBoardController chessBoardController);

    /// <summary>
    /// The 2 character string representing this IPiece on the board
    /// </summary>
    public string Symbol { get; }

    /// <summary>
    /// The color of this IPiece
    /// </summary>
    public PieceColor Color { get; }

    /// <summary>
    /// Given a target position, attempts to move this piece
    /// on the board. If successful, returns true and false otherwise.
    /// </summary>
    public bool AssignPositionAndMoved((int row, int col) target);

    /// <summary>
    /// The position of this IPiece on the board.
    /// </summary>
    public (int row, int col) Position { get; set; }

    bool IsPieceCaptured { get; set; }
    void CapturePiece();
}