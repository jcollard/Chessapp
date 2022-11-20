namespace Chess;
public class Program
{
    public const int DELAY = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";

    private readonly static GameState gameState = new GameState();

    private static void DisplayError(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(DELAY);
    }

    /// <summary>
    /// Prompts the user to select a piece to move. Returns the selected piece and the row / col of that piece.
    /// </summary>
    /// <param name="pieceselect("></param>
    /// <returns></returns>
    private static IPiece PieceSelect()
    {
        while (true)
        {

            Utils.TryClear();
            gameState.PrintBoard();
            Console.WriteLine("select piece to move");
            string select = Utils.ReadLine();
            bool isValidPiece = gameState.TryGetPiece(select, out IPiece piece); 

            if (!isValidPiece)
            {
                DisplayError("Piece does not exist.");
                continue;
            }

            PieceColor player = gameState.GetActivePlayer();
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

    private static string GetTile()
    {
        string tile = Utils.ReadLine();
        tile = tile.ToUpper();

        if (tile == "BACK")
        {
            return "BACK";
        }

        (int row, int col) = BoardPosToIndex(tile);
        // Check that the selected tile is valid
        if (row == -1 || col == -1)
        {
            Console.WriteLine("Please input correct tile address (Example: A5)");
            return GetTile();
        }

        return tile;
    }

    static bool TryTileSelect(string select, (int, int) pos, out string tile)
    {
        while (true)
        {
            IPiece piece = gameState.GetPiece(select);
            piece.GetMoves(pos);
            Utils.TryClear();
            gameState.PrintBoard();

            Console.WriteLine($"Selected Piece: {select} \nPick a tile to move to or type 'BACK' to pick another piece");

            tile = GetTile();
            
            if (tile == "BACK")
            {
                return false;
            }

            (int row, int col) target = BoardPosToIndex(tile);
            if (target.row == -1 || target.col == -1)
            {
                DisplayError("Invalid Move");
                continue;
            }

            // If the piece logic is invalid, display Invalid Move.
            if (!piece.Logic(pos, target))
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid Move");
                Console.ResetColor();
                Utils.TryClear();
                gameState.PrintBoard();
                continue;
            }

            return true;
        }
    }

    private static (int, int) BoardPosToIndex(string tile)
    {
        if (tile.Length != 2)
        {
            return (-1, -1);
        }
        return (Rows.IndexOf(tile[1]), Columns.IndexOf(tile[0]));
    }

    static void Main(string[] args)
    {
        gameState.PrintBoard();
        while (!gameState.IsGameOver())
        {
            Console.WriteLine();
            

            IPiece piece = PieceSelect();
            if(!TryTileSelect(piece.Symbol, piece.Position, out string tile))
            {
                continue;
            }
            
            Utils.TryClear();
            gameState.PrintBoard();
            (int, int) targetPos = BoardPosToIndex(tile);
            piece.Move(targetPos);

            Utils.TryClear();
            gameState.PrintBoard();
        }
    }
}