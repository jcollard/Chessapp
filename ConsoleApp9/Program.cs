using Chessapp.Piece;

namespace Chessapp;
public static class Program
{
    public const int DELAY = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";
    private static readonly GameState _gameState = new();

    static void Main(string[] args)
    {
        while (!_gameState.IsGameOver())
        {
            Utils.TryClear();
            _gameState.PrintBoard();

            IPiece piece = PieceSelect();
            if (TryTileSelect(piece, out (int, int) targetPos))
            {
                piece.Move(targetPos);
            }
        }

        Utils.TryClear();
        _gameState.PrintBoard();
        PieceColor winner = _gameState.GetActivePlayer() == PieceColor.Blue ? PieceColor.Green : PieceColor.Blue;
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
            _gameState.PrintBoard();
            Console.WriteLine("select piece to move");
            string select = Utils.ReadLine();
            bool isValidPiece = _gameState.TryGetPiece(select, out IPiece piece);

            if (!isValidPiece)
            {
                DisplayError("Piece does not exist.");
                continue;
            }

            PieceColor player = _gameState.GetActivePlayer();
            if (player != piece.Color)
            {
                string casing = player == PieceColor.Blue ? "lowercase" : "capital";
                DisplayError($"It's {player}'s turn, Select piece again. {player} uses {casing} letters.");
                continue;
            }

            if (piece.IsCaptured)
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
        while (true)
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
            if (pos.row != -1 && pos.col != -1)
            {
                return true;
            }

            Console.WriteLine("Please input correct tile address (Example: A5)");
        }
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
            IList<(int, int)> moves = piece.GetMoves();
            _gameState.DisplayPossibleMoves(moves);
            Thread.Sleep(Program.DELAY);
            Utils.TryClear();
            _gameState.PrintBoard();

            Console.WriteLine($"Selected Piece: {piece.Symbol}");
            if (!TryGetTile(out target))
            {
                return false;
            }

            if (piece.CheckMove(target))
            {
                return true;
            }

            DisplayError("Invalid Move");
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
        return tile.Length != 2 ? (-1, -1) : (Rows.IndexOf(tile[1]), Columns.IndexOf(tile[0]));
    }
}