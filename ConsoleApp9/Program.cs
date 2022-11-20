namespace Chess;
public class Program
{
    public const int DELAY = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";

    private readonly static GameState gameState = new GameState();
    public readonly static PrintBoard changes = new PrintBoard();

    static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Utils.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Utils.SetCursorPosition(0, currentLineCursor);
    }

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
    static (string, (int, int)) PieceSelect()
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

            return (select, piece.Position);
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

    static string TileSelect(ref string select, ref int[] address)
    {
        while (true)
        {
            IPiece piece = gameState.GetPiece(select);
            piece.GetMoves((address[0], address[1]));
            Utils.TryClear();
            gameState.PrintBoard();

            Console.WriteLine($"Selected Piece: {select} \nPick a tile to move to or type 'BACK' to pick another piece");

            string tile = GetTile();
            
            if (tile == "BACK")
            {
                Utils.TryClear();
                gameState.PrintBoard();
                (select, (address[0], address[1])) = PieceSelect();
            }

            (int row, int col) target = BoardPosToIndex(tile);
            if (target.row == -1 || target.col == -1)
            {
                DisplayError("Invalid Move");
                continue;
            }

            // If the piece logic is invalid, display Invalid Move.
            if (!piece.Logic((address[0], address[1]), target))
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid Move");
                Console.ResetColor();
                Utils.TryClear();
                gameState.PrintBoard();
                continue;
            }

            return tile;
        }
    }

    private static (int, int) BoardPosToIndex(string tile)
    {
        if (tile.Length != 2)
        {
            return (-1, -1);
        }
        string columns = "ABCDEFGH";
        string rows = "12345678";
        return (rows.IndexOf(tile[1]), columns.IndexOf(tile[0]));
    }

    static void Main(string[] args)
    {
        changes.initialize();
        gameState.PrintBoard();
        while (!gameState.IsGameOver())
        {
            Console.WriteLine();
            string select = "";
            int[] address = {0, 0};
            

            (select, (address[0], address[1])) = PieceSelect();
            string tile = TileSelect(ref select, ref address);
            

            Utils.TryClear();
            gameState.PrintBoard();
            (int, int) startPos = (address[0], address[1]);
            (int, int) targetPos = BoardPosToIndex(tile);
            gameState.MovePiece(startPos, targetPos);
            // gameState.MovePiece(startPos, targetPos);

            // TODO(jcollard): Potentially should remove this if? Not sure this
            // ever actually happens
            //needed this to fix some bug i forgot why
            if (changes.deadpieces.Contains(select))
            {
                changes.deadpieces.Remove(select);
            }

            changes.turn++;
            Utils.TryClear();
            gameState.PrintBoard();
        }
    }
}