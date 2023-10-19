using Chessapp;

namespace Chess;
public class Program
{
    private const int Delay = 0;
    private const string Rows = "12345678";
    private const string Columns = "ABCDEFGH";
    private static readonly ChessBoardController ChessBoardController = new();

    static void Main(string[] args)
    {
        while (!ChessBoardController.IsGameOver())
        {
            Utils.TryClear();
            ChessBoardController.PrintBoard();

            IPiece? piece = PieceSelect();
            if (!TryTileSelect(piece, out (int, int) targetPos))
            {
                continue;
            }

            if (piece == null || !piece.AllowableMove(targetPos, ChessBoardController)) continue;
            ChessBoardController.MovePieceOnBoard(piece, targetPos);
            piece?.AssignPositionAndMoved(targetPos);
        }

        Utils.TryClear();
        ChessBoardController.PrintBoard();
        PieceColor winner = ChessBoardController.ActivePlayer() == PieceColor.Blue ? PieceColor.Green : PieceColor.Blue;
        Console.WriteLine($"{winner} is the winner!");
    }

    /// <summary>
    /// Prompts the user to select a piece to move. Returns the selected piece and the row / col of that piece.
    /// </summary>
    private static IPiece? PieceSelect()
    {
        while (true)
        {

            Utils.TryClear();
            ChessBoardController.PrintBoard();
            Console.WriteLine("select piece to move");
            string select = Utils.ReadLine();
            try
            {
                return ChessBoardController.SelectChessPiece(select);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
    private static bool TryTileSelect(IPiece? piece, out (int row, int col) target)
    {
        while (true)
        {
            List<(int, int)>? moves = piece?.GetMoves(ChessBoardController);
            ChessBoardController.DisplayPossibleMoves(moves);
            Thread.Sleep(Program.Delay);
            Utils.TryClear();
            ChessBoardController.PrintBoard();

            Console.WriteLine($"Selected Piece: {piece?.Symbol}");
            if (!TryGetTile(out target))
            {
                return false;
            }
            // TODO(jcollard): I think this is not necessary 
            // if (target.row == -1 || target.col == -1 || !piece.Logic(target))
            if (piece != null && !piece.AllowableMove(target, ChessBoardController))
            {
                Console.WriteLine("Invalid Move");
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