namespace Chess;
public class PawnPiece : AbstractPiece
{

    public PawnPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) start, (int row, int col) target)
    {
        int rowInc = this.Color == PieceColor.Blue ? 1 : -1;
        int targetRow = start.row + rowInc;
        int firstTurnTargetRow = start.row + 2*rowInc;
        int[] targetCols = {start.col - 1, start.col + 1};
        bool isAttack = start.col - target.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (target.row == targetRow && !isAttack && this._gameState.IsEmpty(target))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!this.HasMoved && target.row == firstTurnTargetRow && !isAttack && this._gameState.IsEmpty(target) && this._gameState.IsPathClear(start, target))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (target.row == targetRow && targetCols.Contains(target.col) && !this._gameState.IsEmpty(target))
        {
            return true;
        }

        // TODO: En passant move

        return false;
    }
}