using Chess;

namespace Chessapp.Piece;

public abstract class AbstractPiece : IPiece
{

    protected readonly PieceAttributes PieceAttributes;

    public bool IsPieceCaptured
    {
        get => PieceAttributes.IsCaptured;
        set => PieceAttributes.IsCaptured = value;
    }

    public (int row, int col) Position
    {
        get => PieceAttributes.Position;
        set => PieceAttributes.Position = value;
    }

    public PieceColor Color => PieceAttributes.Color;
    public string Symbol => PieceAttributes.Symbol;


    protected AbstractPiece(string symbol, PieceColor color, (int, int) position)
    {
        PieceAttributes = new PieceAttributes(
            false, 
            false, 
            symbol, 
            color, 
            position);
    }

    protected abstract bool SubLogic((int row, int col) targetPos, List<IPiece?> chessBoardPieces);

    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != PieceAttributes.Color;

    /// <inheritdoc/>
    public bool AssignPositionAndMoved((int row, int col) target)
    {
        PieceAttributes.Position = target;
        PieceAttributes.HasMoved = true;
        return true;
    }

    /// <param name="chessBoardController"></param>
    /// <inheritdoc/>
    public List<(int, int)>? GetMoves(ChessBoardController chessBoardController)
    {
        List<(int, int)>? moves = new ();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (AllowableMove((row, col), chessBoardController))
                {
                    moves.Add((row, col));
                }
            }
        }
        return moves;
    }

    /// <inheritdoc/>
    public bool AllowableMove((int row, int col) target, ChessBoardController chessBoardController)
    {
        // Pieces cannot move onto themselves
        if (PieceAttributes.Position == target)
        {
            return false;
        }
        // Cannot capture pieces of the same color
        if(!chessBoardController.IsEmpty(target) && 
           chessBoardController.RetrievePieceFrom(target) != null && 
           !IsEnemyPiece(chessBoardController.RetrievePieceFrom(target)))
        {
            return false;
        }
        return SubLogic(target, chessBoardController.RetrieveAllPieces());
    }

    public void CapturePiece()
    {
        PieceAttributes.IsCaptured = true;
    }
}