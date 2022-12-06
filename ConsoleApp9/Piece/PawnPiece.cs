using Chessapp;
using Chessapp.Piece;

namespace Chess;
public class PawnPiece : AbstractPiece
{

    public PawnPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }
    protected override bool SubLogic((int row, int col) target, Dictionary<string, IPiece?> chessBoardPieces)
    {
        int rowInc = PieceAttributes.Color == PieceColor.Blue ? 1 : -1;
        int targetRow = PieceAttributes.Position.row + rowInc;
        int firstTurnTargetRow = PieceAttributes.Position.row + 2*rowInc;
        int[] targetCols = {PieceAttributes.Position.col - 1, Position.col + 1};
        bool isAttack = PieceAttributes.Position.col - target.col != 0;

        // Always, pawn may move forward 1 if space is empty
        if (target.row == targetRow && !isAttack && chessBoard.IsEmpty(target))
        {
            return true;
        }

        // On first turn, pawn may move forward 2 spaces if empty
        if (!PieceAttributes.HasMoved && 
            target.row == firstTurnTargetRow && 
            !isAttack && chessBoard.IsEmpty(target) && 
            Rules.IsPathClear(PieceAttributes.Position, target, chessBoard._pieces))
        {
            return true;
        }

        // Can move diagonal 1 if an enemy is in that space
        if (target.row == targetRow && targetCols.Contains(target.col) && !chessBoard.IsEmpty(target))
        {
            return true;
        }

        // TODO: En passant move

        return false;
    }

}