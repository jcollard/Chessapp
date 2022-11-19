namespace Chess;
public class PawnPiece : AbstractPiece
{
    
    public PawnPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

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

        if (char.ToUpper(player) == player)
        {
            if (start.row - target.row == 2 && start.row == 6 && start.col - target.col == 0 && Program.changes.BoardLayout[target.row, target.col].Contains(' ') && Program.changes.BoardLayout[target.row + 1, target.col].Contains(' '))
            {
                return true;
            }
            if (start.row - target.row == 1 && target.col == start.col && Program.changes.BoardLayout[target.row, target.col].Contains(' '))
            {
                return true;
            }
            if (start.row - target.row == 1 && Math.Abs(start.col - target.col) == 1 && Program.changes.pieces.Contains(Program.changes.BoardLayout[target.row, target.col]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (start.row - target.row == -2 && start.row == 1 && start.col - target.col == 0 && Program.changes.BoardLayout[target.row, target.col].Contains(' ') && Program.changes.BoardLayout[target.row - 1, target.col].Contains(' '))
            {
                return true;
            }
            if (start.row - target.row == -2 && start.row == 1 && start.col - target.col == 0 && Program.changes.BoardLayout[target.row, target.col].Contains(' ') && Program.changes.BoardLayout[target.row - 1, target.col].Contains('X'))
            {
                return true;
            }
            if (start.row - target.row == -1 && target.col == start.col && Program.changes.BoardLayout[target.row, target.col].Contains(' '))
            {
                return true;
            }
            if (start.row - target.row == -1 && Math.Abs(start.col - target.col) == 1 && Program.changes.pieces.Contains(Program.changes.BoardLayout[target.row, target.col]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}