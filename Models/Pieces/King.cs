using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class King : Piece
{
    public King(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.King;
    }

    public override void CalculateLegalMoves()
    {
    }

    public override bool SetsCheck()
    {
        
        return false;
    }
}