using System;
using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class Bishop : Piece
{
    public Bishop(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.Bishop;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();
        // Diagonale Haut-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X - i, (int)Position.Y - i))
            {
                AddLegalMove((int)Position.X - i, (int)Position.Y - i);
            }
            if (!board.IsEmpty((int)Position.X - i, (int)Position.Y - i)) break;
        }
        // Diagonale Bas-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X + i, (int)Position.Y + i))
            {
                AddLegalMove((int)Position.X + i, (int)Position.Y + i);
            }
            if (!board.IsEmpty((int)Position.X + i, (int)Position.Y + i)) break;
        }
        // Diagonale Haut-Droite
        for (int i = 1; i <= Math.Min((int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X - i, (int)Position.Y + i))
            {
                AddLegalMove((int)Position.X - i, (int)Position.Y + i);
            }
            if (!board.IsEmpty((int)Position.X - i, (int)Position.Y + i)) break;
        }
        // Diagonale Bas-Gauche
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X + i, (int)Position.Y - i))
            {
                AddLegalMove((int)Position.X + i, (int)Position.Y - i);
            }
            if (!board.IsEmpty((int)Position.X + i, (int)Position.Y - i)) break;
        }
    }

    public override bool SetsCheck()
    {
        // Diagonale Haut-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X - i, (int)Position.Y - i)) continue;
            Piece p = board.GetPiece((int)Position.X - i, (int)Position.Y - i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Bas-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X + i, (int)Position.Y + i)) continue;
            Piece p = board.GetPiece((int)Position.X + i, (int)Position.Y + i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Bas-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X - i, (int)Position.Y + i)) continue;
            Piece p = board.GetPiece((int)Position.X - i, (int)Position.Y + i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Haut-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X + i, (int)Position.Y - i)) continue;
            Piece p = board.GetPiece((int)Position.X + i, (int)Position.Y - i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        return false;
    }
}