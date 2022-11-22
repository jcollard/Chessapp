namespace Chessapp.Piece;

public class KingPiece : AbstractPiece
{

    public KingPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool CheckPieceSpecificMove((int row, int col) targetPos)
    {
        // TODO: Kings can castle if they have not moved yet
        int rowDist = Math.Abs(this.Position.row - targetPos.row);
        int colDist = Math.Abs(this.Position.col - targetPos.col);
        return rowDist <= 1 && colDist <= 1;
    }
}