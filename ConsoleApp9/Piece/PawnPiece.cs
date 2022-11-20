namespace Chess;
public class PawnPiece : AbstractPiece
{

    public PawnPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        char player = Program.changes.BoardLayout[start.row, start.col][0];


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