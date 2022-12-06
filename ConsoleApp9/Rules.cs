using Chess;

namespace Chessapp;

public class Rules
{
    /// <summary>
    /// Given a starting and target position that are orthogonal or diagonal to each
    /// other, returns true if no pieces are found between them and false otherwise.
    /// </summary>
    public static bool IsPathClear(
        (int row, int col) start, 
        (int row, int col) target, 
        Dictionary<string, IPiece?> pieces)
    {
        if (!Rules.IsDiagonal(start, target) && !Rules.IsOrthogonal(start, target))
        {
            return false;
        }
        int rowInc = Rules.GetIncrement(start.row, target.row);
        int colInc = Rules.GetIncrement(start.col, target.col);

        int row = start.row + rowInc;
        int col = start.col + colInc;

        while (row != target.row || col != target.col)
        {
            (int row, int col) pos = (row, col);
            var isEmpty = pieces
                .FirstOrDefault(x => 
                    x.Value != null && 
                    x.Value.Position.col == pos.col && 
                    x.Value.Position.row == pos.row).Value == null;
            if (!isEmpty)
            {
                return false;
            }
            row += rowInc;
            col += colInc;
        }
        return true;
    }

    /// <summary>
    /// Given two integers, returns 0 if they are the same, -1 if start is
    /// greater than target, and 1 if target is greater than start.
    /// </summary>
    public static int GetIncrement(int start, int target)
    {
        if (start == target)
        {
            return 0;
        }

        if (start > target)
        {
            return -1;
        }
    
        return 1;
    }

    public static bool IsOrthogonal((int row, int col) start, (int row , int col) target)
    {
        int rowInc = Utils.GetIncrement(start.row, target.row);
        int colInc = Utils.GetIncrement(start.col, target.col);
        return Math.Abs(rowInc) + Math.Abs(colInc) == 1;
    }

    public static bool IsDiagonal((int row, int col) start, (int row , int col) target)
    {
        return Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col);
    }
}