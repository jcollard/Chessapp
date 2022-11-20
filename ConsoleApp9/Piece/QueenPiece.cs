namespace Chess;

public class QueenPiece : AbstractPiece
{
    public QueenPiece(string symbol, PieceColor color, (int, int) position, GameState gameState) : base(symbol, color, position, gameState) { }

    protected override bool SubLogic((int row, int col) target)
    {
        return this._gameState.IsPathClear(this.Position, target);
    }
}