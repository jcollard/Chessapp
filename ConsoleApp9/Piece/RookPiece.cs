namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    private static int GetIncrement(int start, int target)
    {
        if (start == target)
        {
            return 0;
        }
        else if (start > target)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target)
    {
        int rowInc = GetIncrement(start.row, target.row);
        int colInc = GetIncrement(start.col, target.col);
        if (Math.Abs(rowInc) + Math.Abs(colInc) != 1)
        {
            return false;
        }

        int row = start.row + rowInc;
        int col = start.col + colInc;

        while (row != target.row || col != target.col)
        {
            if (!this._gameState.IsEmpty((row, col)))
            {
                return false;
            }
            row += rowInc;
            col += colInc;
        }
        return true;
    }
}