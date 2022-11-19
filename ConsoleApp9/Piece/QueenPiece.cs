namespace Chess;

public class QueenPiece : AbstractPiece
{
    public QueenPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    public override bool Logic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        if(!GameState.IsEmpty(target) && !this.IsEnemyPiece(target, gameState))
        {
            return false;
        }
        
        int diff = Math.Abs(start.row - target.row) - Math.Abs(start.col - target.col);

        if (!(Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col)) && Math.Abs(diff) != Math.Abs(start.row - target.row) && Math.Abs(diff) != Math.Abs(start.col - target.col))
        {
            return false;
        }


        if (Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col))
        {

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
        else if (Math.Abs(diff) == Math.Abs(start.row - target.row) || Math.Abs(diff) == Math.Abs(start.col - target.col))
        {
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
                    if (Program.changes.pieces.Contains(Program.changes.BoardLayout[i, j]) && Program.changes.BoardLayout[i, target.col] != Program.changes.BoardLayout[target.row, target.col])
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

                    if (Program.changes.pieces.Contains(Program.changes.BoardLayout[i, j]) && Program.changes.BoardLayout[target.row, j] != Program.changes.BoardLayout[target.row, target.col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}