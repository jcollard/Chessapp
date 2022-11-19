namespace Chess;

public abstract class AbstractPiece : IPiece
{
    private readonly string _symbol;
    public string Symbol => _symbol;

    private readonly PieceColor _color;
    public PieceColor Color => _color;

    private (int, int) _position;
    public (int, int) Position => _position;

    public AbstractPiece(string symbol, PieceColor color, (int, int) position)
    {
        this._symbol = symbol;
        this._color = color;
        this._position = position;
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
    protected bool IsEnemyPiece(IPiece other) => other.Color != this.Color;
    protected bool IsEnemyPiece((int row, int col) target, GameState gameState)
    {
        string targetSymbol = Program.changes.BoardLayout[target.row, target.col];
        return gameState.TryGetPiece(targetSymbol, out IPiece? other) && this.IsEnemyPiece(other);
    }
    
    public abstract bool Logic((int row, int col) startPos, (int row, int col) targetPos, GameState gameState);
}