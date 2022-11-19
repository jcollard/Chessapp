namespace Chess;

public class KnightPiece : AbstractPiece
{
    public KnightPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    public override bool Logic((int row, int col) start, (int row, int col) target)
    {
        char player = Program.changes.BoardLayout[start.row, start.col][0];

        if (Program.changes.BoardLayout[target.row, target.col] == Program.changes.BoardLayout[target.row, target.col].ToUpper() && Program.changes.BoardLayout[start.row, start.col] == Program.changes.BoardLayout[start.row, start.col].ToUpper() && !Program.changes.BoardLayout[target.row, target.col].Contains(' '))
        {
            return false;
        }
        if (Program.changes.BoardLayout[target.row, target.col] == Program.changes.BoardLayout[target.row, target.col].ToLower() && Program.changes.BoardLayout[start.row, start.col] == Program.changes.BoardLayout[start.row, start.col].ToLower() && !Program.changes.BoardLayout[target.row, target.col].Contains(' '))
        {
            return false;
        }

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