using Chessapp;
using Chessapp.Piece;

namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        if (!Utils.IsOrthogonal(_pieceAttributes.Position, target))
        {
            return false;
        }
        return chessBoard.IsPathClear(_pieceAttributes.Position, target);
    }
}