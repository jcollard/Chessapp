namespace Chess;
public class PawnPiece : IPiece
{
    private bool IsCaptured { get; set; }
    public bool HasMoved { get; private set; }
    public string Symbol { get; }
    public PieceColor Color { get; }
    private bool IsEnemyPiece(IPiece other) => other.Color != Color;
    public (int row, int col) Position { get; private set; }
    private readonly ChessBoard _chessBoard;

    public PawnPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard)
    {
        Symbol = symbol;
        IsCaptured = false;
        Color = color;
        Position = position;
        _chessBoard = chessBoard;
    }

    private bool SubLogic((int row, int col) target)
    {
        int rowInc = Color == PieceColor.Blue ? 1 : -1;
        int targetRow = Position.row + rowInc;
        int firstTurnTargetRow = Position.row + 2*rowInc;
        int[] targetCols = {Position.col - 1, Position.col + 1};
        bool isAttack = Position.col - target.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (target.row == targetRow && !isAttack && _chessBoard.IsEmpty(target))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!HasMoved && 
            target.row == firstTurnTargetRow && 
            !isAttack && _chessBoard.IsEmpty(target) && 
            _chessBoard.IsPathClear(Position, target))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (target.row == targetRow && targetCols.Contains(target.col) && !_chessBoard.IsEmpty(target))
        {
            return true;
        }

        // TODO: En passant move

        return false;
    }

    /// <inheritdoc/>
    public bool Move(IPiece heroPiece, (int row, int col) target)
    {
        if (!AllowableMove(target))
        {
            return false;
        }
        var other = _chessBoard.GetPiece(target);
        other?.CapturePiece(true);
        _chessBoard.ClearPiece(Position);
        _chessBoard.SetPiece(target, this);
        Position = target;
        HasMoved = true;
        _chessBoard.AddMove(this, target);
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
        if(!_chessBoard.IsEmpty(target) && !IsEnemyPiece(_chessBoard.GetPiece(target)!))
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