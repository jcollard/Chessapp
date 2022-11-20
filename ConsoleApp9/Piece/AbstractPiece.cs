namespace Chess;

public abstract class AbstractPiece : IPiece
{

    public bool IsCaptured { get; set; } = false;
    private bool _hasMoved = false;
    public bool HasMoved => _hasMoved;
    private readonly string _symbol;
    public string Symbol => _symbol;
    private readonly PieceColor _color;
    public PieceColor Color => _color;
    private (int, int) _position;
    public (int row, int col) Position => _position;
    protected readonly GameState _gameState;

    public AbstractPiece(string symbol, PieceColor color, (int, int) position, GameState gameState)
    {
        this._symbol = symbol;
        this._color = color;
        this._position = position;
        this._gameState = gameState;
        this._gameState.SetPiece(position, this);
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
            this._position = target;
            _hasMoved = true;
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

    /// <summary>
    /// Given a target position, checks the piece specific logic for moving this 
    /// piece to that position on the board. If the piece can move there,
    /// returns true and otherwise returns false.
    /// </summary>
    protected abstract bool SubLogic((int row, int col) targetPos);
}