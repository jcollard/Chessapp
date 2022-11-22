namespace Chessapp.Piece;

public class BishopPiece : AbstractPiece
{
    public BishopPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool CheckPieceSpecificMove((int row, int col) targetPos)
    {
        return Utils.IsDiagonal(this.Position, targetPos) && this._gameState.IsPathClear(this.Position, targetPos);
    }
}