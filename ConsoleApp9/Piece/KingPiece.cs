using Chessapp;
using Chessapp.Piece;

namespace Chess;

public class KingPiece : AbstractPiece
{

    public KingPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        // TODO: Kings can castle if they have not moved yet
        int rowDist = Math.Abs(_pieceAttributes.Position.row - target.row);
        int colDist = Math.Abs(_pieceAttributes.Position.col - target.col);
        return rowDist <= 1 && colDist <= 1;
    }
}