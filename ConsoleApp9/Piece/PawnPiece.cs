namespace Chess;
public class PawnPiece : IPiece
{
    private bool IsCaptured { get; set; }
    public bool HasMoved { get; private set; }
    public string Symbol { get; private set; }
    public PieceColor Color { get; private set; }
    private bool IsEnemyPiece(IPiece other) => other.Color != this.Color;
    public (int row, int col) Position { get; private set; }
    private readonly ChessBoard ChessBoard;

    public PawnPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard)
    {
        this.Symbol = symbol;
        IsCaptured = false;
        this.Color = color;
        this.Position = position;
        this.ChessBoard = chessBoard;
        this.ChessBoard.SetPiece(position, this);
    }

    private bool SubLogic((int row, int col) target)
    {
        int rowInc = this.Color == PieceColor.Blue ? 1 : -1;
        int targetRow = this.Position.row + rowInc;
        int firstTurnTargetRow = this.Position.row + 2*rowInc;
        int[] targetCols = {this.Position.col - 1, this.Position.col + 1};
        bool isAttack = this.Position.col - target.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (target.row == targetRow && !isAttack && this.ChessBoard.IsEmpty(target))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!this.HasMoved && 
            target.row == firstTurnTargetRow && 
            !isAttack && this.ChessBoard.IsEmpty(target) && 
            this.ChessBoard.IsPathClear(this.Position, target))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (target.row == targetRow && targetCols.Contains(target.col) && !this.ChessBoard.IsEmpty(target))
        {
            return true;
        }

        // TODO: En passant move

        return false;
    }

    /// <inheritdoc/>
    public bool Move(IPiece heroPiece, (int row, int col) target)
    {
        if (!this.AllowableMove(target))
        {
            return false;
        }
        var other = this.ChessBoard.GetPiece(target);
        other?.CapturePiece(true);
        this.ChessBoard.ClearPiece(this.Position);
        this.ChessBoard.SetPiece(target, this);
        this.Position = target;
        HasMoved = true;
        this.ChessBoard.AddMove(this, target);
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
                if (this.AllowableMove((row, col)))
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
        if (this.Position == target)
        {
            return false;
        }
        // Cannot capture pieces of the same color
        if(!this.ChessBoard.IsEmpty(target) && !this.IsEnemyPiece(this.ChessBoard.GetPiece(target)!))
        {
            return false;
        }
        return SubLogic(target);
    }

    public bool IsPieceCaptured()
    {
        return IsCaptured;
    }

    public void CapturePiece(bool isOnBoard)
    {
        IsCaptured = isOnBoard;
    }
}