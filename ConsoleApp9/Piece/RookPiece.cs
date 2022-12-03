using Chessapp;
using Chessapp.Piece;

namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        if (!Utils.IsOrthogonal(PieceAttributes.Position, target))
        {
            return false;
        }
        return chessBoard.IsPathClear(PieceAttributes.Position, target);
    }
}