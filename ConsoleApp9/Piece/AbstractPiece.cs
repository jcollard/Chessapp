namespace Chess;

public abstract class AbstractPiece : IPiece
{

    public bool IsCaptured { get; set; } = false;
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

    public bool Move((int row, int col) target)
    {
        if (this.Logic(this.Position, target, this._gameState))
        {
            IPiece? other = this._gameState.GetPiece(target);
            if (other != null)
            {
                other.IsCaptured = true;
                Program.changes.deadpieces.Add(other.Symbol);
            }
            Program.changes.BoardLayout[this.Position.row, this.Position.col] = "  ";
            this._gameState.ClearPiece(this.Position);
            this._gameState.SetPiece(target, this);
            this._position = target;
            Program.changes.BoardLayout[target.row, target.col] = this.Symbol;
            return true;
        }
        return false;
    }

    public List<(int, int)> GetMoves((int row, int col) pos, GameState gameState)
    {
        List<(int, int)> moves = new ();
        foreach (string i in Program.BoardLayout)
        {
            int[] arr = Program.indextile(i);
            (int row, int col) target = (arr[0], arr[1]);
            if (this.Logic(pos, target, gameState))
            {
                Program.movecheck.BoardLayout[target.row, target.col] = "XX";
                moves.Add(target);
            }
            else
            {
                Program.movecheck.BoardLayout[target.row, target.col] = Program.changes.BoardLayout[target.row, target.col];
            }
        }

        Utils.TryClear();
        Program.movecheck.Print();
        Thread.Sleep(Program.DELAY);
        Utils.TryClear();
        Program.changes.Print();
        return moves;
    }

    public bool Logic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        if(!GameState.IsEmpty(target) && !this.IsEnemyPiece(target, gameState))
        {
            return false;
        }
        return SubLogic(start, target, gameState);
    }
    
    private bool IsEnemyPiece(IPiece other) => other.Color != this.Color;
    private bool IsEnemyPiece((int row, int col) target, GameState gameState)
    {
        string targetSymbol = Program.changes.BoardLayout[target.row, target.col];
        return gameState.TryGetPiece(targetSymbol, out IPiece? other) && this.IsEnemyPiece(other);
    }

    protected abstract bool SubLogic((int row, int col) startPos, (int row, int col) targetPos, GameState gameState);
}