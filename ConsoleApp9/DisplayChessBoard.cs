using Chess;

namespace Chessapp;

public class DisplayChessBoard
{
    private readonly ChessBoard _chessboard;

    public DisplayChessBoard(ChessBoard chessboard)
    {
        _chessboard = chessboard;
    }

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
        DisplayPlayerTurn(_chessboard.ActivePlayer());
    }

    /// <summary>
    /// Given a list of positions, displays them as possible moves
    /// on the screen.
    /// </summary>
    /// <param name="chessboardMoves"></param>
    internal void DisplayPossibleMoves(List<(int, int)>? moves)
    {
        (int left, int top) = Console.GetCursorPosition();
        Console.ForegroundColor = ConsoleColor.Red;
        if (moves != null)
            foreach ((int row, int col) in moves)
            {
                Console.SetCursorPosition(col * 8, (row * 2) + 3);
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
        string[] columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (string letter in columns)
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
        Console.WriteLine($"List of Pieces Captured: {string.Join(", ", _chessboard._pieces.Values.Where(p => p is { IsPieceCaptured: true }).Select(p => p?.Symbol))}");
        Console.ResetColor();
        Console.WriteLine("-------------------------------------------------------------------------");
    }

    /// <summary>
    /// Displays which player's turn it is.
    /// </summary>
    private void DisplayPlayerTurn(PieceColor playerColor)
    {
        Console.ForegroundColor = playerColor == PieceColor.Blue ? ConsoleColor.Cyan : ConsoleColor.Green;
        Console.WriteLine($"It's {playerColor}'s turn");
        Console.ResetColor();
    }

    /// <summary>
    /// Given a row and column, displays that cell on the screen.
    /// </summary>
    private void DisplayCell(int row, int col)
    {
        string symbol = "  ";
        var isPiecePresent = _chessboard._pieces.Values
            .FirstOrDefault(x => x != null && x.Position.col == col && x.Position.row == row);
        if (isPiecePresent != null)
        {
            symbol = isPiecePresent.Symbol;
            Console.ForegroundColor = isPiecePresent.Color == PieceColor.Blue ? ConsoleColor.Cyan : ConsoleColor.Green;
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