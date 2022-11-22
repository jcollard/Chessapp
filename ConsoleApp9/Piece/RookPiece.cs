namespace Chessapp.Piece;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool CheckPieceSpecificMove((int row, int col) targetPos)
    {
        return Utils.IsOrthogonal(this.Position, targetPos) 
               && this._gameState.IsPathClear(this.Position, targetPos);
    }
}