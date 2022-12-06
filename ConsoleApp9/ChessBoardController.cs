using Chess;

namespace Chessapp;

public class ChessBoardController
{
    public readonly List<(IPiece, (int, int))> _moves = new();
    private readonly DisplayChessBoard _displayChessBoard;
    private readonly ChessBoard _chessboard;

    /// <summary>
    /// Constructs a GameState in a traditional chess layout.
    /// </summary>
    public ChessBoardController()
    {
        _chessboard = new ChessBoard();
        _displayChessBoard = new DisplayChessBoard(_chessboard);

    }

    private void AddMove(IPiece? piece, (int, int) target) => _moves.Add((piece, target)!);

    /// <summary>
    /// Given a symbol name for a piece, checks if that piece
    /// exists in this GameState. If that piece exists, sets
    /// the value of piece and returns true. Otherwise, returns false.
    /// </summary>
    public IPiece? SelectChessPiece(string symbol)
    {
        var activePlayerColor = ActivePlayer();
        var selectedPiece = _chessboard.RetrievePieceFrom(symbol);
        if (selectedPiece == null)
        {
            throw new Exception("Not a valid piece.");
        }
        if (activePlayerColor != selectedPiece.Color)
        {
            throw new Exception($"It's {activePlayerColor}'s turn, Select piece again. {activePlayerColor} uses {(activePlayerColor == PieceColor.Blue ? "lowercase" : "capital")} letters.");
        }
        if (selectedPiece.IsPieceCaptured)
        {
            throw new Exception("That piece has already been captured.");
        }

        return selectedPiece;
    }

    /// <summary>
    /// Returns the color of the active player
    /// </summary>
    public PieceColor ActivePlayer() => _moves.Count % 2 == 0 ? PieceColor.Blue : PieceColor.Green;

    public void MovePieceOnBoard(IPiece? heroPiece, (int row, int col) target)
    {
        IPiece? enemyPiece = _chessboard.RetrievePieceFrom(target);
        
        if (heroPiece != null && !heroPiece.AllowableMove(target, this))
        {
            return;
        }
        if (enemyPiece != null)
        {
            enemyPiece.CapturePiece();
            _chessboard.ClearPiece(enemyPiece.Position);
        }

        if (heroPiece != null) heroPiece.Position = target;
        AddMove(heroPiece, target);
        if (heroPiece != null) _chessboard.ClearPiece(heroPiece.Position);
    }

    /// <summary>
    /// Prints the underlying board to the screen.
    /// </summary>
    public void PrintBoard()
    {
        var _pieces = _chessboard.RetrieveAllPieces();
        var activePlayerColor = ActivePlayer();
        _displayChessBoard.PrintBoard(activePlayerColor, _pieces);
    }

    /// <summary>
    /// Given a list of positions, displays them as possible moves
    /// on the screen.
    /// </summary>
    internal void DisplayPossibleMoves(List<(int, int)>? moves)
    {
        _displayChessBoard.DisplayPossibleMoves(moves);
    }

    public string ActivePlayerCasing()
    {
        return ActivePlayer() == PieceColor.Blue ? "lowercase" : "capital";
    }

    public void PieceIsCaptured(string symbol)
    {
        _chessboard.PieceIsCaptured(symbol);
    }

    public bool IsEmpty((int row, int col) target)
    {
        return _chessboard.IsEmpty(target);
    }

    public IPiece RetrievePieceFrom((int row, int col) target)
    {
        return _chessboard.RetrievePieceFrom(target);
    }

    public List<IPiece?> RetrieveAllPieces()
    {
        return _chessboard.RetrieveAllPieces();
    }

    public bool IsGameOver()
    {
        return false; // FIXME: this is obviously wrong
    }
}