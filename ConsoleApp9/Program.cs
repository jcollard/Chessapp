namespace Chess;
public class Program
{
    public const int DELAY = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";

    private readonly static GameState gameState = new GameState();
    public readonly static PrintBoard changes = new PrintBoard();
    public readonly static PrintBoard movecheck = new PrintBoard();
    public readonly static string[,] BoardLayout = new string[8, 8];

    static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Utils.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Utils.SetCursorPosition(0, currentLineCursor);
    }
    public static int[] indextile(string tile)
    {
        int[] array = new int[2];
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if (BoardLayout[i, j] == tile)
                {
                    array[0] = i; array[1] = j;
                }
            }
        }
        return array;
    }

    static void fillboardaddresses()
    {
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                BoardLayout[i, j] = $"{Columns[j]}{Rows[i]}";
            }
        }

    }

    /// <summary>
    /// Checks if the game has ended and returns true if it has.
    /// </summary>
    /// <returns></returns>
    private static bool IsGameOver()
    {
        if (changes.deadpieces.Contains("K1") || changes.deadpieces.Contains("k1"))
        {
            Utils.TryClear();
            gameState.PrintBoard();
            return true;
        }
        return false;
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
            int[] refadd = indextile(tile);

            // If the piece logic is invalid, display Invalid Move.
            if (!piece.Logic((address[0], address[1]), (refadd[0], refadd[1])))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
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
        movecheck.initialize();
        changes.initialize();
        gameState.PrintBoard();
        fillboardaddresses();
        while (!IsGameOver())
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