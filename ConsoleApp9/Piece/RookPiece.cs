namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target)
    {
        string targetSymbol = Program.changes.BoardLayout[target.row, target.col];

        int diff = Math.Abs(start.row - target.row) - Math.Abs(start.col - target.col);
        if (Math.Abs(diff) != Math.Abs(start.row - target.row) && Math.Abs(diff) != Math.Abs(start.col - target.col))
        {
            return false;
        }
        if (0 != Math.Abs(start.row - target.row) && 0 != Math.Abs(start.col - target.col))
        {
            return false;
        }
        if (Math.Abs(start.row - target.row) != 0)
        {

            for (int i = start.row, j = start.col; i > 0 || i < 7;)
            {
                if (i == target.row)
                {
                    break;
                }
                if (start.row > target.row)
                {
                    i--;
                }
                else
                {
                    i++;
                }
                if (Program.changes.pieces.Contains(Program.changes.BoardLayout[i, j]) && Program.changes.BoardLayout[i, target.col] != targetSymbol)
                {
                    return false;
                }
            }
        }
        if (Math.Abs(start.col - target.col) != 0)
        {
            for (int i = start.row, j = start.col; j > 0 || j < 7;)
            {
                if (j == target.col)
                {
                    break;
                }

                if (start.col > target.col)
                {
                    j--;
                }
                else
                {
                    j++;
                }

                if (Program.changes.pieces.Contains(Program.changes.BoardLayout[i, j]) && Program.changes.BoardLayout[target.row, j] != targetSymbol)
                {
                    return false;
                }
            }
        }
        return true;
    }
}