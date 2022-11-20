namespace Chess;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) target)
    {
        int rowDist = Math.Abs(this.Position.row - target.row);
        int colDist = Math.Abs(this.Position.col - target.col);
        return (rowDist == 1 || colDist == 1) &&
               rowDist + colDist == 3;
    }
}