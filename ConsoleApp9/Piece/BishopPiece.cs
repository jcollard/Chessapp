namespace Chess;

public class BishopPiece : IPiece
{
    private bool IsCaptured { get; set; } = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; private set; }
    public PieceColor Color { get; private set; }
    public (int row, int col) Position { get; private set; }
    private readonly ChessBoard _chessBoard;

    public BishopPiece(
        string symbol, 
        PieceColor color, 
        (int, int) position, 
        ChessBoard chessBoard)
    {
        
        Symbol = symbol;
        Color = color;
        Position = position;
        _chessBoard = chessBoard;
        _chessBoard.SetPiece(position, this);
    }

    /// <summary>
    /// Given a target position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected bool SubLogic((int row, int col) target)
    {
        if (!Utils.IsDiagonal(Position, target))
        {
            return false;
        }
        return _chessBoard.IsPathClear(Position, target);
    }
    
    /// <inheritdoc/>
    public bool Move(IPiece heroPiece, (int row, int col) target)
    {
        if (AllowableMove(target))
        {
            IPiece? other = _chessBoard.GetPiece(target);
            if (other != null)
            {
                other.CapturePiece(true);
            }
            _chessBoard.ClearPiece(Position);
            _chessBoard.SetPiece(target, this);
            Position = target;
            HasMoved = true;
            _chessBoard.AddMove(this, target);
            return true;
        }
        return false;
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
        if(!_chessBoard.IsEmpty(target) && !IsEnemyPiece(_chessBoard.GetPiece(target)!))
        {
            return false;
        }
        return SubLogic(target);
    }
    
    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != Color;

    public bool IsPieceCaptured()
    {
        return IsCaptured;
    }

    public void CapturePiece(bool isOnBoard)
    {
        IsCaptured = isOnBoard;
    }
}