using Chessapp;
using Chessapp.Piece;

namespace Chess;

public class QueenPiece : AbstractPiece
{
    public QueenPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    protected override bool SubLogic((int row, int col) target,
        List<IPiece?> chessBoardPieces)
    {
        return Rules.IsPathClear(PieceAttributes.Position, target, chessBoardPieces);
    }
}