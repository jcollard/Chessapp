namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard) : base(symbol, color, position, chessBoard) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        if (!Utils.IsOrthogonal(this.Position, target))
        {
            return false;
        }
        return this.ChessBoard.IsPathClear(this.Position, target);
    }
}