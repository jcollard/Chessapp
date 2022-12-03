using Chess;

namespace Chessapp.Piece;

public abstract class AbstractPiece : IPiece
{

    // I suspect the below should be a value object
    private bool _isCaptured; // this is actually public
    protected PieceAttributes _pieceAttributes;

    public AbstractPiece(string symbol, PieceColor color, (int, int) position)
    {
        _pieceAttributes = new PieceAttributes(
            false, 
            false, 
            symbol, 
            color, 
            position);
    }

    public PieceColor Color => _pieceAttributes.Color;

    /// <inheritdoc/>
    public bool AssignPositionAndMoved((int row, int col) target)
    {
        _pieceAttributes.Position = target;
        _pieceAttributes.HasMoved = true;
        return true;
    }

    public (int row, int col) Position => _pieceAttributes.Position;

    /// <param name="chessBoard"></param>
    /// <inheritdoc/>
    public List<(int, int)> GetMoves(ChessBoard chessBoard)
    {
        List<(int, int)> moves = new ();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (AllowableMove((row, col), chessBoard))
                {
                    moves.Add((row, col));
                }
            }
        }
        return moves;
    }

    /// <inheritdoc/>
    public bool AllowableMove((int row, int col) target, ChessBoard chessBoard)
    {
        // Pieces cannot move onto themselves
        if (_pieceAttributes.Position == target)
        {
            return false;
        }
        // Cannot capture pieces of the same color
        if(!chessBoard.IsEmpty(target) && !IsEnemyPiece(chessBoard.GetPiece(target)!))
        {
            return false;
        }
        return SubLogic(target, chessBoard);
    }

    public string Symbol => _pieceAttributes.Symbol;

    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != _pieceAttributes.Color;

    /// <summary>
    /// Given a target position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected abstract bool SubLogic((int row, int col) targetPos, ChessBoard chessBoard);

    public bool IsPieceCaptured()
    {
        return _isCaptured;
    }

    public void CapturePiece()
    {
        _isCaptured = true;
    }
}