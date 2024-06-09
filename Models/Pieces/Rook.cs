using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class Rook : Piece
{
    public Rook(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.Rook;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();
        
        // Haut
        for (int i = (int)Position.Y - 1; i >= 0; i--)
        {
            if (board.IsLegalMove(this, (int)Position.X, i))
            {
                AddLegalMove((int)Position.X, i);
            }
            if (!board.IsEmpty((int)Position.X, i)) break;
        }

        // Gauche
        for (int i = (int)Position.X - 1; i >= 0; i--)
        {
            if (board.IsLegalMove(this, i, (int)Position.Y))
            {
                AddLegalMove(i, (int)Position.Y);
            }
            if (!board.IsEmpty(i, (int)Position.Y)) break;
        }

        // Bas
        for (int i = (int)Position.Y + 1; i < 8; i++)
        {
            if (board.IsLegalMove(this, (int)Position.X, i))
            {
                AddLegalMove((int)Position.X, i);
            }
            if (!board.IsEmpty((int)Position.X, i)) break;
        }

        // Droite
        for (int i = (int)Position.X + 1; i < 8; i++)
        {
            if (board.IsLegalMove(this, i, (int)Position.Y))
            {
                AddLegalMove(i, (int)Position.Y);
            }
            if (!board.IsEmpty(i, (int)Position.Y)) break;
        }
    }

    public override bool SetsCheck()
    {
        // Haut
        for (int i = (int)Position.Y - 1; i >= 0; i--)
        {
            if (board.IsEmpty((int)Position.X, i)) continue;
            Piece piece = board.GetPiece((int)Position.X, i);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }
        // Gauche
        for (int i = (int)Position.X - 1; i >= 0; i--)
        {
            if (board.IsEmpty(i, (int)Position.Y)) continue;
            Piece piece = board.GetPiece(i, (int)Position.Y);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }
        // Bas
        for (int i = (int)Position.Y + 1; i < 8; i++)
        {
            if (board.IsEmpty((int)Position.X, i)) continue;
            Piece piece = board.GetPiece((int)Position.X, i);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }

        // Droite
        for (int i = (int)Position.X + 1; i < 8; i++)
        {
            if (board.IsEmpty(i, (int)Position.Y)) continue;
            Piece piece = board.GetPiece(i, (int)Position.Y);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }
        return false;
    }
}