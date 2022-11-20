namespace Chess;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target)
    {
        int rowDist = Math.Abs(start.row - target.row);
        int colDist = Math.Abs(start.col - target.col);
        return (rowDist == 1 || colDist == 1) &&
               rowDist + colDist == 3;
    }
}