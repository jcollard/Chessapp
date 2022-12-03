using Chessapp;
using Chessapp.Piece;

namespace Chess;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target, ChessBoard chessBoard)
    {
        int rowDist = Math.Abs(Position.row - target.row);
        int colDist = Math.Abs(Position.col - target.col);
        return (rowDist == 1 || colDist == 1) &&
               rowDist + colDist == 3;
    }
}