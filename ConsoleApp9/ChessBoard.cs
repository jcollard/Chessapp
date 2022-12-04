using Chess;

namespace Chessapp;

public class ChessBoard
{
    public readonly Dictionary<string, IPiece?> _pieces = new();
    public readonly List<(IPiece, (int, int))> _moves = new();
    private readonly IPiece _blueKing, _greenKing;
    private DisplayChessBoard _displayChessBoard;

    /// <summary>
    /// Constructs a GameState in a traditional chess layout.
    /// </summary>
    public ChessBoard()
    {
        _displayChessBoard = new DisplayChessBoard(this);
        for (int i = 1; i <= 8; i++)
        {
            var pawnPiece = new PawnPiece("p" + i, PieceColor.Blue, (1, i - 1));
            _pieces["p" + i] = pawnPiece;
            var piece = new PawnPiece("P" + i, PieceColor.Green, (6, i - 1));
            _pieces["P" + i] = piece;
        }

        _blueKing = _pieces["k1"] = new KingPiece("k1", PieceColor.Blue, (0, 3));
        _greenKing = _pieces["K1"] = new KingPiece("K1", PieceColor.Green, (7, 4));

        _pieces["q1"] = new QueenPiece("q1", PieceColor.Blue, (0, 4));
        _pieces["Q1"] = new QueenPiece("Q1", PieceColor.Green, (7, 3));

        _pieces["n1"] = new KnightPiece("n1", PieceColor.Blue, (0, 1));
        _pieces["N1"] = new KnightPiece("N1", PieceColor.Green, (7, 1));
        _pieces["n2"] = new KnightPiece("n2", PieceColor.Blue, (0, 6));
        _pieces["N2"] = new KnightPiece("N2", PieceColor.Green, (7, 6));

        _pieces["b1"] = new BishopPiece("b1", PieceColor.Blue, (0, 2));
        _pieces["B1"] = new BishopPiece("B1", PieceColor.Green, (7, 2));
        _pieces["b2"] = new BishopPiece("b2", PieceColor.Blue, (0, 5));
        _pieces["B2"] = new BishopPiece("B2", PieceColor.Green, (7, 5));

        _pieces["r1"] = new RookPiece("r1", PieceColor.Blue, (0, 0));
        _pieces["R1"] = new RookPiece("R1", PieceColor.Green, (7, 0));
        _pieces["r2"] = new RookPiece("r2", PieceColor.Blue, (0, 7));
        _pieces["R2"] = new RookPiece("R2", PieceColor.Green, (7, 7));
        
    }

    private void SetPiece((int row, int col) pos, IPiece? piece)
    {
        if (piece != null) piece.Position = pos;
    }

    private void ClearPiece((int row, int col) pos)
    {
        var piece = GetPiece(pos);
        if (piece != null)
        {
            _pieces["ABCDEFGH".Substring(pos.col, 1) + pos.row] = null;
        }
    }

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

    private void AddMove(IPiece? piece, (int, int) target) => this._moves.Add((piece, target)!);

    /// <summary>
    /// Returns the piece at the specified position or null if no piece is at that
    /// position.
    /// </summary>
    public IPiece? GetPiece((int row, int col) pos) => _pieces.Values.FirstOrDefault(x => x != null && x.Position.row == pos.row && x.Position.col == pos.col);

    /// <summary>
    /// Returns true if there is no piece at the specified position and false otherwise.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsEmpty((int row, int col) pos) => _pieces
        .FirstOrDefault(x => 
            x.Value != null && x.Value.Position.col == pos.col && x.Value.Position.row == pos.row).Value == null;

    /// <summary>
    /// Given a symbol name for a piece, checks if that piece
    /// exists in this GameState. If that piece exists, sets
    /// the value of piece and returns true. Otherwise, returns false.
    /// </summary>
    public IPiece? TryGetPiece(string symbol)
    {
        return _pieces.Values.FirstOrDefault(boardPiece => boardPiece?.Symbol == symbol);
        //FIXME: would it ever reach this?
    }

    /// <summary>
    /// Checks if the game has ended and returns true if it has.
    /// </summary>
    public bool IsGameOver() => _blueKing.IsPieceCaptured || _greenKing.IsPieceCaptured;

    

    /// <summary>
    /// Returns the color of the active player
    /// </summary>
    public PieceColor GetActivePlayer() => _moves.Count % 2 == 0 ? PieceColor.Blue : PieceColor.Green;

    public void MovePieceOnBoard(IPiece? heroPiece, (int row, int col) target)
    {
        IPiece? enemyPiece = GetPiece(target);
        
        if (heroPiece != null && !heroPiece.AllowableMove(target, this))
        {
            return;
        }
        if (enemyPiece != null)
        {
            enemyPiece.CapturePiece();
            ClearPiece(enemyPiece.Position);
        }
        SetPiece(target, heroPiece);
        AddMove(heroPiece, target);
        if (heroPiece != null) ClearPiece(heroPiece.Position);
    }

    /// <summary>
    /// Prints the underlying board to the screen.
    /// </summary>
    public void PrintBoard()
    {
       _displayChessBoard.PrintBoard();
    }

    /// <summary>
    /// Given a list of positions, displays them as possible moves
    /// on the screen.
    /// </summary>
    internal void DisplayPossibleMoves(List<(int, int)>? moves)
    {
        _displayChessBoard.DisplayPossibleMoves(moves);
    }
}