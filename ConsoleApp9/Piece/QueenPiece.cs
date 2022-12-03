namespace Chess;

public class QueenPiece : AbstractPiece
{
    public QueenPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard) : base(symbol, color, position, chessBoard) { }

    protected override bool SubLogic((int row, int col) target)
    {
        return this.ChessBoard.IsPathClear(this.Position, target);
    }
}