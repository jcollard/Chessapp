using Chessapp.Piece;

namespace Chess;

public class QueenPiece : AbstractPiece
{
    public QueenPiece(string symbol, PieceColor color, (int, int) position, ChessBoard chessBoard) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        return chessBoard.IsPathClear(this.Position, target);
    }
}