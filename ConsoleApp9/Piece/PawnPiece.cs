namespace Chess;
public class PawnPiece : IPiece
{

    public bool IsCaptured { get; set; } = false;
    public bool HasMoved { get; private set; }
    public string Symbol { get; private set; }
    public PieceColor Color { get; private set; }
    public (int row, int col) Position { get; private set; }
    protected readonly GameState _gameState;
    public PawnPiece(string symbol, PieceColor color, (int, int) position, GameState gameState)
    {
        this.Symbol = symbol;
        this.Color = color;
        this.Position = position;
        this._gameState = gameState;
        this._gameState.SetPiece(position, this);
    }

    protected bool SubLogic((int row, int col) target)
    {
        int rowInc = this.Color == PieceColor.Blue ? 1 : -1;
        int targetRow = this.Position.row + rowInc;
        int firstTurnTargetRow = this.Position.row + 2*rowInc;
        int[] targetCols = {this.Position.col - 1, this.Position.col + 1};
        bool isAttack = this.Position.col - target.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (target.row == targetRow && !isAttack && this._gameState.IsEmpty(target))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!this.HasMoved && 
            target.row == firstTurnTargetRow && 
            !isAttack && this._gameState.IsEmpty(target) && 
            this._gameState.IsPathClear(this.Position, target))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (target.row == targetRow && targetCols.Contains(target.col) && !this._gameState.IsEmpty(target))
        {
            return true;
        }

        // TODO: En passant move

        return false;
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