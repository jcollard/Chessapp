namespace Chess;

public abstract class AbstractPiece : IPiece
{

    private bool _isCaptured = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; private set; }
    public PieceColor Color { get; private set; }
    public (int row, int col) Position { get; private set; }
    protected readonly ChessBoard ChessBoard;

    public AbstractPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard)
    {
        Symbol = symbol;
        Color = color;
        Position = position;
        ChessBoard = chessBoard;
        ChessBoard.SetPiece(position, this);
    }

    /// <inheritdoc/>
    public bool Move(IPiece heroPiece, (int row, int col) target)
    {
        if (!heroPiece.AllowableMove(target)) return false;
        IPiece? enemyPiece = ChessBoard.GetPiece(target);
        ChessBoard.MovePieceOnBoard(heroPiece, target, enemyPiece);
        Position = target;
        HasMoved = true;
        return true;
    }

    /// <inheritdoc/>
    public List<(int, int)> GetMoves()
    {
        List<(int, int)> moves = new ();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (AllowableMove((row, col)))
                {
                    moves.Add((row, col));
                }
            }
        }
        return moves;
    }

    /// <inheritdoc/>
    public bool AllowableMove((int row, int col) target)
    {
        // Pieces cannot move onto themselves
        if (Position == target)
        {
            return false;
        }
        // Cannot capture pieces of the same color
        if(!ChessBoard.IsEmpty(target) && !IsEnemyPiece(ChessBoard.GetPiece(target)!))
        {
            return false;
        }
        return SubLogic(target);
    }
    
    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != Color;

    /// <summary>
    /// Given a target position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected abstract bool SubLogic((int row, int col) targetPos);

    public bool IsPieceCaptured()
    {
        return _isCaptured;
    }

    public void CapturePiece()
    {
        _isCaptured = true;
    }
}