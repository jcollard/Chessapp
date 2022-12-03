namespace Chess;
public class Program
{
    public const int DELAY = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";
    private readonly static ChessBoard ChessBoard = new ChessBoard();

    static void Main(string[] args)
    {
        while (!ChessBoard.IsGameOver())
        {
            Utils.TryClear();
            ChessBoard.PrintBoard();

            IPiece piece = PieceSelect();
            if (!TryTileSelect(piece, out (int, int) targetPos))
            {
                continue;
            }

            piece.Move(targetPos);
        }

        Utils.TryClear();
        ChessBoard.PrintBoard();
        PieceColor winner = ChessBoard.GetActivePlayer() == PieceColor.Blue ? PieceColor.Green : PieceColor.Blue;
        Console.WriteLine($"{winner} is the winner!");
    }

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    private static void DisplayError(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(DELAY);
    }

    /// <summary>
    /// Prompts the user to select a piece to move. Returns the selected piece and the row / col of that piece.
    /// </summary>
    private static IPiece PieceSelect()
    {
        while (true)
        {

            Utils.TryClear();
            ChessBoard.PrintBoard();
            Console.WriteLine("select piece to move");
            string select = Utils.ReadLine();
            bool isValidPiece = ChessBoard.TryGetPiece(select, out IPiece piece);

            if (!isValidPiece)
            {
                DisplayError("Piece does not exist.");
                continue;
            }

            PieceColor player = ChessBoard.GetActivePlayer();
            if (player != piece.Color)
            {
                string casing = player == PieceColor.Blue ? "lowercase" : "capital";
                DisplayError($"It's {player}'s turn, Select piece again. {player} uses {casing} letters.");
                continue;
            }

            if (piece.IsPieceCaptured())
            {
                DisplayError("That piece has already been captured.");
                continue;
            }

            return piece;
        }
    }
 
    /// <summary>
    /// Prompts the user to enter a tile position. 
    /// Returns true
    /// </summary>
    private static bool TryGetTile(out (int row, int col) pos)
    {
        pos = (-1, -1);
        Console.WriteLine("Pick a tile to move to or type 'BACK' to pick another piece");
        string tile = Utils.ReadLine();
        tile = tile.ToUpper();

        if (tile == "BACK")
        {
            return false;
        }

        pos = BoardPosToIndex(tile);
        // Check that the selected tile is valid
        if (pos.row == -1 || pos.col == -1)
        {
            Console.WriteLine("Please input correct tile address (Example: A5)");
            return TryGetTile(out pos);
        }

        return true;
    }

    /// <summary>
    /// Given a piece, prompts the user to select a tile to move to or to type
    /// "BACK". If the user types back, this method return false. Otherwise,
    /// it returns true and the target parameter is set to the users choice.
    /// </summary>
    private static bool TryTileSelect(IPiece piece, out (int row, int col) target)
    {
        while (true)
        {
            List<(int, int)> moves = piece.GetMoves();
            ChessBoard.DisplayPossibleMoves(moves);
            Thread.Sleep(Program.DELAY);
            Utils.TryClear();
            ChessBoard.PrintBoard();

            Console.WriteLine($"Selected Piece: {piece.Symbol}");
            if (!TryGetTile(out target))
            {
                return false;
            }
            // TODO(jcollard): I think this is not necessary 
            // if (target.row == -1 || target.col == -1 || !piece.Logic(target))
            if (!piece.AllowableMove(target))
            {
                DisplayError("Invalid Move");
                continue;
            }

            return true;
        }
    }

    /// <summary>
    /// Given a two character string of a position on the board, return
    /// its associated row, col values.
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private static (int, int) BoardPosToIndex(string tile)
    {
        if (tile.Length != 2)
        {
            return (-1, -1);
        }
        return (Rows.IndexOf(tile[1]), Columns.IndexOf(tile[0]));
    }
}