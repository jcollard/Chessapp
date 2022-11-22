namespace Chessapp.Piece;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool CheckPieceSpecificMove((int row, int col) targetPos)
    {
        int rowDist = Math.Abs(this.Position.row - targetPos.row);
        int colDist = Math.Abs(this.Position.col - targetPos.col);
        return (rowDist == 1 || colDist == 1) &&
               rowDist + colDist == 3;
    }
}