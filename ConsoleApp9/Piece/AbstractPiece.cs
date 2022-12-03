namespace Chess;

public abstract class AbstractPiece : IPiece
{

    private bool _isCaptured = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; }
    public PieceColor Color { get; }
    public (int row, int col) Position { get; private set; }

    public AbstractPiece(string symbol, PieceColor color, (int, int) position)
    {
        Symbol = symbol;
        Color = color;
        Position = position;
        // ChessBoard.SetPiece(position, this);
    }

    /// <inheritdoc/>
    public bool AssignPositionAndMoved(IPiece heroPiece, (int row, int col) target)
    {
        Position = target;
        HasMoved = true;
        return true;
    }

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
        if (Position == target)
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
    
    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != Color;

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