namespace Chess;
public class Program
{
    public const int DELAY = 1000;
    private readonly static GameState gameState = new GameState();
    public readonly static PrintBoard changes = new PrintBoard();
    public readonly static PrintBoard movecheck = new PrintBoard();
    public readonly static string[,] BoardLayout = new string[8, 8];
    public readonly static List<string> AddressList = new List<string>();
    public readonly static string[] Rows = { "1", "2", "3", "4", "5", "6", "7", "8" };
    public readonly static string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };

    static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Utils.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Utils.SetCursorPosition(0, currentLineCursor);
    }

    static int[] indexselect(string select)
    {
        int[] array = new int[2];
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if (changes.BoardLayout[i, j] == select)
                {
                    array[0] = i;
                    array[1] = j;
                }
            }
        }
        return array;
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
                BoardLayout[i, j] = Columns[j] + Rows[i];
                AddressList.Add(BoardLayout[i, j]);
            }
        }

    }



    static void queenmoves(int[] selectindex)
    {
        foreach (string i in BoardLayout)
        {
            int[] arr = indextile(i);
            if (queenlogic(selectindex, arr))
            {
                movecheck.BoardLayout[arr[0], arr[1]] = "XX";
            }
            else
            {
                movecheck.BoardLayout[arr[0], arr[1]] = changes.BoardLayout[arr[0], arr[1]];
            }
        }

        Utils.TryClear();
        movecheck.Print();
        Thread.Sleep(DELAY);
        Utils.TryClear();
        changes.Print();
    }

    static bool queenlogic(int[] selectindex, int[] tileindex)
    {
        int selecti = selectindex[0];
        int tilei = tileindex[0];
        int selectj = selectindex[1];
        int tilej = tileindex[1];
        if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' '))
        {
            return false;
        }
        if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' '))
        {
            return false;
        }
        int diff = Math.Abs(selecti - tilei) - Math.Abs(selectj - tilej);

        if (!(Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej)) && Math.Abs(diff) != Math.Abs(selecti - tilei) && Math.Abs(diff) != Math.Abs(selectj - tilej))
        {
            return false;
        }


        if (Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))
        {

            for (int i = selecti, j = selectj; j > 0 || j < 7 || i > 0 || i < 7;)
            {
                if (i == tilei && j == tilej)
                {
                    break;
                }
                if (selecti > tilei)
                {
                    i--;
                }
                else
                {
                    i++;
                }
                if (selectj > tilej)
                {
                    j--;
                }
                else
                {
                    j++;
                }

                if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, j] != changes.BoardLayout[tilei, tilej])
                {
                    return false;
                }
            }
            return true;
        }
        else if (Math.Abs(diff) == Math.Abs(selecti - tilei) || Math.Abs(diff) == Math.Abs(selectj - tilej))
        {
            if (0 != Math.Abs(selecti - tilei) && 0 != Math.Abs(selectj - tilej))
            {
                return false;
            }

            if (Math.Abs(selecti - tilei) != 0)
            {

                for (int i = selecti, j = selectj; i > 0 || i < 7;)
                {
                    if (i == tilei)
                    {
                        break;
                    }

                    if (selecti > tilei)
                    {
                        i--;
                    }
                    else
                    {
                        i++;
                    }
                    if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, tilej] != changes.BoardLayout[tilei, tilej])
                    {
                        return false;
                    }
                }
            }
            if (Math.Abs(selectj - tilej) != 0)
            {
                for (int i = selecti, j = selectj; j > 0 || j < 7;)
                {
                    if (j == tilej)
                    {
                        break;
                    }

                    if (selectj > tilej)
                    {
                        j--;
                    }
                    else
                    {
                        j++;
                    }

                    if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[tilei, j] != changes.BoardLayout[tilei, tilej])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
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
            changes.Print();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Prompts the user to select a piece to move. Returns the selected piece and the row / col of that piece.
    /// </summary>
    /// <param name="pieceselect("></param>
    /// <returns></returns>
    static (string, int[] address) PieceSelect()
    {
        while (true)
        {

            Utils.TryClear();
            changes.Print();
            Console.WriteLine("select piece to move");
            string select = Utils.ReadLine();
            int[] address = indexselect(select);
            Utils.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            if (changes.turn % 2 == 0 && select.ToLower() == select)
            {
                Console.WriteLine("It's Green's turn, Select piece again. Green uses capital letters");
            }
            else if (changes.turn % 2 != 0 && select.ToUpper() == select)
            {
                Console.WriteLine("It's Blue's turn, Select piece again. Blue uses Lowercase letters");
            }
            else if (!changes.pieces.Contains(select))
            {
                Console.WriteLine("Piece does not exist");
            }

            else if (changes.deadpieces.Contains(select))
            {
                Console.WriteLine("Piece does not exist on the board");
            }
            else
            {
                return (select, address);
            }
        }
    }

    static string tileselect(ref string select, ref int[] address)
    {
        while (true)
        {
            IPiece? piece = gameState.GetPiece(select);
            if (piece == null)
            {
                if (select.Contains('q') || select.Contains('Q'))
                {
                    queenmoves(address);

                }
            }
            else
            {
                piece.GetMoves((address[0], address[1]));
            }

            Utils.TryClear();
            changes.Print();

            Console.WriteLine($"Selected Piece: {select} \nPick a tile to move to or type 'BACK' to pick another piece");

            string tile = Utils.ReadLine();
            tile = tile.ToUpper();


            if (tile == "BACK")
            {
                Utils.TryClear();
                changes.Print();
                (select, address) = PieceSelect();
            }
            int[] refadd = indextile(tile);


            if (!AddressList.Contains(tile))
            {
                Console.WriteLine("Please input correct tile address (Example: A5)");
            }
            else if (piece != null)
            {
                if (!piece.Logic((address[0], address[1]), (refadd[0], refadd[1])))
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
                    Console.ResetColor();
                    Utils.TryClear();
                    changes.Print();
                }
                else
                {
                    return tile;
                }
            }
            else if (select.Contains('q') || select.Contains('Q'))
            {
                if (!queenlogic(address, refadd))
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
                    Utils.TryClear();
                    changes.Print();

                    Console.ResetColor();
                }
                else { return tile; }
            }

        }
    }

    /// <summary>
    /// Updates the board by putting a blank on the selected pieces location
    /// and placing it in the tile. If a piece was in the tile, it is added to the deadpieces
    /// list (captured).
    /// </summary>
    /// <param name="select"></param>
    /// <param name="tile"></param>
    private static void MovePiece(string select, string tile)
    {
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if (tile == BoardLayout[i, j])
                {
                    if (changes.BoardLayout[i, j] != "  ")
                    {
                        changes.deadpieces.Add(changes.BoardLayout[i, j]);
                    }
                    changes.BoardLayout[i, j] = select;
                }
                else if (select == changes.BoardLayout[i, j])
                {
                    changes.BoardLayout[i, j] = "  ";
                }
            }
        }
    }



    static void Main(string[] args)
    {
        movecheck.initialize();
        changes.initialize();
        changes.Print();
        fillboardaddresses();
        while (!IsGameOver())
        {
            Console.WriteLine();
            string select = "";
            int[] address = indexselect(select);

            (select, address) = PieceSelect();
            string tile = tileselect(ref select, ref address);

            Utils.TryClear();
            changes.Print();
            MovePiece(select, tile);

            // TODO(jcollard): Potentially should remove this if? Not sure this
            // ever actually happens
            //needed this to fix some bug i forgot why
            if (changes.deadpieces.Contains(select))
            {
                changes.deadpieces.Remove(select);
            }

            changes.turn++;
            Utils.TryClear();
            changes.Print();
        }
    }
}
