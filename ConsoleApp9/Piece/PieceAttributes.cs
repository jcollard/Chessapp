using Chess;

namespace Chessapp.Piece;

public class PieceAttributes {
    public PieceAttributes(bool isCaptured, bool hasMoved, string symbol, PieceColor color, (int, int) position)
    {
        IsCaptured = isCaptured;
        HasMoved = hasMoved;
        Symbol = symbol;
        Color = color;
        Position = position;
    }

    public bool IsCaptured { set; get; }
    public bool HasMoved { get; set; } 
    public string Symbol { get; set; }
    public PieceColor Color { get; set; } 
    public (int row, int col) Position { get; set; }
    
};