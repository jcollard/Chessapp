namespace Chess;

public class RookPiece : AbstractPiece
{
    public RookPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) target)
    {
        if (!Utils.IsOrthogonal(this.Position, target))
        {
            return false;
        }
        return this._gameState.IsPathClear(this.Position, target);
    }
}