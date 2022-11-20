namespace Chess;

public class BishopPiece : AbstractPiece
{
    public BishopPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) target)
    {
        if (!Utils.IsDiagonal(this.Position, target))
        {
            return false;
        }
        return this._gameState.IsPathClear(this.Position, target);
    }
}