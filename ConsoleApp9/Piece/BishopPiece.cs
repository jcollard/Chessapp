namespace Chess;

public class BishopPiece : IPiece
{
    public bool IsCaptured { get; set; } = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; private set; }
    public PieceColor Color { get; private set; }
    public (int row, int col) Position { get; private set; }
    protected readonly GameState _gameState;

    public BishopPiece(
        string symbol, 
        PieceColor color, 
        (int, int) position, 
        GameState gameState)
    {
        
        this.Symbol = symbol;
        this.Color = color;
        this.Position = position;
        this._gameState = gameState;
        this._gameState.SetPiece(position, this);
    }

    /// <summary>
    /// Given a target position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected bool SubLogic((int row, int col) target)
    {
        if (!Utils.IsDiagonal(this.Position, target))
        {
            return false;
        }
        return this._gameState.IsPathClear(this.Position, target);
    }
    
    /// <inheritdoc/>
    public bool Move((int row, int col) target)
    {
        if (this.Logic(target))
        {
            IPiece? other = this._gameState.GetPiece(target);
            if (other != null)
            {
                other.IsCaptured = true;
            }
            this._gameState.ClearPiece(this.Position);
            this._gameState.SetPiece(target, this);
            this.Position = target;
            HasMoved = true;
            this._gameState.AddMove(this, target);
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
                if (this.Logic((row, col)))
                {
                    moves.Add((row, col));
                }
            }
        }
        return moves;
    }

    /// <inheritdoc/>
    public bool Logic((int row, int col) target)
    {
        // Pieces cannot move onto themselves
        if (this.Position == target)
        {
            return false;
        }
        // Cannot capture pieces of the same color
        if(!this._gameState.IsEmpty(target) && !this.IsEnemyPiece(this._gameState.GetPiece(target)!))
        {
            return false;
        }
        return SubLogic(target);
    }
    
    /// <inheritdoc/>
    private bool IsEnemyPiece(IPiece other) => other.Color != this.Color;

}