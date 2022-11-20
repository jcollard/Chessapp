namespace Chess;

public class BishopPiece : AbstractPiece
{
    public BishopPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        if (!(Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col)))
        {
            return false;
        }

        for (int i = start.row, j = start.col; j > 0 || j < 7 || i > 0 || i < 7;)
        {
            if (i == target.row && j == target.col)
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

            if (start.col > target.col)
            {
                j--;
            }
            else
            {
                j++;
            }

            if (Program.changes.pieces.Contains(Program.changes.BoardLayout[i, j]) && Program.changes.BoardLayout[i, j] != Program.changes.BoardLayout[target.row, target.col])
            {
                return false;
            }
        }
        return true;
    }
}