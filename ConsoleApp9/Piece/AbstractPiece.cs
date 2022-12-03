using Chess;

namespace Chessapp.Piece;

public abstract class AbstractPiece : IPiece
{
    // I suspect the below should be a value object


    protected readonly PieceAttributes PieceAttributes;
    public bool IsPieceCaptured => PieceAttributes.IsCaptured; // this is actually public

    protected AbstractPiece(string symbol, PieceColor color, (int, int) position)
    {
        PieceAttributes = new PieceAttributes(
            false, 
            false, 
            symbol, 
            color, 
            position);
    }

    protected abstract bool SubLogic((int row, int col) targetPos, ChessBoard chessBoard);

    public PieceColor Color => PieceAttributes.Color;

    public (int row, int col) Position => PieceAttributes.Position;

    public string Symbol => PieceAttributes.Symbol;

    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != PieceAttributes.Color;

    /// <inheritdoc/>
    public bool AssignPositionAndMoved((int row, int col) target)
    {
        PieceAttributes.Position = target;
        PieceAttributes.HasMoved = true;
        return true;
    }

    /// <param name="chessBoard"></param>
    /// <inheritdoc/>
    public List<(int, int)>? GetMoves(ChessBoard chessBoard)
    {
        List<(int, int)>? moves = new ();
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
        if (PieceAttributes.Position == target)
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

    public void CapturePiece()
    {
        PieceAttributes.IsCaptured = true;
    }
}