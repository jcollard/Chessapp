namespace Chess;

public class ChessBoard
{
    private readonly Dictionary<string, IPiece> pieces = new();
    private readonly IPiece?[,] board = new IPiece?[8, 8];
    private readonly List<(IPiece, (int, int))> moves = new();
    private readonly IPiece blueKing, greenKing;

    /// <summary>
    /// Constructs a GameState in a traditional chess layout.
    /// </summary>
    public ChessBoard()
    {
        for (int i = 1; i <= 8; i++)
        {
            pieces["p" + i] = new PawnPiece("p" + i, PieceColor.Blue, (1, i - 1), this);
            pieces["P" + i] = new PawnPiece("P" + i, PieceColor.Green, (6, i - 1), this);
        }
        blueKing = pieces["k1"] = new KingPiece("k1", PieceColor.Blue, (0, 3), this);
        greenKing = pieces["K1"] = new KingPiece("K1", PieceColor.Green, (7, 4), this);
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

    /// <summary>
    /// Given a starting and target position that are orthogonal or diagonal to each
    /// other, returns true if no pieces are found between them and false otherwise.
    /// </summary>
    public bool IsPathClear((int row, int col) start, (int row, int col) target)
    {
        if (!Utils.IsDiagonal(start, target) && !Utils.IsOrthogonal(start, target))
        {
            return false;
        }
        int rowInc = Utils.GetIncrement(start.row, target.row);
        int colInc = Utils.GetIncrement(start.col, target.col);

        int row = start.row + rowInc;
        int col = start.col + colInc;

        while (row != target.row || col != target.col)
        {
            if (!this.IsEmpty((row, col)))
            {
                return false;
            }
            row += rowInc;
            col += colInc;
        }
        return true;
    }

    internal void AddMove(IPiece piece, (int, int) target) => this.moves.Add((piece, target));

    /// <summary>
    /// Returns the piece at the specified position or null if no piece is at that
    /// position.
    /// </summary>
    public IPiece? GetPiece((int row, int col) pos) => board[pos.row, pos.col];
    /// <summary>
    /// Returns the piece at the specified position or null if no piece is at that
    /// position.
    /// </summary>
    public ICaptured? RetrievePiece((int row, int col) pos) => board[pos.row, pos.col];

    /// <summary>
    /// Returns true if there is no piece at the specified position and false otherwise.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsEmpty((int row, int col) pos) => board[pos.row, pos.col] == null;

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

    /// <summary>
    /// Checks if the game has ended and returns true if it has.
    /// </summary>
    public bool IsGameOver() => blueKing.IsPieceCaptured() || greenKing.IsPieceCaptured();

    

    /// <summary>
    /// Returns the color of the active player
    /// </summary>
    public PieceColor GetActivePlayer() => moves.Count % 2 == 0 ? PieceColor.Blue : PieceColor.Green;

    /// <summary>
    /// Prints the underlying board to the screen.
    /// </summary>
    public void PrintBoard()
    {
        Console.Clear();
        DisplayLegend();
        Console.WriteLine();

        for (int row = 0; row < 8; row++)
        {
            DisplayRow(row);
        }
        DisplayColumns();
        Console.WriteLine();

        DisplayCapturedPieces();
        DisplayPlayerTurn();
    }

    /// <summary>
    /// Given a list of positions, displays them as possible moves
    /// on the screen.
    /// </summary>
    internal void DisplayPossibleMoves(List<(int, int)> moves)
    {
        (int left, int top) = Console.GetCursorPosition();
        Console.ForegroundColor = ConsoleColor.Red;
        foreach ((int row, int col) in moves)
        {
            Console.SetCursorPosition(col*8, (row*2)+3);
            Console.Write("|  XX  |");
        }
        Console.ResetColor();
        Console.SetCursorPosition(left, top);
    }

    /// <summary>
    /// Displays the bottom row of columns under the board.
    /// </summary>
    private static void DisplayColumns()
    {
        string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (string letter in Columns)
        {
            Console.Write("   {0}    ", letter);   
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the legend at the top of the screen.
    /// </summary>
    private static void DisplayLegend()
    {
        Console.WriteLine("Legend: Use capital letters if you're green, lowercase if you're blue");
        Console.WriteLine("R = Rook, N = Knight, P = Pawn, K = King, B = Bishop, Q = Queen");
    }

    /// <summary>
    /// Displays a list of captured pieces.
    /// </summary>
    private void DisplayCapturedPieces()
    {
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"List of Pieces Captured: {string.Join(", ", pieces.Values.Where(p => p.IsPieceCaptured()).Select(p => p.Symbol))}");
        Console.ResetColor();
        Console.WriteLine("-------------------------------------------------------------------------");
    }

    /// <summary>
    /// Displays which player's turn it is.
    /// </summary>
    private void DisplayPlayerTurn()
    {
        PieceColor player = GetActivePlayer();
        Console.ForegroundColor = player == PieceColor.Blue ? ConsoleColor.Cyan : ConsoleColor.Green;
        Console.WriteLine($"It's {player}'s turn");
        Console.ResetColor();
    }

    /// <summary>
    /// Given a row and column, displays that cell on the screen.
    /// </summary>
    private void DisplayCell(int row, int col)
    {
        string symbol = "  ";
        if (board[row, col] != null)
        {
            IPiece piece = board[row, col]!;
            symbol = piece.Symbol;
            Console.ForegroundColor = piece.Color == PieceColor.Blue ? ConsoleColor.Cyan : ConsoleColor.Green;
        }
        Console.Write($"|  {symbol}  |");
        Console.ResetColor();
    }

    /// <summary>
    /// Given a row, displays that row on the screen
    /// </summary>
    private void DisplayRow(int row)
    {
        for (int col = 0; col < 8; col++)
        {
            DisplayCell(row, col);
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(row + 1);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write("-----------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(row + 1);
        Console.ResetColor();
    }

}