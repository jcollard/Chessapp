namespace Chess;

public class GameState
{
    
    private readonly Dictionary<string, IPiece> pieces = new ();
    private readonly IPiece?[,] board = new IPiece?[8,8];


    public GameState()
    {
        for (int i = 1; i <= 8; i++)
        {
            pieces["p" + i] = new PawnPiece("p" + i, PieceColor.Blue, (1, i-1), this);
            pieces["P" + i] = new PawnPiece("P" + i, PieceColor.Green, (6, i-1), this);
        }
        pieces["k1"] = new KingPiece("k1", PieceColor.Blue, (0, 3), this);
        pieces["K1"] = new KingPiece("K1", PieceColor.Green, (7, 4), this);
        pieces["q1"] = new QueenPiece("q1", PieceColor.Blue, (0, 4), this);
        pieces["Q1"] = new QueenPiece("Q1", PieceColor.Green, (7, 3), this);

        pieces["n1"] = new KnightPiece("n1", PieceColor.Blue, (0, 1), this);
        pieces["N1"] = new KnightPiece("N1", PieceColor.Green, (7, 1), this);
        pieces["n2"] = new KnightPiece("n2", PieceColor.Blue, (0, 6), this);
        pieces["N2"] = new KnightPiece("N2", PieceColor.Green, (7, 6), this);

        pieces["b1"] = new BishopPiece("b1", PieceColor.Blue, (0, 2), this);
        pieces["B1"] = new BishopPiece("B1", PieceColor.Green, (7, 2), this);
        pieces["b2"] = new BishopPiece("b2", PieceColor.Blue, (0, 5), this);
        pieces["B2"] = new BishopPiece("B2", PieceColor.Green, (7, 5), this);

        pieces["r1"] = new RookPiece("r1", PieceColor.Blue, (0, 0), this);
        pieces["R1"] = new RookPiece("R1", PieceColor.Green, (7, 0), this);
        pieces["r2"] = new RookPiece("r2", PieceColor.Blue, (0, 7), this);
        pieces["R2"] = new RookPiece("R2", PieceColor.Green, (7, 7), this);
    }

    internal void SetPiece((int row, int col) pos, IPiece piece) => board[pos.row, pos.col] = piece;
    internal void ClearPiece((int row, int col) pos) => board[pos.row, pos.col] = null;

    public bool MovePiece((int row, int col) pos, (int, int) target)
    {
        if (board[pos.row, pos.col] == null)
        {
            return false;
        }
        IPiece piece = board[pos.row, pos.col]!;
        bool result = piece.Move(target);     
        return result;
    }

    public IPiece? GetPiece((int row, int col) pos) => board[pos.row, pos.col];

    public static bool IsEmpty((int row, int col) pos)
    {
        string targetSymbol = Program.changes.BoardLayout[pos.row, pos.col];
        return targetSymbol.Contains(' ');
    }

    /// <summary>
    /// Given a symbol name for a piece, checks if that piece
    /// exists in this GameState. If that piece exists, sets
    /// the value of piece and returns true. Otherwise, returns false.
    /// </summary>
    public bool TryGetPiece(string symbol, out IPiece piece)
    {
        if (pieces.TryGetValue(symbol, out piece!))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Given a symbol name for a piece, returns the associated IPiece.
    /// If no such piece is on the board, throw an ArgumentException.
    /// </summary>
    public IPiece GetPiece(string symbol)
    {
        if (pieces.TryGetValue(symbol, out IPiece? value))
        {
            return value;
        }
        throw new ArgumentException($"The symbol {symbol} is not a valid piece on this board.");
    }
}