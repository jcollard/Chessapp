namespace Chess;

public class BishopPiece : AbstractPiece
{
    public BishopPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target)
    {
        // Can only move diagonally
        if (!(Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col)))
        {
            return false;
        }

        // Bishop cannot move THROUGH a piece
        int rowInc = start.row > target.row ? -1 : 1;
        int colInc = start.col > target.col ? -1 : 1;
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