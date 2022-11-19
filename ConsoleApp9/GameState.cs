namespace Chess;

public class GameState
{
    
    private readonly Dictionary<string, IPiece> pieces = new ();

    public GameState()
    {
        for (int i = 1; i <= 8; i++)
        {
            pieces["p" + i] = new PawnPiece("p" + i, PieceColor.Blue, (1, i-1));
            pieces["P" + i] = new PawnPiece("P" + i, PieceColor.Green, (6, i-1));
        }
        pieces["k1"] = new KingPiece("k1", PieceColor.Blue, (0, 3));
        pieces["K1"] = new KingPiece("K1", PieceColor.Green, (7, 4));
        pieces["q1"] = new QueenPiece("q1", PieceColor.Blue, (0, 4));
        pieces["Q1"] = new QueenPiece("Q1", PieceColor.Green, (7, 3));

        pieces["n1"] = new KnightPiece("n1", PieceColor.Blue, (0, 1));
        pieces["N1"] = new KnightPiece("N1", PieceColor.Green, (7, 1));
        pieces["n2"] = new KnightPiece("n2", PieceColor.Blue, (0, 6));
        pieces["N2"] = new KnightPiece("N2", PieceColor.Green, (7, 6));

        pieces["b1"] = new BishopPiece("b1", PieceColor.Blue, (0, 2));
        pieces["B1"] = new BishopPiece("B1", PieceColor.Green, (7, 2));
        pieces["b2"] = new BishopPiece("b2", PieceColor.Blue, (0, 5));
        pieces["B2"] = new BishopPiece("B2", PieceColor.Green, (7, 5));

        pieces["r1"] = new RookPiece("r1", PieceColor.Blue, (0, 0));
        pieces["R1"] = new RookPiece("R1", PieceColor.Green, (7, 0));
        pieces["r2"] = new RookPiece("r2", PieceColor.Blue, (0, 7));
        pieces["R2"] = new RookPiece("R2", PieceColor.Green, (7, 7));
    }

    public IPiece GetPiece(string symbol)
    {
        if (pieces.TryGetValue(symbol, out IPiece? value))
        {
            return value;
        }
        throw new ArgumentException($"The symbol {symbol} is not a valid piece on this board.");
    }
}