namespace Chessapp.Piece;

public abstract class AbstractPiece : IPiece
{
    public bool IsCaptured { get; set; } = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; }
    public PieceColor Color { get; }
    public (int row, int col) Position { get; private set; }
    protected readonly GameState _gameState;

    protected AbstractPiece(string symbol, PieceColor color, (int, int) position, GameState gameState)
    {
        this.Symbol = symbol;
        this.Color = color;
        this.Position = position;
        this._gameState = gameState;
        this._gameState.SetPiece(position, this);
    }

    /// <inheritdoc/>
    public bool Move((int row, int col) targetPos)
    {
        if (!this.CheckMove(targetPos))
        {
            return false;
        }

        IPiece? other = this._gameState.GetPiece(targetPos);
        if (other != null)
        {
            other.IsCaptured = true;
        }

        this._gameState.ClearPiece(this.Position);
        this._gameState.SetPiece(targetPos, this);
        this.Position = targetPos;
        HasMoved = true;
        this._gameState.AddMove(this, targetPos);

        return true;
    }

    /// <inheritdoc/>
    public IList<(int, int)> GetMoves()
    {
        List<(int, int)> moves = new ();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (this.CheckMove((row, col)))
                {
                    moves.Add((row, col));
                }
            }
        }
        return moves;
    }

    /// <inheritdoc/>
    public bool CheckMove((int row, int col) targetPos)
    {
        // Pieces cannot move onto themselves
        if (this.Position == targetPos)
        {
            return false;
        }

        // Cannot capture pieces of the same color
        if(!this._gameState.IsEmpty(targetPos) && !this.IsEnemyPiece(this._gameState.GetPiece(targetPos)!))
        {
            return false;
        }

        return CheckPieceSpecificMove(targetPos);
    }
    
    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != this.Color;

    /// <summary>
    /// Given a targetPos position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected abstract bool CheckPieceSpecificMove((int row, int col) targetPos);
}