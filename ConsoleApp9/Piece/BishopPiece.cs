namespace Chess;

public class BishopPiece : AbstractPiece
{
    public BishopPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    public override bool Logic((int row, int col) start, (int row, int col) target)
    {
        if (Program.changes.BoardLayout[target.row, target.col] == Program.changes.BoardLayout[target.row, target.col].ToUpper() && Program.changes.BoardLayout[start.row, start.col] == Program.changes.BoardLayout[start.row, start.col].ToUpper() && !Program.changes.BoardLayout[target.row, target.col].Contains(' '))
        {
            return false;
        }
        if (Program.changes.BoardLayout[target.row, target.col] == Program.changes.BoardLayout[target.row, target.col].ToLower() && Program.changes.BoardLayout[start.row, start.col] == Program.changes.BoardLayout[start.row, start.col].ToLower() && !Program.changes.BoardLayout[target.row, target.col].Contains(' '))
        {
            return false;
        }
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