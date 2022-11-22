namespace Chessapp.Piece;
public class PawnPiece : AbstractPiece
{
    public PawnPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool CheckPieceSpecificMove((int row, int col) targetPos)
    {
        int rowInc = this.Color == PieceColor.Blue ? 1 : -1;
        int targetRow = this.Position.row + rowInc;
        int firstTurnTargetRow = this.Position.row + 2*rowInc;
        int[] targetCols = {this.Position.col - 1, this.Position.col + 1};
        bool isAttack = this.Position.col - targetPos.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (targetPos.row == targetRow && !isAttack && this._gameState.IsEmpty(targetPos))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!this.HasMoved && 
            targetPos.row == firstTurnTargetRow && 
            !isAttack && this._gameState.IsEmpty(targetPos) && 
            this._gameState.IsPathClear(this.Position, targetPos))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (targetPos.row == targetRow && targetCols.Contains(targetPos.col) && !this._gameState.IsEmpty(targetPos))
        {
            return true;
        }

        // TODO: En passant move

        return false;
    }
}