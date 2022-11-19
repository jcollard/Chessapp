namespace Chess;

public class KingPiece : AbstractPiece
{

    public KingPiece(string symbol, PieceColor color, (int, int) position) : base(symbol, color, position) { }

    public override bool Logic((int row, int col) start, (int row, int col) target, GameState gameState)
    {
        if(!GameState.IsEmpty(target) && !this.IsEnemyPiece(target, gameState))
        {
            return false;
        }

        if (Math.Abs(start.row - target.row) != 1 && Math.Abs(start.col - target.col) != 1)
        {
            return false;
        }
        else if (Math.Abs(start.row - target.row) != 1 && Math.Abs(start.row - target.row) != 0)
        {
            return false;
        }
        else if (Math.Abs(start.col - target.col) != 0 && Math.Abs(start.col - target.col) != 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}