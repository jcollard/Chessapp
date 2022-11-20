namespace Chess;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        char player = Program.changes.BoardLayout[start.row, start.col][0];



        if (Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col))
        {
            return false;
        }

        if (Math.Abs(start.row - target.row) != 1 && Math.Abs(start.col - target.col) != 1)
        {
            return false;
        }
        if (Math.Abs(start.col - target.col) != 2 && Math.Abs(start.row - target.row) != 2)
        {
            return false;
        }

        return true;
    }
}